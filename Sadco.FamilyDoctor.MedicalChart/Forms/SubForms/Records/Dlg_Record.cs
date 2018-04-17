using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class Dlg_Record : Form
    {
        public Dlg_Record()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
        }

        private Cl_Record m_Record = null;
        public Cl_Record p_Record {
            get {
                return m_Record;
            }
            set {
                f_SetRecord(value);
            }
        }

        private int m_PaddingX = 5;
        [Category("p_Padding")]
        [DefaultValue(false)]
        public int p_PaddingX {
            get { return m_PaddingX; }
            set {
                if (this.m_PaddingX != value)
                {
                    m_PaddingX = value;
                }
            }
        }

        private int m_PaddingY = 5;
        [Category("p_Padding")]
        [DefaultValue(false)]
        public int p_PaddingY {
            get { return m_PaddingY; }
            set {
                if (this.m_PaddingY != value)
                {
                    m_PaddingY = value;
                    f_UpdateControls();
                }
            }
        }

        private void f_UpdateControls()
        {
            ctrlPContent.Controls.Clear();
            if (m_Record != null && m_Record.p_Template != null)
            {
                if (m_Record.p_Template.p_TemplateElements == null)
                {
                    var cTe = Cl_App.m_DataContext.Entry(m_Record.p_Template).Collection(g => g.p_TemplateElements).Query().Include(te => te.p_ChildElement).Include(te => te.p_ChildTemplate);
                    cTe.Load();
                }
                var ctrlTemp = new Ctrl_Template();
                ctrlTemp.Dock = DockStyle.Fill;
                ctrlTemp.p_Template = m_Record.p_Template;
                ctrlTemp.p_PaddingX = p_PaddingX;
                ctrlTemp.p_PaddingY = p_PaddingY;
                ctrlTemp.f_InitUIControls();
                ctrlPContent.Controls.Add(ctrlTemp);
            }
        }

        public void f_SetRecord(Cl_Record a_Record)
        {
            m_Record = a_Record;
            if (m_Record != null && m_Record.p_Template != null)
            {
                m_Record.p_Template.f_LoadTemplatesElements();
                ctrlUserFIO.Text = m_Record.p_UserFIO;
                ctrlPatientFIO.Text = string.Format("{0} ({1}, {2})", m_Record.p_PatientFIO, m_Record.p_Sex == Core.Permision.Cl_User.E_Sex.Man ? "Мужчина" : "Женьщина", m_Record.p_DateBirth.ToShortDateString());
                Text = string.Format("Запись \"{0}\" v{1}", m_Record.p_Template.p_Name, ConfigurationManager.AppSettings["Version"]);
                f_UpdateControls();
            }
        }
    }
}
