using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
    public class Cl_EntityLog : IDisposable
    {
        private E_EntityTypes entityLogType = E_EntityTypes.Elements;
        private Dictionary<PropertyInfo, object> lastValues = null;
        private I_ELog logObject { get; set; }

        /// <summary>
        /// Определяет был ли изменен объект
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool f_IsChanged(I_ELog obj)
        {
            bool isChanged = false;

            Cl_ELogClassAttribute classAtr = Cl_EntityLog.f_GetClassAttribute<Cl_ELogClassAttribute>(obj);

            if (classAtr == null) return true;
            if (this.entityLogType != classAtr.p_EntityType) return true;

            Dictionary<PropertyInfo, object> currentValues = f_GetValues(obj);

            foreach (KeyValuePair<PropertyInfo, object> item in currentValues)
            {
                if (!lastValues.ContainsKey(item.Key)) continue;
                if (Cl_EntityCompare.f_IsCompare(item.Key.PropertyType, lastValues[item.Key], item.Value) == false)
                {
                    isChanged = true;
                    break;
                }
            }

            return isChanged;
        }

        /// <summary>
        /// Установка первоначального объекта
        /// </summary>
        /// <param name="obj"></param>
        public void f_SetEntity(I_ELog obj)
        {
            if (logObject != null || obj == null) return;

            Cl_ELogClassAttribute classAtr = Cl_EntityLog.f_GetClassAttribute<Cl_ELogClassAttribute>(obj);
            if (classAtr == null) return;

            this.logObject = obj;
            this.entityLogType = classAtr.p_EntityType;

            lastValues = f_GetValues(logObject);
        }

        /// <summary>
        /// Вызывается после сохранения элемента, что бы определить какие изменения были сделаны
        /// </summary>
        /// <param name="obj"></param>
        public void f_SaveEntity(I_ELog obj)
        {
            if (obj == null) return;

            Cl_ELogClassAttribute classAtr = Cl_EntityLog.f_GetClassAttribute<Cl_ELogClassAttribute>(obj);
            if (classAtr == null) return;

            Cl_Log newEvent = null;

            if (this.f_IsChanged(obj))
            {
                StringBuilder sbAction = new StringBuilder();

                if (f_IsNew(obj))
                    sbAction.AppendLine("Создан новый элемент");
                else
                {
                    Dictionary<PropertyInfo, object> changedValues = f_GetChangedValues(obj);
                    foreach (KeyValuePair<PropertyInfo, object> item in changedValues)
                    {
                        Cl_ELogPropertyAttribute propAttr = item.Key.GetCustomAttributes(typeof(Cl_ELogPropertyAttribute), true).FirstOrDefault() as Cl_ELogPropertyAttribute;

                        string action = "";

                        if (!propAttr.p_IsComputedLog)
                        {
                            if (propAttr.p_IsCustomDescription)
                            {
                                if (!string.IsNullOrEmpty(propAttr.p_Description))
                                    action = propAttr.p_Description + ".";
                            }
                            else
                            {
                                action = "Изменилось поле: \"";

                                if (string.IsNullOrEmpty(propAttr.p_Description))
                                    action += item.Key.Name + "\".";
                                else
                                    action += propAttr.p_Description + "\".";
                            }
                        }

                        if (!propAttr.p_IgnoreValue)
                        {
                            if (!propAttr.p_IsNewValueOnly && propAttr.p_IsComputedLog == false)
                                action += " Старое значение: \"" + Cl_EntityValue.f_GetValue(item.Key, lastValues[item.Key], null) + "\".";

                            action += (propAttr.p_IsComputedLog ? "" : " Новое значение: \"") + Cl_EntityValue.f_GetValue(item.Key, item.Value, lastValues[item.Key]) + (propAttr.p_IsComputedLog ? "" : "\".");
                        }

                        sbAction.AppendLine(action);
                    }
                }

                newEvent = f_CreateEvent(obj, sbAction.ToString().Trim());
            }
            else
            {
                newEvent = f_CreateEvent(obj, "Без изменений");
            }


            Cl_App.m_DataContext.p_Logs.Add(newEvent);
            Cl_App.m_DataContext.SaveChanges();

            if (lastValues != null)
            {
                lastValues.Clear();
                lastValues = f_GetValues(obj);
            }

            logObject = obj;
        }

        /// <summary>
        /// Записывает индивидуальное сообщение лога для переданного объекта
        /// </summary>
        /// <param name="obj">Объект логирования</param>
        /// <param name="message">Сообщение лога</param>
        public static void f_CustomMessageLog(I_ELog obj, string message)
        {
            Cl_ELogClassAttribute classAtr = Cl_EntityLog.f_GetClassAttribute<Cl_ELogClassAttribute>(obj);
            if (classAtr == null) return;

            Cl_Log outEvent = new Cl_Log();

            outEvent.p_ElementID = obj.p_GetLogEntityID;
            outEvent.p_Version = obj.p_Version;
            outEvent.p_EntityType = classAtr.p_EntityType;
            outEvent.p_ChangeTime = DateTime.Now;
            outEvent.p_Event = message;
            outEvent.p_UserName = Cl_SessionFacade.f_GetInstance().p_User.p_FIO;

            Cl_App.m_DataContext.p_Logs.Add(outEvent);
            Cl_App.m_DataContext.SaveChanges();
        }

        /// <summary>
        /// Определяет имеются ли записи логирования у переданного объекта
        /// </summary>
        /// <returns>True - записей нет, False - записи имеются</returns>
        private bool f_IsNew(I_ELog obj)
        {
            Cl_ELogClassAttribute classAtr = Cl_EntityLog.f_GetClassAttribute<Cl_ELogClassAttribute>(obj);
            if (classAtr == null) return false;

            Cl_Log prevEvent = null;
            if (obj.p_GetLogEntityID != 0)
                prevEvent = Cl_App.m_DataContext.p_Logs.Where(l => l.p_ElementID == obj.p_GetLogEntityID && l.p_EntityType == classAtr.p_EntityType).OrderByDescending(d => d.p_ChangeTime).FirstOrDefault();

            return prevEvent == null;
        }

        /// <summary>
        /// Возвращает сформированный объект сообщения лога для текущего объекта
        /// </summary>
        private Cl_Log f_CreateEvent(I_ELog obj, string message)
        {
            Cl_Log outEvent = new Cl_Log();

            outEvent.p_ElementID = obj.p_GetLogEntityID;
            outEvent.p_Version = obj.p_Version;
            outEvent.p_EntityType = this.entityLogType;
            outEvent.p_ChangeTime = DateTime.Now;
            outEvent.p_Event = message;
            outEvent.p_UserName = Cl_SessionFacade.f_GetInstance().p_User.p_FIO;

            return outEvent;
        }

        /// <summary>
        /// Возвращает список логируемых свойств объекта
        /// </summary>
        private Dictionary<PropertyInfo, object> f_GetValues(I_ELog obj)
        {
            Dictionary<PropertyInfo, object> values = new Dictionary<PropertyInfo, object>();
            Type type = obj.GetType();

            foreach (PropertyInfo mInfo in type.GetProperties())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() == typeof(Cl_ELogPropertyAttribute))
                    {
                        values.Add(mInfo, mInfo.GetValue(obj, null));
                    }
                }
            }

            return values;
        }

        /// <summary>
        /// Возвращает список измененных свойств объекта
        /// </summary>
        private Dictionary<PropertyInfo, object> f_GetChangedValues(I_ELog obj)
        {
            Dictionary<PropertyInfo, object> outValues = new Dictionary<PropertyInfo, object>();
            Dictionary<PropertyInfo, object> currentValues = f_GetValues(obj);

            foreach (KeyValuePair<PropertyInfo, object> item in currentValues)
            {
                if (!lastValues.ContainsKey(item.Key)) continue;
                if (Cl_EntityCompare.f_IsCompare(item.Key.PropertyType, lastValues[item.Key], item.Value) == false)
                {
                    outValues.Add(item.Key, item.Value);
                }
            }

            return outValues;
        }

        private static T f_GetClassAttribute<T>(I_ELog obj)
        {
            Type type = obj.GetType();
            return (T)type.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        #region Disposable
        public void Dispose()
        {
            lastValues.Clear();
            lastValues = null;
            logObject = null;
        }
        #endregion
    }
}
