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
			this.panel2 = new System.Windows.Forms.Panel();
			this.ctrl_B_Save = new System.Windows.Forms.Button();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctrl_P_Designer
			// 
			this.ctrl_P_Designer.AllowDrop = true;
			this.ctrl_P_Designer.BackColor = System.Drawing.Color.LightGray;
			this.ctrl_P_Designer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_P_Designer.Location = new System.Drawing.Point(0, 0);
			this.ctrl_P_Designer.Name = "ctrl_P_Designer";
			this.ctrl_P_Designer.Size = new System.Drawing.Size(470, 396);
			this.ctrl_P_Designer.TabIndex = 0;
			this.ctrl_P_Designer.DragDrop += new System.Windows.Forms.DragEventHandler(this.ctrl_P_Designer_DragDrop);
			this.ctrl_P_Designer.DragEnter += new System.Windows.Forms.DragEventHandler(this.ctrl_P_Designer_DragEnter);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ctrl_B_Save);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 396);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(2);
			this.panel2.Size = new System.Drawing.Size(470, 30);
			this.panel2.TabIndex = 0;
			// 
			// ctrl_B_Save
			// 
			this.ctrl_B_Save.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctrl_B_Save.Location = new System.Drawing.Point(393, 2);
			this.ctrl_B_Save.Margin = new System.Windows.Forms.Padding(0);
			this.ctrl_B_Save.Name = "ctrl_B_Save";
			this.ctrl_B_Save.Size = new System.Drawing.Size(75, 26);
			this.ctrl_B_Save.TabIndex = 0;
			this.ctrl_B_Save.Text = "Сохранить";
			this.ctrl_B_Save.UseVisualStyleBackColor = true;
			this.ctrl_B_Save.Click += new System.EventHandler(this.ctrl_B_Save_Click);
			// 
			// UC_TemplateDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ctrl_P_Designer);
			this.Controls.Add(this.panel2);
			this.Name = "UC_TemplateDesigner";
			this.Size = new System.Drawing.Size(470, 426);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel ctrl_P_Designer;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ctrl_B_Save;
	}
}
