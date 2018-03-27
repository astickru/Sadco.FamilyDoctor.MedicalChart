using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public partial class Ctrl_DesignerPanel : Panel {
		public Ctrl_DesignerPanel() {
			InitializeComponent();
			Initialize();
		}

		private ServiceContainer serviceContainer = null;

		private Ctrl_ToolboxService m_ToolboxService = null;
		public Ctrl_ToolboxService p_ToolboxService {
			get { return m_ToolboxService; }
			set {
				m_ToolboxService = value;
				if (m_ToolboxService != null) {
					serviceContainer.AddService(typeof(IToolboxService), m_ToolboxService);
                    m_ToolboxService.m_DesignPanel = this;
					//PopulateToolbox(p_ToolboxService);
				}
			}
		}

		private void Initialize() {
			IDesignerHost host;
			IRootDesigner rootDesigner;
			Control view;

			// Initialise service container and designer host
			serviceContainer = new ServiceContainer();
			serviceContainer.AddService(typeof(INameCreationService), new Cl_NameCreationService());
			//serviceContainer.AddService(typeof(IUIService), new Cl_UIService(this));
			host = new Cl_DesignerHost(serviceContainer);

            UserControl root = (UserControl)host.CreateComponent(typeof(UserControl));
            root.Dock = DockStyle.Fill;
            root.BackColor = System.Drawing.Color.LightGray;

            FlowLayoutPanel flp = (FlowLayoutPanel)host.CreateComponent(typeof(FlowLayoutPanel));
            flp.Dock = DockStyle.Fill;
            flp.BackColor = System.Drawing.Color.LightGray;
            flp.FlowDirection = FlowDirection.TopDown;
            flp.Padding = new Padding(1);
            root.Controls.Add(flp);

            rootDesigner = (IRootDesigner)host.GetDesigner(root);
            view = (Control)rootDesigner.GetView(ViewTechnology.Default);
            view.Dock = DockStyle.Fill;
            view.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(view);

            // Subscribe to the selectionchanged event and activate the designer
            ISelectionService s = (ISelectionService)serviceContainer.GetService(typeof(ISelectionService));
			//s.SelectionChanged += new EventHandler(OnSelectionChanged);
			host.Activate();
		}
	}
}
