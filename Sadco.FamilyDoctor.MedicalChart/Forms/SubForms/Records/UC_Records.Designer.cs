namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class UC_Records
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Records));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlLPatientName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrlBReportAdd = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctrl_TRecords = new OutlookStyleControls.OutlookGrid();
            this.p_MedicalCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_ClinikName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_DateForming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_CategoryTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_UserFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPDFViewer = new AxAcroPDFLib.AxAcroPDF();
            this.ctrlCMViewer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrlHTMLViewer = new System.Windows.Forms.WebBrowser();
            this.ctrlMIPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlPDFViewer)).BeginInit();
            this.ctrlCMViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ctrlLPatientName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 39);
            this.panel1.TabIndex = 89;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Медкарты пациента:";
            // 
            // ctrlLPatientName
            // 
            this.ctrlLPatientName.AutoSize = true;
            this.ctrlLPatientName.Location = new System.Drawing.Point(152, 13);
            this.ctrlLPatientName.Name = "ctrlLPatientName";
            this.ctrlLPatientName.Size = new System.Drawing.Size(94, 13);
            this.ctrlLPatientName.TabIndex = 0;
            this.ctrlLPatientName.Text = "Пациент ФИО";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctrlBReportAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 306);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 38);
            this.panel2.TabIndex = 90;
            // 
            // ctrlBReportAdd
            // 
            this.ctrlBReportAdd.Location = new System.Drawing.Point(8, 8);
            this.ctrlBReportAdd.Name = "ctrlBReportAdd";
            this.ctrlBReportAdd.Size = new System.Drawing.Size(99, 23);
            this.ctrlBReportAdd.TabIndex = 0;
            this.ctrlBReportAdd.Text = "добавить";
            this.ctrlBReportAdd.UseVisualStyleBackColor = true;
            this.ctrlBReportAdd.Click += new System.EventHandler(this.ctrlBReportAdd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ctrl_TRecords);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ctrlPDFViewer);
            this.splitContainer1.Panel2.Controls.Add(this.ctrlHTMLViewer);
            this.splitContainer1.Size = new System.Drawing.Size(789, 267);
            this.splitContainer1.SplitterDistance = 480;
            this.splitContainer1.TabIndex = 91;
            // 
            // ctrl_TRecords
            // 
            this.ctrl_TRecords.AllowUserToAddRows = false;
            this.ctrl_TRecords.AllowUserToResizeRows = false;
            this.ctrl_TRecords.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ctrl_TRecords.CollapseIcon = null;
            this.ctrl_TRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrl_TRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.p_MedicalCardID,
            this.p_ClinikName,
            this.p_DateForming,
            this.p_CategoryTotal,
            this.p_Title,
            this.p_UserFIO});
            this.ctrl_TRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_TRecords.ExpandIcon = null;
            this.ctrl_TRecords.Location = new System.Drawing.Point(0, 0);
            this.ctrl_TRecords.Name = "ctrl_TRecords";
            this.ctrl_TRecords.ReadOnly = true;
            this.ctrl_TRecords.RowHeadersVisible = false;
            this.ctrl_TRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrl_TRecords.ShowCellErrors = false;
            this.ctrl_TRecords.ShowCellToolTips = false;
            this.ctrl_TRecords.ShowEditingIcon = false;
            this.ctrl_TRecords.ShowRowErrors = false;
            this.ctrl_TRecords.Size = new System.Drawing.Size(480, 267);
            this.ctrl_TRecords.TabIndex = 88;
            this.ctrl_TRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TRecords_CellClick);
            this.ctrl_TRecords.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TRecords_CellDoubleClick);
            // 
            // p_MedicalCardID
            // 
            this.p_MedicalCardID.HeaderText = "Медкарта";
            this.p_MedicalCardID.Name = "p_MedicalCardID";
            this.p_MedicalCardID.ReadOnly = true;
            this.p_MedicalCardID.Width = 150;
            // 
            // p_ClinikName
            // 
            this.p_ClinikName.HeaderText = "Клиника";
            this.p_ClinikName.Name = "p_ClinikName";
            this.p_ClinikName.ReadOnly = true;
            // 
            // p_DateForming
            // 
            this.p_DateForming.HeaderText = "Дата";
            this.p_DateForming.Name = "p_DateForming";
            this.p_DateForming.ReadOnly = true;
            this.p_DateForming.Width = 150;
            // 
            // p_CategoryTotal
            // 
            this.p_CategoryTotal.HeaderText = "Общая категория";
            this.p_CategoryTotal.Name = "p_CategoryTotal";
            this.p_CategoryTotal.ReadOnly = true;
            this.p_CategoryTotal.Width = 170;
            // 
            // p_Title
            // 
            this.p_Title.HeaderText = "Запись";
            this.p_Title.Name = "p_Title";
            this.p_Title.ReadOnly = true;
            this.p_Title.Width = 250;
            // 
            // p_UserFIO
            // 
            this.p_UserFIO.HeaderText = "Специалист";
            this.p_UserFIO.Name = "p_UserFIO";
            this.p_UserFIO.ReadOnly = true;
            this.p_UserFIO.Width = 150;
            // 
            // ctrlPDFViewer
            // 
            this.ctrlPDFViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPDFViewer.Enabled = true;
            this.ctrlPDFViewer.Location = new System.Drawing.Point(0, 0);
            this.ctrlPDFViewer.Name = "ctrlPDFViewer";
            this.ctrlPDFViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ctrlPDFViewer.OcxState")));
            this.ctrlPDFViewer.Size = new System.Drawing.Size(305, 267);
            this.ctrlPDFViewer.TabIndex = 1;
            // 
            // ctrlCMViewer
            // 
            this.ctrlCMViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMIOpen,
            this.ctrlMIArchive,
            this.ctrlMIPrint});
            this.ctrlCMViewer.Name = "ctrlCMViewer";
            this.ctrlCMViewer.Size = new System.Drawing.Size(153, 92);
            // 
            // ctrlHTMLViewer
            // 
            this.ctrlHTMLViewer.ContextMenuStrip = this.ctrlCMViewer;
            this.ctrlHTMLViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlHTMLViewer.IsWebBrowserContextMenuEnabled = false;
            this.ctrlHTMLViewer.Location = new System.Drawing.Point(0, 0);
            this.ctrlHTMLViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.ctrlHTMLViewer.Name = "ctrlHTMLViewer";
            this.ctrlHTMLViewer.Size = new System.Drawing.Size(305, 267);
            this.ctrlHTMLViewer.TabIndex = 0;
            // 
            // ctrlMIPrint
            // 
            this.ctrlMIPrint.Name = "ctrlMIPrint";
            this.ctrlMIPrint.Size = new System.Drawing.Size(152, 22);
            this.ctrlMIPrint.Text = "Печать";
            this.ctrlMIPrint.Click += new System.EventHandler(this.ctrlMIPrint_Click);
            // 
            // ctrlMIArhive
            // 
            this.ctrlMIArchive.Name = "ctrlMIArhive";
            this.ctrlMIArchive.Size = new System.Drawing.Size(152, 22);
            this.ctrlMIArchive.Text = "Архивировать";
            this.ctrlMIArchive.Click += new System.EventHandler(this.ctrlMIArhive_Click);
            // 
            // ctrlMIOpen
            // 
            this.ctrlMIOpen.Name = "ctrlMIOpen";
            this.ctrlMIOpen.Size = new System.Drawing.Size(152, 22);
            this.ctrlMIOpen.Text = "Открыть";
            this.ctrlMIOpen.Click += new System.EventHandler(this.ctrlMIOpen_Click);
            // 
            // UC_Records
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximumSize = new System.Drawing.Size(1600, 1000);
            this.Name = "UC_Records";
            this.Size = new System.Drawing.Size(789, 344);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlPDFViewer)).EndInit();
            this.ctrlCMViewer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ctrlLPatientName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ctrlBReportAdd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser ctrlHTMLViewer;
        private AxAcroPDFLib.AxAcroPDF ctrlPDFViewer;
        private OutlookStyleControls.OutlookGrid ctrl_TRecords;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_MedicalCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_ClinikName;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_DateForming;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_CategoryTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_UserFIO;
        private System.Windows.Forms.ContextMenuStrip ctrlCMViewer;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIPrint;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIArchive;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIOpen;
    }
}
