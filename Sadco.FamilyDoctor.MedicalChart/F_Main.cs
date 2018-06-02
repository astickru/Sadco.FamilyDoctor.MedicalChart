using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Permision;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Catalogs;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
    public partial class F_Main : Form
    {
        private UI_PanelManager p_PanelManager = null;

        public F_Main(string[] args)
        {
            f_InitSession(args);
            Tag = string.Format("Мегашаблон v{0}", ConfigurationManager.AppSettings["Version"]);
            Cl_App.Initialize();
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
            p_PanelManager = new UI_PanelManager(ctrl_CustomControls);

            menuMegaTemplate.Visible = false;
            menuTemplate.Visible = false;

            if (Cl_SessionFacade.f_GetInstance().p_User.p_Permission.p_IsEditMegaTemplates)
            {
                menuMegaTemplate.Visible = Cl_SessionFacade.f_GetInstance().p_User.p_Permission.p_IsEditMegaTemplates;
            }

            if (Cl_SessionFacade.f_GetInstance().p_User.p_Permission.p_IsEditTemplates)
            {
                menuTemplate.Visible = Cl_SessionFacade.f_GetInstance().p_User.p_Permission.p_IsEditTemplates;
            }

            if (Cl_SessionFacade.f_GetInstance().p_User.p_Permission.p_IsShowDeleted)
            {
                menuMegaTemplateDeleted.Visible = Cl_SessionFacade.f_GetInstance().p_User.p_Permission.p_IsShowDeleted;
            }

            f_SetControl<UC_Records>();
        }

        private void f_InitSession(string[] args)
        {
            Cl_User user = new Cl_User();
            user.p_ClinikName = args[0];
            user.p_UserID = int.Parse(args[1]);
            user.p_UserSurName = args[2];
            user.p_UserName = args[3];
            user.p_UserLastName = args[4];
            user.p_Permission = new Cl_UserPermission(args[5]);
            Cl_User patient = new Cl_User();
            patient.p_UserID = int.Parse(args[6]);
            patient.p_UserSurName = args[7];
            patient.p_UserName = args[8];
            patient.p_UserLastName = args[9];
            patient.p_Sex = (Cl_User.E_Sex)Enum.Parse(typeof(Cl_User.E_Sex), args[10]);
            patient.p_DateBirth = DateTime.Parse(args[11]);
            if (args.Length == 14)
                Cl_SessionFacade.f_GetInstance().f_Init(user, patient, DateTime.Parse(args[12]), DateTime.Parse(args[13]));
            else
                Cl_SessionFacade.f_GetInstance().f_Init(user, patient);
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
    }
}
