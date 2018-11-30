namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class Dlg_Record
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
            this.ctrl_CMTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrl_MIGroupNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlPContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlTitle = new System.Windows.Forms.Label();
            this.ctrlDTPTimeReception = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrlDTPDateReception = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrl_Version = new System.Windows.Forms.Label();
            this.ctrlPatientFIO = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrlBMKB = new System.Windows.Forms.Button();
            this.ctrlBRating = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctrlBHistory = new System.Windows.Forms.Button();
            this.ctrlBFormatByPattern = new System.Windows.Forms.Button();
            this.ctrlBSave = new System.Windows.Forms.Button();
            this.ctrl_CMTemplate.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrl_CMTemplate
            // 
            this.ctrl_CMTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_MIGroupNew,
            this.ctrl_MIGroupEdit,
            this.ctrl_MIGroupDelete,
            this.ctrl_MITemplateNew,
            this.ctrl_MITemplateEdit,
            this.ctrl_MITemplateDelete});
            this.ctrl_CMTemplate.Name = "ctrl_CMTemplate";
            this.ctrl_CMTemplate.Size = new System.Drawing.Size(177, 136);
            // 
            // ctrl_MIGroupNew
            // 
            this.ctrl_MIGroupNew.Name = "ctrl_MIGroupNew";
            this.ctrl_MIGroupNew.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MIGroupNew.Tag = "MI_GroupNew";
            this.ctrl_MIGroupNew.Text = "Добавить группу";
            // 
            // ctrl_MIGroupEdit
            // 
            this.ctrl_MIGroupEdit.Name = "ctrl_MIGroupEdit";
            this.ctrl_MIGroupEdit.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MIGroupEdit.Tag = "MI_GroupEdit";
            this.ctrl_MIGroupEdit.Text = "Изменить группу";
            // 
            // ctrl_MIGroupDelete
            // 
            this.ctrl_MIGroupDelete.Name = "ctrl_MIGroupDelete";
            this.ctrl_MIGroupDelete.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MIGroupDelete.Tag = "MI_GroupDelete";
            this.ctrl_MIGroupDelete.Text = "Удалить группу";
            // 
            // ctrl_MITemplateNew
            // 
            this.ctrl_MITemplateNew.Name = "ctrl_MITemplateNew";
            this.ctrl_MITemplateNew.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MITemplateNew.Tag = "MI_TemplateNew";
            this.ctrl_MITemplateNew.Text = "Добавить шаблон";
            // 
            // ctrl_MITemplateEdit
            // 
            this.ctrl_MITemplateEdit.Name = "ctrl_MITemplateEdit";
            this.ctrl_MITemplateEdit.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MITemplateEdit.Tag = "MI_TemplateEdit";
            this.ctrl_MITemplateEdit.Text = "Изменить шаблон";
            // 
            // ctrl_MITemplateDelete
            // 
            this.ctrl_MITemplateDelete.Name = "ctrl_MITemplateDelete";
            this.ctrl_MITemplateDelete.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MITemplateDelete.Tag = "MI_TemplateDelete";
            this.ctrl_MITemplateDelete.Text = "Удалить шаблон";
            // 
            // ctrlPContent
            // 
            this.ctrlPContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPContent.Location = new System.Drawing.Point(14, 92);
            this.ctrlPContent.Name = "ctrlPContent";
            this.ctrlPContent.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.ctrlPContent.Size = new System.Drawing.Size(1556, 927);
            this.ctrlPContent.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.ctrlTitle);
            this.panel1.Controls.Add(this.ctrlDTPTimeReception);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ctrlDTPDateReception);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ctrl_Version);
            this.panel1.Controls.Add(this.ctrlPatientFIO);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(14, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1556, 82);
            this.panel1.TabIndex = 2;
            // 
            // ctrlTitle
            // 
            this.ctrlTitle.AutoSize = true;
            this.ctrlTitle.Location = new System.Drawing.Point(165, 29);
            this.ctrlTitle.Name = "ctrlTitle";
            this.ctrlTitle.Size = new System.Drawing.Size(77, 13);
            this.ctrlTitle.TabIndex = 74;
            this.ctrlTitle.Text = "Заголовок";
            // 
            // ctrlDTPTimeReception
            // 
            this.ctrlDTPTimeReception.CustomFormat = "HH:mm";
            this.ctrlDTPTimeReception.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ctrlDTPTimeReception.Location = new System.Drawing.Point(411, 52);
            this.ctrlDTPTimeReception.Name = "ctrlDTPTimeReception";
            this.ctrlDTPTimeReception.ShowUpDown = true;
            this.ctrlDTPTimeReception.Size = new System.Drawing.Size(63, 21);
            this.ctrlDTPTimeReception.TabIndex = 73;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(353, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Время:";
            // 
            // ctrlDTPDateReception
            // 
            this.ctrlDTPDateReception.Location = new System.Drawing.Point(165, 52);
            this.ctrlDTPDateReception.Name = "ctrlDTPDateReception";
            this.ctrlDTPDateReception.Size = new System.Drawing.Size(171, 21);
            this.ctrlDTPDateReception.TabIndex = 71;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 69;
            this.label4.Text = "Дата:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Заголовок:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(1470, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(57, 18);
            this.label3.TabIndex = 65;
            this.label3.Text = "Версия:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrl_Version
            // 
            this.ctrl_Version.AutoSize = true;
            this.ctrl_Version.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrl_Version.Location = new System.Drawing.Point(1527, 0);
            this.ctrl_Version.Margin = new System.Windows.Forms.Padding(3, 0, 40, 0);
            this.ctrl_Version.Name = "ctrl_Version";
            this.ctrl_Version.Padding = new System.Windows.Forms.Padding(0, 5, 14, 0);
            this.ctrl_Version.Size = new System.Drawing.Size(29, 18);
            this.ctrl_Version.TabIndex = 64;
            this.ctrl_Version.Text = "0";
            this.ctrl_Version.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrlPatientFIO
            // 
            this.ctrlPatientFIO.AutoSize = true;
            this.ctrlPatientFIO.Location = new System.Drawing.Point(162, 7);
            this.ctrlPatientFIO.Name = "ctrlPatientFIO";
            this.ctrlPatientFIO.Size = new System.Drawing.Size(98, 13);
            this.ctrlPatientFIO.TabIndex = 3;
            this.ctrlPatientFIO.Text = "ctrlPatientFIO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пациент:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctrlBMKB);
            this.panel2.Controls.Add(this.ctrlBRating);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(14, 1019);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(1556, 32);
            this.panel2.TabIndex = 0;
            // 
            // ctrlBMKB
            // 
            this.ctrlBMKB.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctrlBMKB.Location = new System.Drawing.Point(102, 3);
            this.ctrlBMKB.Name = "ctrlBMKB";
            this.ctrlBMKB.Size = new System.Drawing.Size(99, 26);
            this.ctrlBMKB.TabIndex = 4;
            this.ctrlBMKB.Text = "МКБ";
            this.ctrlBMKB.UseVisualStyleBackColor = true;
            this.ctrlBMKB.Click += new System.EventHandler(this.ctrlBMKB_Click);
            // 
            // ctrlBRating
            // 
            this.ctrlBRating.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctrlBRating.Location = new System.Drawing.Point(3, 3);
            this.ctrlBRating.Name = "ctrlBRating";
            this.ctrlBRating.Size = new System.Drawing.Size(99, 26);
            this.ctrlBRating.TabIndex = 2;
            this.ctrlBRating.Text = "оценка";
            this.ctrlBRating.UseVisualStyleBackColor = true;
            this.ctrlBRating.Click += new System.EventHandler(this.ctrlBRating_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.ctrlBHistory);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBFormatByPattern);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1137, 3);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(416, 26);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // ctrlBHistory
            // 
            this.ctrlBHistory.Location = new System.Drawing.Point(0, 0);
            this.ctrlBHistory.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBHistory.Name = "ctrlBHistory";
            this.ctrlBHistory.Size = new System.Drawing.Size(99, 25);
            this.ctrlBHistory.TabIndex = 1;
            this.ctrlBHistory.Text = "история";
            this.ctrlBHistory.UseVisualStyleBackColor = true;
            this.ctrlBHistory.Click += new System.EventHandler(this.ctrlBHistory_Click);
            // 
            // ctrlBFormatByPattern
            // 
            this.ctrlBFormatByPattern.Location = new System.Drawing.Point(114, 0);
            this.ctrlBFormatByPattern.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBFormatByPattern.Name = "ctrlBFormatByPattern";
            this.ctrlBFormatByPattern.Size = new System.Drawing.Size(188, 25);
            this.ctrlBFormatByPattern.TabIndex = 2;
            this.ctrlBFormatByPattern.Text = "заполнить по патерну";
            this.ctrlBFormatByPattern.UseVisualStyleBackColor = true;
            this.ctrlBFormatByPattern.Visible = false;
            this.ctrlBFormatByPattern.Click += new System.EventHandler(this.ctrlBFormatByPattern_Click);
            // 
            // ctrlBSave
            // 
            this.ctrlBSave.Location = new System.Drawing.Point(317, 0);
            this.ctrlBSave.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlBSave.Name = "ctrlBSave";
            this.ctrlBSave.Size = new System.Drawing.Size(99, 25);
            this.ctrlBSave.TabIndex = 0;
            this.ctrlBSave.Text = "сохранить";
            this.ctrlBSave.UseVisualStyleBackColor = true;
            this.ctrlBSave.Click += new System.EventHandler(this.ctrlBSave_Click);
            // 
            // Dlg_Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1584, 1061);
            this.Controls.Add(this.ctrlPContent);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximumSize = new System.Drawing.Size(1600, 1200);
            this.Name = "Dlg_Record";
            this.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ctrl_CMTemplate.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip ctrl_CMTemplate;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupEdit;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateEdit;
        private System.Windows.Forms.Panel ctrlPContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ctrlBSave;
        private System.Windows.Forms.Label ctrlPatientFIO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ctrl_Version;
        private System.Windows.Forms.Button ctrlBHistory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ctrlBRating;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button ctrlBFormatByPattern;
        private System.Windows.Forms.Button ctrlBMKB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker ctrlDTPDateReception;
		private System.Windows.Forms.DateTimePicker ctrlDTPTimeReception;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label ctrlTitle;
	}
}
