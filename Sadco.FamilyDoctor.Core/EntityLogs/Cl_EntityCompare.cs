using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
    public static class Cl_EntityCompare
    {
        public static bool f_IsCompare(Type type, object val1, object val2)
        {
            if (val1 == null && val2 == null) return true;
            bool outResult = false;
            if (type.GetInterface(nameof(I_Comparable)) != null)
            {
                if (val1 != null) outResult = ((I_Comparable)val1).f_Equals(val2);
            }
            else if (type == typeof(String))
                outResult = f_String(val1, val2);
            else if (type == typeof(Byte))
                outResult = f_Byte(val1, val2);
            else if (type == typeof(Boolean))
                outResult = f_Boolean(val1, val2);
            else if (type == typeof(Decimal))
                outResult = f_Decimal(val1, val2);
            else if (type == typeof(Array))
                outResult = f_Array(val1, val2);
            else
            {
                if (type.Name == "ICollection`1")
                    outResult = f_Collection(val1, val2);
                else if (type.Name == "List`1")
                    outResult = f_Collection(val1, val2);
                else
                {
                    if (type.BaseType != null)
                    {
                        if (type.BaseType == typeof(Enum))
                            outResult = f_Enum(val1, val2);
                        else if (type.BaseType == typeof(Array))
                            outResult = f_Array(val1, val2);
                        else
                            throw new NotImplementedException();
                    }
                    else
                        throw new NotImplementedException();
                }
            }

            return outResult;
        }

        private static bool f_String(object val1, object val2)
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

        private static bool f_Byte(object val1, object val2)
        {
            if (val1 != null)
                return val1.Equals(val2);
            else if (val2 != null)
                return val2.Equals(val1);
            else
                return true;
        }

        private static bool f_Collection(object val1, object val2)
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
                    outResult = f_Collection_Cl_TemplateElement(val1, val2);
                    break;
                case nameof(Cl_RecordValue):
                    outResult = f_Collection_Cl_RecordValue(val1, val2);
                    break;
                case nameof(Cl_RecordParam):
                    outResult = f_Array(val1, val2);
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

        private static bool f_Collection_Cl_RecordValue(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;
                if (val1 == null && val2 == null)
                    return true;
            }

            ICollection<Cl_RecordValue> col1 = (ICollection<Cl_RecordValue>)val1;
            ICollection<Cl_RecordValue> col2 = (ICollection<Cl_RecordValue>)val2;

            if (col1.Count() != col2.Count())
                return false;

            for (int i = 0; i < col1.Count(); i++)
            {
                if (f_Cl_RecordValue(col1.ElementAt(i), col2.ElementAt(i)) == false)
                    return false;
            }

            return true;
        }

        private static bool f_Cl_RecordValue(object val1, object val2)
        {
            Cl_RecordValue elm1 = (Cl_RecordValue)val1;
            Cl_RecordValue elm2 = (Cl_RecordValue)val2;
            Cl_Element baseElement = elm1.p_Element;

            bool isEqual = true;
            bool isEqualPart = true;

            if (elm1 == null || elm2 == null)
            {
                if ((elm1 == null && elm2 != null) || (elm1 != null && elm2 == null))
                    return false;
                if (elm1 == null && elm2 == null)
                    return true;
            }

            if (baseElement.p_IsText)
            {
                if (baseElement.p_IsPartLocations)
                {
                    isEqualPart = f_Array(elm1.p_PartLocations, elm2.p_PartLocations);
                }

                if (baseElement.p_IsTextFromCatalog)
                {
                    isEqual = f_Array(elm1.p_ValuesCatalog, elm2.p_ValuesCatalog);
                    if (baseElement.p_Symmetrical && isEqual)
                    {
                        isEqual = f_Array(elm1.p_ValuesDopCatalog, elm2.p_ValuesDopCatalog);
                    }
                }
                else
                {
                    isEqual = f_String(elm1.p_ValueUser, elm2.p_ValueUser);
                    if (baseElement.p_Symmetrical && isEqual)
                    {
                        isEqual = f_String(elm1.p_ValueDopUser, elm2.p_ValueDopUser);
                    }
                }
            }
            else if (baseElement.p_IsImage)
            {
                return f_Array_Byte(elm1.p_ImageBytes, elm2.p_ImageBytes);
            }
            else
                throw new NotImplementedException();

            return isEqual && isEqualPart;
        }

        private static bool f_Collection_Cl_TemplateElement(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;
                if (val1 == null && val2 == null)
                    return true;
            }

            ICollection<Cl_TemplateElement> col1 = (ICollection<Cl_TemplateElement>)val1;
            ICollection<Cl_TemplateElement> col2 = (ICollection<Cl_TemplateElement>)val2;

            if (col1.Count() != col2.Count())
                return false;

            for (int i = 0; i < col1.Count(); i++)
            {
                if (f_Cl_TemplateElement(col1.ElementAt(i), col2.ElementAt(i)) == false)
                    return false;
            }

            return true;
        }

        private static bool f_Cl_TemplateElement(object val1, object val2)
        {
            Cl_TemplateElement elm1 = (Cl_TemplateElement)val1;
            Cl_TemplateElement elm2 = (Cl_TemplateElement)val2;

            bool isElement1 = false;
            bool isElement2 = false;

            if (elm1 == null || elm2 == null)
            {
                if ((elm1 == null && elm2 != null) || (elm1 != null && elm2 == null))
                    return false;
                if (elm1 == null && elm2 == null)
                    return true;
            }

            isElement1 = elm1.p_ChildElement != null;
            isElement2 = elm2.p_ChildElement != null;

            if (isElement1 && isElement2)
                return elm1.p_ChildElementID == elm2.p_ChildElementID;
            else if (isElement1 == false && isElement2 == false)
                return elm1.p_ChildTemplateID == elm2.p_ChildTemplateID;
            else
                return false;
        }

        private static bool f_Array(object val1, object val2)
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

            if (elementType == typeof(Cl_ElementParam))
                return f_Array_Cl_ElementsParams(val1, val2);
            else if (elementType == typeof(Byte))
                return f_Array_Byte(val1, val2);
            else if (elementType == typeof(Cl_RecordParam))
                return f_Array_Cl_RecordParam(val1, val2);
            else
                return Array.Equals(val1, val2);
        }

        private static bool f_Array_Cl_RecordParam(object val1, object val2)
        {
            Cl_RecordParam[] elm1 = (Cl_RecordParam[])val1;
            Cl_RecordParam[] elm2 = (Cl_RecordParam[])val2;

            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;
                if (val1 == null && val2 == null)
                    return true;
            }

            if (elm1.Count() != elm2.Count())
                return false;

            for (int i = 0; i < elm1.Count(); i++)
            {
                if (f_Cl_RecordParam(elm1.ElementAt(i), elm2.ElementAt(i)) == false)
                    return false;
            }

            return true;
        }

        private static bool f_Cl_RecordParam(object elm1, object elm2)
        {
            if (elm1 == null || elm2 == null)
            {
                if ((elm1 == null && elm2 != null) || (elm1 != null && elm2 == null))
                    return false;
                if (elm1 == null && elm2 == null)
                    return true;
            }

            Cl_RecordParam val1 = (Cl_RecordParam)elm1;
            Cl_RecordParam val2 = (Cl_RecordParam)elm2;

            if (val1.p_ElementParam.p_Value != val2.p_ElementParam.p_Value)
                return false;

            return true;
        }

        private static bool f_Array_Byte(object val1, object val2)
        {
            if (val1 == null || val2 == null) return false;
 
            byte[] a1 = (byte[])val1;
            byte[] a2 = (byte[])val2;

            if (a1.Count() != a2.Count())
                return false;

            for (int i = 0; i < a1.Count(); i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }

        private static bool f_Array_Cl_ElementsParams(object val1, object val2)
        {
            Cl_ElementParam[] elm1 = (Cl_ElementParam[])val1;
            Cl_ElementParam[] elm2 = (Cl_ElementParam[])val2;

            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;
                if (val1 == null && val2 == null)
                    return true;
            }

            if (elm1.Count() != elm2.Count())
                return false;

            for (int i = 0; i < elm1.Count(); i++)
            {
                if (!elm1.ElementAt(i).f_Equals(elm2.ElementAt(i)))
                    return false;
            }

            return true;
        }

        private static bool f_Decimal(object val1, object val2)
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

        private static bool f_Boolean(object val1, object val2)
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

        private static bool f_Enum(object val1, object val2)
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
    }
}
