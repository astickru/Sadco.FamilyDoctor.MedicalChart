namespace Sadco.FamilyDoctor.Core.Controls
{
	partial class Dlg_HistoryViewer
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ctrl_DGLogs = new System.Windows.Forms.DataGridView();
			this.cl_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cl_event = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cl_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ctrl_DGLogs)).BeginInit();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ctrl_DGLogs);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(5);
			this.panel2.Size = new System.Drawing.Size(684, 211);
			this.panel2.TabIndex = 64;
			// 
			// ctrl_DGLogs
			// 
			this.ctrl_DGLogs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.ctrl_DGLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ctrl_DGLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cl_date,
            this.cl_event,
            this.cl_user});
			this.ctrl_DGLogs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_DGLogs.Location = new System.Drawing.Point(5, 5);
			this.ctrl_DGLogs.Name = "ctrl_DGLogs";
			this.ctrl_DGLogs.Size = new System.Drawing.Size(674, 201);
			this.ctrl_DGLogs.TabIndex = 0;
			// 
			// cl_date
			// 
			this.cl_date.HeaderText = "Дата";
			this.cl_date.Name = "cl_date";
			this.cl_date.ReadOnly = true;
			this.cl_date.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.cl_date.Width = 150;
			// 
			// cl_event
			// 
			this.cl_event.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.cl_event.DefaultCellStyle = dataGridViewCellStyle1;
			this.cl_event.HeaderText = "Событие";
			this.cl_event.Name = "cl_event";
			this.cl_event.ReadOnly = true;
			this.cl_event.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// cl_user
			// 
			this.cl_user.HeaderText = "Пользователь";
			this.cl_user.Name = "cl_user";
			this.cl_user.ReadOnly = true;
			this.cl_user.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// Dlg_HistoryViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(684, 211);
			this.Controls.Add(this.panel2);
			this.MinimumSize = new System.Drawing.Size(600, 250);
			this.Name = "Dlg_HistoryViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "История";
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ctrl_DGLogs)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.DataGridView ctrl_DGLogs;
		private System.Windows.Forms.DataGridViewTextBoxColumn cl_date;
		private System.Windows.Forms.DataGridViewTextBoxColumn cl_event;
		private System.Windows.Forms.DataGridViewTextBoxColumn cl_user;
	}
}