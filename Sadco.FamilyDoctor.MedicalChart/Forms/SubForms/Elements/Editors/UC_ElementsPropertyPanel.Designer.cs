namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Elements.Editors
{
	partial class UC_ElementsPropertyPanel
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
            this.ctrl_P_Properties = new System.Windows.Forms.Panel();
            this.ctrl_P_ControlConteiner = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrl_B_Save = new System.Windows.Forms.Button();
            this.ctrl_P_Properties.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrl_P_Properties
            // 
            this.ctrl_P_Properties.AutoSize = true;
            this.ctrl_P_Properties.Controls.Add(this.ctrl_P_ControlConteiner);
            this.ctrl_P_Properties.Controls.Add(this.panel2);
            this.ctrl_P_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_P_Properties.Location = new System.Drawing.Point(0, 0);
            this.ctrl_P_Properties.Name = "ctrl_P_Properties";
            this.ctrl_P_Properties.Size = new System.Drawing.Size(229, 196);
            this.ctrl_P_Properties.TabIndex = 1;
            // 
            // ctrl_P_ControlConteiner
            // 
            this.ctrl_P_ControlConteiner.AutoSize = true;
            this.ctrl_P_ControlConteiner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_P_ControlConteiner.Location = new System.Drawing.Point(0, 0);
            this.ctrl_P_ControlConteiner.Name = "ctrl_P_ControlConteiner";
            this.ctrl_P_ControlConteiner.Size = new System.Drawing.Size(229, 155);
            this.ctrl_P_ControlConteiner.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctrl_B_Save);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 155);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(229, 41);
            this.panel2.TabIndex = 16;
            // 
            // ctrl_B_Save
            // 
            this.ctrl_B_Save.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrl_B_Save.Location = new System.Drawing.Point(154, 0);
            this.ctrl_B_Save.Name = "ctrl_B_Save";
            this.ctrl_B_Save.Size = new System.Drawing.Size(75, 41);
            this.ctrl_B_Save.TabIndex = 4;
            this.ctrl_B_Save.Text = "Сохранить";
            this.ctrl_B_Save.UseVisualStyleBackColor = true;
            this.ctrl_B_Save.Click += new System.EventHandler(this.ctrl_B_Save_Click);
            // 
            // UC_ElementsPropertyPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.ctrl_P_Properties);
            this.Name = "UC_ElementsPropertyPanel";
            this.Size = new System.Drawing.Size(229, 196);
            this.ctrl_P_Properties.ResumeLayout(false);
            this.ctrl_P_Properties.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel ctrl_P_Properties;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ctrl_B_Save;
		private System.Windows.Forms.Panel ctrl_P_ControlConteiner;
	}
}
