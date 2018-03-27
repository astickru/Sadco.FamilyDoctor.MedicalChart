using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    public class Cl_ToolboxItemElement : ToolboxItem
    {
        public Cl_ToolboxItemElement(Type a_Type)
            : base(a_Type)
        {

        }

        private Ctrl_TreeNodeElement m_TreeNodeElement = null;
        public Ctrl_TreeNodeElement p_TreeNodeElement {
            get {
                return m_TreeNodeElement;
            }
            set {
                m_TreeNodeElement = value;
                if (m_TreeNodeElement != null)
                {
                    m_TreeNodeElement.Tag = this;
                }
            }
        }
    }
}
