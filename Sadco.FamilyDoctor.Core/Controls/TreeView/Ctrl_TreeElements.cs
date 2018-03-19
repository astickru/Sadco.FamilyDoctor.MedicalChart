using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeElements : Ctrl_TreeView
    {
        public Ctrl_TreeElements()
        {
            InitializeComponent();
        }

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem ctrl_ElementNew;
        private System.Windows.Forms.ToolStripMenuItem ctrl_ElementDelete;
        public Ctrl_TreeNodeElement p_SelectedElement {
            get { return SelectedNode as Ctrl_TreeNodeElement; }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ctrl_ElementNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_ElementDelete = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // ctrl_MIControlNew
            // 
            this.ctrl_ElementNew.Name = "ctrl_ElementNew";
            this.ctrl_ElementNew.Size = new System.Drawing.Size(175, 22);
            this.ctrl_ElementNew.Tag = "ElementNew";
            this.ctrl_ElementNew.Text = "Добавить элемент";
            this.ctrl_ElementNew.Click += Ctrl_ElementNew_Click;
            // 
            // ctrl_MIControlDelete
            // 
            this.ctrl_ElementDelete.Name = "ctrl_ElementDelete";
            this.ctrl_ElementDelete.Size = new System.Drawing.Size(175, 22);
            this.ctrl_ElementDelete.Tag = "ElementDelete";
            this.ctrl_ElementDelete.Text = "Удалить элемент";
            this.ctrl_ElementDelete.Click += Ctrl_ElementNew_Click;
            // 
            // ctrl_CMTree
            // 
            this.ImageList.Images.Add("label", Properties.Resources.label);
            this.ImageList.Images.Add("check_box", Properties.Resources.check_box);
            this.ImageList.Images.Add("combo_box", Properties.Resources.combo_box);
            this.ctrl_Tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_ElementNew,
            this.ctrl_ElementDelete });

            this.ResumeLayout(false);
        }



        protected override void f_Tree_Opening(object sender, CancelEventArgs e)
        {
            base.f_Tree_Opening(sender, e);
            if (p_SelectedElement != null)
            {
                ctrl_ElementNew.Visible = false;
                ctrl_ElementDelete.Visible = true;
            }
            else
            {
                ctrl_ElementNew.Visible = true;
                ctrl_ElementDelete.Visible = false;
            }
        }

        protected override void f_TreeView_DragDrop(object sender, DragEventArgs e, Ctrl_TreeNodeGroup a_TargetNodeGroup)
        {
            if (a_TargetNodeGroup != null)
            {
                Ctrl_TreeNodeElement draggedNodeElement = (Ctrl_TreeNodeElement)e.Data.GetData(typeof(Ctrl_TreeNodeElement));
                if (e.Effect == DragDropEffects.Move)
                {
                    var elsDraggeds = Cl_App.m_DataContext.p_Elements.Where(el => el.p_ElementID == draggedNodeElement.p_Element.p_ElementID);
                    if (elsDraggeds != null)
                    {
                        bool isChange = false;
                        foreach (Cl_Element el in elsDraggeds)
                        {
                            el.p_ParentGroupID = a_TargetNodeGroup.p_Group.p_ID;
                            isChange = true;
                        }
                        if (isChange)
                        {
                            Cl_App.m_DataContext.SaveChanges();
                            draggedNodeElement.Remove();
                            a_TargetNodeGroup.Nodes.Insert(f_GetFirstGroupInNode(a_TargetNodeGroup.Nodes), draggedNodeElement);
                        }
                    }
                    else
                    {
                        throw new Exception("Не найдена элемент для шаблонов");
                    }
                }
            }
        }

        private void Ctrl_ElementNew_Click(object sender, EventArgs e)
        {
            Cl_Element newElement = (Cl_Element)Activator.CreateInstance(typeof(Cl_Element));
            Cl_GroupElements group = null;
            if (p_SelectedGroup != null && p_SelectedGroup.p_Group != null)
            {
                group = p_SelectedGroup.p_Group;
            }
            Dlg_EditorElement dlg = new Dlg_EditorElement();
            dlg.Text = "Новый элемент";
            if (group != null)
            {
                newElement.p_ParentGroup = p_SelectedGroup.p_Group;
                dlg.ctrl_LGroupValue.Text = p_SelectedGroup.p_Group.f_GetFullName();
            }
            if (dlg.ShowDialog() != DialogResult.OK) return;
            newElement.p_Name = dlg.ctrl_TBName.Text;
            newElement.p_Comment = dlg.ctrl_TBDecs.Text;
            newElement.p_ElementType = (Cl_Element.E_ElementsTypes)dlg.ctrl_CB_ControlType.f_GetSelectedItem();
            Cl_App.m_DataContext.p_Elements.Add(newElement);
            Cl_App.m_DataContext.SaveChanges();
            SelectedNode.Nodes.Add(new Ctrl_TreeNodeElement(group, newElement));
        }

        private void Ctrl_ElementNewDelete_Click(object sender, EventArgs e)
        {
            if (p_SelectedElement == null && p_SelectedElement.p_Element == null) return;

            var els = Cl_App.m_DataContext.p_Elements.Where(el => el.p_ElementID == p_SelectedElement.p_Element.p_ElementID);
            if (els != null)
            {
                bool isChange = false;
                foreach (Cl_Element el in els)
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
