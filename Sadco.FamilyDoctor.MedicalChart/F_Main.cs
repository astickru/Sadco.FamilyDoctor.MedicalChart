using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;
using Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate;

namespace Sadco.FamilyDoctor.MedicalChart {
	public partial class F_Main : Form {
		public F_Main() {
			InitializeComponent();
			f_InitTVTemplates();
		}

		private Cl_GroupTemplates m_CurrentGroup = null;

		private void f_PopulateTVTemplates(Cl_GroupTemplates a_Group, TreeNodeCollection a_TreeNodes) {
			TreeNode node = a_TreeNodes.Add(a_Group.p_ID.ToString(), a_Group.p_Name);
			var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(c => c.p_SubGroups);
			if (!dcGroups.IsLoaded) {
				dcGroups.Load();
			}
			foreach (Cl_GroupTemplates group in a_Group.p_SubGroups) {
				f_PopulateTVTemplates(group, node.Nodes);
			}
		}

		private void f_InitTVTemplates() {
			ctrl_TVTemplates.Nodes.Clear();
			Cl_GroupTemplates[] groups = Cl_App.m_DataContext.p_GroupsTepmlates.Include(g => g.p_SubGroups).Where(e => e.p_ParentID == null).ToArray();
			foreach (Cl_GroupTemplates group in groups) {
				f_PopulateTVTemplates(group, ctrl_TVTemplates.Nodes);
			}
		}

		private void ctrl_BSearch_Click(object sender, EventArgs e) {

		}

		private void ctrl_MITemplateNew_Click(object sender, EventArgs e) {
			F_GroupTemplate fGroup = new F_GroupTemplate();
			int groupID = 0;
			if (ctrl_TVTemplates.SelectedNode != null) {
				groupID = Convert.ToInt32(ctrl_TVTemplates.SelectedNode.Name);
				Cl_GroupTemplates group = Cl_App.m_DataContext.p_GroupsTepmlates.FirstOrDefault(g => g.p_ID == groupID);
				if (group != null) {
					fGroup.ctrl_LParentValue.Text = group.f_GetFullName();
				} else {
					throw new Exception("Не найдена группа шаблонов");
				}
			}
			if (fGroup.ShowDialog() == DialogResult.OK) {
				Cl_GroupTemplates group = new Cl_GroupTemplates() { p_Name = fGroup.ctrl_TBName.Text };
				if (ctrl_TVTemplates.SelectedNode != null) {
					group.p_ParentID = groupID;
				}
				Cl_App.m_DataContext.p_GroupsTepmlates.Add(group);
				Cl_App.m_DataContext.SaveChanges();
				if (ctrl_TVTemplates.SelectedNode != null) {
					ctrl_TVTemplates.SelectedNode.Nodes.Add(group.p_ID.ToString(), group.p_Name);
				} else {
					ctrl_TVTemplates.Nodes.Add(group.p_ID.ToString(), group.p_Name);
				}
			}
		}

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

		private bool f_ContainsNode(TreeNode node1, TreeNode node2) {
			if (node2 == null || node2.Parent == null)
				return false;
			if (node2.Parent.Equals(node1))
				return true;
			return f_ContainsNode(node1, node2.Parent);
		}

