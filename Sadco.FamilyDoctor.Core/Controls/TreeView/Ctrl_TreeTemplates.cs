using Sadco.FamilyDoctor.Core.Entities;
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
            this.ImageList.Images.Add("label", Properties.Resources.label);
            this.ImageList.Images.Add("check_box", Properties.Resources.check_box);
            this.ImageList.Images.Add("combo_box", Properties.Resources.combo_box);
            this.ctrl_Tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_TemplateNew,
            this.ctrl_TemplateEdit,
            this.ctrl_TemplateDelete });

            this.ResumeLayout(false);
        }



        protected override void f_Tree_Opening(object sender, CancelEventArgs e)
        {
            base.f_Tree_Opening(sender, e);
            if (p_SelectedGroup != null)
            {
                ctrl_TemplateNew.Visible = false;
                ctrl_TemplateEdit.Visible = true;
                ctrl_TemplateDelete.Visible = true;
            }
            else
            {
                ctrl_TemplateNew.Visible = true;
                ctrl_TemplateEdit.Visible = false;
                ctrl_TemplateDelete.Visible = false;
            }
        }

        protected override void f_TreeView_DragDrop(object sender, DragEventArgs e, Ctrl_TreeNodeGroup a_TargetNodeGroup)
        {
            if (a_TargetNodeGroup != null)
            {
                Ctrl_TreeNodeTemplate draggedNodeTemplate = (Ctrl_TreeNodeTemplate)e.Data.GetData(typeof(Ctrl_TreeNodeTemplate));
                if (e.Effect == DragDropEffects.Move)
                {
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
                            Cl_App.m_DataContext.SaveChanges();
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
            Cl_Template newTemplate = (Cl_Template)Activator.CreateInstance(typeof(Cl_Template));
            Cl_GroupElements group = null;
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
            SelectedNode.Nodes.Add(new Ctrl_TreeNodeTemplate(group, newTemplate));
        }

        private void ctrl_TemplateEdit_Click(object sender, EventArgs e)
        {

        }

        private void ctrl_TemplateDelete_Click(object sender, EventArgs e)
        {
            if (p_SelectedGroup == null && p_SelectedGroup.p_Group == null) return;

            var els = Cl_App.m_DataContext.p_Templates.Where(el => el.p_TemplateID == p_SelectedGroup.p_Element.p_ElementID);
            if (els != null)
            {
                bool isChange = false;
                foreach (Cl_Template el in els)
                {
                    el.p_IsArhive = true;
                    isChange = true;
                }
                if (isChange)
                {
                    Cl_App.m_DataContext.SaveChanges();
                    SelectedNode.Remove();
                }
            }
            else
            {
                throw new Exception("Не найдена элемент для шаблонов");
            }
        }
    }
}
