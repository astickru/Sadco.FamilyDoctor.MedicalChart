namespace Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate {
	partial class F_GroupTemplate {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ctrl_LParentValue = new System.Windows.Forms.Label();
			this.ctrl_TBName = new System.Windows.Forms.TextBox();
			this.ctrl_LName = new System.Windows.Forms.Label();
			this.ctrl_BCancel = new System.Windows.Forms.Button();
			this.ctrl_BOk = new System.Windows.Forms.Button();
			this.ctrl_LParentTitle = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ctrl_LParentValue);
			this.groupBox1.Controls.Add(this.ctrl_TBName);
			this.groupBox1.Controls.Add(this.ctrl_LName);
			this.groupBox1.Controls.Add(this.ctrl_BCancel);
			this.groupBox1.Controls.Add(this.ctrl_BOk);
			this.groupBox1.Controls.Add(this.ctrl_LParentTitle);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(15, 15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(20);
			this.groupBox1.Size = new System.Drawing.Size(367, 171);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Группа";
			// 
			// ctrl_LParentValue
			// 
			this.ctrl_LParentValue.AutoSize = true;
			this.ctrl_LParentValue.Location = new System.Drawing.Point(114, 33);
			this.ctrl_LParentValue.Name = "ctrl_LParentValue";
			this.ctrl_LParentValue.Size = new System.Drawing.Size(68, 13);
			this.ctrl_LParentValue.TabIndex = 6;
			this.ctrl_LParentValue.Text = "Неизвестно";
			// 
			// ctrl_TBName
			// 
			this.ctrl_TBName.Location = new System.Drawing.Point(117, 75);
			this.ctrl_TBName.Name = "ctrl_TBName";
			this.ctrl_TBName.Size = new System.Drawing.Size(237, 20);
			this.ctrl_TBName.TabIndex = 5;
			// 
			// ctrl_LName
			// 
			this.ctrl_LName.AutoSize = true;
			this.ctrl_LName.Location = new System.Drawing.Point(12, 78);
			this.ctrl_LName.Name = "ctrl_LName";
			this.ctrl_LName.Size = new System.Drawing.Size(83, 13);
			this.ctrl_LName.TabIndex = 4;
			this.ctrl_LName.Text = "Наименование";
			// 
			// ctrl_BCancel
			// 
			this.ctrl_BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ctrl_BCancel.Location = new System.Drawing.Point(279, 134);
			this.ctrl_BCancel.Name = "ctrl_BCancel";
			this.ctrl_BCancel.Size = new System.Drawing.Size(75, 23);
			this.ctrl_BCancel.TabIndex = 3;
			this.ctrl_BCancel.Text = "Отмена";
			this.ctrl_BCancel.UseVisualStyleBackColor = true;
			// 
			// ctrl_BOk
			// 
			this.ctrl_BOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ctrl_BOk.Location = new System.Drawing.Point(190, 134);
			this.ctrl_BOk.Name = "ctrl_BOk";
			this.ctrl_BOk.Size = new System.Drawing.Size(75, 23);
			this.ctrl_BOk.TabIndex = 2;
			this.ctrl_BOk.Text = "ОК";
			this.ctrl_BOk.UseVisualStyleBackColor = true;
			// 
			// ctrl_LParentTitle
			// 
			this.ctrl_LParentTitle.AutoSize = true;
			this.ctrl_LParentTitle.Location = new System.Drawing.Point(12, 33);
			this.ctrl_LParentTitle.Name = "ctrl_LParentTitle";
			this.ctrl_LParentTitle.Size = new System.Drawing.Size(42, 13);
			this.ctrl_LParentTitle.TabIndex = 0;
			this.ctrl_LParentTitle.Text = "Группа";
			// 
			// F_GroupTemplate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(397, 201);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "F_GroupTemplate";
			this.Padding = new System.Windows.Forms.Padding(15);
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Группа шаблонов";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label ctrl_LParentTitle;
		private System.Windows.Forms.Button ctrl_BCancel;
		private System.Windows.Forms.Button ctrl_BOk;
		protected internal System.Windows.Forms.TextBox ctrl_TBName;
		private System.Windows.Forms.Label ctrl_LName;
		protected internal System.Windows.Forms.Label ctrl_LParentValue;
	}
}