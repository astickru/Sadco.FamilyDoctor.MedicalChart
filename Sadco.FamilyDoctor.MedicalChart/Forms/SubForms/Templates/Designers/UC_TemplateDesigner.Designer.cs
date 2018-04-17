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
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrl_B_Save = new System.Windows.Forms.Button();
            this.ctrl_B_History = new System.Windows.Forms.Button();
            this.ctrl_EditorPanel = new Sadco.FamilyDoctor.Core.Controls.Ctrl_DesignerPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrl_Version = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctrl_B_Save);
            this.panel2.Controls.Add(this.ctrl_B_History);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 501);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(2);
            this.panel2.Size = new System.Drawing.Size(760, 30);
            this.panel2.TabIndex = 0;
            // 
            // ctrl_B_Save
            // 
            this.ctrl_B_Save.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrl_B_Save.Location = new System.Drawing.Point(659, 2);
            this.ctrl_B_Save.Margin = new System.Windows.Forms.Padding(0);
            this.ctrl_B_Save.Name = "ctrl_B_Save";
            this.ctrl_B_Save.Size = new System.Drawing.Size(99, 26);
            this.ctrl_B_Save.TabIndex = 0;
            this.ctrl_B_Save.Text = "cохранить";
            this.ctrl_B_Save.UseVisualStyleBackColor = true;
            this.ctrl_B_Save.Click += new System.EventHandler(this.ctrl_B_Save_Click);
            // 
            // ctrl_B_History
            // 
            this.ctrl_B_History.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctrl_B_History.Location = new System.Drawing.Point(2, 2);
            this.ctrl_B_History.Margin = new System.Windows.Forms.Padding(0);
            this.ctrl_B_History.Name = "ctrl_B_History";
            this.ctrl_B_History.Size = new System.Drawing.Size(99, 26);
            this.ctrl_B_History.TabIndex = 1;
            this.ctrl_B_History.Text = "история";
            this.ctrl_B_History.UseVisualStyleBackColor = true;
            this.ctrl_B_History.Click += new System.EventHandler(this.ctrl_B_History_Click);
            // 
            // ctrl_EditorPanel
            // 
            this.ctrl_EditorPanel.AllowDrop = true;
            this.ctrl_EditorPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctrl_EditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_EditorPanel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ctrl_EditorPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ctrl_EditorPanel.ItemHeight = 24;
            this.ctrl_EditorPanel.Location = new System.Drawing.Point(0, 23);
            this.ctrl_EditorPanel.Name = "ctrl_EditorPanel";
            this.ctrl_EditorPanel.p_ReadOnly = false;
            this.ctrl_EditorPanel.p_ToolboxService = null;
            this.ctrl_EditorPanel.Size = new System.Drawing.Size(760, 478);
            this.ctrl_EditorPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ctrl_Version);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 23);
            this.panel1.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(670, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(57, 18);
            this.label2.TabIndex = 67;
            this.label2.Text = "Версия:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrl_Version
            // 
            this.ctrl_Version.AutoSize = true;
            this.ctrl_Version.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrl_Version.Location = new System.Drawing.Point(727, 0);
            this.ctrl_Version.Margin = new System.Windows.Forms.Padding(3, 0, 54, 0);
            this.ctrl_Version.Name = "ctrl_Version";
            this.ctrl_Version.Padding = new System.Windows.Forms.Padding(0, 5, 18, 0);
            this.ctrl_Version.Size = new System.Drawing.Size(33, 18);
            this.ctrl_Version.TabIndex = 66;
            this.ctrl_Version.Text = "0";
            this.ctrl_Version.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UC_TemplateDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrl_EditorPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "UC_TemplateDesigner";
            this.Size = new System.Drawing.Size(760, 531);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ctrl_B_Save;
		public Core.Controls.Ctrl_DesignerPanel ctrl_EditorPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ctrl_Version;
        private System.Windows.Forms.Button ctrl_B_History;
    }
}
