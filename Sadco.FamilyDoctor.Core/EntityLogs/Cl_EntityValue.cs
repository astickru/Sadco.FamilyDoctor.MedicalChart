using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
    public static class Cl_EntityValue
    {
        private static object lastValue = null;

        public static string f_GetValue(PropertyInfo pInfo, object value, object lastValue)
        {
            Cl_EntityValue.lastValue = lastValue;
            Type type = pInfo.PropertyType;
            string outValue = "";

            if (type == typeof(String))
                outValue = f_GetDefaultValue(value);
            else if (type == typeof(Byte))
                outValue = f_GetDefaultValue(value);
            else if (type == typeof(Cl_Group))
                outValue = f_GetGroupValue(value);
            else if (type == typeof(Cl_ElementParam))
                outValue = f_GetElementParamValue(value);
            else if (type == typeof(Boolean))
                outValue = f_GetBoolValue(value);
            else if (type == typeof(Decimal))
                outValue = f_GetDecimalValue(value);
            else
            {
                if (type.Name == "ICollection`1")
                    outValue = f_GetCollectionValue(pInfo, value);
                else
                {
                    if (type.BaseType != null)
                    {
                        if (type.BaseType == typeof(Enum))
                            outValue = f_GetEnumValue(value);
                        else if (type.BaseType == typeof(Array))
                            outValue = f_GetArraysValue(value);
                        else
                            throw new NotImplementedException();
                    }
                    else
                        throw new NotImplementedException();
                }
            }

            return outValue;
        }

        private static string f_GetCollectionValue(PropertyInfo pInfo, object value)
        {
            Type valType = pInfo.PropertyType.GetGenericArguments().Single();

            if (valType.Name == nameof(Cl_TemplateElement))
                return f_GetTemplateElementsValue(pInfo, value);
            else
                throw new NotImplementedException();
        }

        private static string f_GetTemplateElementsValue(PropertyInfo pInfo, object value)
        {
            StringBuilder sBuilder = new StringBuilder();
            ICollection<Cl_TemplateElement> last = (ICollection<Cl_TemplateElement>)Cl_EntityValue.lastValue;
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

        private static string f_GetGroupValue(object value)
        {
            if (value == null) return "";

            Cl_Group group = value as Cl_Group;
            return group.f_GetFullName();
        }

        private static string f_GetElementParamValue(object value)
        {
            if (value == null) return "";

            Cl_ElementParam group = value as Cl_ElementParam;
            return group.p_Value;
        }

        private static string f_GetEnumValue(object value)
        {
            if (value == null) return "";

            MemberInfo info = value.GetType().GetMember(value.ToString()).FirstOrDefault();

            if (info != null)
            {
                DescriptionAttribute attribute = (DescriptionAttribute)info.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                return attribute.Description;
            }
            return value.ToString();
        }

        private static string f_GetBoolValue(object value)
        {
            if (value == null) return "";

            bool tmp = (bool)value;
            return tmp ? "Да" : "Нет";
        }

        private static string f_GetDecimalValue(object value)
        {
            if (value == null) return "";

            decimal dec = (decimal)value;
            if (dec == 0)
                return "0";
            else
                return dec.ToString();
        }

        private static string f_GetArraysValue(object value)
        {
            if (value == null) return "";

            Type type = value.GetType();
            Type elementType = type.GetElementType();
            string result = "";

            using (MD5 md5Hash = MD5.Create())
            {
                if (elementType == typeof(Cl_ElementParam))
                {
                    Cl_ElementParam[] elements = value as Cl_ElementParam[];
                    foreach (Cl_ElementParam item in elements)
                    {
                        //result = CalcStringHash(md5Hash, item.p_Value);
                        result += item.p_Value + "\r\n";
                    }
                    result = result.Trim();
                }
                else if (elementType == typeof(byte))
                {
                    byte[] bytes = value as byte[];
                    result = f_CalcByteHash(md5Hash, bytes);
                }
                else
                {
                    //result = CalcByteHash(md5Hash, bytes);
                }
            }

            return result;
        }

        private static string f_CalcStringHash(MD5 md5Hash, string input)
        {
            return f_CalcByteHash(md5Hash, Encoding.UTF8.GetBytes(input));
        }

        private static string f_CalcByteHash(MD5 md5Hash, byte[] source)
        {
            StringBuilder sBuilder = new StringBuilder();
            byte[] data = md5Hash.ComputeHash(source);

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private static string f_GetDefaultValue(object value)
        {
            if (value == null) return "";
            return value.ToString();
        }
    }
}
