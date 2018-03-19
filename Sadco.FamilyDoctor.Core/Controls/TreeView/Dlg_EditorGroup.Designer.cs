﻿namespace Sadco.FamilyDoctor.Core.Controls
{
	partial class Dlg_EditorGroup
    {
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
            this.ctrl_LGroupValue = new System.Windows.Forms.Label();
            this.ctrl_LTitleGroup = new System.Windows.Forms.Label();
            this.ctrl_TBName = new System.Windows.Forms.TextBox();
            this.ctrl_BCancel = new System.Windows.Forms.Button();
            this.ctrl_BOk = new System.Windows.Forms.Button();
            this.ctrl_LTitleName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrl_LGroupValue
            // 
            this.ctrl_LGroupValue.AutoSize = true;
            this.ctrl_LGroupValue.Location = new System.Drawing.Point(118, 14);
            this.ctrl_LGroupValue.Name = "ctrl_LGroupValue";
            this.ctrl_LGroupValue.Size = new System.Drawing.Size(68, 13);
            this.ctrl_LGroupValue.TabIndex = 24;
            this.ctrl_LGroupValue.Text = "Неизвестно";
            // 
            // ctrl_LTitleGroup
            // 
            this.ctrl_LTitleGroup.AutoSize = true;
            this.ctrl_LTitleGroup.Location = new System.Drawing.Point(16, 14);
            this.ctrl_LTitleGroup.Name = "ctrl_LTitleGroup";
            this.ctrl_LTitleGroup.Size = new System.Drawing.Size(42, 13);
            this.ctrl_LTitleGroup.TabIndex = 23;
            this.ctrl_LTitleGroup.Text = "Группа";
            // 
            // ctrl_TBName
            // 
            this.ctrl_TBName.Location = new System.Drawing.Point(121, 50);
            this.ctrl_TBName.Name = "ctrl_TBName";
            this.ctrl_TBName.Size = new System.Drawing.Size(237, 20);
            this.ctrl_TBName.TabIndex = 22;
            // 
            // ctrl_BCancel
            // 
            this.ctrl_BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctrl_BCancel.Location = new System.Drawing.Point(280, 85);
            this.ctrl_BCancel.Name = "ctrl_BCancel";
            this.ctrl_BCancel.Size = new System.Drawing.Size(75, 23);
            this.ctrl_BCancel.TabIndex = 19;
            this.ctrl_BCancel.Text = "Отмена";
            this.ctrl_BCancel.UseVisualStyleBackColor = true;
            // 
            // ctrl_BOk
            // 
            this.ctrl_BOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctrl_BOk.Location = new System.Drawing.Point(191, 85);
            this.ctrl_BOk.Name = "ctrl_BOk";
            this.ctrl_BOk.Size = new System.Drawing.Size(75, 23);
            this.ctrl_BOk.TabIndex = 18;
            this.ctrl_BOk.Text = "ОК";
            this.ctrl_BOk.UseVisualStyleBackColor = true;
            // 
            // ctrl_LTitleName
            // 
            this.ctrl_LTitleName.AutoSize = true;
            this.ctrl_LTitleName.Location = new System.Drawing.Point(16, 53);
            this.ctrl_LTitleName.Name = "ctrl_LTitleName";
            this.ctrl_LTitleName.Size = new System.Drawing.Size(83, 13);
            this.ctrl_LTitleName.TabIndex = 17;
            this.ctrl_LTitleName.Text = "Наименование";
            // 
            // Dlg_EditorGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 120);
            this.Controls.Add(this.ctrl_LGroupValue);
            this.Controls.Add(this.ctrl_LTitleGroup);
            this.Controls.Add(this.ctrl_TBName);
            this.Controls.Add(this.ctrl_BCancel);
            this.Controls.Add(this.ctrl_BOk);
            this.Controls.Add(this.ctrl_LTitleName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dlg_EditorGroup";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Группа";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        protected internal System.Windows.Forms.Label ctrl_LGroupValue;
        private System.Windows.Forms.Label ctrl_LTitleGroup;
        protected internal System.Windows.Forms.TextBox ctrl_TBName;
        private System.Windows.Forms.Button ctrl_BCancel;
        private System.Windows.Forms.Button ctrl_BOk;
        private System.Windows.Forms.Label ctrl_LTitleName;
    }
}