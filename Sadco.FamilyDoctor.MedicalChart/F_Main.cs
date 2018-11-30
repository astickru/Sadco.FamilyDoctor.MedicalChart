using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Permision;
using Sadco.FamilyDoctor.Core.Settings;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Catalogs;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
    public partial class F_Main : Form
    {
        private UI_PanelManager p_PanelManager = null;

        public F_Main(string[] args)
        {
            try
            {
                this.FormClosing += F_Main_FormClosing;
                Tag = string.Format("Мегашаблон v{0}", ConfigurationManager.AppSettings["Version"]);
                if (Cl_App.Initialize())
                {
                    if (f_InitSession(args))
                    {
                        Cl_App.f_SetRecordSetting(f_GetRecordSetting());

                        Cl_SessionFacade sess = Cl_SessionFacade.f_GetInstance();
                        Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.AppEvents, string.Format("Запуск ЭМК. Пользователь: {0}/({1}). Пациент: {2}/({3})", sess.p_Doctor.f_GetInitials(), sess.p_Doctor.p_UserID, sess.p_Patient.f_GetInitials(), sess.p_Patient.p_UserID));


                        this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                                float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                                (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                                System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        InitializeComponent();

                        string rolesVal = "";
                        var role = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_Role;
                        var memInfo = typeof(E_Roles).GetMember(typeof(E_Roles).GetEnumName(role));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            rolesVal = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        ctrlSessionInfo.Text = string.Format("Пользователь: {0}, {1} | Расположение: {2}", Cl_SessionFacade.f_GetInstance().p_Doctor.p_FIO, rolesVal, Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicName);

                        p_PanelManager = new UI_PanelManager(ctrl_CustomControls);
                        bool visibleEditor = false;
                        visibleEditor |= menuMegaTemplate.Visible = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditMegaTemplates;
                        visibleEditor |= menuTemplate.Visible = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditTemplates;
                        visibleEditor |= menuMegaTemplateDeleted.Visible = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsShowDeleted;
                        visibleEditor |= menuCatalogs.Visible = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditCatalogs;
                        visibleEditor |= menuPatterns.Visible = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditAllRecords || Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditSelfRecords;
                        ctrlMIEditor.Visible = visibleEditor;

                        ctrlMISettingsPrint.Checked = Cl_SessionFacade.f_GetInstance().p_SettingsPrintWithParams;

                        f_SetControl<UC_Records>();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_App", "В приложении возникла ошибка", er, null, null);
            }
        }

        private Cl_RecordSetting f_GetRecordSetting()
        {
            var recordSetting = new Cl_RecordSetting();
            if (int.TryParse(ConfigurationManager.AppSettings["RecordSizeH1"], out int sizeH1))
                recordSetting.p_SizeH1 = sizeH1;
            recordSetting.p_RecordBackColor = Color.FromName(ConfigurationManager.AppSettings["RecordBackColor"]);
            recordSetting.p_RecordReadOnlyBackColor = Color.FromName(ConfigurationManager.AppSettings["RecordReadOnlyBackColor"]);
            recordSetting.p_RecordCurrentEditColor = Color.FromName(ConfigurationManager.AppSettings["RecordCurrentEditColor"]);
            recordSetting.p_RecordOutRangeColor = Color.FromName(ConfigurationManager.AppSettings["RecordOutRangeColor"]);
            recordSetting.p_RecordPatientControlBorderColor = Color.FromName(ConfigurationManager.AppSettings["RecordPatientControlBorderColor"]);
            if (int.TryParse(ConfigurationManager.AppSettings["RecordPatientControlBorderWidth"], out int borderWidth))
                recordSetting.p_RecordPatientControlBorderWidth = borderWidth;
            return recordSetting;
        }

        private void F_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.AppEvents, "Завершение работы с ЭМК.");
        }

        private bool f_InitSession(string[] args)
        {
            Cl_User user = new Cl_User();
            user.p_ClinicName = args[0];
            user.p_UserID = int.Parse(args[1]);
            user.p_UserSurName = args[2];
            user.p_UserName = args[3];
            user.p_UserLastName = args[4];
            user.p_ClinicCat = args[5];
            user.p_Permission = new Cl_UserPermission(args[6]);
            int patientId = 0;
            if (int.TryParse(args[8], out patientId))
            {
                var medCard = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCard(args[7], patientId);
                if (medCard != null)
                {
                    if (user.p_Permission.p_Role == E_Roles.Assistant)
                    {
                        user.p_ParentUser = new Cl_User();
                        user.p_ParentUser.p_ClinicName = user.p_ClinicName;
                        user.p_ParentUser.p_ClinicCat = user.p_ClinicCat;
                        user.p_ParentUser.p_UserID = int.Parse(args[11]);
                        user.p_ParentUser.p_UserSurName = args[12];
                        user.p_ParentUser.p_UserName = args[13];
                        user.p_ParentUser.p_UserLastName = args[14];
                    }

                    if (user.p_Permission.p_Role == E_Roles.Inspector)
                        return Cl_SessionFacade.f_GetInstance().f_Init(user, medCard, DateTime.Parse(args[9]), DateTime.Parse(args[10]));
                    else
                        return Cl_SessionFacade.f_GetInstance().f_Init(user, medCard);
                }
                else
                {
                    MonitoringStub.Error("Error_AppInit", "Не найдена медицинская карта", null, null, null);
                    return false;
                }
            }
            else
            {
                MonitoringStub.Error("Error_AppInit", "Не верно указан Id пациента", null, null, null);
                return false;
            }
        }

        private void ctrlMIMedicalCards_Click(object sender, EventArgs e)
        {
            f_SetControl<UC_MedicalCards>();
        }

        private void ctrlMIRecord_Click(object sender, EventArgs e)
        {
            f_SetControl<UC_Records>();
        }

        private void ctrl_MenuShowTemplates_Click(object sender, EventArgs e)
        {
            f_SetControl<UC_EditorTemplates>();
        }

        private void ctrl_MenuShowElements_Click(object sender, EventArgs e)
        {
            f_SetControl<UC_EditorElements>();
        }

        private void f_SetControl<T>() where T : UserControl
        {
            menuMegaTemplateDeleted.Checked = false;
            if (typeof(T) == typeof(UC_EditorElements))
                menuMegaTemplateDeleted.Enabled = true;
            else
                menuMegaTemplateDeleted.Enabled = false;

            this.Text = p_PanelManager.f_SetElement<T>().Tag.ToString();
        }

        private void ctrlMIExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuMegaTemplateDeleted_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            menuItem.Checked = !menuItem.Checked;
            if (p_PanelManager.p_ActiveControl is UC_EditorElements)
            {
                var winApp = p_PanelManager.p_ActiveControl as UC_EditorElements;
                winApp.p_IsShowDeleted = menuItem.Checked;
                winApp.f_ShowDeletedElements(menuItem.Checked);
            }
            else if (p_PanelManager.p_ActiveControl is UC_Records)
            {
                var winApp = p_PanelManager.p_ActiveControl as UC_Records;
                winApp.p_IsShowDeleted = menuItem.Checked;
            }
        }

        private void menuCategories_Click(object sender, EventArgs e)
        {
            var wCategories = new F_Categories();
            wCategories.ShowDialog();
        }

        private void menuPatterns_Click(object sender, EventArgs e)
        {
            var wPatterns = new F_RecordsPatterns();
            wPatterns.ShowDialog();
        }

        private void ctrlMISettingsPrint_CheckedChanged(object sender, EventArgs e)
        {
            Cl_SessionFacade.f_GetInstance().p_SettingsPrintWithParams = ctrlMISettingsPrint.Checked;
        }
    }
}
