namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class UC_EditorTab
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrl_Version = new System.Windows.Forms.Label();
            this.ctrl_Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ctrl_Version);
            this.panel1.Controls.Add(this.ctrl_Name);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 33);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(566, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(57, 18);
            this.label2.TabIndex = 92;
            this.label2.Text = "Версия:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrl_Version
            // 
            this.ctrl_Version.AutoSize = true;
            this.ctrl_Version.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrl_Version.Location = new System.Drawing.Point(623, 0);
            this.ctrl_Version.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
            this.ctrl_Version.Name = "ctrl_Version";
            this.ctrl_Version.Padding = new System.Windows.Forms.Padding(0, 5, 10, 0);
            this.ctrl_Version.Size = new System.Drawing.Size(25, 18);
            this.ctrl_Version.TabIndex = 91;
            this.ctrl_Version.Text = "0";
            this.ctrl_Version.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrl_Name
            // 
            this.ctrl_Name.Location = new System.Drawing.Point(148, 6);
            this.ctrl_Name.Margin = new System.Windows.Forms.Padding(0);
            this.ctrl_Name.Name = "ctrl_Name";
            this.ctrl_Name.Size = new System.Drawing.Size(359, 21);
            this.ctrl_Name.TabIndex = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Название вкладки";
            // 
            // UC_EditorTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_EditorTab";
            this.Size = new System.Drawing.Size(648, 33);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ctrl_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ctrl_Version;
	}
}
