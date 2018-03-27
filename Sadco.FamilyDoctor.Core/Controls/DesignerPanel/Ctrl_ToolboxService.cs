// IMPORTANT: Read the license included with this code archive.
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Design;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel {
	public class Ctrl_ToolboxService : Ctrl_TreeElements, IToolboxService {
		internal Control m_DesignPanel = null;
		//private ImageList m_ImageList = null;

		public Ctrl_ToolboxService() {
			//m_ImageList = new ImageList();
			//ImageList = m_ImageList;
		}

		public void AddCreator(System.Drawing.Design.ToolboxItemCreatorCallback creator, string format, System.ComponentModel.Design.IDesignerHost host) {
			// No implementation
		}

		public void AddCreator(System.Drawing.Design.ToolboxItemCreatorCallback creator, string format) {
			// No implementation
		}

		public void AddLinkedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category, System.ComponentModel.Design.IDesignerHost host) {
			// No implementation
		}

		public void AddLinkedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, System.ComponentModel.Design.IDesignerHost host) {
			// No implementation
		}

		public void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category) {
			AddToolboxItem(toolboxItem);
		}

		public void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem) {
            if (toolboxItem is Cl_ToolboxItemElement)
            {
                Ctrl_TreeNodeElement node = ((Cl_ToolboxItemElement)toolboxItem).p_TreeNodeElement;
                Nodes.Add(node);
            }
            else
            {
                TreeNode node = new TreeNode(toolboxItem.DisplayName);
                node.Tag = toolboxItem;
                Nodes.Add(node);
            }
        }

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem(object serializedObject, System.ComponentModel.Design.IDesignerHost host) {
			return null;
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem(object serializedObject) {
			return null;
		}

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem(System.ComponentModel.Design.IDesignerHost host) {
			return GetSelectedToolboxItem();
		}

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem() {
            if (SelectedNode == null)
                return null;
            else
            {
                return (ToolboxItem)SelectedNode.Tag;
            }
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(string category, System.ComponentModel.Design.IDesignerHost host) {
			return GetToolboxItems();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(string category) {
			return GetToolboxItems();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(System.ComponentModel.Design.IDesignerHost host) {
			return GetToolboxItems();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems() {
			ToolboxItem[] t = new ToolboxItem[Nodes.Count];
			Nodes.CopyTo(t, 0);
			return new ToolboxItemCollection(t);
		}

		public bool IsSupported(object serializedObject, System.Collections.ICollection filterAttributes) {
			return false;
		}

		public bool IsSupported(object serializedObject, System.ComponentModel.Design.IDesignerHost host) {
			return false;
		}

		public bool IsToolboxItem(object serializedObject, System.ComponentModel.Design.IDesignerHost host) {
			return false;
		}

		public bool IsToolboxItem(object serializedObject) {
			return false;
		}

		//		public void Refresh()
		//		{
		//			base.Refresh();
		//		}

		public void RemoveCreator(string format, System.ComponentModel.Design.IDesignerHost host) {
			// No implementation
		}

		public void RemoveCreator(string format) {
			// No implementation
		}

		public void RemoveToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category) {
			RemoveToolboxItem(toolboxItem);
		}

		public void RemoveToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem) {
			Nodes.RemoveByKey(toolboxItem.TypeName);
		}

		public void SelectedToolboxItemUsed() {
			base.SelectedNode = null;
		}

		public object SerializeToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem) {
			return null;
		}

		public bool SetCursor() {
			if (base.SelectedNode == null)
				m_DesignPanel.Cursor = Cursors.Default;
			else
				m_DesignPanel.Cursor = Cursors.Cross;

			return true;
		}

		public void SetSelectedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem) {
			TreeNode node = new TreeNode(toolboxItem.TypeName);
			node.Tag = toolboxItem;
			base.SelectedNode = node;
		}

		public System.Drawing.Design.CategoryNameCollection CategoryNames {
			get {
				return null;
			}
		}

		public string SelectedCategory {
			get {
				return null;
			}
			set {
			}
		}

		private bool ShouldSerializeItems() {
			return false;
		}
	}
}
