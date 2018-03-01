using System;
using System.Windows.Forms;
using Sadco.FamilyDoctor.Core.Entities.Controls;

namespace Sadco.FamilyDoctor.MedicalChart
{
	public interface I_EditPanel {
		void ConfirmChanges();
		void SetControl(I_BaseControl p_Control);
	}

	public class UI_PanelManager
	{
		public UserControl ActiveControl { get; private set; }

		private Panel mainPanel = null;

		public UI_PanelManager(Panel panel) {
			mainPanel = panel ?? throw new ArgumentNullException("panel");
		}

		public T SetControl<T>() where T : UserControl {
			UserControl control = (UserControl)Activator.CreateInstance(typeof(T));

			this.DeleteControl();

			this.ActiveControl = control;
			this.mainPanel.Controls.Add(this.ActiveControl);
			this.ActiveControl.Dock = DockStyle.Fill;

			return (T)ActiveControl;
		}

		public void DeleteControl() {
			if (ActiveControl == null) return;

			this.ActiveControl.Dispose();
			this.mainPanel.Controls.Remove(ActiveControl);
		}
	}
}
