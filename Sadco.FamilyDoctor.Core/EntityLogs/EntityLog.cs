using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sadco.FamilyDoctor.Core.Entities;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
	public class EntityLog : IDisposable
	{
		private EntityTypes entityLogType = EntityTypes.Elements;
		private Dictionary<PropertyInfo, object> lastValues = null;

		private I_ELog logObject { get; set; }


		public void SetEntity(I_ELog obj) {
			if (logObject != null || obj == null) return;

			ELogClassAttribute classAtr = GetClassAttribute<ELogClassAttribute>(obj);
			if (classAtr == null) return;

			this.logObject = obj;
			this.entityLogType = classAtr.EntityType;

			lastValues = GetValues(logObject);
		}

		public void UpdateEntity(I_ELog obj) {
			if (obj == null) return;

			bool IsChanged = false;
			Dictionary<PropertyInfo, object> currentValues = null;
			ELogClassAttribute classAtr = GetClassAttribute<ELogClassAttribute>(obj);

			if (classAtr == null) return;
			if (this.entityLogType != classAtr.EntityType) return;

			currentValues = GetValues(obj);
			StringBuilder sbAction = new StringBuilder();
			if (logObject.p_ID == obj.p_ID) {
				sbAction.Append("Создание");
				IsChanged = true;
			} else {
				foreach (KeyValuePair<PropertyInfo, object> item in currentValues) {
					if (!lastValues.ContainsKey(item.Key)) continue;

					if (!lastValues[item.Key].Equals(item.Value)) {
						ELogPropertyAttribute propAttr = item.Key.GetCustomAttributes(typeof(ELogPropertyAttribute), true).FirstOrDefault() as ELogPropertyAttribute;

						string action = "";

						if (propAttr.IsCustomDescription)
							action = propAttr.Description + ".";
						else {
							action = "Изменилось поле: \"";

							if (string.IsNullOrEmpty(propAttr.Description))
								action += item.Key.Name + "\"";
							else
								action += propAttr.Description + "\"";
						}

						if (!propAttr.IgnoreValue)
							action += " Старое значение: \"" + lastValues[item.Key].ToString() + "\"";

						sbAction.AppendLine(action);
						IsChanged = true;
					}
				}
			}

			if (IsChanged) {
				Cl_App.m_DataContext.p_Logs.Add(CreateEvent(obj.p_ID, logObject.p_ID, sbAction.ToString().Trim()));
				Cl_App.m_DataContext.SaveChanges();
			}

			lastValues.Clear();
			lastValues = currentValues;

			logObject = obj;
		}

		private Cl_Log CreateEvent(int curID, int lastID, string action) {
			Cl_Log outEvent = new Cl_Log();

			outEvent.p_ElementID = curID;
			if (curID == lastID)
				outEvent.p_PrevElementID = 0;
			else
				outEvent.p_PrevElementID = lastID;

			outEvent.p_EntityType = this.entityLogType;
			outEvent.p_ChangeTime = DateTime.Now;
			outEvent.p_Event = action;
			outEvent.p_UserName = "TestUserName";

			return outEvent;
		}

		private Dictionary<PropertyInfo, object> GetValues(I_ELog obj) {
			Dictionary<PropertyInfo, object> values = new Dictionary<PropertyInfo, object>();
			Type type = obj.GetType();

			foreach (PropertyInfo mInfo in type.GetProperties()) {
				foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo)) {
					if (attr.GetType() == typeof(ELogPropertyAttribute)) {
						values.Add(mInfo, NormalizeValue(mInfo.GetValue(obj, null)));
					}
				}
			}

			return values;
		}

		private T GetClassAttribute<T>(I_ELog obj) {
			Type type = obj.GetType();
			return (T)type.GetCustomAttributes(typeof(T), true).FirstOrDefault();
		}

		#region NormalizeValue
		private string NormalizeValue(object value) {
			string outValue = "";

			if (value != null) {
				if (value is Cl_Group) {
					outValue = GetGroupValue(value);
				} else if (value is Enum) {
					outValue = GetEnumValue(value);
				} else if (value is Boolean) {
					outValue = GetBoolValue(value);
				}else if(value is Decimal) {
					outValue = GetDecimalValue(value);
				} else {
					outValue = GetDefaultValue(value);
				}
			}

			return outValue;
		}

		private string GetGroupValue(object value) {
			Cl_Group group = value as Cl_Group;
			return group.f_GetFullName();
		}

		private string GetEnumValue(object value) {
			MemberInfo info = value.GetType().GetMember(value.ToString()).FirstOrDefault();

			if (info != null) {
				DescriptionAttribute attribute = (DescriptionAttribute)info.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
				return attribute.Description;
			}
			return value.ToString();
		}

		private string GetBoolValue(object value) {
			bool tmp = (bool)value;
			return tmp ? "Да" : "Нет";
		}

		private string GetDecimalValue(object value) {
			decimal dec = (decimal)value;
			if (dec == 0)
				return "0";
			else
				return dec.ToString();
		}

		private string GetDefaultValue(object value) {
			return value.ToString();
		}
		#endregion

		#region Disposable
		public void Dispose() {
			lastValues.Clear();
			lastValues = null;
			logObject = null;
		}
		#endregion
	}
}
