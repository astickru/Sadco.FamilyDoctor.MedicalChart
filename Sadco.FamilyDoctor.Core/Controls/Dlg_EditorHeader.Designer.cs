namespace Sadco.FamilyDoctor.Core.Controls
{
	partial class Dlg_EditorHeader
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
            this.ctrl_TBName = new System.Windows.Forms.TextBox();
            this.ctrl_BCancel = new System.Windows.Forms.Button();
            this.ctrl_BOk = new System.Windows.Forms.Button();
            this.ctrl_LTitleName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrl_TBName
            // 
            this.ctrl_TBName.Location = new System.Drawing.Point(101, 16);
            this.ctrl_TBName.Name = "ctrl_TBName";
            this.ctrl_TBName.Size = new System.Drawing.Size(322, 20);
            this.ctrl_TBName.TabIndex = 0;
            // 
            // ctrl_BCancel
            // 
            this.ctrl_BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctrl_BCancel.Location = new System.Drawing.Point(336, 52);
            this.ctrl_BCancel.Name = "ctrl_BCancel";
            this.ctrl_BCancel.Size = new System.Drawing.Size(87, 23);
            this.ctrl_BCancel.TabIndex = 2;
            this.ctrl_BCancel.Text = "oтмена";
            this.ctrl_BCancel.UseVisualStyleBackColor = true;
            // 
            // ctrl_BOk
            // 
            this.ctrl_BOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctrl_BOk.Location = new System.Drawing.Point(233, 52);
            this.ctrl_BOk.Name = "ctrl_BOk";
            this.ctrl_BOk.Size = new System.Drawing.Size(87, 23);
            this.ctrl_BOk.TabIndex = 1;
            this.ctrl_BOk.Text = "ok";
            this.ctrl_BOk.UseVisualStyleBackColor = true;
            // 
            // ctrl_LTitleName
            // 
            this.ctrl_LTitleName.AutoSize = true;
            this.ctrl_LTitleName.Location = new System.Drawing.Point(16, 19);
            this.ctrl_LTitleName.Name = "ctrl_LTitleName";
            this.ctrl_LTitleName.Size = new System.Drawing.Size(65, 13);
            this.ctrl_LTitleName.TabIndex = 17;
            this.ctrl_LTitleName.Text = "Название";
            // 
            // Dlg_EditorHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 90);
            this.Controls.Add(this.ctrl_TBName);
            this.Controls.Add(this.ctrl_BCancel);
            this.Controls.Add(this.ctrl_BOk);
            this.Controls.Add(this.ctrl_LTitleName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dlg_EditorHeader";
            this.Padding = new System.Windows.Forms.Padding(17, 15, 17, 15);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заголовок";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion
        protected internal System.Windows.Forms.TextBox ctrl_TBName;
        private System.Windows.Forms.Button ctrl_BCancel;
        private System.Windows.Forms.Button ctrl_BOk;
        private System.Windows.Forms.Label ctrl_LTitleName;
    }
}