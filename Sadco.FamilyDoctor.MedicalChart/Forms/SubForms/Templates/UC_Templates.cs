using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.MedicalChart.Entities.Controls;
using Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate;
using System;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	public partial class UC_EditorTemplates : UserControl
	{
		public const string WinTitle = "Редактор шаблонов v0.3";

		private Cl_GroupTemplate m_CurrentGroup = null;
		private Cl_CtrlTemplateNode m_CurrentControl = null;
		private Cl_GroupTemplate m_ParentGroup = null;
		private Em_NodeTypes m_SelectedNodeType = Em_NodeTypes.Nothing;
		private TreeNode m_SelectedNode = null;

		private enum Em_NodeTypes
		{
			Nothing,
			Group,
			Template
		}

		public UC_EditorTemplates() {
			this.Tag = WinTitle;

			InitializeComponent();
			f_InitTVControls();
		}

		#region Initialize tree menu
		private void f_InitTVControls() {
			UI_Helper.InitTreeView(ctrl_TVTemplates);

			Cl_GroupTemplate[] groups = Cl_App.m_DataContext.p_GroupsTemplate.Include(g => g.p_SubGroups).Where(e => e.p_ParentID == null).ToArray();
			foreach (Cl_GroupTemplate group in groups) {
				f_PopulateTVTemplates(group, ctrl_TVTemplates.Nodes);
			}
		}

		private void f_PopulateTVTemplates(Cl_GroupTemplate a_Group, TreeNodeCollection a_TreeNodes) {
			TreeNode node = UI_Helper.CreateNodeGroup(a_Group, a_TreeNodes);

			// Загрузка темплейтов
			foreach (Cl_Template control in Cl_App.m_DataContext.p_Templates.Where(t => t.p_ParentGroupID == a_Group.p_ID).ToArray()) {
				//UI_Helper.AddTreeNodeControl(new Cl_CtrlTemplateNode(), node.Nodes, control);
			}

			var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(c => c.p_SubGroups);
			if (!dcGroups.IsLoaded) dcGroups.Load();
			// Загрузка групп
			foreach (Cl_GroupTemplate group in a_Group.p_SubGroups) {
				f_PopulateTVTemplates(group, node.Nodes);
			}
		}
		#endregion

		/// <summary>
		/// Возвращает тип элемента
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private Em_NodeTypes f_GetTypeNode(TreeNode node) {
			Em_NodeTypes outType = Em_NodeTypes.Nothing;

			if (node == null) return outType;

			if (node.Tag is Cl_CtrlTemplateNode) {
				outType = Em_NodeTypes.Template;
			} else {
				outType = Em_NodeTypes.Group;
			}

			return outType;
		}

		/// <summary>
		/// Определяет какие элементы были выбраны в UI меню
		/// </summary>
		private void f_GetSelectedItems() {
			int itemID = Convert.ToInt32(ctrl_TVTemplates.SelectedNode.Name);

			m_SelectedNode = ctrl_TVTemplates.SelectedNode;
			m_SelectedNodeType = f_GetTypeNode(ctrl_TVTemplates.SelectedNode);

			if (m_SelectedNodeType == Em_NodeTypes.Template) {
				m_CurrentControl = (Cl_CtrlTemplateNode)ctrl_TVTemplates.SelectedNode.Tag;
				if (m_CurrentControl == null) {
					m_CurrentControl = null;
					m_CurrentGroup = null;
					m_ParentGroup = null;
					return;
				}

				m_CurrentGroup = m_CurrentControl.p_Template.p_ParentGroup;
			} else if (m_SelectedNodeType == Em_NodeTypes.Group) {
				m_CurrentControl = null;
				m_CurrentGroup = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == itemID);
			} else {
				m_CurrentControl = null;
				m_CurrentGroup = null;
				m_ParentGroup = null;
				return;
			}

			m_ParentGroup = m_CurrentGroup.p_Parent;
		}

		#region unused
		/// <summary>
		/// Возвращает номер позиции группы в меню
		/// </summary>
		/// <param name="nodes">Список элементов, в котором осуществляется поиск</param>
		/// <returns></returns>
		private int f_GetFirstGroupInNode(TreeNodeCollection nodes) {
			int idx = 1;
			foreach (TreeNode itemNode in nodes) {
				idx = itemNode.Index;
				if (f_GetTypeNode(itemNode) == Em_NodeTypes.Group) break;
			}

			return idx;
		}

		private bool f_ContainsNode(TreeNode node1, TreeNode node2) {
			if (node2 == null || node2.Parent == null)
				return false;
			if (node2.Parent.Equals(node1))
				return true;
			return f_ContainsNode(node1, node2.Parent);
		}
		#endregion

		/// <summary>
		/// Создание новой группы с сохранением её в базе и в ветке меню
		/// </summary>
		private void f_CreateNewGroup() {
			F_GroupTemplate fGroup = new F_GroupTemplate();
			fGroup.ctrl_LParentValue.Text = m_CurrentGroup.f_GetFullName();
			if (fGroup.ShowDialog() == DialogResult.OK) {
				Cl_GroupTemplate group = new Cl_GroupTemplate();
				group.p_Name = fGroup.ctrl_TBName.Text;
				if (m_CurrentGroup != null) group.p_ParentID = m_CurrentGroup.p_ID;

				Cl_App.m_DataContext.p_GroupsTemplate.Add(group);
				Cl_App.m_DataContext.SaveChanges();

				switch (m_SelectedNodeType) {
					case Em_NodeTypes.Template:
						UI_Helper.CreateNodeGroup(group, ctrl_TVTemplates.SelectedNode.Parent.Nodes);
						break;
					case Em_NodeTypes.Group:
						UI_Helper.CreateNodeGroup(group, ctrl_TVTemplates.SelectedNode.Nodes);
						break;
					case Em_NodeTypes.Nothing:
						UI_Helper.CreateNodeGroup(group, ctrl_TVTemplates.Nodes);
						return;
				}
			}
		}

		/// <summary>
		/// Удаляет выбранную группу
		/// </summary>
		private void f_DeleteGroup() {
			if (m_CurrentGroup == null) return;

			Cl_GroupTemplate parentGroup = m_CurrentGroup.p_Parent;
			if (parentGroup == null) return;

			Cl_App.m_DataContext.p_GroupsTemplate.Remove(m_CurrentGroup);
			ctrl_TVTemplates.SelectedNode.Remove();

			Cl_App.m_DataContext.SaveChanges();
		}

		/// <summary>
		/// Создание нового элемента шаблона с сохранением его в базе и в ветке меню
		/// </summary>
		private void f_CreateNewTemplate() {
			F_Template fTemplate = new F_Template();
			fTemplate.ctrl_LGroupValue.Text = m_CurrentGroup.f_GetFullName();
			if (fTemplate.ShowDialog() != DialogResult.OK) return;

			Cl_Template newTemplate = new Cl_Template();
			newTemplate.p_ParentGroupID = m_CurrentGroup.p_ID;
			newTemplate.p_Name = fTemplate.ctrl_TBName.Text;
			newTemplate.p_Description = fTemplate.ctrl_TBDecs.Text;

			//UI_Helper.AddTreeNodeControl(new Cl_CtrlTemplateNode(), m_SelectedNodeType == Em_NodeTypes.Group ? m_SelectedNode.Nodes : m_SelectedNode.Parent.Nodes, newTemplate);

			Cl_App.m_DataContext.p_Templates.Add(newTemplate);
			Cl_App.m_DataContext.SaveChanges();
		}

		/// <summary>
		/// Удаляет выбранный шаблон
		/// </summary>
		private void f_DeleteTemplate() {
			if (m_SelectedNodeType != Em_NodeTypes.Template) return;

			Cl_CtrlTemplateNode curControl = m_CurrentControl;
			if (curControl == null) return;

			Cl_App.m_DataContext.p_Templates.Remove(curControl.p_Template);
			curControl.Remove();
			Cl_App.m_DataContext.SaveChanges();
		}

		/// <summary>
		/// Обновление списка темплейтов в области свойств группы
		/// </summary>
		private void f_LoadTemplates() {
			if (m_SelectedNodeType != Em_NodeTypes.Nothing) ctrl_LVTemplates.Items.Clear();

			if (m_SelectedNodeType == Em_NodeTypes.Group) {
				Cl_Template[] templates = Cl_App.m_DataContext.p_Templates.Where(t => t.p_ParentGroupID == m_CurrentGroup.p_ID).ToArray();
				foreach (Cl_Template template in templates) {
					ListViewItem listitem = new ListViewItem(new string[] { template.p_Name, template.p_Description });
					listitem.Tag = template.p_ID;
					ctrl_LVTemplates.Items.Add(listitem);
				}
			} else if (m_SelectedNodeType == Em_NodeTypes.Template) {
				Cl_TemplatesElements[] controls = Cl_App.m_DataContext.p_TemplatesElements.Where(t => t.p_TemplateID == m_CurrentControl.p_Template.p_ID).ToArray();
				foreach (Cl_TemplatesElements control in controls) {
					if (control.p_Element == null) continue;

					ListViewItem listitem = new ListViewItem(new string[] { control.p_Element.p_Name, control.p_ControlType });
					ctrl_LVTemplates.Items.Add(listitem);
				}
			}
		}

		#region UI
		private void Ctrl_CMTemplate_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			f_GetSelectedItems();

			if (ctrl_TVTemplates.SelectedNode == null) {
				ctrl_MIGroupNew.Visible = true;
				ctrl_MIGroupEdit.Visible = false;
				ctrl_MIGroupDelete.Visible = false;
				ctrl_MITemplateNew.Visible = false;
				ctrl_MITemplateEdit.Visible = false;
				ctrl_MITemplateDelete.Visible = false;
				return;
			}

			switch (m_SelectedNodeType) {
				case Em_NodeTypes.Template:
					ctrl_MIGroupNew.Visible = false;
					ctrl_MIGroupEdit.Visible = false;
					ctrl_MIGroupDelete.Visible = false;
					ctrl_MITemplateNew.Visible = false;
					ctrl_MITemplateEdit.Visible = true;
					ctrl_MITemplateDelete.Visible = true;
					break;
				case Em_NodeTypes.Group:
					if (m_CurrentGroup == null) {
						e.Cancel = true;
						return;
					}

					ctrl_MIGroupNew.Visible = true;
					ctrl_MIGroupEdit.Visible = true;
					ctrl_MIGroupDelete.Visible = true;
					ctrl_MITemplateNew.Visible = true;
					ctrl_MITemplateEdit.Visible = false;
					ctrl_MITemplateDelete.Visible = false;

					break;
				default:
					e.Cancel = true;
					return;
			}
		}

		/// <summary>
		/// Меню -> Добавить группу/шаблон
		/// </summary>
		private void ctrl_MITemplates_Click(object sender, EventArgs e) {
			ToolStripMenuItem mItem = (ToolStripMenuItem)sender;

			switch (mItem.Tag.ToString()) {
				case "MI_GroupNew":
					f_CreateNewGroup();
					break;
				case "MI_GroupEdit":
					f_EditGroup();
					break;
				case "MI_GroupDelete":
					f_DeleteGroup();
					break;
				case "MI_TemplateNew":
					f_CreateNewTemplate();
					break;
				case "MI_TemplateEdit":
					f_EditTemplate();
					break;
				case "MI_TemplateDelete":
					f_DeleteTemplate();
					break;
			}
		}

		private void f_EditTemplate() {
			F_LocationEditor editor = new F_LocationEditor();
			editor.p_ActiveTemplate = m_CurrentControl.p_Template;
			editor.FormClosed += Editor_FormClosed;

			editor.Show(this.ParentForm);
		}

		private void Editor_FormClosed(object sender, FormClosedEventArgs e) {
			f_LoadTemplates();
		}

		#region TVTemplates
		#region TVTemplates drag events
		private void ctrl_TVTemplates_ItemDrag(object sender, ItemDragEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				DoDragDrop(e.Item, DragDropEffects.Move);
			} else if (e.Button == MouseButtons.Right) {
				DoDragDrop(e.Item, DragDropEffects.Copy);
			}
		}

		private void ctrl_TVTemplates_DragOver(object sender, DragEventArgs e) {
			Point targetPoint = ctrl_TVTemplates.PointToClient(new Point(e.X, e.Y));
			ctrl_TVTemplates.SelectedNode = ctrl_TVTemplates.GetNodeAt(targetPoint);
		}

		private void ctrl_TVTemplates_DragEnter(object sender, DragEventArgs e) {
			e.Effect = e.AllowedEffect;
		}

		private void ctrl_TVTemplates_DragDrop(object sender, DragEventArgs e) {
			/*Point targetPoint = ctrl_TVTemplates.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = ctrl_TVTemplates.GetNodeAt(targetPoint);
			TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

			//if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Group && draggedNode.Name == "1")
			//	return;

			if (targetNode == null) {
				if (e.Effect == DragDropEffects.Move) {
					if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Template) return;

					draggedNode.Remove();
					ctrl_TVTemplates.Nodes.Add(draggedNode);
					int targetID = Convert.ToInt32(draggedNode.Name);
					Cl_GroupsTemplate group = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == targetID);
					if (group != null) {
						group.p_ParentID = null;
					} else {
						throw new Exception("Не найдена группа шаблонов");
					}

				} else if (e.Effect == DragDropEffects.Copy) {
					if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Template) return;

					ctrl_TVTemplates.Nodes.Add((TreeNode)draggedNode.Clone());
					Cl_GroupsTemplate group = new Cl_GroupsTemplate() { p_Name = draggedNode.Text };
					Cl_App.m_DataContext.p_GroupsTemplate.Add(group);
				}
				Cl_App.m_DataContext.SaveChanges();
			} else if (!draggedNode.Equals(targetNode) && !f_ContainsNode(draggedNode, targetNode)) {
				if (e.Effect == DragDropEffects.Move) {
					int draggedID = Convert.ToInt32(draggedNode.Name);
					int targetID = Convert.ToInt32(targetNode.Name);

					if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Template) {
						Cl_Template draggedTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == draggedID);
						Cl_GroupsTemplate newParentGroup = null;

						if (f_GetTypeNode(targetNode) == Em_NodeTypes.Template) {
							Cl_Template targetTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == targetID);
							newParentGroup = targetTemplate.p_ParentGroup;
							if (draggedTemplate.p_ParentGroup.p_ID == newParentGroup.p_ID) return;

							draggedNode.Remove();
							targetNode.Parent.Nodes.Insert(f_GetFirstGroupInNode(targetNode.Parent.Nodes), draggedNode);
						} else {
							newParentGroup = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == targetID);
							if (draggedTemplate.p_ParentGroup.p_ID == newParentGroup.p_ID) return;

							draggedNode.Remove();
							targetNode.Nodes.Insert(f_GetFirstGroupInNode(targetNode.Nodes), draggedNode);
						}

						draggedTemplate.p_ParentGroup = newParentGroup;
					} else {
						Cl_GroupsTemplate draggedGroup = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == draggedID); ;
						Cl_GroupsTemplate newParentGroup = null;

						if (f_GetTypeNode(targetNode) == Em_NodeTypes.Template) {
							Cl_Template targetTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == targetID);
							newParentGroup = targetTemplate.p_ParentGroup;
							if (draggedGroup.p_Parent != null && draggedGroup.p_Parent.p_ID == newParentGroup.p_ID) return;

							draggedNode.Remove();
							targetNode.Parent.Nodes.Add(draggedNode);
						} else {
							newParentGroup = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == targetID);
							if (draggedGroup.p_Parent != null && draggedGroup.p_Parent.p_ID == newParentGroup.p_ID) return;

							draggedNode.Remove();
							targetNode.Nodes.Add(draggedNode);
						}

						draggedGroup.p_Parent = newParentGroup;
					}
				} else if (e.Effect == DragDropEffects.Copy) {
					int draggedID = Convert.ToInt32(draggedNode.Name);
					int targetID = Convert.ToInt32(targetNode.Name);
					TreeNode parentNode = null;

					if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Template) {
						Cl_Template draggedTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == draggedID);
						Cl_GroupsTemplate newParentGroup = null;

						if (f_GetTypeNode(targetNode) == Em_NodeTypes.Template) {
							Cl_Template targetTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == targetID);
							newParentGroup = targetTemplate.p_ParentGroup;
							parentNode = targetNode.Parent;
						} else {
							newParentGroup = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == targetID);
							parentNode = targetNode;
						}

						Cl_Template template = new Cl_Template() {
							p_ParentGroup = newParentGroup,
							p_Name = draggedTemplate.p_Name,
							p_Description = draggedTemplate.p_Description
						};
						Cl_App.m_DataContext.p_Teplates.Add(template);
						Cl_App.m_DataContext.SaveChanges();
						f_CreateNodeTemplate(template, parentNode);
					} else {
						Cl_GroupsTemplate draggedGroup = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == draggedID); ;
						Cl_GroupsTemplate newParentGroup = null;

						if (f_GetTypeNode(targetNode) == Em_NodeTypes.Template) {
							Cl_Template targetTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == targetID);
							newParentGroup = targetTemplate.p_ParentGroup;
							parentNode = targetNode.Parent;
						} else {
							newParentGroup = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == targetID);
							parentNode = targetNode;
						}

						Cl_GroupsTemplate group = new Cl_GroupsTemplate();
						group.p_Name = draggedGroup.p_Name;
						group.p_Parent = newParentGroup;
						Cl_App.m_DataContext.p_GroupsTemplate.Add(group);
						Cl_App.m_DataContext.SaveChanges();
						f_CreateNodeGroup(group, parentNode.Nodes);
					}
				}

				targetNode.Expand();
				Cl_App.m_DataContext.SaveChanges();
			}*/
		}
		#endregion

		private void ctrl_TVTemplates_AfterSelect(object sender, TreeViewEventArgs e) {
			f_GetSelectedItems();
			f_LoadTemplates();
		}
		#endregion

		#region Events button
		private void f_EditGroup() {
			if (m_SelectedNodeType != Em_NodeTypes.Group) return;

			F_Template fTemplate = new F_Template();
			fTemplate.ctrl_LGroupValue.Text = m_CurrentGroup.f_GetFullName();
			fTemplate.ctrl_TBName.Text = m_CurrentGroup.p_Name;
			fTemplate.ctrl_TBDecs.Visible = false;

			if (fTemplate.ShowDialog() != DialogResult.OK) return;

			m_CurrentGroup.p_Name = fTemplate.ctrl_TBName.Text;
			m_SelectedNode.Text = m_CurrentGroup.p_Name;

			Cl_App.m_DataContext.SaveChanges();

			f_LoadTemplates();
		}
		#endregion
		#endregion
	}
}
