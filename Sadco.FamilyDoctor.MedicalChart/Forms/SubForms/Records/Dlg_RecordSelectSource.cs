using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Collections.Generic;
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
            Text = string.Format("Выбор источника для новой записи v{0}", ConfigurationManager.AppSettings["Version"]);
            InitializeComponent();
            f_InitTreeView();

            var userId = Cl_SessionFacade.f_GetInstance().p_Doctor.p_UserID;
            ctrlTablePatterns.Columns.Clear();
            m_Patterns = Cl_App.m_DataContext.p_RecordsPatterns.Include(p => p.p_Template).Include(p => p.p_CategoryClinic).Include(p => p.p_CategoryTotal)
                        .Include(p => p.p_Values).Include(r => r.p_Values.Select(v => v.p_Params)).Where(p => p.p_DoctorID == userId).ToList();
            var patterns = m_Patterns.Select(p => new { p.p_ID, p.p_Name, p_TemplateName = p.p_Template.p_Name }).ToList();
            ctrlTablePatterns.DataSource = patterns;
            ctrlTablePatterns.Columns[0].Visible = false;
            ctrlTablePatterns.Columns[1].Width = p_Name.Width;
            ctrlTablePatterns.Columns[1].HeaderText = p_Name.HeaderText;
            ctrlTablePatterns.Columns[2].Width = p_TemplateName.Width;
            ctrlTablePatterns.Columns[2].HeaderText = p_TemplateName.HeaderText;
        }

        private List<Cl_RecordPattern> m_Patterns = null;

        public Cl_Template p_SelectedTemplate {
            get {
                if (ctrlTBSources.SelectedIndex == 0 && ctrl_TreeTemplates.SelectedNode != null)
                {
                    var node = ctrl_TreeTemplates.SelectedNode as Ctrl_TreeNodeTemplate;
                    if (node != null)
                    {
                        return node.p_Template;
                    }
                }
                return null;
            }
        }

        public Cl_RecordPattern p_SelectedRecordPattern {
            get {
                if (ctrlTBSources.SelectedIndex == 1 && ctrlTablePatterns.SelectedRows != null && ctrlTablePatterns.SelectedRows.Count == 1)
                {
                    var id = (int)ctrlTablePatterns.SelectedRows[0].Cells[0].Value;
                    var pattern = m_Patterns.FirstOrDefault(p => p.p_ID == id);
                    if (pattern != null && pattern.p_Template != null)
                    {
                        return pattern;
                    }
                    return pattern;
                }
                return null;
            }
        }

        private void f_InitTreeView()
        {
            try
            {
                Cl_Group[] groups = Cl_App.m_DataContext.p_Groups.Include(g => g.p_SubGroups).Where(g => g.p_Type == Cl_Group.E_Type.Templates && g.p_ParentID == null && !g.p_IsDelete).ToArray();
                foreach (Cl_Group group in groups)
                {
                    f_PopulateTreeGroup(group, ctrl_TreeTemplates.Nodes);
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось инициализировать дерево шаблонов", er, null, null);
            }
        }

        private void f_PopulateTreeGroup(Cl_Group a_Group, TreeNodeCollection a_TreeNodes)
        {
            TreeNode node = new Ctrl_TreeNodeGroup(a_Group);
            a_TreeNodes.Add(node);
            var tpls = Cl_App.m_DataContext.p_Templates
                .Where(t => t.p_ParentGroupID == a_Group.p_ID && t.p_Type == Cl_Template.E_TemplateType.Template && !t.p_IsDelete).GroupBy(t => t.p_TemplateID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version)
                        .FirstOrDefault());
            foreach (Cl_Template tpl in tpls)
            {
                node.Nodes.Add(new Ctrl_TreeNodeTemplate(a_Group, tpl));
            }
            var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(g => g.p_SubGroups);
            if (!dcGroups.IsLoaded) dcGroups.Load();
            foreach (Cl_Group group in a_Group.p_SubGroups)
            {
                if (!group.p_IsDelete)
                    f_PopulateTreeGroup(group, node.Nodes);
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

        private void ctrlTablePatterns_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (p_SelectedRecordPattern != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
