namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class UC_EditorImage
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
            this.ctrl_Note = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ctrl_Hint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlTag = new System.Windows.Forms.TextBox();
            this.ctrl_Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrl_Default = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrl_Version = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ctrl_Version);
            this.panel1.Controls.Add(this.ctrl_Note);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.ctrl_Hint);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ctrlTag);
            this.panel1.Controls.Add(this.ctrl_Name);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ctrl_Default);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 177);
            this.panel1.TabIndex = 1;
            // 
            // ctrl_Note
            // 
            this.ctrl_Note.Location = new System.Drawing.Point(148, 98);
            this.ctrl_Note.Name = "ctrl_Note";
            this.ctrl_Note.Size = new System.Drawing.Size(437, 66);
            this.ctrl_Note.TabIndex = 90;
            this.ctrl_Note.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 81;
            this.label4.Text = "Подсказка";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0, 98);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 83;
            this.label9.Text = "Примечание";
            // 
            // ctrl_Hint
            // 
            this.ctrl_Hint.Location = new System.Drawing.Point(148, 75);
            this.ctrl_Hint.Margin = new System.Windows.Forms.Padding(0);
            this.ctrl_Hint.Name = "ctrl_Hint";
            this.ctrl_Hint.Size = new System.Drawing.Size(500, 20);
            this.ctrl_Hint.TabIndex = 82;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 84;
            this.label1.Text = "Тэг элемента";
            // 
            // ctrlTag
            // 
            this.ctrlTag.Location = new System.Drawing.Point(148, 29);
            this.ctrlTag.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlTag.Name = "ctrlTag";
            this.ctrlTag.Size = new System.Drawing.Size(163, 20);
            this.ctrlTag.TabIndex = 85;
            // 
            // ctrl_Name
            // 
            this.ctrl_Name.Location = new System.Drawing.Point(148, 6);
            this.ctrl_Name.Margin = new System.Windows.Forms.Padding(0);
            this.ctrl_Name.Name = "ctrl_Name";
            this.ctrl_Name.Size = new System.Drawing.Size(163, 20);
            this.ctrl_Name.TabIndex = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Название элемента";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 55);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 88;
            this.label6.Text = "Значение по-умолчанию ";
            // 
            // ctrl_Default
            // 
            this.ctrl_Default.Location = new System.Drawing.Point(148, 52);
            this.ctrl_Default.Margin = new System.Windows.Forms.Padding(0);
            this.ctrl_Default.Name = "ctrl_Default";
            this.ctrl_Default.Size = new System.Drawing.Size(500, 20);
            this.ctrl_Default.TabIndex = 89;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(588, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(47, 18);
            this.label2.TabIndex = 92;
            this.label2.Text = "Версия:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrl_Version
            // 
            this.ctrl_Version.AutoSize = true;
            this.ctrl_Version.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrl_Version.Location = new System.Drawing.Point(635, 0);
            this.ctrl_Version.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
            this.ctrl_Version.Name = "ctrl_Version";
            this.ctrl_Version.Padding = new System.Windows.Forms.Padding(0, 5, 10, 0);
            this.ctrl_Version.Size = new System.Drawing.Size(23, 18);
            this.ctrl_Version.TabIndex = 91;
            this.ctrl_Version.Text = "0";
            this.ctrl_Version.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UC_EditorImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_EditorImage";
            this.Size = new System.Drawing.Size(658, 177);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ctrlTag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ctrl_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ctrl_Hint;
        private System.Windows.Forms.RichTextBox ctrl_Note;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ctrl_Default;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ctrl_Version;
    }
}
