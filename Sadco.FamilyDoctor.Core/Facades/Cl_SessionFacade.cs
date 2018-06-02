﻿using Sadco.FamilyDoctor.Core.Permision;
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
        public bool f_Init(Cl_User a_User, Cl_User a_Patient, DateTime? a_DateStart = null, DateTime? a_DateEnd = null)
        {
            m_IsInit = f_Update(a_User, a_Patient, a_DateStart, a_DateEnd);
            return m_IsInit;
        }

        /// <summary>Обновление фасада</summary>
        public bool f_Update(Cl_User a_User, Cl_User a_Patient, DateTime? a_DateStart = null, DateTime? a_DateEnd = null)
        {
            p_User = a_User;
            p_Patient = a_Patient;
            p_DateStart = a_DateStart;
            p_DateEnd = a_DateEnd;
            return true;
        }


        /// <summary>Текущий пользователь в сессии</summary>
        public Cl_User p_User { get; private set; }

        /// <summary>Текущий пациент в сессии</summary>
        public Cl_User p_Patient { get; private set; }

        /// <summary>Начало выбранного диапазона даты</summary>
		public DateTime? p_DateStart { get; private set; }

        /// <summary>Конец выбранного диапазона даты</summary>
        public DateTime? p_DateEnd { get; private set; }
    }
}