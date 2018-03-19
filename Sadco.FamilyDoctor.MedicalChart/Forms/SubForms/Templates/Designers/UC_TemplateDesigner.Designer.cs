namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class UC_TemplateDesigner
	{
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
			this.ctrl_P_Designer = new System.Windows.Forms.Panel();
			this.ctrl_DesignerPanel1 = new Sadco.FamilyDoctor.Core.Controls.Ctrl_DesignerPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ctrl_B_Save = new System.Windows.Forms.Button();
			this.ctrl_EditorPanel = new Sadco.FamilyDoctor.Core.Controls.Ctrl_DesignerPanel();
			this.ctrl_P_Designer.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctrl_P_Designer
			// 
			this.ctrl_P_Designer.AllowDrop = true;
			this.ctrl_P_Designer.BackColor = System.Drawing.Color.LightSkyBlue;
			this.ctrl_P_Designer.Controls.Add(this.ctrl_DesignerPanel1);
			this.ctrl_P_Designer.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctrl_P_Designer.Location = new System.Drawing.Point(0, 0);
			this.ctrl_P_Designer.Name = "ctrl_P_Designer";
			this.ctrl_P_Designer.Size = new System.Drawing.Size(570, 216);
			this.ctrl_P_Designer.TabIndex = 0;
			this.ctrl_P_Designer.DragDrop += new System.Windows.Forms.DragEventHandler(this.ctrl_P_Designer_DragDrop);
			this.ctrl_P_Designer.DragEnter += new System.Windows.Forms.DragEventHandler(this.ctrl_P_Designer_DragEnter);
			// 
			// ctrl_DesignerPanel1
			// 
			this.ctrl_DesignerPanel1.Cursor = System.Windows.Forms.Cursors.Default;
			this.ctrl_DesignerPanel1.Location = new System.Drawing.Point(3, 222);
			this.ctrl_DesignerPanel1.Name = "ctrl_DesignerPanel1";
			this.ctrl_DesignerPanel1.p_ToolboxService = null;
			this.ctrl_DesignerPanel1.Size = new System.Drawing.Size(195, 86);
			this.ctrl_DesignerPanel1.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ctrl_B_Save);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 501);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(2);
			this.panel2.Size = new System.Drawing.Size(570, 30);
			this.panel2.TabIndex = 0;
			// 
			// ctrl_B_Save
			// 
			this.ctrl_B_Save.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctrl_B_Save.Location = new System.Drawing.Point(493, 2);
			this.ctrl_B_Save.Margin = new System.Windows.Forms.Padding(0);
			this.ctrl_B_Save.Name = "ctrl_B_Save";
			this.ctrl_B_Save.Size = new System.Drawing.Size(75, 26);
			this.ctrl_B_Save.TabIndex = 0;
			this.ctrl_B_Save.Text = "Сохранить";
			this.ctrl_B_Save.UseVisualStyleBackColor = true;
			this.ctrl_B_Save.Click += new System.EventHandler(this.ctrl_B_Save_Click);
			// 
			// ctrl_EditorPanel
			// 
			this.ctrl_EditorPanel.BackColor = System.Drawing.Color.Transparent;
			this.ctrl_EditorPanel.Cursor = System.Windows.Forms.Cursors.Default;
			this.ctrl_EditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_EditorPanel.Location = new System.Drawing.Point(0, 216);
			this.ctrl_EditorPanel.Name = "ctrl_EditorPanel";
			this.ctrl_EditorPanel.p_ToolboxService = null;
			this.ctrl_EditorPanel.Size = new System.Drawing.Size(570, 285);
			this.ctrl_EditorPanel.TabIndex = 1;
			// 
			// UC_TemplateDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ctrl_EditorPanel);
			this.Controls.Add(this.ctrl_P_Designer);
			this.Controls.Add(this.panel2);
			this.Name = "UC_TemplateDesigner";
			this.Size = new System.Drawing.Size(570, 531);
			this.ctrl_P_Designer.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel ctrl_P_Designer;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ctrl_B_Save;
		private Core.Controls.Ctrl_DesignerPanel ctrl_DesignerPanel1;
		public Core.Controls.Ctrl_DesignerPanel ctrl_EditorPanel;
	}
}
