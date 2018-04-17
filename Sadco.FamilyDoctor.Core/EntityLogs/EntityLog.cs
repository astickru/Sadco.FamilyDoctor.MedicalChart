using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
    public class EntityLog : IDisposable
    {
        private EntityTypes entityLogType = EntityTypes.Elements;
        private Dictionary<PropertyInfo, object> lastValues = null;
        private I_ELog logObject { get; set; }

        /// <summary>
        /// Определяет был ли изменен объект
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsChanged(I_ELog obj)
        {
            bool isChanged = false;

            ELogClassAttribute classAtr = EntityLog.GetClassAttribute<ELogClassAttribute>(obj);

            if (classAtr == null) return isChanged;
            if (this.entityLogType != classAtr.EntityType) return isChanged;

            Dictionary<PropertyInfo, object> currentValues = GetValues(obj);

            foreach (KeyValuePair<PropertyInfo, object> item in currentValues)
            {
                if (!lastValues.ContainsKey(item.Key)) continue;
                if (CompareValues(item.Key.PropertyType, lastValues[item.Key], item.Value) == false)
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
        public void SetEntity(I_ELog obj)
        {
            if (logObject != null || obj == null) return;

            ELogClassAttribute classAtr = EntityLog.GetClassAttribute<ELogClassAttribute>(obj);
            if (classAtr == null) return;

            this.logObject = obj;
            this.entityLogType = classAtr.EntityType;

            lastValues = GetValues(logObject);
        }

        private bool IsNew(I_ELog obj, EntityTypes type)
        {
            Cl_Log prevEvent = null;
            if (obj.p_GetLogEntityID != 0)
            {
                if (type == EntityTypes.Elements)
                    prevEvent = Cl_App.m_DataContext.p_Logs.Where(l => l.p_ElementID == obj.p_GetLogEntityID).OrderByDescending(d => d.p_ChangeTime).FirstOrDefault();
                else
                    prevEvent = Cl_App.m_DataContext.p_Logs.Where(l => l.p_ElementID == obj.p_GetLogEntityID).OrderByDescending(d => d.p_ChangeTime).FirstOrDefault();
            }

            return prevEvent == null;
        }

        /// <summary>
        /// Вызывается после сохранения элемента, что бы определить какие изменения были сделаны
        /// </summary>
        /// <param name="obj"></param>
        public void SaveEntity(I_ELog obj)
        {
            if (obj == null) return;

            ELogClassAttribute classAtr = EntityLog.GetClassAttribute<ELogClassAttribute>(obj);
            if (classAtr == null) return;

            Cl_Log newEvent = null;

            isSaving = true;
            if (this.IsChanged(obj))
            {
                StringBuilder sbAction = new StringBuilder();

                if (IsNew(obj, classAtr.EntityType))
                    sbAction.AppendLine("Создан новый элемент");

                Dictionary<PropertyInfo, object> changedValues = GetChangedValues(obj);
                foreach (KeyValuePair<PropertyInfo, object> item in changedValues)
                {
                    ELogPropertyAttribute propAttr = item.Key.GetCustomAttributes(typeof(ELogPropertyAttribute), true).FirstOrDefault() as ELogPropertyAttribute;

                    string action = "";

                    if (propAttr.IsCustomDescription)
                        action = propAttr.Description + ".";
                    else
                    {
                        action = "Изменилось поле: \"";

                        if (string.IsNullOrEmpty(propAttr.Description))
                            action += item.Key.Name + "\".";
                        else
                            action += propAttr.Description + "\".";
                    }

                    if (!propAttr.IgnoreValue)
                    {
                        if (!propAttr.IsNewValueOnly)
                            action += " Старое значение: \"" + GetNormalizeValue(item.Key, lastValues[item.Key]) + "\".";

                        action += " Новое значение: \"" + GetNormalizeValue(item.Key, item.Value) + "\".";
                    }

                    sbAction.AppendLine(action);
                }

                newEvent = CreateEvent(obj, sbAction.ToString().Trim());
            }
            else
            {
                newEvent = CreateEvent(obj, "Без изменений");
            }


            Cl_App.m_DataContext.p_Logs.Add(newEvent);
            Cl_App.m_DataContext.SaveChanges();

            if (lastValues != null)
            {
                lastValues.Clear();
                lastValues = GetValues(obj);
            }

            logObject = obj;
            isSaving = false;
        }

        private Cl_Log CreateEvent(I_ELog obj, string action)
        {
            Cl_Log outEvent = new Cl_Log();

            outEvent.p_ElementID = obj.p_GetLogEntityID;
            outEvent.p_Version = obj.p_Version;
            outEvent.p_EntityType = this.entityLogType;
            outEvent.p_ChangeTime = DateTime.Now;
            outEvent.p_Event = action;
            outEvent.p_UserName = UserSession.Name;

            return outEvent;
        }

        public static void CustomMessageLog(I_ELog obj, string action)
        {
            ELogClassAttribute classAtr = EntityLog.GetClassAttribute<ELogClassAttribute>(obj);
            if (classAtr == null) return;

            Cl_Log outEvent = new Cl_Log();

            outEvent.p_ElementID = obj.p_GetLogEntityID;
            outEvent.p_Version = obj.p_Version;
            outEvent.p_EntityType = classAtr.EntityType;
            outEvent.p_ChangeTime = DateTime.Now;
            outEvent.p_Event = action;
            outEvent.p_UserName = UserSession.Name;

            Cl_App.m_DataContext.p_Logs.Add(outEvent);
            Cl_App.m_DataContext.SaveChanges();
        }

        private Dictionary<PropertyInfo, object> GetValues(I_ELog obj)
        {
            Dictionary<PropertyInfo, object> values = new Dictionary<PropertyInfo, object>();
            Type type = obj.GetType();

            foreach (PropertyInfo mInfo in type.GetProperties())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() == typeof(ELogPropertyAttribute))
                    {
                        values.Add(mInfo, mInfo.GetValue(obj, null));
                    }
                }
            }

            return values;
        }

        private Dictionary<PropertyInfo, object> GetChangedValues(I_ELog obj)
        {
            Dictionary<PropertyInfo, object> outValues = new Dictionary<PropertyInfo, object>();
            Dictionary<PropertyInfo, object> currentValues = GetValues(obj);

            foreach (KeyValuePair<PropertyInfo, object> item in currentValues)
            {
                if (!lastValues.ContainsKey(item.Key)) continue;
                if (CompareValues(item.Key.PropertyType, lastValues[item.Key], item.Value) == false)
                {
                    outValues.Add(item.Key, item.Value);
                }
            }

            return outValues;
        }

        private static T GetClassAttribute<T>(I_ELog obj)
        {
            Type type = obj.GetType();
            return (T)type.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        private bool CompareValues(Type type, object val1, object val2)
        {
            bool outResult = false;

            if (type == typeof(String))
                outResult = Compare_String(val1, val2);
            else if (type == typeof(Cl_Group))
                outResult = Compare_Cl_Group(val1, val2);
            else if (type == typeof(Byte))
                outResult = Compare_Enum(val1, val2);
            else if (type == typeof(Boolean))
                outResult = Compare_Boolean(val1, val2);
            else if (type == typeof(Decimal))
                outResult = Compare_Decimal(val1, val2);
            else
            {
                if (type.Name == "ICollection`1")
                    outResult = Compare_Collection(val1, val2);
                else
                {
                    if (type.BaseType != null)
                    {
                        if (type.BaseType == typeof(Enum))
                            outResult = Compare_Enum(val1, val2);
                        else if (type.BaseType == typeof(Array))
                            outResult = Compare_Array(val1, val2);
                        else
                            throw new NotImplementedException();
                    }
                    else
                        throw new NotImplementedException();
                }
            }

            return outResult;
        }

        private bool Compare_String(object val1, object val2)
        {
            if (val1 != null)
                if ((string)val1 == "" && val2 == null)
                    return true;
                else
                    return val1.Equals(val2);
            else if (val2 != null)
                if ((string)val2 == "" && val1 == null)
                    return true;
                else
                    return val2.Equals(val1);
            else
                return true;
        }

        private bool Compare_Byte(object val1, object val2)
        {
            if (val1 != null)
                return val1.Equals(val2);
            else if (val2 != null)
                return val2.Equals(val1);
            else
                return true;
        }

        private bool Compare_Cl_Group(object val1, object val2)
        {
            if (val1.GetType().BaseType != val2.GetType().BaseType)
                return false;

            Cl_Group group1 = (Cl_Group)val1;
            Cl_Group group2 = (Cl_Group)val2;

            return group1.p_ID == group2.p_ID;
        }

        private bool Compare_Collection(object val1, object val2)
        {
            bool outResult = false;

            Type valType = null;
            if (val1 != null)
                valType = val1.GetType().GetGenericArguments().Single();
            else if (val2 != null)
                valType = val2.GetType().GetGenericArguments().Single();
            else
                return true;

            switch (valType.Name)
            {
                case nameof(Cl_TemplateElement):
                    outResult = Compare_Collection_Cl_TemplateElement(val1, val2);
                    break;
                default:
                    if (val1 != null)
                        outResult = val1.Equals(val2);
                    else if (val2 != null)
                        outResult = val2.Equals(val1);
                    else
                        outResult = true;
                    break;
            }

            return outResult;
        }

        private bool Compare_Collection_Cl_TemplateElement(object val1, object val2)
        {
            ICollection<Cl_TemplateElement> col1 = (ICollection<Cl_TemplateElement>)val1;
            ICollection<Cl_TemplateElement> col2 = (ICollection<Cl_TemplateElement>)val2;

            if ((col1 == null && val2 != null) || (col1 != null && val2 == null))
                return false;

            if (col1.Count() != col2.Count())
                return false;

            for (int i = 0; i < col1.Count(); i++)
            {
                if (Compare_Cl_TemplateElement(col1.ElementAt(i), col2.ElementAt(i)) == false)
                    return false;
            }

            return true;
        }

        private bool Compare_Cl_TemplateElement(object val1, object val2)
        {
            Cl_TemplateElement elm1 = (Cl_TemplateElement)val1;
            Cl_TemplateElement elm2 = (Cl_TemplateElement)val2;

            if (elm1 == null || elm2 == null)
            {
                if ((elm1 == null && elm2 != null) || (elm1 != null && elm2 == null))
                    return false;
            }
            if (elm1.p_ChildElementID != elm2.p_ChildElementID)
                return false;

            return true;
        }

        private bool Compare_Array(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;
                if (val1 == null && val2 == null)
                    return true;
            }

            Type type = val1.GetType();
            Type elementType = type.GetElementType();

            if (elementType == typeof(Cl_ElementsParams))
                return Compare_Array_Cl_ElementsParams(val1, val2);
            else
                return Array.Equals(val1, val2);
        }

        private bool Compare_Array_Cl_ElementsParams(object val1, object val2)
        {
            Cl_ElementsParams[] elm1 = (Cl_ElementsParams[])val1;
            Cl_ElementsParams[] elm2 = (Cl_ElementsParams[])val2;

            if (elm1.Count() != elm2.Count())
                return false;

            for (int i = 0; i < elm1.Count(); i++)
            {
                if (Compare_Cl_ElementsParams(elm1.ElementAt(i), elm2.ElementAt(i)) == false)
                    return false;
            }

            return true;
        }

        private bool Compare_Cl_ElementsParams(Cl_ElementsParams elm1, Cl_ElementsParams elm2)
        {
            if (elm1 == null || elm2 == null)
            {
                if ((elm1 == null && elm2 != null) || (elm1 != null && elm2 == null))
                    return false;
            }
            if (elm1.p_Value != elm2.p_Value)
                return false;

            return true;
        }

        private bool Compare_Decimal(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;

                if (val1 == null && val2 == null)
                    return true;
            }

            decimal dec1 = (decimal)val1;
            decimal dec2 = (decimal)val2;

            return dec1 == dec2;
        }

        private bool Compare_Boolean(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;

                if (val1 == null && val2 == null)
                    return true;
            }

            bool b1 = (bool)val1;
            bool b2 = (bool)val2;

            return b1 == b2;
        }

        private bool Compare_Enum(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;

                if (val1 == null && val2 == null)
                    return true;
            }

            return Byte.Equals(val1, val2);
        }

        #region NormalizeValue
        private string GetNormalizeValue(PropertyInfo pInfo, object value)
        {
            string outValue = "";
            if (value == null)
                return outValue;

            if (value is String)
                outValue = GetDefaultValue(value);
            else if (value is Byte)
                outValue = GetDefaultValue(value);
            else if (value is Cl_Group)
                outValue = GetGroupValue(value);
            else if (value is Enum)
                outValue = GetEnumValue(value);
            else if (value is Boolean)
                outValue = GetBoolValue(value);
            else if (value is Decimal)
                outValue = GetDecimalValue(value);
            else if (value is Array)
                outValue = GetArraysValue(value);
            else
            {
                if (value.GetType().Name == "HashSet`1")
                    outValue = GetCollectionTemplate(pInfo, value);
                else
                    outValue = GetDefaultValue(value);
            }
            return outValue;
        }

        private string GetCollectionTemplate(PropertyInfo pInfo, object value)
        {
            Type valType = pInfo.PropertyType.GetGenericArguments().Single();

            if (valType.Name == nameof(Cl_TemplateElement))
                return GetTemplateElementsValue(pInfo, value);
            else
                return "";
        }

        private string GetTemplateElementsValue(PropertyInfo pInfo, object value)
        {
            int index = 0;
            StringBuilder sBuilder = new StringBuilder();
            ICollection<Cl_TemplateElement> last = (ICollection<Cl_TemplateElement>)lastValues[pInfo];
            ICollection<Cl_TemplateElement> current = (ICollection<Cl_TemplateElement>)value;
            bool flag = false;

            if (last == null)
                last = new List<Cl_TemplateElement>();
            if (current == null)
                current = new List<Cl_TemplateElement>();

            // new element
            foreach (Cl_TemplateElement cur in current)
            {
                flag = false;
                foreach (Cl_TemplateElement l in last)
                {
                    if (cur.p_ChildElement != null)
                    {
                        if (cur.p_ChildElementID == l.p_ChildElementID)
                        {
                            flag = true;
                            break;
                        }
                    }
                    else
                    {
                        if (cur.p_ChildTemplateID == l.p_ChildTemplateID)
                        {
                            flag = true;
                            break;
                        }
                    }
                }

                if (flag == false)
                    if (cur.p_ChildElement != null)
                        sBuilder.AppendLine("Добавлен новый элемент \"" + cur.p_ChildElement.p_Name + "\" (" + cur.p_ChildElement.p_GetElementName + ")");
                    else
                        sBuilder.AppendLine("Добавлен новый блок \"" + cur.p_ChildTemplate.p_Name + "\" (" + cur.p_ChildTemplate.p_GetElementName + ")");
            }

            // new position
            for (int x = 0; x < current.Count(); x++)
            {
                Cl_TemplateElement cur = current.ElementAt(x);
                flag = false;
                for (int y = 0; y < last.Count(); y++)
                {
                    Cl_TemplateElement l = last.ElementAt(y);

                    if (cur.p_ChildElement != null)
                    {
                        if (cur.p_ChildElementID == l.p_ChildElementID)
                        {
                            flag = (x != y);
                            break;
                        }
                    }
                    else
                    {
                        if (cur.p_ChildTemplateID == l.p_ChildTemplateID)
                        {
                            flag = (x != y);
                            break;
                        }
                    }
                }

                if (flag)
                    if (cur.p_ChildElement != null)
                        sBuilder.AppendLine("Изменилась позиция элемента \"" + cur.p_ChildElement.p_Name + "\" (" + cur.p_ChildElement.p_GetElementName + ")");
                    else
                        sBuilder.AppendLine("Изменилась позиция блока \"" + cur.p_ChildTemplate.p_Name + "\" (" + cur.p_ChildTemplate.p_GetElementName + ")");
            }

            // delete element
            foreach (Cl_TemplateElement l in last)
            {
                flag = true;
                foreach (Cl_TemplateElement cur in current)
                {
                    if (cur.p_ChildElement != null)
                    {
                        if (cur.p_ChildElementID == l.p_ChildElementID)
                        {
                            flag = false;
                            break;
                        }
                    }
                    else
                    {
                        if (cur.p_ChildTemplateID == l.p_ChildTemplateID)
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (flag)
                    if (l.p_ChildElement != null)
                        sBuilder.AppendLine("Удален элемент \"" + l.p_ChildElement.p_Name + "\" (" + l.p_ChildElement.p_GetElementName + ")");
                    else
                        sBuilder.AppendLine("Удален элемент \"" + l.p_ChildTemplate.p_Name + "\" (" + l.p_ChildTemplate.p_GetElementName + ")");
            }

            return sBuilder.ToString().Trim();
        }

        private string GetGroupValue(object value)
        {
            Cl_Group group = value as Cl_Group;
            return group.f_GetFullName();
        }

        private string GetEnumValue(object value)
        {
            MemberInfo info = value.GetType().GetMember(value.ToString()).FirstOrDefault();

            if (info != null)
            {
                DescriptionAttribute attribute = (DescriptionAttribute)info.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                return attribute.Description;
            }
            return value.ToString();
        }

        private string GetBoolValue(object value)
        {
            bool tmp = (bool)value;
            return tmp ? "Да" : "Нет";
        }

        private string GetDecimalValue(object value)
        {
            decimal dec = (decimal)value;
            if (dec == 0)
                return "0";
            else
                return dec.ToString();
        }

        private string GetArraysValue(object value)
        {
            Type type = value.GetType();
            Type elementType = type.GetElementType();
            string result = "";

            using (MD5 md5Hash = MD5.Create())
            {
                if (elementType == typeof(Cl_ElementsParams))
                {
                    Cl_ElementsParams[] elements = value as Cl_ElementsParams[];
                    foreach (Cl_ElementsParams item in elements)
                    {
                        //result = CalcStringHash(md5Hash, item.p_Value);
                        result += item.p_Value + "\r\n";
                    }
                    result = result.Trim();
                }
                else if (elementType == typeof(byte))
                {
                    byte[] bytes = value as byte[];
                    result = CalcByteHash(md5Hash, bytes);
                }
                else
                {
                    //result = CalcByteHash(md5Hash, bytes);
                }
            }

            return result;
        }

        private string CalcStringHash(MD5 md5Hash, string input)
        {
            return CalcByteHash(md5Hash, Encoding.UTF8.GetBytes(input));
        }

        private string CalcByteHash(MD5 md5Hash, byte[] source)
        {
            StringBuilder sBuilder = new StringBuilder();
            byte[] data = md5Hash.ComputeHash(source);

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private bool isSaving = false;
        private Dictionary<string, Dictionary<int, object[]>> fields = new Dictionary<string, Dictionary<int, object[]>>();
        private string lastColletionMessage = "";

        /*private Dictionary<string, Dictionary<int, object[]>> fields = new Dictionary<string, Dictionary<int, object[]>>();
        private string lastColletionMessage = "";
        private bool isSaving = false;
        private string GetCollectionTemplate(string name, object value)
        {
            int index = 0;
            StringBuilder sBuilder = new StringBuilder();
            bool isInit = !fields.ContainsKey(name);
            ICollection<Cl_TemplateElement> coll = value as ICollection<Cl_TemplateElement>;
            Dictionary<int, object[]> pos = new Dictionary<int, object[]>();

            if (isInit)
            {
                Update(name, coll);
            }
            else
            {
                pos = fields[name];
                index = 0;
                // new position
                foreach (Cl_TemplateElement template in coll)
                {
                    index++;
                    if (!pos.ContainsKey((int)template.p_ChildElementID)) continue;
                    if (index == (int)pos[(int)template.p_ChildElementID][0]) continue;
                    sBuilder.AppendLine("Изменилась позиция элемента \"" + template.p_ChildElement.p_Name + "\" (" + template.p_ChildElement.p_GetElementName + ")");
                }

                index = 0;
                // new items
                foreach (Cl_TemplateElement template in coll)
                {
                    if (pos.ContainsKey((int)template.p_ChildElementID)) continue;
                    sBuilder.AppendLine("Добавлен новый элемент \"" + template.p_ChildElement.p_Name + "\" (" + template.p_ChildElement.p_GetElementName + ")");
                }

                index = 0;
                // deleted items
                foreach (KeyValuePair<int, object[]> delTemplate in pos)
                {
                    bool define = false;
                    foreach (Cl_TemplateElement template in coll)
                    {
                        if (template.p_ChildElementID == delTemplate.Key)
                        {
                            define = true;
                            break;
                        }
                    }

                    if (define == false)
                    {
                        sBuilder.AppendLine("Удален элемент: " + (string)delTemplate.Value[1]);
                    }
                }

                if (isSaving)
                {
                    lastColletionMessage = sBuilder.ToString().Trim();
                    Update(name, coll);
                }
            }

            if (sBuilder.ToString().Trim() == "")
                sBuilder.Append(lastColletionMessage);

            return sBuilder.ToString().Trim();
        }

        private void Update(string name, ICollection<Cl_TemplateElement> coll)
        {
            int index = 0;
            Dictionary<int, object[]> pos = new Dictionary<int, object[]>();

            foreach (Cl_TemplateElement template in coll)
            {
                index++;
                pos.Add((int)template.p_ChildElementID, new object[] { index, "\"" + template.p_ChildElement.p_Name + "\" (" + template.p_ChildElement.p_GetElementName + ")" });
            }
            if (fields.ContainsKey(name))
                fields[name] = pos;
            else
                fields.Add(name, pos);
        }*/

        private string GetDefaultValue(object value)
        {
            return value.ToString();
        }
        #endregion

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
