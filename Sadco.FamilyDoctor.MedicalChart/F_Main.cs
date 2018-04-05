using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Permision;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms;
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
			InitSession(args);
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

			if (UserSession.Permission.IsEditMegaTemplates)
			{
				f_SetControl<UC_EditorElements>();
				menuMegaTemplate.Visible = UserSession.Permission.IsEditMegaTemplates;
			}

			if (UserSession.Permission.IsEditTemplates)
			{
				f_SetControl<UC_EditorTemplates>();
				menuTemplate.Visible = UserSession.Permission.IsEditTemplates;
			}
		}

		private void InitSession(string[] args)
		{
			UserSession.ID = int.Parse(args[0]);
			UserSession.Name = args[1];
			UserSession.Permission = new UserPermission(args[2]);
			UserSession.PatientID = int.Parse(args[3]);
			UserSession.PatientName = args[4];
			UserSession.PatientBirthday = DateTime.Parse(args[5]);

			if (args.Length == 8)
			{
				UserSession.TimeIntervalStart = DateTime.Parse(args[6]);
				UserSession.TimeIntervalEnd = DateTime.Parse(args[7]);
			}
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
			this.Text = p_PanelManager.f_SetElement<T>().Tag.ToString();
		}

		private void ctrlMIExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
