using FD.dat.mon.stb.lib;
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
        private ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ctrl_TemplateNew;
        private System.Windows.Forms.ToolStripMenuItem ctrl_BlockNew;
        private System.Windows.Forms.ToolStripMenuItem ctrl_TableNew;
        private System.Windows.Forms.ToolStripMenuItem ctrl_TemplateEditParams;
        private System.Windows.Forms.ToolStripMenuItem ctrl_TemplateEdit;
        private System.Windows.Forms.ToolStripMenuItem ctrl_TemplateDelete;
        public Ctrl_TreeNodeTemplate p_SelectedTemplate {
            get { return SelectedNode as Ctrl_TreeNodeTemplate; }
        }
        public event TreeViewEventHandler e_EditTemplateParams;
        public event TreeViewEventHandler e_EditTemplate;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctrl_TemplateNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_BlockNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_TableNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_TemplateEditParams = new System.Windows.Forms.ToolStripMenuItem();
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
            // ctrl_BlockNew
            // 
            this.ctrl_BlockNew.Name = "ctrl_BlockNew";
            this.ctrl_BlockNew.Size = new System.Drawing.Size(176, 22);
            this.ctrl_BlockNew.Tag = "ctrl_BlockNew";
            this.ctrl_BlockNew.Text = "Добавить блок";
            this.ctrl_BlockNew.Click += new System.EventHandler(this.ctrl_BlockNew_Click);
            // 
            // ctrl_TableNew
            // 
            this.ctrl_TableNew.Name = "ctrl_TableNew";
            this.ctrl_TableNew.Size = new System.Drawing.Size(176, 22);
            this.ctrl_TableNew.Tag = "ctrl_TableNew";
            this.ctrl_TableNew.Text = "Добавить таблицу";
            this.ctrl_TableNew.Click += new System.EventHandler(this.ctrl_TableNew_Click);
            // 
            // ctrl_TemplateEditParams
            // 
            this.ctrl_TemplateEditParams.Name = "ctrl_TemplateEditParams";
            this.ctrl_TemplateEditParams.Size = new System.Drawing.Size(176, 22);
            this.ctrl_TemplateEditParams.Tag = "TemplateEditParams";
            this.ctrl_TemplateEditParams.Text = "Изменить параметы шаблона";
            this.ctrl_TemplateEditParams.Click += new System.EventHandler(this.ctrl_TemplateEditParams_Click);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // ctrl_Tree
            // 
            this.ImageList.Images.Add("TEMPLATE_16", Properties.Resources.TEMPLATE_16);
            this.ImageList.Images.Add("BLOCK_16", Properties.Resources.BLOCK_16);
            this.ImageList.Images.Add("TABLE_16", Properties.Resources.TABLE_16);

            this.ctrl_Tree.Items.Insert(0, this.ctrl_TemplateNew);
            this.ctrl_Tree.Items.Insert(1, this.ctrl_BlockNew);
            this.ctrl_Tree.Items.Insert(2, this.ctrl_TableNew);
            this.ctrl_Tree.Items.Insert(3, this.ctrl_TemplateEditParams);
            this.ctrl_Tree.Items.Insert(4, this.ctrl_TemplateEdit);
            this.ctrl_Tree.Items.Insert(5, this.ctrl_TemplateDelete);
            this.ctrl_Tree.Items.Insert(6, this.toolStripSeparator1);

            this.ResumeLayout(false);
        }

        protected override Cl_Group.E_Type p_Type => Cl_Group.E_Type.Templates;

        protected override void f_Tree_Opening(object sender, CancelEventArgs e)
        {
            base.f_Tree_Opening(sender, e);
            if (p_SelectedGroup != null)
            {
                ctrl_TemplateNew.Visible = ctrl_BlockNew.Visible = ctrl_TableNew.Visible = true;
                ctrl_TemplateEdit.Visible = false;
                ctrl_TemplateDelete.Visible = false;
            }
            else if (p_SelectedTemplate != null)
            {
                ctrl_TemplateNew.Visible = ctrl_BlockNew.Visible = ctrl_TableNew.Visible = false;
                ctrl_TemplateEdit.Visible = true;
                ctrl_TemplateDelete.Visible = true;
                if (p_SelectedTemplate.p_Template.p_Type == Cl_Template.E_TemplateType.Template)
                {
                    ctrl_TemplateEdit.Text = "Изменить шаблон";
                    ctrl_TemplateDelete.Text = "Удалить шаблон";
                    ctrl_TemplateEditParams.Text = "Изменить параметы шаблона";
                }
                else if (p_SelectedTemplate.p_Template.p_Type == Cl_Template.E_TemplateType.Block)
                {
                    ctrl_TemplateEdit.Text = "Изменить блок";
                    ctrl_TemplateDelete.Text = "Удалить блок";
                    ctrl_TemplateEditParams.Text = "Изменить параметы блокa";
                }
                else if (p_SelectedTemplate.p_Template.p_Type == Cl_Template.E_TemplateType.Table)
                {
                    ctrl_TemplateEdit.Text = "Изменить таблицу";
                    ctrl_TemplateDelete.Text = "Удалить таблицу";
                    ctrl_TemplateEditParams.Text = "Изменить параметы таблицы";
                }
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
                        Ctrl_TreeNodeTemplate draggedNodeTemplate = (Ctrl_TreeNodeTemplate)e.Data.GetData(typeof(Ctrl_TreeNodeTemplate));
                        if (e.Effect == DragDropEffects.Move)
                        {
                            Cl_EntityLog eLog = new Cl_EntityLog();
                            eLog.f_SetEntity(draggedNodeTemplate.p_Template);

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
                                    eLog.f_SaveEntity(draggedNodeTemplate.p_Template);
                                    transaction.Commit();
                                    draggedNodeTemplate.Remove();
                                    a_TargetNodeGroup.Nodes.Insert(f_GetFirstGroupInNode(a_TargetNodeGroup.Nodes), draggedNodeTemplate);
                                }
                            }
                            else
                            {
                                MonitoringStub.Error("Error_Tree", "Не найден шаблон", new Exception("EX ERROR"), "draggedNodeTemplate.p_Template.p_TemplateID = " + draggedNodeTemplate.p_Template.p_TemplateID, null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MonitoringStub.Error("Error_Tree", "При перемещении шаблона произошла ошибка", ex, null, null);
                        return;
                    }
                }
            }
        }

        private void f_TemplateNew(Cl_Template.E_TemplateType a_TemplateType)
        {
            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    Cl_EntityLog eLog = new Cl_EntityLog();

                    Cl_Template newTemplate = (Cl_Template)Activator.CreateInstance(typeof(Cl_Template));
                    eLog.f_SetEntity(newTemplate);
                    Cl_Group group = null;
                    if (p_SelectedGroup != null && p_SelectedGroup.p_Group != null)
                    {
                        group = p_SelectedGroup.p_Group;
                    }
                    Dlg_EditorTemplate dlg = new Dlg_EditorTemplate();
                    dlg.ctrlPCategories.Enabled = a_TemplateType == Cl_Template.E_TemplateType.Template;
                    dlg.p_CountColumn = 2;
                    if (a_TemplateType == Cl_Template.E_TemplateType.Template)
                        dlg.Text = "Новый шаблон";
                    else if (a_TemplateType == Cl_Template.E_TemplateType.Block)
                        dlg.Text = "Новый блок";
                    else if (a_TemplateType == Cl_Template.E_TemplateType.Table)
                        dlg.Text = "Новая таблица";
                    if (group != null)
                    {
                        newTemplate.p_ParentGroup = p_SelectedGroup.p_Group;
                        dlg.ctrl_LGroupValue.Text = p_SelectedGroup.p_Group.p_Name;
                    }
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    newTemplate.p_Name = dlg.ctrl_TBName.Text;
                    newTemplate.p_Title = dlg.ctrlTitle.Text;
                    newTemplate.p_Type = a_TemplateType;
                    if (a_TemplateType == Cl_Template.E_TemplateType.Template)
                    {
                        var catTotal = (Cl_Category)dlg.ctrlCategoriesTotal.SelectedItem;
                        newTemplate.p_CategoryTotalID = catTotal.p_ID;
                        newTemplate.p_CategoryTotal = catTotal;
                        var catClinic = (Cl_Category)dlg.ctrlCategoriesClinic.SelectedItem;
                        newTemplate.p_CategoryClinicID = catClinic.p_ID;
                        newTemplate.p_CategoryClinic = catClinic;
                    }
                    Cl_App.m_DataContext.p_Templates.Add(newTemplate);
                    Cl_App.m_DataContext.SaveChanges();
                    newTemplate.p_TemplateID = newTemplate.p_ID;
                    Cl_App.m_DataContext.SaveChanges();
                    eLog.f_SaveEntity(newTemplate);
                    transaction.Commit();

                    var newNode = new Ctrl_TreeNodeTemplate(group, newTemplate);
                    SelectedNode.Nodes.Add(newNode);
                    e_EditTemplate?.Invoke(ctrl_TemplateEditParams, new TreeViewEventArgs(newNode));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Tree", "При создании нового шаблона произошла ошибка", ex, null, null);
                    return;
                }
            }
        }

        private void ctrl_TemplateNew_Click(object sender, EventArgs e)
        {
            f_TemplateNew(Cl_Template.E_TemplateType.Template);
        }

        private void ctrl_BlockNew_Click(object sender, EventArgs e)
        {
            f_TemplateNew(Cl_Template.E_TemplateType.Block);
        }

        private void ctrl_TableNew_Click(object sender, EventArgs e)
        {
            f_TemplateNew(Cl_Template.E_TemplateType.Table);
        }

        private void ctrl_TemplateEditParams_Click(object sender, EventArgs e)
        {
            if (p_SelectedTemplate != null)
            {
                e_EditTemplateParams?.Invoke(sender, new TreeViewEventArgs(p_SelectedTemplate));
            }
            var tpl = Cl_App.m_DataContext.p_Templates.Where(t => t.p_TemplateID == p_SelectedTemplate.p_Template.p_TemplateID && !t.p_IsDelete).OrderByDescending(v => v.p_Version).FirstOrDefault();
            if (tpl != null)
            {
                Dlg_EditorTemplate dlg = new Dlg_EditorTemplate();
                dlg.ctrlPCategories.Enabled = dlg.ctrlPCountColumns.Enabled = tpl.p_Type == Cl_Template.E_TemplateType.Template;
                dlg.p_CountColumn = tpl.p_CountColumn;
                if (tpl.p_Type == Cl_Template.E_TemplateType.Template)
                    dlg.Text = "Редактирование параметров шаблона";
                else if (tpl.p_Type == Cl_Template.E_TemplateType.Block)
                    dlg.Text = "Редактирование параметров блока";
                else if (tpl.p_Type == Cl_Template.E_TemplateType.Table)
                    dlg.Text = "Редактирование параметров таблицы";
                dlg.ctrl_LGroupValue.Text = tpl.p_ParentGroup.p_Name;
                dlg.ctrl_TBName.Text = tpl.p_Name;
                dlg.ctrlTitle.Text = tpl.p_Title;
                dlg.ctrlCategoriesTotal.SelectedItem = tpl.p_CategoryTotal;
                dlg.ctrlCategoriesClinic.SelectedItem = tpl.p_CategoryClinic;
                if (dlg.ShowDialog() != DialogResult.OK) return;
                tpl.p_Name = dlg.ctrl_TBName.Text;
                tpl.p_Title = dlg.ctrlTitle.Text;
                if (tpl.p_Type == Cl_Template.E_TemplateType.Template)
                {
                    var catTotal = (Cl_Category)dlg.ctrlCategoriesTotal.SelectedItem;
                    tpl.p_CategoryTotalID = catTotal.p_ID;
                    tpl.p_CategoryTotal = catTotal;
                    var catClinic = (Cl_Category)dlg.ctrlCategoriesClinic.SelectedItem;
                    tpl.p_CategoryClinicID = catClinic.p_ID;
                    tpl.p_CategoryClinic = catClinic;
                    tpl.p_CountColumn = dlg.p_CountColumn;
                }
                Cl_App.m_DataContext.SaveChanges();
                SelectedNode.Text = tpl.p_Name;
            }
        }

        private void ctrl_TemplateEdit_Click(object sender, EventArgs e)
        {
            if (p_SelectedTemplate != null)
            {
                e_EditTemplate?.Invoke(sender, new TreeViewEventArgs(p_SelectedTemplate));
            }
        }

        private void ctrl_TemplateDelete_Click(object sender, EventArgs e)
        {
            if (p_SelectedTemplate == null && p_SelectedTemplate.p_Template == null) return;
            string typeName = "шаблон";
            string typeNameR = "шаблона";
            if (p_SelectedTemplate.p_Template.p_Type == Cl_Template.E_TemplateType.Block)
            {
                typeName = "блок";
                typeNameR = "блока";
            }
            else if(p_SelectedTemplate.p_Template.p_Type == Cl_Template.E_TemplateType.Table)
            {
                typeName = "таблицу";
                typeNameR = "таблицы";
            }
            if (MessageBox.Show($"Удалить \"{typeName} {p_SelectedTemplate.p_Template.p_Name}\"?", $"Удаление {typeNameR}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    Cl_EntityLog eLog = new Cl_EntityLog();
                    var els = Cl_App.m_DataContext.p_Templates.Where(el => el.p_TemplateID == p_SelectedTemplate.p_Template.p_TemplateID).OrderByDescending(v => v.p_Version);
                    if (els != null)
                    {
                        Cl_Template lastVersion = els.FirstOrDefault();
                        eLog.f_SetEntity(lastVersion);
                        bool isChange = false;
                        foreach (Cl_Template el in els)
                        {
                            el.p_IsDelete = true;
                            isChange = true;
                        }
                        if (isChange)
                        {
                            Cl_App.m_DataContext.SaveChanges();
                            eLog.f_SaveEntity(lastVersion);
                            transaction.Commit();
                            SelectedNode.Remove();
                        }
                    }
                    else
                    {
                        MonitoringStub.Error("Error_Tree", "Не найдена шаблон", new Exception("EX ERROR"), "p_SelectedTemplate.p_Template.p_TemplateID = " + p_SelectedTemplate.p_Template.p_TemplateID, null);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Tree", "При удалении шаблона произошла ошибка", ex, null, null);
                    return;
                }
            }
        }
    }
}
