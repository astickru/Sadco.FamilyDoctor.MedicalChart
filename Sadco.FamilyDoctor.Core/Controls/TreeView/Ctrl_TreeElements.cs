using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Permision;
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
        private System.Windows.Forms.ToolStripMenuItem ctrl_ImageNew;
        private System.Windows.Forms.ToolStripMenuItem ctrl_ElementDelete;

        public Ctrl_TreeNodeElement p_SelectedElement {
            get { return SelectedNode as Ctrl_TreeNodeElement; }
        }
        public event TreeViewEventHandler e_AfterCreateElement;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ctrl_ElementNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_ImageNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_ElementDelete = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // ctrl_ElementNew
            // 
            this.ctrl_ElementNew.Name = "ctrl_ElementNew";
            this.ctrl_ElementNew.Size = new System.Drawing.Size(175, 22);
            this.ctrl_ElementNew.Tag = "ElementNew";
            this.ctrl_ElementNew.Text = "Добавить текстовый элемент";
            this.ctrl_ElementNew.Click += Ctrl_ElementNew_Click;
            // 
            // ctrl_ImageNew
            // 
            this.ctrl_ImageNew.Name = "ctrl_ImageNew";
            this.ctrl_ImageNew.Size = new System.Drawing.Size(175, 22);
            this.ctrl_ImageNew.Tag = "ImageNew";
            this.ctrl_ImageNew.Text = "Добавить рисунок";
            this.ctrl_ImageNew.Click += Ctrl_ImageNew_Click;
            // 
            // ctrl_ElementDelete
            // 
            this.ctrl_ElementDelete.Name = "ctrl_ElementDelete";
            this.ctrl_ElementDelete.Size = new System.Drawing.Size(175, 22);
            this.ctrl_ElementDelete.Tag = "ElementDelete";
            this.ctrl_ElementDelete.Text = "Удалить элемент";
            this.ctrl_ElementDelete.Click += Ctrl_ElementDelete_Click;
            // 
            // ctrl_Tree
            // 
            this.ImageList.Images.Add("TEMPLATE_16", Properties.Resources.TEMPLATE_16);
            this.ImageList.Images.Add("BLOCK_16", Properties.Resources.BLOCK_16);
            this.ImageList.Images.Add("TABLE_16", Properties.Resources.TABLE_16);
            this.ImageList.Images.Add("FLOAT_16", Properties.Resources.FLOAT_16);
            this.ImageList.Images.Add("LINE_16", Properties.Resources.LINE_16);
            this.ImageList.Images.Add("BIGBOX_16", Properties.Resources.BIGBOX_16);
            this.ImageList.Images.Add("IMAGE_16", Properties.Resources.IMAGE_16);
            this.ImageList.Images.Add("FLOAT_16_DEL", Properties.Resources.FLOAT_16_DEL);
            this.ImageList.Images.Add("LINE_16_DEL", Properties.Resources.LINE_16_DEL);
            this.ImageList.Images.Add("BIGBOX_16_DEL", Properties.Resources.BIGBOX_16_DEL);
            this.ImageList.Images.Add("IMAGE_16_DEL", Properties.Resources.IMAGE_16_DEL);

            this.ctrl_Tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                this.ctrl_ElementNew,
                                this.ctrl_ImageNew,
                                this.ctrl_ElementDelete });

            this.ResumeLayout(false);
        }

        protected override Cl_Group.E_Type p_Type => Cl_Group.E_Type.Elements;

        protected override void f_Tree_Opening(object sender, CancelEventArgs e)
        {
            base.f_Tree_Opening(sender, e);
            if (p_SelectedElement != null)
            {
                ctrl_ElementNew.Visible = false;
                ctrl_ImageNew.Visible = false;
                ctrl_ElementDelete.Visible = true;
                ctrl_ElementDelete.Enabled = !p_SelectedElement.p_Element.p_IsDelete;
            }
            else
            {
                ctrl_ElementNew.Visible = true;
                ctrl_ImageNew.Visible = true;
                ctrl_ElementDelete.Visible = false;
            }

            if (p_SelectedGroup != null)
            {
                ctrl_ElementNew.Enabled = !p_SelectedGroup.p_Group.p_IsDelete;
                ctrl_ImageNew.Enabled = !p_SelectedGroup.p_Group.p_IsDelete;
                ctrl_ElementDelete.Enabled = !p_SelectedGroup.p_Group.p_IsDelete;
            }
        }

        protected override void f_TreeView_DragDrop(object sender, DragEventArgs e, Ctrl_TreeNodeGroup a_TargetNodeGroup)
        {
            if (a_TargetNodeGroup != null)
            {
                using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
                {
                    try
                    {
                        Ctrl_TreeNodeElement draggedNodeElement = (Ctrl_TreeNodeElement)e.Data.GetData(typeof(Ctrl_TreeNodeElement));
                        if (e.Effect == DragDropEffects.Move)
                        {
                            EntityLog eLog = new EntityLog();
                            eLog.SetEntity(draggedNodeElement.p_Element);

                            var elsDraggeds = Cl_App.m_DataContext.p_Elements.Where(el => el.p_ElementID == draggedNodeElement.p_Element.p_ElementID);
                            if (elsDraggeds != null)
                            {
                                bool isChange = false;
                                foreach (Cl_Element el in elsDraggeds)
                                {
                                    //el.p_ParentGroupID = a_TargetNodeGroup.p_Group.p_ID;
                                    el.p_ParentGroup = a_TargetNodeGroup.p_Group;
                                    isChange = true;
                                }
                                if (isChange)
                                {
                                    Cl_App.m_DataContext.f_SaveChanges(eLog, draggedNodeElement.p_Element);
                                    transaction.Commit();
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
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("При перемещении элемента произошла ошибка");
                        return;
                    }
                }
            }
        }

        private void Ctrl_ElementNew_Click(object sender, EventArgs e)
        {
            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    EntityLog eLog = new EntityLog();

                    Cl_Element newElement = (Cl_Element)Activator.CreateInstance(typeof(Cl_Element));
                    eLog.SetEntity(newElement);
                    Cl_Group group = null;
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
                    newElement.p_IsPartPre = true;
                    if (dlg.ctrl_TBName.Text.Length > 0)
                    {
                        newElement.p_PartPre = dlg.ctrl_TBName.Text[0].ToString().ToUpper();
                        if (dlg.ctrl_TBName.Text.Length > 1)
                        {
                            newElement.p_PartPre += dlg.ctrl_TBName.Text.Substring(1, dlg.ctrl_TBName.Text.Length - 1);
                        }
                    }
                    newElement.p_SymmetryParamLeft = "Слева";
                    newElement.p_SymmetryParamRight = "Справа";
                    Cl_App.m_DataContext.p_Elements.Add(newElement);

                    Cl_App.m_DataContext.SaveChanges();
                    newElement.p_ElementID = newElement.p_ID;
                    Cl_App.m_DataContext.SaveChanges();
                    eLog.SaveEntity(newElement);
                    transaction.Commit();

                    Ctrl_TreeNodeElement newNode = new Ctrl_TreeNodeElement(group, newElement);
                    SelectedNode.Nodes.Add(newNode);
                    SelectedNode = newNode;
                    e_AfterCreateElement?.Invoke(this, new TreeViewEventArgs(newNode));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("При создании нового элемента произошла ошибка");
                    return;
                }
            }
        }

        private void Ctrl_ImageNew_Click(object sender, EventArgs e)
        {
            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    EntityLog eLog = new EntityLog();

                    Cl_Element newElement = (Cl_Element)Activator.CreateInstance(typeof(Cl_Element));
                    eLog.SetEntity(newElement);
                    Cl_Group group = null;
                    if (p_SelectedGroup != null && p_SelectedGroup.p_Group != null)
                    {
                        group = p_SelectedGroup.p_Group;
                    }
                    Dlg_EditorImage dlg = new Dlg_EditorImage();
                    dlg.Text = "Новый рисунок";
                    if (group != null)
                    {
                        newElement.p_ParentGroup = p_SelectedGroup.p_Group;
                        dlg.ctrl_LGroupValue.Text = p_SelectedGroup.p_Group.f_GetFullName();
                    }
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    newElement.p_Name = dlg.ctrl_TBName.Text;
                    newElement.p_Comment = dlg.ctrl_TBDecs.Text;
                    newElement.p_ElementType = Cl_Element.E_ElementsTypes.Image;
                    Cl_App.m_DataContext.p_Elements.Add(newElement);
                    Cl_App.m_DataContext.SaveChanges();
                    newElement.p_ElementID = newElement.p_ID;
                    Cl_App.m_DataContext.SaveChanges();
                    eLog.SaveEntity(newElement);
                    transaction.Commit();

                    Ctrl_TreeNodeElement newNode = new Ctrl_TreeNodeElement(group, newElement);
                    SelectedNode.Nodes.Add(newNode);
                    SelectedNode = newNode;
                    e_AfterCreateElement?.Invoke(this, new TreeViewEventArgs(newNode));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("При создании нового элемента произошла ошибка");
                    return;
                }
            }
        }

        private void Ctrl_ElementDelete_Click(object sender, EventArgs e)
        {
            if (p_SelectedElement == null && p_SelectedElement.p_Element == null) return;

            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    EntityLog eLog = new EntityLog();
                    var els = Cl_App.m_DataContext.p_Elements.Where(el => el.p_ElementID == p_SelectedElement.p_Element.p_ElementID).OrderByDescending(v => v.p_Version);
                    if (els != null)
                    {
                        Cl_Element lastVersion = els.FirstOrDefault();
                        eLog.SetEntity(lastVersion);
                        bool isChange = false;
                        foreach (Cl_Element el in els)
                        {
                            el.p_IsDelete = true;
                            isChange = true;
                        }
                        if (isChange)
                        {
                            Cl_App.m_DataContext.SaveChanges();
                            eLog.SaveEntity(lastVersion);
                            transaction.Commit();

                            if (!UserSession.IsShowDeletedMegTemplates)
                                SelectedNode.Remove();
                            else
                                p_SelectedElement.p_Element = p_SelectedElement.p_Element;
                        }
                    }
                    else
                    {
                        throw new Exception("Не найдена элемент");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("При удалении элемента произошла ошибка");
                    return;
                }
            }
        }
    }
}
