namespace Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate {
	partial class F_Template {
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
			this.ctrl_TBDecs = new System.Windows.Forms.TextBox();
			this.ctrl_LTitleDesc = new System.Windows.Forms.Label();
			this.ctrl_BCancel = new System.Windows.Forms.Button();
			this.ctrl_BOk = new System.Windows.Forms.Button();
			this.ctrl_LTitleName = new System.Windows.Forms.Label();
			this.ctrl_TBName = new System.Windows.Forms.TextBox();
			this.ctrl_LTitleGroup = new System.Windows.Forms.Label();
			this.ctrl_LGroupValue = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ctrl_TBDecs
			// 
			this.ctrl_TBDecs.Location = new System.Drawing.Point(128, 95);
			this.ctrl_TBDecs.Name = "ctrl_TBDecs";
			this.ctrl_TBDecs.Size = new System.Drawing.Size(237, 20);
			this.ctrl_TBDecs.TabIndex = 11;
			// 
			// ctrl_LTitleDesc
			// 
			this.ctrl_LTitleDesc.AutoSize = true;
			this.ctrl_LTitleDesc.Location = new System.Drawing.Point(23, 98);
			this.ctrl_LTitleDesc.Name = "ctrl_LTitleDesc";
			this.ctrl_LTitleDesc.Size = new System.Drawing.Size(57, 13);
			this.ctrl_LTitleDesc.TabIndex = 10;
			this.ctrl_LTitleDesc.Text = "Описание";
			// 
			// ctrl_BCancel
			// 
			this.ctrl_BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ctrl_BCancel.Location = new System.Drawing.Point(287, 143);
			this.ctrl_BCancel.Name = "ctrl_BCancel";
			this.ctrl_BCancel.Size = new System.Drawing.Size(75, 23);
			this.ctrl_BCancel.TabIndex = 9;
			this.ctrl_BCancel.Text = "Отмена";
			this.ctrl_BCancel.UseVisualStyleBackColor = true;
			// 
			// ctrl_BOk
			// 
			this.ctrl_BOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ctrl_BOk.Location = new System.Drawing.Point(198, 143);
			this.ctrl_BOk.Name = "ctrl_BOk";
			this.ctrl_BOk.Size = new System.Drawing.Size(75, 23);
			this.ctrl_BOk.TabIndex = 8;
			this.ctrl_BOk.Text = "ОК";
			this.ctrl_BOk.UseVisualStyleBackColor = true;
			// 
			// ctrl_LTitleName
			// 
			this.ctrl_LTitleName.AutoSize = true;
			this.ctrl_LTitleName.Location = new System.Drawing.Point(23, 59);
			this.ctrl_LTitleName.Name = "ctrl_LTitleName";
			this.ctrl_LTitleName.Size = new System.Drawing.Size(83, 13);
			this.ctrl_LTitleName.TabIndex = 7;
			this.ctrl_LTitleName.Text = "Наименование";
			// 
			// ctrl_TBName
			// 
			this.ctrl_TBName.Location = new System.Drawing.Point(128, 56);
			this.ctrl_TBName.Name = "ctrl_TBName";
			this.ctrl_TBName.Size = new System.Drawing.Size(237, 20);
			this.ctrl_TBName.TabIndex = 12;
			// 
			// ctrl_LTitleGroup
			// 
			this.ctrl_LTitleGroup.AutoSize = true;
			this.ctrl_LTitleGroup.Location = new System.Drawing.Point(23, 20);
			this.ctrl_LTitleGroup.Name = "ctrl_LTitleGroup";
			this.ctrl_LTitleGroup.Size = new System.Drawing.Size(42, 13);
			this.ctrl_LTitleGroup.TabIndex = 15;
			this.ctrl_LTitleGroup.Text = "Группа";
			// 
			// ctrl_LGroupValue
			// 
			this.ctrl_LGroupValue.AutoSize = true;
			this.ctrl_LGroupValue.Location = new System.Drawing.Point(125, 20);
			this.ctrl_LGroupValue.Name = "ctrl_LGroupValue";
			this.ctrl_LGroupValue.Size = new System.Drawing.Size(68, 13);
			this.ctrl_LGroupValue.TabIndex = 16;
			this.ctrl_LGroupValue.Text = "Неизвестно";
			// 
			// F_Template
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(386, 182);
			this.Controls.Add(this.ctrl_LGroupValue);
			this.Controls.Add(this.ctrl_LTitleGroup);
			this.Controls.Add(this.ctrl_TBName);
			this.Controls.Add(this.ctrl_TBDecs);
			this.Controls.Add(this.ctrl_LTitleDesc);
			this.Controls.Add(this.ctrl_BCancel);
			this.Controls.Add(this.ctrl_BOk);
			this.Controls.Add(this.ctrl_LTitleName);
			this.Name = "F_Template";
			this.Padding = new System.Windows.Forms.Padding(20);
			this.Text = "F_Template";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		protected internal System.Windows.Forms.TextBox ctrl_TBDecs;
		private System.Windows.Forms.Label ctrl_LTitleDesc;
		private System.Windows.Forms.Button ctrl_BCancel;
		private System.Windows.Forms.Button ctrl_BOk;
		private System.Windows.Forms.Label ctrl_LTitleName;
		protected internal System.Windows.Forms.TextBox ctrl_TBName;
		private System.Windows.Forms.Label ctrl_LTitleGroup;
		protected internal System.Windows.Forms.Label ctrl_LGroupValue;
	}
}