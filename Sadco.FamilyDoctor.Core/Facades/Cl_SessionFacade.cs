using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Entities;
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
        public bool f_Init(Cl_User a_Doctor, Cl_MedicalCard a_MedCard, DateTime? a_DateStart = null, DateTime? a_DateEnd = null)
        {
            m_IsInit = f_Update(a_Doctor, a_MedCard, a_DateStart, a_DateEnd);

            if (p_SessionID == null || p_SessionID == Guid.Empty)
                p_SessionID = Guid.NewGuid();

            return m_IsInit;
        }

        /// <summary>Обновление фасада</summary>
        public bool f_Update(Cl_User a_Doctor, Cl_MedicalCard a_MedCard, DateTime? a_DateStart = null, DateTime? a_DateEnd = null)
        {
            if (a_Doctor == null)
            {
                MonitoringStub.Error("Error_SessionFacade_Update", "Не указан доктор", null, null, null);
                return false;
            }
            if (a_MedCard == null)
            {
                MonitoringStub.Error("Error_SessionFacade_Update", "Не указана медкарта", null, null, null);
                return false;
            }
            p_Doctor = a_Doctor;

            p_Patient = new Cl_User();
            p_Patient.p_UserID = a_MedCard.p_PatientID;
            p_Patient.p_UserSurName = a_MedCard.p_PatientSurName;
            p_Patient.p_UserName = a_MedCard.p_PatientName;
            p_Patient.p_UserLastName = a_MedCard.p_PatientLastName;
            p_Patient.p_Sex = a_MedCard.p_PatientSex;
            p_Patient.p_DateBirth = a_MedCard.p_PatientDateBirth;
            p_DateStart = a_DateStart;
            p_DateEnd = a_DateEnd;
            p_MedicalCard = a_MedCard;
            return true;
        }


        /// <summary>Текущий пользователь в сессии</summary>
        public Cl_User p_Doctor { get; private set; }

        /// <summary>Текущий пациент в сессии</summary>
        public Cl_User p_Patient { get; private set; }

        /// <summary>Медкарты</summary>
        public Cl_MedicalCard p_MedicalCard { get; private set; }

        /// <summary>Начало выбранного диапазона даты</summary>
		public DateTime? p_DateStart { get; private set; }

        /// <summary>Конец выбранного диапазона даты</summary>
        public DateTime? p_DateEnd { get; private set; }

        /// <summary>ID сессии</summary>
        public Guid p_SessionID { get; private set; }

        /// <summary>Настройки. Печать с окном настроек</summary>
        public bool p_SettingsPrintWithParams { get; set; }
    }
}
