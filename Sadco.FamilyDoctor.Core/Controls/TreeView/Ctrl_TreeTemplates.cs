using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeTemplates : Ctrl_TreeView
    {
        public Ctrl_TreeTemplates()
        {
            InitializeComponent();
        }

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem ctrl_TemplateNew;
        private System.Windows.Forms.ToolStripMenuItem ctrl_TemplateEdit;
        private System.Windows.Forms.ToolStripMenuItem ctrl_TemplateDelete;
        public Ctrl_TreeNodeTemplate p_SelectedTemplate {
            get { return SelectedNode as Ctrl_TreeNodeTemplate; }
        }
        public event TreeViewEventHandler e_EditElement;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ctrl_TemplateNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_TemplateEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_TemplateDelete = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // ctrl_TemplateNew
            // 
            this.ctrl_TemplateNew.Name = "ctrl_TemplateNew";
            this.ctrl_TemplateNew.Size = new System.Drawing.Size(176, 22);
            this.ctrl_TemplateNew.Tag = "TemplateNew";
            this.ctrl_TemplateNew.Text = "Добавить шаблон";
            this.ctrl_TemplateNew.Click += new System.EventHandler(this.ctrl_TemplateNew_Click);
            // 
            // ctrl_TemplateEdit
            // 
            this.ctrl_TemplateEdit.Name = "ctrl_TemplateEdit";
            this.ctrl_TemplateEdit.Size = new System.Drawing.Size(176, 22);
            this.ctrl_TemplateEdit.Tag = "TemplateEdit";
            this.ctrl_TemplateEdit.Text = "Изменить шаблон";
            this.ctrl_TemplateEdit.Click += new System.EventHandler(this.ctrl_TemplateEdit_Click);
            // 
            // ctrl_TemplateDelete
            // 
            this.ctrl_TemplateDelete.Name = "ctrl_TemplateDelete";
            this.ctrl_TemplateDelete.Size = new System.Drawing.Size(176, 22);
            this.ctrl_TemplateDelete.Tag = "TemplateDelete";
            this.ctrl_TemplateDelete.Text = "Удалить шаблон";
            this.ctrl_TemplateDelete.Click += new System.EventHandler(this.ctrl_TemplateDelete_Click);
            // 
            // ctrl_Tree
            // 
            this.ImageList.Images.Add("TEMPLATE_16", Properties.Resources.TEMPLATE_16);
            this.ctrl_Tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_TemplateNew,
            this.ctrl_TemplateEdit,
            this.ctrl_TemplateDelete });

            this.ResumeLayout(false);
        }

        protected override Cl_Group.E_Type p_Type => Cl_Group.E_Type.Templates;

        protected override void f_Tree_Opening(object sender, CancelEventArgs e)
        {
            base.f_Tree_Opening(sender, e);
            if (p_SelectedGroup != null)
            {
                ctrl_TemplateNew.Visible = true;
                ctrl_TemplateEdit.Visible = false;
                ctrl_TemplateDelete.Visible = false;
            }
            else if (p_SelectedTemplate != null)
            {
                ctrl_TemplateNew.Visible = false;
                ctrl_TemplateEdit.Visible = true;
                ctrl_TemplateDelete.Visible = true;
            }
        }

        protected override void f_TreeView_DragDrop(object sender, DragEventArgs e, Ctrl_TreeNodeGroup a_TargetNodeGroup)
        {
            if (a_TargetNodeGroup != null)
            {
                Ctrl_TreeNodeTemplate draggedNodeTemplate = (Ctrl_TreeNodeTemplate)e.Data.GetData(typeof(Ctrl_TreeNodeTemplate));
                if (e.Effect == DragDropEffects.Move)
                {
                    EntityLog eLog = new EntityLog();
                    eLog.SetEntity(draggedNodeTemplate.p_Template);

                    var elsDraggeds = Cl_App.m_DataContext.p_Templates.Where(el => el.p_TemplateID == draggedNodeTemplate.p_Template.p_TemplateID);
                    if (elsDraggeds != null)
                    {
                        bool isChange = false;
                        foreach (Cl_Template el in elsDraggeds)
                        {
                            el.p_ParentGroupID = a_TargetNodeGroup.p_Group.p_ID;
                            isChange = true;
                        }
                        if (isChange)
                        {
                            Cl_App.m_DataContext.f_SaveChanges(eLog, draggedNodeTemplate.p_Template);
                            draggedNodeTemplate.Remove();
                            a_TargetNodeGroup.Nodes.Insert(f_GetFirstGroupInNode(a_TargetNodeGroup.Nodes), draggedNodeTemplate);
                        }
                    }
                    else
                    {
                        throw new Exception("Не найдена элемент для шаблонов");
                    }
                }
            }
        }

        private void ctrl_TemplateNew_Click(object sender, EventArgs e)
        {
            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    EntityLog eLog = new EntityLog();

                    Cl_Template newTemplate = (Cl_Template)Activator.CreateInstance(typeof(Cl_Template));
                    eLog.SetEntity(newTemplate);
                    Cl_Group group = null;
                    if (p_SelectedGroup != null && p_SelectedGroup.p_Group != null)
                    {
                        group = p_SelectedGroup.p_Group;
                    }
                    Dlg_EditorTemplate dlg = new Dlg_EditorTemplate();
                    dlg.Text = "Новый шаблон";
                    if (group != null)
                    {
                        newTemplate.p_ParentGroup = p_SelectedGroup.p_Group;
                        dlg.ctrl_LGroupValue.Text = p_SelectedGroup.p_Group.f_GetFullName();
                    }
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    newTemplate.p_Name = dlg.ctrl_TBName.Text;
                    Cl_App.m_DataContext.p_Templates.Add(newTemplate);
                    Cl_App.m_DataContext.SaveChanges();
                    newTemplate.p_TemplateID = newTemplate.p_ID;
                    Cl_App.m_DataContext.SaveChanges();
                    eLog.SaveEntity(newTemplate);
                    transaction.Commit();

                    SelectedNode.Nodes.Add(new Ctrl_TreeNodeTemplate(group, newTemplate));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("При создании нового шаблона произошла ошибка");
                    return;
                }
            }
        }

        private void ctrl_TemplateEdit_Click(object sender, EventArgs e)
        {
            if (p_SelectedTemplate != null)
            {
                e_EditElement?.Invoke(sender, new TreeViewEventArgs(p_SelectedTemplate));
            }
        }

        private void ctrl_TemplateDelete_Click(object sender, EventArgs e)
        {
            if (p_SelectedTemplate == null && p_SelectedTemplate.p_Template == null) return;

            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    EntityLog eLog = new EntityLog();
                    var els = Cl_App.m_DataContext.p_Templates.Where(el => el.p_TemplateID == p_SelectedTemplate.p_Template.p_TemplateID).OrderByDescending(v => v.p_Version);
                    if (els != null)
                    {
                        Cl_Template lastVersion = els.FirstOrDefault();
                        eLog.SetEntity(lastVersion);
                        bool isChange = false;
                        foreach (Cl_Template el in els)
                        {
                            el.p_IsArhive = true;
                            isChange = true;
                        }
                        if (isChange)
                        {
                            Cl_App.m_DataContext.SaveChanges();
                            eLog.SaveEntity(lastVersion);
                            transaction.Commit();

                            SelectedNode.Remove();
                        }
                    }
                    else
                    {
                        throw new Exception("Не найдена элемент для шаблонов");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("При удалении шаблона произошла ошибка");
                    return;
                }
            }
        }
    }
}
