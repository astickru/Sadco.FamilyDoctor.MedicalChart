using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Controls {
	partial class Ctrl_DesignerPanel {
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.ctrlMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrlMenuDel = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMenu.SuspendLayout();

            this.ItemHeight = 24;
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;

            // 
            // ctrlMenu
            // 
            this.ctrlMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMenuDel});
            this.ctrlMenu.Name = "ctrlMenu";
            this.ctrlMenu.Size = new System.Drawing.Size(119, 26);
            // 
            // ctrlMenuDel
            // 
            this.ctrlMenuDel.Name = "ctrlMenuDel";
            this.ctrlMenuDel.Size = new System.Drawing.Size(118, 22);
            this.ctrlMenuDel.Text = "Удалить";
            this.ctrlMenuDel.Click += ctrlMenuDel_Click;

            ContextMenuStrip = ctrlMenu;
            this.ctrlMenu.ResumeLayout(false);
            this.SuspendLayout();
			this.ResumeLayout(false);
            this.AllowDrop = true;
        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctrlMenu;
        private System.Windows.Forms.ToolStripMenuItem ctrlMenuDel;
    }
}
