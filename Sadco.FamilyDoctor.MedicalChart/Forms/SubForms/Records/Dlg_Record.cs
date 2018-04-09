using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
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
            Text = string.Format("Записи v{0}", ConfigurationManager.AppSettings["Version"]);
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

        public void f_SetRecord(Cl_Record a_Record)
        {
            m_Record = a_Record;
            if (m_Record != null && m_Record.p_Template != null)
            {
                if (m_Record.p_Template.p_TemplateElements == null)
                {
                    var cTe = Cl_App.m_DataContext.Entry(m_Record.p_Template).Collection(g => g.p_TemplateElements).Query().Include(te => te.p_ChildElement).Include(te => te.p_ChildTemplate);
                    cTe.Load();
                }
                if (m_Record.p_Template.p_TemplateElements != null)
                {
                    foreach (var te in a_Record.p_Template.p_TemplateElements)
                    {
                        if (te.p_ChildElement != null)
                        {
                            var ctrlEl = new Ctrl_Element();
                            ctrlEl.p_Element = te.p_ChildElement;
                            ctrlPContent.Controls.Add(ctrlEl);
                        }
                    }
                }
            }
        }
    }
}