		private void ctrl_TVTemplates_DragDrop(object sender, DragEventArgs e) {
			Point targetPoint = ctrl_TVTemplates.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = ctrl_TVTemplates.GetNodeAt(targetPoint);
			TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			if (targetNode == null) {
				if (e.Effect == DragDropEffects.Move) {
					draggedNode.Remove();
					ctrl_TVTemplates.Nodes.Add(draggedNode);
					int targetID = Convert.ToInt32(draggedNode.Name);
					Cl_GroupTemplates group = Cl_App.m_DataContext.p_GroupsTepmlates.FirstOrDefault(g => g.p_ID == targetID);
					if (group != null) {
						group.p_ParentID = null;
					} else {
						throw new Exception("Не найдена группа шаблонов");
					}

				} else if (e.Effect == DragDropEffects.Copy) {
					ctrl_TVTemplates.Nodes.Add((TreeNode)draggedNode.Clone());
					Cl_GroupTemplates group = new Cl_GroupTemplates() { p_Name = draggedNode.Text };
					Cl_App.m_DataContext.p_GroupsTepmlates.Add(group);
				}
				Cl_App.m_DataContext.SaveChanges();
			} else if (!draggedNode.Equals(targetNode) && !f_ContainsNode(draggedNode, targetNode)) {
				if (e.Effect == DragDropEffects.Move) {
					draggedNode.Remove();
					targetNode.Nodes.Add(draggedNode);
					int draggedID = Convert.ToInt32(draggedNode.Name);
					Cl_GroupTemplates group = Cl_App.m_DataContext.p_GroupsTepmlates.FirstOrDefault(g => g.p_ID == draggedID);
					if (group != null) {
						group.p_ParentID = Convert.ToInt32(targetNode.Name);
					} else {
						throw new Exception("Не найдена группа шаблонов");
					}
				} else if (e.Effect == DragDropEffects.Copy) {
					targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
					Cl_GroupTemplates group = new Cl_GroupTemplates() { p_Name = draggedNode.Text };
					group.p_ParentID = Convert.ToInt32(targetNode.Name);
					Cl_App.m_DataContext.p_GroupsTepmlates.Add(group);
				}
				targetNode.Expand();
				Cl_App.m_DataContext.SaveChanges();
			}
		}

		private void f_LoadTemplates(Cl_GroupTemplates a_GroupTemplates) {
			ctrl_TemplateTitle.Text = a_GroupTemplates.f_GetFullName();
			Cl_Template[] templates = Cl_App.m_DataContext.p_Teplates.Where(t => t.p_GroupTeplatesID == a_GroupTemplates.p_ID).ToArray();
			ctrl_LVTemplates.Items.Clear();
			foreach (Cl_Template template in templates) {
				ListViewItem listitem = new ListViewItem(new string[] { template.p_Name, template.p_Description });
				listitem.Tag = template.p_ID;
				ctrl_LVTemplates.Items.Add(listitem);
			}
		}

		private void ctrl_TVTemplates_AfterSelect(object sender, TreeViewEventArgs e) {
			int groupID = Convert.ToInt32(ctrl_TVTemplates.SelectedNode.Name);
			m_CurrentGroup = Cl_App.m_DataContext.p_GroupsTepmlates.FirstOrDefault(g => g.p_ID == groupID);
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
			F_Template fTemplate = new F_Template();
			fTemplate.ctrl_LGroupValue.Text = m_CurrentGroup.f_GetFullName();
			if (fTemplate.ShowDialog() == DialogResult.OK) {
				Cl_App.m_DataContext.p_Teplates.Add(new Cl_Template() {
					p_GroupTeplatesID = m_CurrentGroup.p_ID,
					p_Name = fTemplate.ctrl_TBName.Text,
					p_Description = fTemplate.ctrl_TBDecs.Text
				});
				Cl_App.m_DataContext.SaveChanges();
				f_LoadTemplates(m_CurrentGroup);
			}
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
						f_LoadTemplates(m_CurrentGroup);
					}
				} else {
					throw new Exception("Не найден шаблон");
				}
			}
		}

		private void ctrl_BTemplateDelete_Click(object sender, EventArgs e) {
			if (ctrl_LVTemplates.SelectedItems != null && ctrl_LVTemplates.SelectedItems.Count == 1) {
				int templateId = (int)ctrl_LVTemplates.SelectedItems[0].Tag;
				Cl_Template template = Cl_App.m_DataContext.p_Teplates.FirstOrDefault(t => t.p_ID == templateId);
				if (template != null) {
					Cl_App.m_DataContext.p_Teplates.Remove(template);
				} else {
					throw new Exception("Не найден шаблон");
				}
				Cl_App.m_DataContext.SaveChanges();
				f_LoadTemplates(m_CurrentGroup);
			}
		}
	}
}
