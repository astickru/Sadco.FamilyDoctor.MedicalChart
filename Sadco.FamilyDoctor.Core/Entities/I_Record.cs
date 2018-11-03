using Sadco.FamilyDoctor.Core.Permision;
using System.Collections.Generic;

namespace Sadco.FamilyDoctor.Core.Entities
{
    public interface I_Record
    {
        /// <summary>Ключ записи</summary>
        int p_ID { get; set; }

        /// <summary>Название клиники</summary>
        string p_ClinicName { get; set; }

        /// <summary>ID доктора</summary>
        int p_DoctorID { get; set; }

        /// <summary>Имя доктора</summary>
        string p_DoctorName { get; set; }

        /// <summary>Фамиля доктора</summary>
        string p_DoctorSurName { get; set; }

        /// <summary>Отчество доктора</summary>
        string p_DoctorLastName { get; set; }

        /// <summary>Инициалы пользователя</summary>
        string p_DoctorFIO { get; }

        /// <summary>Установка пользователя</summary>
        void f_SetDoctor(Cl_User a_User);

        /// <summary>ID общей категории</summary>
        int? p_CategoryTotalID { get; set; }
        
        /// <summary>Общая категория шаблонов</summary>
        Cl_Category p_CategoryTotal { get; set; }

        /// <summary>ID клинической категории</summary>
        int? p_CategoryClinicID { get; set; }

        /// <summary>Клиническая категория шаблонов</summary>
        Cl_Category p_CategoryClinic { get; set; }

        /// <summary>ID шаблона</summary>
        int? p_TemplateID { get; set; }
        
        /// <summary>Шаблон</summary>
        Cl_Template p_Template { get; set; }

        /// <summary>Установка шаблона</summary>
        void f_SetTemplate(Cl_Template a_Template);

        /// <summary>Получение значений элементов записи</summary>
        IEnumerable<I_RecordValue> f_GetRecordsValues();
    }
}
