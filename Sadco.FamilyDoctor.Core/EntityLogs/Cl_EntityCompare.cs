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

            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;

                if (val1 == null && val2 == null)
                    return true;
            }

            bool outResult = false;
            if (type.GetInterface(nameof(I_Comparable)) != null)
            {
                if (val1 != null) outResult = ((I_Comparable)val1).f_Equals(val2);
            }
            else if (type == typeof(String))
                outResult = f_String(val1, val2);
            else if (type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                outResult = f_Object(val1, val2);
            else if (type == typeof(Byte))
                outResult = f_Byte(val1, val2);
            else if (type == typeof(Boolean))
                outResult = f_Boolean(val1, val2);
            else if (type == typeof(Decimal))
                outResult = f_Decimal(val1, val2);
            else if (type.GetInterface(nameof(System.Collections.IEnumerable)) != null)
                outResult = f_IEnumerable(type, val1, val2);
            else
            {
                if (type.BaseType != null)
                {
                    if (type.BaseType == typeof(Enum))
                        outResult = f_Enum(val1, val2);
                    else
                        throw new NotImplementedException("Не реализован метод сравнения для типа " + type.Name);
                }
                else
                    throw new NotImplementedException("Не реализован метод сравнения для типа " + type.Name);
            }

            return outResult;
        }

        /// <summary>
        /// Сравнение объектов с типом Boolean
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_Boolean(object val1, object val2)
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

        /// <summary>
        /// Сравнение объектов с неопределенным типом
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_Object(object val1, object val2)
        {
            if (val1 != null)
                return val1.Equals(val2);
            else if (val2 != null)
                return val2.Equals(val1);
            else
                return true;
        }

        /// <summary>
        /// Сравнение объектов с типом Byte
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_Byte(object val1, object val2)
        {
            if (val1 != null)
                return val1.Equals(val2);
            else if (val2 != null)
                return val2.Equals(val1);
            else
                return true;
        }

        /// <summary>
        /// Сравнение объектов с типом Decimal
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_Decimal(object val1, object val2)
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

        /// <summary>
        /// Сравнение объектов с типом String
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_String(object val1, object val2)
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

        /// <summary>
        /// Сравнение объектов с типом Enum
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_Enum(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;

                if (val1 == null && val2 == null)
                    return true;
            }

            return Enum.Equals(val1, val2);
        }

        /// <summary>
        /// Сравнение объектов коллекций
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        private static bool f_IEnumerable(Type type, object val1, object val2)
        {
            bool outResult = false;

            if (type.IsArray || type == typeof(Array))
                outResult = f_Array(val1, val2);
            else if (type.IsGenericType)
                outResult = f_Collection(val1, val2);
            else
                throw new NotImplementedException("Не реализован метод сравнения коллекций для типа " + type.Name);

            return outResult;
        }

        private static bool f_Collection(object val1, object val2)
        {
            bool outResult = false;
            Type valType = null;

            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;
                if (val1 == null && val2 == null)
                    return true;
            }

            if (val1 != null)
                valType = val1.GetType().GetGenericArguments().Single();
            else if (val2 != null)
                valType = val2.GetType().GetGenericArguments().Single();
            else
                return true;

            if (valType.GetInterface(nameof(I_Comparable)) != null)
                outResult = f_Collection_I_Comparable(val1, val2);
            else
                throw new NotImplementedException("Не реализован метод сравнения коллекций для типа " + valType.Name);

            return outResult;
        }

        private static bool f_Collection_I_Comparable(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;
                if (val1 == null && val2 == null)
                    return true;
            }

            IEnumerable<I_Comparable> col1 = (IEnumerable<I_Comparable>)val1;
            IEnumerable<I_Comparable> col2 = (IEnumerable<I_Comparable>)val2;

            if (col1.Count() != col2.Count())
                return false;

            for (int i = 0; i < col1.Count(); i++)
            {
                if (col1.ElementAt(i).f_Equals(col2.ElementAt(i)) == false)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Сравнение объектов массива
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_Array(object val1, object val2)
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

            if (elementType.GetInterface(nameof(I_Comparable)) != null)
                return f_Array_I_Comparable(val1, val2);
            else if (elementType == typeof(Byte))
                return f_Array_Byte(val1, val2);
            else if (elementType == typeof(I_RecordParam))
                return f_Array_I_RecordParam(val1, val2);
            else
                return Array.Equals(val1, val2);
        }

        /// <summary>
        /// Сравнение массивов с типом Byte
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_Array_Byte(object val1, object val2)
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

        /// <summary>
        /// Сравнение массивов с типом Byte
        /// </summary>
        /// <param name="val1">Сравниваемый объект</param>
        /// <param name="val2">Сравниваемый объект</param>
        /// <returns></returns>
        public static bool f_Array_I_RecordParam(object val1, object val2)
        {
            if (val1 == null || val2 == null) return false;

            I_RecordParam[] a1 = (I_RecordParam[])val1;
            I_RecordParam[] a2 = (I_RecordParam[])val2;

            if (a1.Count() != a2.Count())
                return false;

            for (int i = 0; i < a1.Count(); i++)
                if (a1[i].p_ElementParam.f_Equals(a2[i].p_ElementParam) == false)
                    return false;

            return true;
        }

        private static bool f_Array_I_Comparable(object val1, object val2)
        {
            if (val1 == null || val2 == null)
            {
                if ((val1 == null && val2 != null) || (val1 != null && val2 == null))
                    return false;
                if (val1 == null && val2 == null)
                    return true;
            }

            I_Comparable[] arr1 = (I_Comparable[])val1;
            I_Comparable[] arr2 = (I_Comparable[])val2;

            if (arr1.Count() != arr2.Count())
                return false;

            for (int i = 0; i < arr1.Count(); i++)
            {
                if ((arr1.ElementAt(i)).f_Equals(arr2.ElementAt(i)) == false)
                    return false;
            }

            return true;
        }
    }
}
