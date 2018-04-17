using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FD.dat.mon.stb.lib;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeView : TreeView
    {
        public Ctrl_TreeView()
        {
            InitializeComponent();
        }

        protected ContextMenuStrip ctrl_Tree;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem ctrl_GroupNew;
        private ToolStripMenuItem ctrl_GroupEdit;
        private ToolStripMenuItem ctrl_GroupDelete;
        public Ctrl_TreeNodeGroup p_SelectedGroup {
            get { return SelectedNode as Ctrl_TreeNodeGroup; }
        }

        private void InitializeComponent()
        {
            this.AllowDrop = true;
            this.ItemDrag += Ctrl_TreeView_ItemDrag;
            this.DragEnter += Ctrl_TreeView_DragEnter;
            this.DragOver += Ctrl_TreeView_DragOver;
            this.DragDrop += Ctrl_TreeView_DragDrop;

            this.components = new System.ComponentModel.Container();
            this.ctrl_Tree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrl_Tree.Opening += Ctrl_CMTree_Opening;
            this.ctrl_GroupNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_GroupEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_GroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_Tree.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrl_CMTree
            // 
            this.ctrl_Tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.ctrl_GroupNew,
                this.ctrl_GroupEdit,
                this.ctrl_GroupDelete});
            this.ctrl_Tree.Name = "ctrl_Tree";
            this.ctrl_Tree.Size = new System.Drawing.Size(170, 76);
            // 
            // ctrl_MIGroupNew
            // 
            this.ctrl_GroupNew.Name = "ctrl_GroupNew";
            this.ctrl_GroupNew.Size = new System.Drawing.Size(169, 22);
            this.ctrl_GroupNew.Tag = "GroupNew";
            this.ctrl_GroupNew.Text = "Добавить группу";
            this.ctrl_GroupNew.Click += Ctrl_GroupNew_Click;
            // 
            // ctrl_MIGroupEdit
            // 
            this.ctrl_GroupEdit.Name = "ctrl_GroupEdit";
            this.ctrl_GroupEdit.Size = new System.Drawing.Size(169, 22);
            this.ctrl_GroupEdit.Tag = "GroupEdit";
            this.ctrl_GroupEdit.Text = "Изменить группу";
            this.ctrl_GroupEdit.Click += Ctrl_GroupEdit_Click;
            // 
            // ctrl_MIGroupDelete
            // 
            this.ctrl_GroupDelete.Name = "ctrl_GroupDelete";
            this.ctrl_GroupDelete.Size = new System.Drawing.Size(169, 22);
            this.ctrl_GroupDelete.Tag = "GroupDelete";
            this.ctrl_GroupDelete.Text = "Удалить группу";
            this.ctrl_GroupDelete.Click += Ctrl_GroupDelete_Click;
            // 
            // Ctrl_TreeView
            // 
            this.ContextMenuStrip = this.ctrl_Tree;
            this.ImageList = new ImageList();
            this.ImageList.Images.Add("FOLDER_16", Properties.Resources.FOLDER);
            this.ImageList.Images.Add("FOLDER_16_DEL", Properties.Resources.FOLDER_DEL);
            this.ImageKey = "FOLDER_16";
            this.SelectedImageKey = "FOLDER_16";
            this.LineColor = System.Drawing.Color.Black;
            this.ctrl_Tree.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private bool m_ReadOnly = false;
        /// <summary>Флаг только чтения</summary>
        public bool p_ReadOnly {
            get {
                return m_ReadOnly;
            }
            set {
                m_ReadOnly = value;
                if (m_ReadOnly)
                {
                    ContextMenuStrip = null;
                    AllowDrop = false;
                }
                else
                {
                    ContextMenuStrip = ctrl_Tree;
                    AllowDrop = true;
                }
            }
        }

        /// <summary>Флаг отображения удаленных элементов</summary>
        public bool p_IsShowDeleted { get; set; }

        /// <summary>Тип группы</summary>
        protected virtual Cl_Group.E_Type p_Type { get; }

        private void Ctrl_CMTree_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SelectedNode == null)
            {
                ctrl_GroupNew.Visible = true;
                ctrl_GroupDelete.Visible = false;
                e.Cancel = true;
                return;
            }
            if (p_SelectedGroup != null)
            {
                ctrl_GroupNew.Visible = true;
                ctrl_GroupEdit.Visible = true;
                ctrl_GroupDelete.Visible = true;
            }
            else
            {
                ctrl_GroupNew.Visible = false;
                ctrl_GroupEdit.Visible = false;
                ctrl_GroupDelete.Visible = false;
            }

            if (p_SelectedGroup != null)
            {
                ctrl_GroupNew.Enabled = !p_SelectedGroup.p_Group.p_IsDelete;
                ctrl_GroupEdit.Enabled = !p_SelectedGroup.p_Group.p_IsDelete;
                ctrl_GroupDelete.Enabled = !p_SelectedGroup.p_Group.p_IsDelete;
            }

            f_Tree_Opening(sender, e);
        }

        protected virtual void f_Tree_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Ctrl_GroupNew_Click(object sender, EventArgs e)
        {
            f_CreateNewGroup();
        }

        private void Ctrl_GroupEdit_Click(object sender, EventArgs e)
        {
            f_EditGroup();
        }

        private void Ctrl_GroupDelete_Click(object sender, EventArgs e)
        {
            f_DeleteGroup();
        }


        /// <summary>Создание новой группы с сохранением её в базе и в ветке меню</summary>
        private void f_CreateNewGroup()
        {
            if (p_SelectedGroup == null) return;
            Dlg_EditorGroup dlg = new Dlg_EditorGroup();
            dlg.Text = "Новая группа";
            if (p_SelectedGroup.p_Group != null)
                dlg.ctrl_LGroupValue.Text = p_SelectedGroup.p_Group.f_GetFullName();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Cl_Group group = new Cl_Group();
                group.p_Type = p_Type;
                group.p_Name = dlg.ctrl_TBName.Text;
                if (p_SelectedGroup.p_Group != null)
                    group.p_ParentID = p_SelectedGroup.p_Group.p_ID;
                Cl_App.m_DataContext.p_Groups.Add(group);
                Cl_App.m_DataContext.SaveChanges();
                if (p_SelectedGroup != null)
                {
                    p_SelectedGroup.Nodes.Add(new Ctrl_TreeNodeGroup(group));
                }
                else
                {
                    Nodes.Add(new Ctrl_TreeNodeGroup(group));
                }
            }
        }

        /// <summary>Редактирование выбранной группы</summary>
        private void f_EditGroup()
        {
            if (p_SelectedGroup == null && p_SelectedGroup.p_Group == null) return;
            Dlg_EditorGroup dlg = new Dlg_EditorGroup();
            dlg.Text = "Изменение группы";
            dlg.ctrl_LGroupValue.Text = p_SelectedGroup.p_Group.f_GetFullName();
            dlg.ctrl_TBName.Text = p_SelectedGroup.p_Group.p_Name;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            p_SelectedGroup.f_SetGroupName(dlg.ctrl_TBName.Text);
            Cl_App.m_DataContext.SaveChanges();
        }

        /// <summary>Удаляет выбранную группу</summary>
        private void f_DeleteGroup()
        {
            if (p_SelectedGroup == null && p_SelectedGroup.p_Group == null) return;
            Cl_Group parentGroup = p_SelectedGroup.p_Group.p_Parent;
            if (parentGroup == null) return;
            p_SelectedGroup.p_Group.p_IsDelete = true;
            Cl_App.m_DataContext.SaveChanges();

            if (!p_IsShowDeleted)
                SelectedNode.Remove();
            else
                p_SelectedGroup.p_Group = p_SelectedGroup.p_Group;
        }

        /// <summary>Возвращает проверку наличия одной ветки в другой</summary>
        /// <param name="a_NodeParent">Родительская ветка</param>
        /// <param name="a_Node">Дочерняя ветка</param>
        /// <returns></returns>
        private bool f_ContainsNode(TreeNode a_NodeParent, TreeNode a_Node)
        {
            if (a_Node == null || a_Node.Parent == null)
                return false;
            if (a_Node.Parent.Equals(a_NodeParent))
                return true;
            return f_ContainsNode(a_NodeParent, a_Node.Parent);
        }
        /// <summary>
        /// Возвращает номер позиции группы в меню
        /// </summary>
        /// <param name="nodes">Список элементов, в котором осуществляется поиск</param>
        /// <returns></returns>
        protected int f_GetFirstGroupInNode(TreeNodeCollection nodes)
        {
            int idx = 1;
            foreach (TreeNode itemNode in nodes)
            {
                idx = itemNode.Index;
                if (itemNode is Ctrl_TreeNodeGroup) break;
            }
            return idx;
        }

        #region drag events
        private void Ctrl_TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
            else if (e.Button == MouseButtons.Right)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }


        private void Ctrl_TreeView_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = PointToClient(new Point(e.X, e.Y));
            SelectedNode = GetNodeAt(targetPoint);
        }


        private void Ctrl_TreeView_DragEnter(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            foreach (string format in formats)
            {
                var item = e.Data.GetData(format);
                if (item is TreeNode)
                {
                    TreeNode node = (TreeNode)item;
                    if (Equals(node.TreeView))
                    {
                        e.Effect = e.AllowedEffect;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        protected virtual void f_TreeView_DragDrop(object sender, DragEventArgs e, Ctrl_TreeNodeGroup a_TargetNodeGroup)
        {

        }

        private void Ctrl_TreeView_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = PointToClient(new Point(e.X, e.Y));
            Ctrl_TreeNodeGroup targetNodeGroup = GetNodeAt(targetPoint) as Ctrl_TreeNodeGroup;
            Ctrl_TreeNodeGroup draggedNodeGroup = (Ctrl_TreeNodeGroup)e.Data.GetData(typeof(Ctrl_TreeNodeGroup));
            if (draggedNodeGroup != null)
            {
                if (!draggedNodeGroup.Equals(targetNodeGroup) && !f_ContainsNode(draggedNodeGroup, targetNodeGroup))
                {
                    if (e.Effect == DragDropEffects.Move)
                    {
                        Cl_Group groupDragged = Cl_App.m_DataContext.p_Groups.FirstOrDefault(g => g.p_ID == draggedNodeGroup.p_Group.p_ID);
                        if (groupDragged != null)
                        {
                            if (targetNodeGroup == null)
                                groupDragged.p_ParentID = null;
                            else
                                groupDragged.p_ParentID = targetNodeGroup.p_Group.p_ID;
                            Cl_App.m_DataContext.SaveChanges();

                            draggedNodeGroup.Remove();
                            if (targetNodeGroup == null)
                                Nodes.Add(draggedNodeGroup);
                            else
                                targetNodeGroup.Nodes.Insert(f_GetFirstGroupInNode(targetNodeGroup.Nodes), draggedNodeGroup);
                        }
                        else
                        {
                            MonitoringStub.Error("Error_Tree", "Не найдена группа шаблонов", new Exception("EX ERROR"), "groupDragged = null", null);
                        }
                    }
                }
            }
            else
            {
                f_TreeView_DragDrop(sender, e, targetNodeGroup);
            }
        }
        #endregion
    }
}
