using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Entities.Controls;
using Sadco.FamilyDoctor.MedicalChart.Entities.Controls;
using Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
	public partial class UC_EditorElements : UserControl
	{
		ImageList myImageList = new ImageList();

		private Cl_GroupsControl m_CurrentGroup = null;
		private Cl_CtrlTreeNode m_CurrentControl = null;
		private Cl_GroupsControl m_ParentGroup = null;
		private Em_NodeTypes m_SelectedNodeType = Em_NodeTypes.Nothing;
		private TreeNode m_SelectedNode = null;

		private enum Em_NodeTypes
		{
			Nothing,
			Group,
			Control
		}

		public UC_EditorElements() {
			InitializeComponent();
			f_InitTVControls();
		}


		#region Initialize
		private void f_InitCreateControls() {
			var type = typeof(I_Control);
			var types = AppDomain.CurrentDomain.GetAssemblies()
				 .SelectMany(s => s.GetTypes())
				 .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract && p.CustomAttributes.Count() > 0);

			foreach (var item in types) {
				DescriptionAttribute description = (DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute));

				if (description == null) continue;

				ToolStripItem newItem = ctrl_MIControlNew.DropDownItems.Add(description.Description);
				newItem.Click += ctrl_MITemplates_Click;
				newItem.Tag = item.Name;
			}

			ctrl_TVTemplates.ImageList = myImageList;

			f_SetMenuImage(Properties.Resources.folder, "folder");
			ctrl_TVTemplates.ImageKey = "folder";
			ctrl_TVTemplates.SelectedImageKey = "folder";
		}

		private void f_InitTVControls() {
			f_InitCreateControls();

			ctrl_TVTemplates.Nodes.Clear();
			Cl_GroupsControl[] groups = Cl_App.m_DataContext.p_GroupsControl.Include(g => g.p_SubGroups).Where(e => e.p_ParentID == null).ToArray();
			foreach (Cl_GroupsControl group in groups) {
				f_PopulateTVControls(group, ctrl_TVTemplates.Nodes);
			}
		}

		private void f_SetMenuImage(Image img, string nameKey) {
			if (myImageList.Images.ContainsKey(nameKey)) return;
			myImageList.Images.Add(nameKey, img);
		}

		private void f_PopulateTVControls(Cl_GroupsControl a_Group, TreeNodeCollection a_TreeNodes) {
			TreeNode node = f_CreateNodeGroup(a_Group, a_TreeNodes);

			var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(c => c.p_SubGroups);
			if (!dcGroups.IsLoaded) dcGroups.Load();

			// Загрузка контролов
			foreach (Cl_CtrlText control in Cl_App.m_DataContext.p_Elms_Text.Include(el => el.p_BaseControl).Where(t => t.p_BaseControl.p_ParentGroupID == a_Group.p_ID)) {
				Cl_CtrlTreeNode ctrlTreeNode = new Cl_CtrlTreeNode();
				ctrlTreeNode.p_Control = control;
				ctrlTreeNode.f_AddToTreeNode(node.Nodes);
				f_SetMenuImage(control.p_BaseControl.p_MenuIcon, control.p_BaseControl.p_Image);
				ctrlTreeNode.ImageKey = control.p_BaseControl.p_Image;
				ctrlTreeNode.SelectedImageKey = control.p_BaseControl.p_Image;
			}

			foreach (Cl_CtrlComboBox control in Cl_App.m_DataContext.p_Elms_ComboBox.Include(el => el.p_BaseControl).Where(t => t.p_BaseControl.p_ParentGroupID == a_Group.p_ID)) {
				Cl_CtrlTreeNode ctrlTreeNode = new Cl_CtrlTreeNode();
				ctrlTreeNode.p_Control = control;
				ctrlTreeNode.f_AddToTreeNode(node.Nodes);
				f_SetMenuImage(control.p_BaseControl.p_MenuIcon, control.p_BaseControl.p_Image);
				ctrlTreeNode.ImageKey = control.p_BaseControl.p_Image;
				ctrlTreeNode.SelectedImageKey = control.p_BaseControl.p_Image;
			}

			// Загрузка групп
			foreach (Cl_GroupsControl group in a_Group.p_SubGroups) {
				f_PopulateTVControls(group, node.Nodes);
			}

			ctrl_Pnl_Properties.Visible = false;
		}
		#endregion


		/// <summary>
		/// Определяет какие элементы были выбраны в UI меню
		/// </summary>
		private void f_GetSelectedItems() {
			int itemID = Convert.ToInt32(ctrl_TVTemplates.SelectedNode.Name);

			m_SelectedNode = ctrl_TVTemplates.SelectedNode;
			m_SelectedNodeType = f_GetTypeNode(ctrl_TVTemplates.SelectedNode);

			if (m_SelectedNodeType == Em_NodeTypes.Control) {
				m_CurrentControl = (Cl_CtrlTreeNode)ctrl_TVTemplates.SelectedNode.Tag;
				if (m_CurrentControl == null) {
					m_CurrentControl = null;
					m_CurrentGroup = null;
					m_ParentGroup = null;
					return;
				}

				m_CurrentGroup = m_CurrentControl.p_Control.p_BaseControl.p_ParentGroup;
			} else if (m_SelectedNodeType == Em_NodeTypes.Group) {
				m_CurrentControl = null;
				m_CurrentGroup = Cl_App.m_DataContext.p_GroupsControl.FirstOrDefault(g => g.p_ID == itemID);
			} else {
				m_CurrentControl = null;
				m_CurrentGroup = null;
				m_ParentGroup = null;
				return;
			}

			m_ParentGroup = m_CurrentGroup.p_Parent;
		}

		/// <summary>
		/// Возвращает тип элемента
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private Em_NodeTypes f_GetTypeNode(TreeNode node) {
			Em_NodeTypes outType = Em_NodeTypes.Nothing;

			if (node == null) return outType;

			if (node.Tag is Cl_CtrlTreeNode) {
				outType = Em_NodeTypes.Control;
			} else {
				outType = Em_NodeTypes.Group;
			}

			return outType;
		}

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

		/// <summary>
		/// Добавление нового элемента группы в передаваемую колекцию элементов
		/// </summary>
		/// <param name="group">Объект создаваемой группы</param>
		/// <param name="nodes">Колекция, в которую помещается новый элемент</param>
		/// <returns></returns>
		private TreeNode f_CreateNodeGroup(Cl_GroupsControl group, TreeNodeCollection nodes) {
			return nodes.Add(group.p_ID.ToString(), group.p_Name);
		}

		private bool f_ContainsNode(TreeNode node1, TreeNode node2) {
			if (node2 == null || node2.Parent == null)
				return false;
			if (node2.Parent.Equals(node1))
				return true;
			return f_ContainsNode(node1, node2.Parent);
		}

		/// <summary>
		/// Создание новой группы с сохранением её в базе и в ветке меню
		/// </summary>
		private void f_CreateNewGroup() {
			F_GroupTemplate fGroup = new F_GroupTemplate();
			fGroup.ctrl_LParentValue.Text = m_CurrentGroup.f_GetFullName();
			if (fGroup.ShowDialog() == DialogResult.OK) {
				Cl_GroupsControl group = new Cl_GroupsControl();
				group.p_Name = fGroup.ctrl_TBName.Text;
				if (m_CurrentGroup != null) group.p_ParentID = m_CurrentGroup.p_ID;

				Cl_App.m_DataContext.p_GroupsControl.Add(group);
				Cl_App.m_DataContext.SaveChanges();

				switch (m_SelectedNodeType) {
					case Em_NodeTypes.Control:
						f_CreateNodeGroup(group, ctrl_TVTemplates.SelectedNode.Parent.Nodes);
						break;
					case Em_NodeTypes.Group:
						f_CreateNodeGroup(group, ctrl_TVTemplates.SelectedNode.Nodes);
						break;
					case Em_NodeTypes.Nothing:
						f_CreateNodeGroup(group, ctrl_TVTemplates.Nodes);
						return;
				}
			}
		}

		/// <summary>
		/// Удаляет выбранную группу
		/// </summary>
		private void f_DeleteGroup() {
			if (m_CurrentGroup == null) return;

			Cl_GroupsControl parentGroup = m_CurrentGroup.p_Parent;
			if (parentGroup == null) return;

			Cl_App.m_DataContext.p_GroupsControl.Remove(m_CurrentGroup);
			ctrl_TVTemplates.SelectedNode.Remove();

			Cl_App.m_DataContext.SaveChanges();
		}

		private void f_CreateNewControl(string v) {
			I_Control newControl = null;
			Cl_CtrlTreeNode newNode = new Cl_CtrlTreeNode();

			switch (v) {
				case "Cl_CtrlText":
					newControl = new Cl_CtrlText();
					break;
				case "Cl_ComboBox":
					newControl = new Cl_CtrlComboBox();
					break;
				default:
					return;
			}

			F_Template fTemplate = new F_Template();
			if (fTemplate.ShowDialog() != DialogResult.OK) return;

			newControl.p_BaseControl.p_ParentGroup = m_CurrentGroup;
			newControl.p_BaseControl.p_Name = fTemplate.ctrl_TBName.Text;
			newControl.p_BaseControl.p_Comment = fTemplate.ctrl_TBDecs.Text;

			newNode.p_Control = newControl;
			newNode.f_AddToTreeNode(m_SelectedNodeType == Em_NodeTypes.Group ? m_SelectedNode.Nodes : m_SelectedNode.Parent.Nodes);
			f_SetMenuImage(newControl.p_BaseControl.p_MenuIcon, newControl.p_BaseControl.p_Image);

			newNode.ImageKey = newControl.p_BaseControl.p_Image;
			newNode.SelectedImageKey = newControl.p_BaseControl.p_Image;

			switch (v) {
				case "Cl_CtrlText":
					Cl_App.m_DataContext.p_Elms_Text.Add((Cl_CtrlText)newControl);
					break;
				case "Cl_ComboBox":
					Cl_App.m_DataContext.p_Elms_ComboBox.Add((Cl_CtrlComboBox)newControl);
					break;
			}

			Cl_App.m_DataContext.SaveChanges();
		}

		private void f_DeleteControl() {
			if (m_SelectedNodeType != Em_NodeTypes.Control) return;

			Cl_CtrlTreeNode curControl = m_CurrentControl;
			if (curControl == null) return;

			Cl_App.m_DataContext.p_BaseControls.Remove(curControl.p_Control.p_BaseControl);
			if (curControl.p_Control is Cl_CtrlComboBox) {
				Cl_CtrlComboBox cl_item = (Cl_CtrlComboBox)curControl.p_Control;
				Cl_App.m_DataContext.p_Elms_ComboBox.Remove(cl_item);
			} else if (curControl.p_Control is Cl_CtrlText) {
				Cl_CtrlText cl_item = (Cl_CtrlText)curControl.p_Control;
				Cl_App.m_DataContext.p_Elms_Text.Remove(cl_item);
			} else {
				return;
			}

			curControl.Remove();

			Cl_App.m_DataContext.SaveChanges();
		}

		private void f_LoadProperties() {
			I_Control activeControl = m_CurrentControl.p_Control;

			ctrl_TB_Name.Text = activeControl.p_BaseControl.p_Name;
			ctrl_TB_Hint.Text = activeControl.p_BaseControl.p_Help;
			ctrl_CB_IsRequiredFIeld.Checked = activeControl.p_BaseControl.p_Required;
			ctrl_CB_IsEditing.Checked = activeControl.p_BaseControl.p_Editing;
			ctrl_CB_IsVisible.Checked = activeControl.p_BaseControl.p_Visible;
			ctrl_CB_IsSymmentry.Checked = activeControl.p_BaseControl.p_Symmetrical;
			ctrl_TB_Symmetry1.Text = activeControl.p_BaseControl.p_SymmetryParamLeft;
			ctrl_TB_Symmetry2.Text = activeControl.p_BaseControl.p_SymmetryParamRight;
			ctrl_TB_Note.Text = activeControl.p_BaseControl.p_Comment;

			if (activeControl is Cl_CtrlComboBox) {
				Cl_CtrlComboBox cl_item = (Cl_CtrlComboBox)activeControl;
				ctrl_LVTemplates.Visible = true;
				panel4.Visible = true;
				ctrl_TB_TextItem.Visible = false;

				ctrl_LVTemplates.Items.Clear();
				foreach (string item in cl_item.p_Elements) {
					ctrl_LVTemplates.Items.Add(item);
				}
			} else if (activeControl is Cl_CtrlText) {
				Cl_CtrlText cl_item = (Cl_CtrlText)activeControl;
				ctrl_LVTemplates.Visible = false;
				panel4.Visible = false;
				ctrl_TB_TextItem.Visible = true;

				ctrl_TB_TextItem.Text = cl_item.p_Text;
			} else {
				ctrl_LVTemplates.Visible = false;
				panel4.Visible = false;
				ctrl_TB_TextItem.Visible = false;
			}
		}

		private void f_SaveProperties(Cl_CtrlTreeNode editingControl) {
			editingControl.p_Control.p_BaseControl.p_Name = ctrl_TB_Name.Text;
			editingControl.p_Control.p_BaseControl.p_Help = ctrl_TB_Hint.Text;
			editingControl.p_Control.p_BaseControl.p_Required = ctrl_CB_IsRequiredFIeld.Checked;
			editingControl.p_Control.p_BaseControl.p_Editing = ctrl_CB_IsEditing.Checked;
			editingControl.p_Control.p_BaseControl.p_Visible = ctrl_CB_IsVisible.Checked;
			editingControl.p_Control.p_BaseControl.p_Symmetrical = ctrl_CB_IsSymmentry.Checked;
			editingControl.p_Control.p_BaseControl.p_SymmetryParamLeft = ctrl_TB_Symmetry1.Text;
			editingControl.p_Control.p_BaseControl.p_SymmetryParamRight = ctrl_TB_Symmetry2.Text;
			editingControl.p_Control.p_BaseControl.p_Comment = ctrl_TB_Note.Text;

			editingControl.Text = editingControl.p_ControlName;

			Cl_App.m_DataContext.SaveChanges();
		}

		#region UI
		private void Ctrl_CMTemplate_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			if (ctrl_TVTemplates.SelectedNode == null) {
				ctrl_MIGroupNew.Visible = true;
				ctrl_MIGroupDelete.Visible = false;
				ctrl_MIControlNew.Visible = false;
				ctrl_MIControlDelete.Visible = false;

				return;
			}

			f_GetSelectedItems();
			switch (m_SelectedNodeType) {
				case Em_NodeTypes.Control:
					ctrl_MIGroupNew.Visible = false;
					ctrl_MIGroupDelete.Visible = false;
					ctrl_MIControlNew.Visible = false;
					ctrl_MIControlDelete.Visible = true;
					break;
				case Em_NodeTypes.Group:
					if (m_CurrentGroup == null) {
						e.Cancel = true;
						return;
					}

					ctrl_MIGroupNew.Visible = true;
					ctrl_MIGroupDelete.Visible = true;
					ctrl_MIGroupDelete.Enabled = (m_ParentGroup != null);
					ctrl_MIControlNew.Visible = true;
					ctrl_MIControlDelete.Visible = false;

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
			f_GetSelectedItems();

			ToolStripMenuItem mItem = (ToolStripMenuItem)sender;

			switch (mItem.Tag.ToString()) {
				case "MI_GroupNew":
					f_CreateNewGroup();
					break;
				case "MI_GroupDelete":
					f_DeleteGroup();
					break;
				case "Cl_CtrlText":
					f_CreateNewControl("Cl_CtrlText");
					break;
				case "Cl_CtrlComboBox":
					f_CreateNewControl("Cl_ComboBox");
					break;
				case "MI_ControlDelete":
					f_DeleteControl();
					break;
			}
		}

		private void ctrl_TVTemplates_AfterSelect(object sender, TreeViewEventArgs e) {
			f_GetSelectedItems();

			ctrl_Pnl_Properties.Visible = (m_SelectedNodeType == Em_NodeTypes.Control);
			if (m_SelectedNodeType == Em_NodeTypes.Control) {
				f_LoadProperties();
			}
		}

		private void ctrl_BTemplateSave_Click(object sender, EventArgs e) {
			if (m_SelectedNodeType != Em_NodeTypes.Control) return;
			f_SaveProperties(m_CurrentControl);

			if (m_CurrentControl.p_Control is Cl_CtrlComboBox) {
				Cl_CtrlComboBox cl_item = (Cl_CtrlComboBox)m_CurrentControl.p_Control;
				cl_item.p_Elements.Clear();
				foreach (ListViewItem item in ctrl_LVTemplates.Items) {
					cl_item.p_Elements.Add(item.Text);
				}
			} else if (m_CurrentControl.p_Control is Cl_CtrlText) {
				Cl_CtrlText cl_item = (Cl_CtrlText)m_CurrentControl.p_Control;

				cl_item.p_Text = ctrl_TB_TextItem.Text;
			} else return;

			Cl_App.m_DataContext.SaveChanges();
		}

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
					if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Control) return;

					draggedNode.Remove();
					ctrl_TVTemplates.Nodes.Add(draggedNode);
					int targetID = Convert.ToInt32(draggedNode.Name);
					Cl_GroupsControl group = Cl_App.m_DataContext.p_GroupsControl.FirstOrDefault(g => g.p_ID == targetID);
					if (group != null) {
						group.p_ParentID = null;
					} else {
						throw new Exception("Не найдена группа шаблонов");
					}

				} else if (e.Effect == DragDropEffects.Copy) {
					if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Control) return;

					ctrl_TVTemplates.Nodes.Add((TreeNode)draggedNode.Clone());
					Cl_GroupsControl group = new Cl_GroupsControl() { p_Name = draggedNode.Text };
					Cl_App.m_DataContext.p_GroupsControl.Add(group);
				}
				Cl_App.m_DataContext.SaveChanges();
			} else if (!draggedNode.Equals(targetNode) && !f_ContainsNode(draggedNode, targetNode)) {
				if (e.Effect == DragDropEffects.Move) {
					int draggedID = Convert.ToInt32(draggedNode.Name);
					int targetID = Convert.ToInt32(targetNode.Name);

					if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Control) {
						Cl_Template draggedTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == draggedID);
						Cl_GroupsControl newParentGroup = null;

						if (f_GetTypeNode(targetNode) == Em_NodeTypes.Control) {
							Cl_Template targetTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == targetID);
							newParentGroup = targetTemplate.p_ParentGroup;
							if (draggedTemplate.p_ParentGroup.p_ID == newParentGroup.p_ID) return;

							draggedNode.Remove();
							targetNode.Parent.Nodes.Insert(f_GetFirstGroupInNode(targetNode.Parent.Nodes), draggedNode);
						} else {
							newParentGroup = Cl_App.m_DataContext.p_GroupsControl.FirstOrDefault(g => g.p_ID == targetID);
							if (draggedTemplate.p_ParentGroup.p_ID == newParentGroup.p_ID) return;

							draggedNode.Remove();
							targetNode.Nodes.Insert(f_GetFirstGroupInNode(targetNode.Nodes), draggedNode);
						}

						draggedTemplate.p_ParentGroup = newParentGroup;
					} else {
						Cl_GroupsControl draggedGroup = Cl_App.m_DataContext.p_GroupsControl.FirstOrDefault(g => g.p_ID == draggedID); ;
						Cl_GroupsControl newParentGroup = null;

						if (f_GetTypeNode(targetNode) == Em_NodeTypes.Control) {
							Cl_Template targetTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == targetID);
							newParentGroup = targetTemplate.p_ParentGroup;
							if (draggedGroup.p_Parent != null && draggedGroup.p_Parent.p_ID == newParentGroup.p_ID) return;

							draggedNode.Remove();
							targetNode.Parent.Nodes.Add(draggedNode);
						} else {
							newParentGroup = Cl_App.m_DataContext.p_GroupsControl.FirstOrDefault(g => g.p_ID == targetID);
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

					if (f_GetTypeNode(draggedNode) == Em_NodeTypes.Control) {
						Cl_Template draggedTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == draggedID);
						Cl_GroupsControl newParentGroup = null;

						if (f_GetTypeNode(targetNode) == Em_NodeTypes.Control) {
							Cl_Template targetTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == targetID);
							newParentGroup = targetTemplate.p_ParentGroup;
							parentNode = targetNode.Parent;
						} else {
							newParentGroup = Cl_App.m_DataContext.p_GroupsControl.FirstOrDefault(g => g.p_ID == targetID);
							parentNode = targetNode;
						}

						Cl_Template template = new Cl_Template() {
							p_ParentGroup = newParentGroup,
							p_Name = draggedTemplate.p_Name,
							p_Description = draggedTemplate.p_Description
						};
						Cl_App.m_DataContext.p_Teplates.Add(template);
						Cl_App.m_DataContext.SaveChanges();
						f_CreateNodeControl(template, parentNode);
					} else {
						Cl_GroupsControl draggedGroup = Cl_App.m_DataContext.p_GroupsControl.FirstOrDefault(g => g.p_ID == draggedID); ;
						Cl_GroupsControl newParentGroup = null;

						if (f_GetTypeNode(targetNode) == Em_NodeTypes.Control) {
							Cl_Template targetTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == targetID);
							newParentGroup = targetTemplate.p_ParentGroup;
							parentNode = targetNode.Parent;
						} else {
							newParentGroup = Cl_App.m_DataContext.p_GroupsControl.FirstOrDefault(g => g.p_ID == targetID);
							parentNode = targetNode;
						}

						Cl_GroupsControl group = new Cl_GroupsControl();
						group.p_Name = draggedGroup.p_Name;
						group.p_Parent = newParentGroup;
						Cl_App.m_DataContext.p_GroupsControl.Add(group);
						Cl_App.m_DataContext.SaveChanges();
						f_CreateNodeGroup(group, parentNode.Nodes);
					}
				}

				targetNode.Expand();
				Cl_App.m_DataContext.SaveChanges();
			}*/
		}
		#endregion

		#endregion

		private void ctrl_B_CBAddElement_Click(object sender, EventArgs e) {
			if (m_SelectedNodeType != Em_NodeTypes.Control) return;
			if (!(m_CurrentControl.p_Control is Cl_CtrlComboBox)) return;
			Cl_CtrlComboBox cl_item = (Cl_CtrlComboBox)m_CurrentControl.p_Control;

			F_Template fTemplate = new F_Template();
			if (fTemplate.ShowDialog() != DialogResult.OK) return;

			ctrl_LVTemplates.Items.Add(fTemplate.ctrl_TBName.Text);
		}

		private void button1_Click(object sender, EventArgs e) {
			if (ctrl_LVTemplates.SelectedItems.Count == 0) return;
			if (m_SelectedNodeType != Em_NodeTypes.Control) return;
			if (!(m_CurrentControl.p_Control is Cl_CtrlComboBox)) return;

			List<ListViewItem> delElements = new List<ListViewItem>();
			foreach (ListViewItem selItem in ctrl_LVTemplates.SelectedItems) {
				delElements.Add(selItem);
			}

			foreach (ListViewItem item in delElements) {
				ctrl_LVTemplates.Items.Remove(item);
			}
		}
	}
}
