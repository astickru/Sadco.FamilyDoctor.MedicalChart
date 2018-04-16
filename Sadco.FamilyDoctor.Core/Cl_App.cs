
using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Data;
using System;
using System.Configuration;
using System.Reflection;

namespace Sadco.FamilyDoctor.Core
{
    public static class Cl_App
    {
        public static Cl_DataContextMegaTemplate m_DataContext;

        private static string ConnectionKey = "MedicalDatabase";
        private static string ConnectionValue = Properties.Settings.Default.MedicalDatabase;

        public static void Initialize()
        {
            Cl_App.m_DataContext = new Cl_DataContextMegaTemplate(getConnectionString());
            Cl_App.m_DataContext.f_Init();
        }

        private static string getConnectionString()
        {
            Configuration appConfig = GetConfiguration();
            return appConfig.ConnectionStrings.ConnectionStrings[ConnectionKey].ConnectionString;
        }

        static Configuration GetConfiguration()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

            if (config.ConnectionStrings.ConnectionStrings[ConnectionKey] == null)
            {
                ConnectionStringSettings connStrSettings = new ConnectionStringSettings();
                connStrSettings.Name = ConnectionKey;
                connStrSettings.ConnectionString = ConnectionValue;
                connStrSettings.ProviderName = "System.Data.SqlClient";

                config.ConnectionStrings.ConnectionStrings.Add(connStrSettings);

                try
                {
                    config.Save(ConfigurationSaveMode.Full);
                }
                catch (Exception ex)
                {
                    MonitoringStub.Error("Error_Tree", "Не удалось подключиться к базе данных", ex, null, null);
                }
            }

            return config;
        }
    }
}
