namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class F_DesignerTemplate
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
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ctrl_TreeElements = new Sadco.FamilyDoctor.Core.Controls.Ctrl_TreeElements();
			this.ctrl_P_DesignConteiner = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ctrl_TreeElements);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ctrl_P_DesignConteiner);
			this.splitContainer1.Size = new System.Drawing.Size(975, 680);
			this.splitContainer1.SplitterDistance = 262;
			this.splitContainer1.TabIndex = 0;
			// 
			// ctrl_TreeElements
			// 
			this.ctrl_TreeElements.AllowDrop = true;
			this.ctrl_TreeElements.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_TreeElements.ImageIndex = 0;
			this.ctrl_TreeElements.Location = new System.Drawing.Point(0, 0);
			this.ctrl_TreeElements.Name = "ctrl_TreeElements";
			this.ctrl_TreeElements.p_ReadOnly = false;
			this.ctrl_TreeElements.SelectedImageIndex = 0;
			this.ctrl_TreeElements.Size = new System.Drawing.Size(262, 680);
			this.ctrl_TreeElements.TabIndex = 5;
			// 
			// ctrl_P_DesignConteiner
			// 
			this.ctrl_P_DesignConteiner.AutoSize = true;
			this.ctrl_P_DesignConteiner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_P_DesignConteiner.Location = new System.Drawing.Point(0, 0);
			this.ctrl_P_DesignConteiner.Name = "ctrl_P_DesignConteiner";
			this.ctrl_P_DesignConteiner.Size = new System.Drawing.Size(709, 680);
			this.ctrl_P_DesignConteiner.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Controls.Add(this.splitContainer1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(3);
			this.panel1.Size = new System.Drawing.Size(981, 686);
			this.panel1.TabIndex = 0;
			// 
			// F_DesignerTemplate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(981, 686);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximumSize = new System.Drawing.Size(1600, 1000);
			this.Name = "F_DesignerTemplate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "F_DesignerTemplate";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel ctrl_P_DesignConteiner;
		private Core.Controls.Ctrl_TreeElements ctrl_TreeElements;
	}
}