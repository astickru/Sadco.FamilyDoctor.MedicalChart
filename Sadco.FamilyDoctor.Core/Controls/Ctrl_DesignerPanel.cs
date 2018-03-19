using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Sadco.FamilyDoctor.Core.Controls {
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
					p_ToolboxService.m_DesignPanel = this;
					PopulateToolbox(p_ToolboxService);
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

			// Add toolbox service

			//Ctrl_ToolboxService lstToolbox = new Ctrl_ToolboxService();
			//serviceContainer.AddService(typeof(IToolboxService), lstToolbox);
			//lstToolbox.designPanel = this;
			//PopulateToolbox(lstToolbox);
			//this.Controls.Add(lstToolbox);

			//// Add menu command service
			//menuService = new MenuCommandService();
			//serviceContainer.AddService(typeof(IMenuCommandService), menuService);


			UserControl form = (UserControl)host.CreateComponent(typeof(UserControl));
			//form.TopLevel = false;
			//form.MaximizeBox = false;
			//form.Text = "Form1";
			//form.Locked
			form.Dock = DockStyle.Fill;
			form.BackColor = System.Drawing.Color.LightGray;


			// Get the root designer for the form and add its design view to this form
			rootDesigner = (IRootDesigner)host.GetDesigner(form);
			view = (Control)rootDesigner.GetView(ViewTechnology.Default);
			
			view.Dock = DockStyle.Fill;
			view.BackColor = System.Drawing.Color.LightGray;
			this.Controls.Add(view);




			// Start the designer host off with a Form to design
			//form = (Form)host.CreateComponent(typeof(Form));
			//form.TopLevel = false;
			//form.Text = "Form1";

			//// Get the root designer for the form and add its design view to this form
			//rootDesigner = (IRootDesigner)host.GetDesigner(form);
			//view = (Control)rootDesigner.GetView(ViewTechnology.WindowsForms);
			//view.Dock = DockStyle.Fill;
			//this.Controls.Add(view);

			// Subscribe to the selectionchanged event and activate the designer
			ISelectionService s = (ISelectionService)serviceContainer.GetService(typeof(ISelectionService));
			//s.SelectionChanged += new EventHandler(OnSelectionChanged);
			host.Activate();
		}

		private void PopulateToolbox(IToolboxService toolbox) {
			//toolbox.AddToolboxItem(new ToolboxItem(typeof(UC_Text)) { Bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject("label") });
			toolbox.AddToolboxItem(new ToolboxItem(typeof(Button)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ListView)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(TreeView)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(TextBox)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(Label)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(TabControl)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(OpenFileDialog)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(CheckBox)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ComboBox)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(GroupBox)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ImageList)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(Panel)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ProgressBar)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ToolBar)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ToolTip)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(StatusBar)));
		}

		//public Ctrl_DesignerPanel(IContainer container) {
		//	container.Add(this);

		//	InitializeComponent();
		//}
	}
}
