
using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Facades;
using System;

//using System.Configuration;
//using System.Reflection;

namespace Sadco.FamilyDoctor.Core
{
    public static class Cl_App
    {
        public static Cl_DataContextMegaTemplate m_DataContext;

        public static bool Initialize(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString))
                MonitoringStub.Error("Error_Tree", "Входящий параметр \"connectionString\" не может быть пустым.", null, null, null);

            try
            {
                /*Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                var con = config.ConnectionStrings.ConnectionStrings["MedicalChartDatabase"];
                if (con == null)
                {
                    MonitoringStub.Error("Error_Load", "Отсутствует настройка подключения к БД MedicalChartDatabase в конфигурационном фале", null, null, null);
                    return false;
                }
                m_DataContext = new Cl_DataContextMegaTemplate(config.ConnectionStrings.ConnectionStrings["MedicalChartDatabase"].ConnectionString);*/

                m_DataContext = new Cl_DataContextMegaTemplate(connectionString);
                m_DataContext.f_Init();
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Tree", "Не удалось подключиться к базе данных", er, null, null);
                return false;
            }
            if (!Cl_TemplatesFacade.f_GetInstance().f_Init(m_DataContext))
            {
                MonitoringStub.Error("Error_Load", "Не удалось инициализировать фасад работы с шаблонами", null, null, null);
                return false;
            }
            if (!Cl_RecordsFacade.f_GetInstance().f_Init(m_DataContext))
            {
                MonitoringStub.Error("Error_Load", "Не удалось инициализировать фасад работы с записями", null, null, null);
                return false;
            }
            if (!Cl_CatalogsFacade.f_GetInstance().f_Init(m_DataContext))
            {
                MonitoringStub.Error("Error_Load", "Не удалось инициализировать фасад работы со справочниками", null, null, null);
                return false;
            }
            return true;
        }
    }
}
