using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class Dlg_RecordSelectSource : Form
    {
        public Dlg_RecordSelectSource()
        {
            Text = string.Format("Выбор шаблона для новой записи v{0}", ConfigurationManager.AppSettings["Version"]);
            InitializeComponent();
            f_InitTreeView();
        }

        public Cl_Template p_SelectedTemplate {
            get {
                var node = ctrl_TreeTemplates.SelectedNode as Ctrl_TreeNodeTemplate;
                if (node != null)
                {
                    return node.p_Template;
                }
                return null;
            }
        }

        private void f_InitTreeView()
        {
            try
            {
                var clinicCat = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicCat.ToLower();
                var tpls = Cl_App.m_DataContext.p_Templates.Include(t => t.p_CategoryClinic)
                    .Where(t => t.p_Type == Cl_Template.E_TemplateType.Template && !t.p_IsDelete && t.p_CategoryClinic != null && t.p_CategoryClinic.p_Name.ToLower() == clinicCat)
                    .GroupBy(t => t.p_TemplateID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version)
                        .FirstOrDefault());
                foreach (Cl_Template tpl in tpls)
                {
                    ctrl_TreeTemplates.Nodes.Add(new Ctrl_TreeNodeTemplate(new Cl_Group(), tpl));
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось инициализировать дерево шаблонов", er, null, null);
            }
        }

        private void ctrl_TreeTemplates_DoubleClick(object sender, System.EventArgs e)
        {
            var tree = (Ctrl_TreeTemplates)sender;
            if (tree.p_SelectedTemplate != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
