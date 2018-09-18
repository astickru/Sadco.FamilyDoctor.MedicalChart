using Sadco.FamilyDoctor.Core.Permision;
using System;

namespace Sadco.FamilyDoctor.Core.Facades
{
    /// <summary>
	/// Фасад работы с сессией пользователя
	/// </summary>
    public class Cl_SessionFacade
    {
        private static Cl_SessionFacade INSTANCE = new Cl_SessionFacade();
        public static Cl_SessionFacade f_GetInstance()
        {
            return INSTANCE;
        }

        private bool m_IsInit = false;

        /// <summary>Инициализация фасада</summary>
        public bool f_Init(Cl_User a_Doctor, Cl_User a_Patient, int a_MedCardNumber, string a_ConnectionString, DateTime? a_DateStart = null, DateTime? a_DateEnd = null)
        {
            m_IsInit = f_Update(a_Doctor, a_Patient, a_MedCardNumber, a_ConnectionString, a_DateStart, a_DateEnd);

            if (p_SessionID == null || p_SessionID == Guid.Empty)
                p_SessionID = Guid.NewGuid();

            return m_IsInit;
        }

        /// <summary>Обновление фасада</summary>
        public bool f_Update(Cl_User a_Doctor, Cl_User a_Patient, int a_MedCardNumber, string a_ConnectionString, DateTime? a_DateStart = null, DateTime? a_DateEnd = null)
        {
            p_Doctor = a_Doctor;
            p_Patient = a_Patient;
            p_DateStart = a_DateStart;
            p_DateEnd = a_DateEnd;
            p_MedCardNumber = a_MedCardNumber;
            p_ConnectionString = a_ConnectionString;
            return true;
        }


        /// <summary>Текущий пользователь в сессии</summary>
        public Cl_User p_Doctor { get; private set; }

        /// <summary>Текущий пациент в сессии</summary>
        public Cl_User p_Patient { get; private set; }

        /// <summary>Номер медкарты</summary>
        public int p_MedCardNumber { get; private set; }

        /// <summary>Начало выбранного диапазона даты</summary>
		public DateTime? p_DateStart { get; private set; }

        /// <summary>Конец выбранного диапазона даты</summary>
        public DateTime? p_DateEnd { get; private set; }

        /// <summary>Строка подключения</summary>
        public string p_ConnectionString { get; private set; }

        /// <summary>
        /// ID сессии
        /// </summary>
        public Guid p_SessionID { get; private set; }
    }
}
