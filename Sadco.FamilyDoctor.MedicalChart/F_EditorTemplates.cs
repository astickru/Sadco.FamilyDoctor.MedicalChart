using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate;
using System;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
	public partial class F_EditorTemplates : Form
	{
		public F_EditorTemplates() {
			InitializeComponent();
			f_InitTVTemplates();
		}

		private Cl_GroupsTemplate m_CurrentGroup = null;
		private Cl_Template m_CurrentTemplate = null;
		private Cl_GroupsTemplate m_ParentGroup = null;
		private Em_NodeTypes m_SelectedNode = Em_NodeTypes.Nothing;

		private enum Em_NodeTypes
		{
			Nothing,
			Group,
			Template
		}

		private void f_InitTVTemplates() {
			ctrl_TVTemplates.Nodes.Clear();
			Cl_GroupsTemplate[] groups = Cl_App.m_DataContext.p_GroupsTemplate.Include(g => g.p_SubGroups).Where(e => e.p_ParentID == null).ToArray();
			foreach (Cl_GroupsTemplate group in groups) {
				f_PopulateTVTemplates(group, ctrl_TVTemplates.Nodes);
			}
		}

		private void f_PopulateTVTemplates(Cl_GroupsTemplate a_Group, TreeNodeCollection a_TreeNodes) {
			TreeNode node = f_CreateNodeGroup(a_Group, a_TreeNodes);

			var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(c => c.p_SubGroups);
			if (!dcGroups.IsLoaded) {
				dcGroups.Load();
			}

			// Загрузка темплейтов
			Cl_Template[] templates = Cl_App.m_DataContext.p_Teplates.Where(t => t.p_ParentGroupID == a_Group.p_ID).ToArray();
			foreach (Cl_Template template in templates) {
				f_CreateNodeTemplate(template, node);
			}

			// Загрузка групп
			foreach (Cl_GroupsTemplate group in a_Group.p_SubGroups) {
				f_PopulateTVTemplates(group, node.Nodes);
			}
		}

		/// <summary>
		/// Определяет какие элементы были выбраны в UI меню
		/// </summary>
		private void f_GetSelectedItems() {
			int itemID = Convert.ToInt32(ctrl_TVTemplates.SelectedNode.Name);

			m_SelectedNode = f_GetTypeNode(ctrl_TVTemplates.SelectedNode);

			if (m_SelectedNode == Em_NodeTypes.Template) {
				m_CurrentTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(g => g.p_ID == itemID);
				if (m_CurrentTemplate == null) {
					m_CurrentTemplate = null;
					m_CurrentGroup = null;
					m_ParentGroup = null;
					return;
				}

				m_CurrentGroup = m_CurrentTemplate.p_ParentGroup;
			} else if (m_SelectedNode == Em_NodeTypes.Group) {
				m_CurrentTemplate = null;
				m_CurrentGroup = Cl_App.m_DataContext.p_GroupsTemplate.FirstOrDefault(g => g.p_ID == itemID);

			} else {
				m_CurrentTemplate = null;
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

			switch (node.Tag.ToString()) {
				case "template":
					outType = Em_NodeTypes.Template;
					break;
				case "group":
					outType = Em_NodeTypes.Group;
					break;
				default:
					break;
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
		/// Добавление нового элемента шаблона к выбранной ветке
		/// </summary>
		/// <param name="template">Объект создаваемого шаблона</param>
		/// <param name="parentNode">Родительский элемент, в который помещается новый элемент</param>
		/// <returns></returns>
		private TreeNode f_CreateNodeTemplate(Cl_Template template, TreeNode parentNode) {
			TreeNode nodeTemplate = parentNode.Nodes.Insert(f_GetFirstGroupInNode(parentNode.Nodes), template.p_ID.ToString(), template.p_Name);
			nodeTemplate.Tag = "template";
			nodeTemplate.ForeColor = Color.Blue;

			return nodeTemplate;
		}

		/// <summary>
		/// Добавление нового элемента группы в передаваемую колекцию элементов
		/// </summary>
		/// <param name="group">Объект создаваемой группы</param>
		/// <param name="nodes">Колекция, в которую помещается новый элемент</param>
		/// <returns></returns>
		private TreeNode f_CreateNodeGroup(Cl_GroupsTemplate group, TreeNodeCollection nodes) {
			TreeNode nodeGroup = nodes.Add(group.p_ID.ToString(), group.p_Name);
			nodeGroup.Tag = "group";

			return nodeGroup;
		}

		private bool f_ContainsNode(TreeNode node1, TreeNode node2) {
			if (node2 == null || node2.Parent == null)
				return false;
			if (node2.Parent.Equals(node1))
				return true;
			return f_ContainsNode(node1, node2.Parent);
		}

		/// <summary>
		/// Обновление списка темплейтов в области свойств группы
		/// </summary>
		/// <param name="a_GroupTemplates"></param>
		private void f_LoadTemplates(Cl_GroupsTemplate a_GroupTemplates) {
			ctrl_LVTemplates.Items.Clear();
			ctrl_TemplateTitle.Text = a_GroupTemplates.f_GetFullName();
			Cl_Template[] templates = Cl_App.m_DataContext.p_Teplates.Where(t => t.p_ParentGroupID == a_GroupTemplates.p_ID).ToArray();
			foreach (Cl_Template template in templates) {
				ListViewItem listitem = new ListViewItem(new string[] { template.p_Name, template.p_Description });
				listitem.Tag = template.p_ID;
				ctrl_LVTemplates.Items.Add(listitem);
			}
		}

		/// <summary>
		/// Создание нового элемента шаблона с сохранением его в базе и в ветке меню
		/// </summary>
		private void f_CreateNewTemplate() {
			F_Template fTemplate = new F_Template();
			fTemplate.ctrl_LGroupValue.Text = m_CurrentGroup.f_GetFullName();
			if (fTemplate.ShowDialog() == DialogResult.OK) {
				Cl_Template template = new Cl_Template() {
					p_ParentGroupID = m_CurrentGroup.p_ID,
					p_Name = fTemplate.ctrl_TBName.Text,
					p_Description = fTemplate.ctrl_TBDecs.Text
				};

				Cl_App.m_DataContext.p_Teplates.Add(template);
				Cl_App.m_DataContext.SaveChanges();

				switch (m_SelectedNode) {
					case Em_NodeTypes.Template:
						f_CreateNodeTemplate(template, ctrl_TVTemplates.SelectedNode.Parent);
						break;
					case Em_NodeTypes.Group:
						f_CreateNodeTemplate(template, ctrl_TVTemplates.SelectedNode);
						break;
					default:
						return;
				}

				f_LoadTemplates(m_CurrentGroup);
			}
		}

		/// <summary>
		/// Создание новой группы с сохранением её в базе и в ветке меню
		/// </summary>
		private void f_CreateNewGroup() {
			F_GroupTemplate fGroup = new F_GroupTemplate();
			fGroup.ctrl_LParentValue.Text = m_CurrentGroup.f_GetFullName();
			if (fGroup.ShowDialog() == DialogResult.OK) {
				Cl_GroupsTemplate group = new Cl_GroupsTemplate();
				group.p_Name = fGroup.ctrl_TBName.Text;
				if (m_CurrentGroup != null) group.p_ParentID = m_CurrentGroup.p_ID;

				Cl_App.m_DataContext.p_GroupsTemplate.Add(group);
				Cl_App.m_DataContext.SaveChanges();

				switch (m_SelectedNode) {
					case Em_NodeTypes.Template:
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

			Cl_GroupsTemplate parentGroup = m_CurrentGroup.p_Parent;
			if (parentGroup == null) return;

			Cl_App.m_DataContext.p_GroupsTemplate.Remove(m_CurrentGroup);
			ctrl_TVTemplates.SelectedNode.Remove();

			Cl_App.m_DataContext.SaveChanges();
			f_LoadTemplates(m_CurrentGroup);
		}

		/// <summary>
		/// Удаляет выбранный шаблон
		/// </summary>
		private void f_DeleteTemplate(bool fromMenu) {
			Cl_Template removingTemplate = null;

			f_GetSelectedItems();

			if (fromMenu) {
				if (m_CurrentTemplate == null) return;

				removingTemplate = m_CurrentTemplate;
			} else {
				if (ctrl_LVTemplates.SelectedItems != null && ctrl_LVTemplates.SelectedItems.Count == 1) {
					int templateId = (int)ctrl_LVTemplates.SelectedItems[0].Tag;
					removingTemplate = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(t => t.p_ID == templateId);
				}
			}

			if (removingTemplate != null) {
				Cl_App.m_DataContext.p_Teplates.Remove(m_CurrentTemplate);

				TreeNode removingNode = f_FindInActiveNodeTemplate(removingTemplate.p_ID.ToString());
				if (removingNode != null) removingNode.Remove();
			} else {
				throw new Exception("Не найден шаблон");
			}

			Cl_App.m_DataContext.SaveChanges();
			f_LoadTemplates(m_CurrentGroup);
		}

		/// <summary>
		/// Осуществляет поиск указанного темплейта в активной ветке
		/// </summary>
		/// <param name="templateID"></param>
		/// <returns></returns>
		private TreeNode f_FindInActiveNodeTemplate(string templateID) {
			TreeNodeCollection collection = null;
			TreeNode outNode = null;
			if (m_SelectedNode == Em_NodeTypes.Group) {
				collection = ctrl_TVTemplates.SelectedNode.Nodes;
			} else if (m_SelectedNode == Em_NodeTypes.Template) {
				collection = ctrl_TVTemplates.SelectedNode.Parent.Nodes;
			}

			foreach (TreeNode item in collection) {
				if (item.Name == templateID) {
					outNode = item;
					break;
				}
			}

			return outNode;
		}

		#region UI
		private void ctrl_BSearch_Click(object sender, EventArgs e) {

		}

		private void Ctrl_CMTemplate_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			if (ctrl_TVTemplates.SelectedNode == null) {
				ctrl_MIGroupNew.Visible = true;
				ctrl_MIGroupDelete.Visible = false;
				ctrl_MITemplateNew.Visible = false;
				ctrl_MITemplateDelete.Visible = false;
				return;
			}

			f_GetSelectedItems();
			switch (m_SelectedNode) {
				case Em_NodeTypes.Template:
					ctrl_MIGroupNew.Visible = false;
					ctrl_MIGroupDelete.Visible = false;
					ctrl_MITemplateNew.Visible = false;
					ctrl_MITemplateDelete.Visible = true;
					break;
				case Em_NodeTypes.Group:
					if (m_CurrentGroup == null) {
						e.Cancel = true;
						return;
					}

					ctrl_MIGroupNew.Visible = true;
					ctrl_MIGroupDelete.Visible = true;
					ctrl_MIGroupDelete.Enabled = (m_ParentGroup != null);
					ctrl_MITemplateNew.Visible = true;
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
				case "MI_GroupDelete":
					f_DeleteGroup();
					break;
				case "MI_TemplateNew":
					f_CreateNewTemplate();
					break;
				case "MI_TemplateDelete":
					f_DeleteTemplate(true);
					break;
			}
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
			Point targetPoint = ctrl_TVTemplates.PointToClient(new Point(e.X, e.Y));
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
			}
		}
		#endregion

		private void ctrl_TVTemplates_AfterSelect(object sender, TreeViewEventArgs e) {
			f_GetSelectedItems();

			if (m_CurrentGroup != null) {
				f_LoadTemplates(m_CurrentGroup);
			} else {
				throw new Exception("Не найдена группа шаблонов");
			}
		}

		private void ctrl_LVTemplates_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			ctrl_BTemplateDelete.Visible = ctrl_BTemplateEdit.Visible = e.IsSelected;
		}

		private void ctrl_LVTemplates_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (ctrl_LVTemplates.SelectedItems != null && ctrl_LVTemplates.SelectedItems.Count == 1) {
				int templateId = (int)ctrl_LVTemplates.SelectedItems[0].Tag;
				Cl_Template template = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(t => t.p_ID == templateId);
				if (template != null) {

				} else {
					throw new Exception("Не найден шаблон");
				}
			}
		}

		private void ctrl_BTemplateAdd_Click(object sender, EventArgs e) {
			f_CreateNewTemplate();
		}

		private void ctrl_BTemplateEdit_Click(object sender, EventArgs e) {
			if (ctrl_LVTemplates.SelectedItems != null && ctrl_LVTemplates.SelectedItems.Count == 1) {
				int templateId = (int)ctrl_LVTemplates.SelectedItems[0].Tag;
				Cl_Template template = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(t => t.p_ID == templateId);
				if (template != null) {
					F_Template fTemplate = new F_Template();
					fTemplate.ctrl_LGroupValue.Text = m_CurrentGroup.f_GetFullName();
					fTemplate.ctrl_TBName.Text = template.p_Name;
					fTemplate.ctrl_TBDecs.Text = template.p_Description;
					if (fTemplate.ShowDialog() == DialogResult.OK) {
						template.p_Name = fTemplate.ctrl_TBName.Text;
						template.p_Description = fTemplate.ctrl_TBDecs.Text;
						Cl_App.m_DataContext.SaveChanges();

						TreeNode changingNode = f_FindInActiveNodeTemplate(templateId.ToString());
						changingNode.Text = template.p_Name;

						f_LoadTemplates(m_CurrentGroup);
					}
				} else {
					throw new Exception("Не найден шаблон");
				}
			}
		}

		private void ctrl_BTemplateDelete_Click(object sender, EventArgs e) {
			f_DeleteTemplate(false);
		}
		#endregion
	}
}
