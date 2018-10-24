
using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Configuration;
using System.Reflection;
using System.Data.Entity.Migrations;

namespace Sadco.FamilyDoctor.Core
{
    public static class Cl_App
    {
        public static Cl_DataContextMegaTemplate m_DataContext;

        private static bool _IsInit = false;

        public static bool Initialize()
        {
            if (!_IsInit)
            {
                string localResourcesPath = "";
                try
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                    var con = config.ConnectionStrings.ConnectionStrings["MedicalChartDatabase"];
                    localResourcesPath = config.AppSettings.Settings["LocalResourcesPath"].Value;
                    if (con == null)
                    {
                        MonitoringStub.Error("Error_AppInit", "Отсутствует настройка подключения к БД MedicalChartDatabase в конфигурационном фале", null, null, null);
                        return false;
                    }
                    m_DataContext = new Cl_DataContextMegaTemplate(con.ConnectionString);

                    m_DataContext.Database.Connection.Open();

                    m_DataContext.f_Init();
                }
                catch (Exception er)
                {
                    MonitoringStub.Error("Error_AppInit", "Не удалось подключиться к базе данных", er, null, null);
                    return false;
                }
                if (!Cl_TemplatesFacade.f_GetInstance().f_Init(m_DataContext))
                {
                    MonitoringStub.Error("Error_AppInit", "Не удалось инициализировать фасад работы с шаблонами", null, null, null);
                    return false;
                }
                if (!Cl_RecordsFacade.f_GetInstance().f_Init(m_DataContext, localResourcesPath))
                {
                    MonitoringStub.Error("Error_AppInit", "Не удалось инициализировать фасад работы с записями", null, null, null);
                    return false;
                }
                if (!Cl_MedicalCardsFacade.f_GetInstance().f_Init(m_DataContext))
                {
                    MonitoringStub.Error("Error_AppInit", "Не удалось инициализировать фасад работы с медкартами", null, null, null);
                    return false;
                }
                if (!Cl_CatalogsFacade.f_GetInstance().f_Init(m_DataContext))
                {
                    MonitoringStub.Error("Error_AppInit", "Не удалось инициализировать фасад работы со справочниками", null, null, null);
                    return false;
                }
                _IsInit = true;
            }
            return _IsInit;
        }
    }
}
