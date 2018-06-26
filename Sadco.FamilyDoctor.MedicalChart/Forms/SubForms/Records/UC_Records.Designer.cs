using System;

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
            this.panel3 = new System.Windows.Forms.Panel();
            this.ctrlBReportAddRecord = new System.Windows.Forms.Button();
            this.ctrlBReportAddPattern = new System.Windows.Forms.Button();
            this.ctrlBReportEdit = new System.Windows.Forms.Button();
            this.ctrlBReportArchive = new System.Windows.Forms.Button();
            this.ctrlBReportRating = new System.Windows.Forms.Button();
            this.ctrlBReportSyncBMK = new System.Windows.Forms.Button();
            this.ctrlBReportPrintDoctor = new System.Windows.Forms.Button();
            this.ctrlBReportPrintPatient = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlLPatientName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctrlHTMLViewer = new System.Windows.Forms.WebBrowser();
            this.ctrlCMViewer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrlMIEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIRating = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMISyncBMK = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIPrintDoctor = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIPrintPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlPDFViewer = new AxAcroPDFLib.AxAcroPDF();
            this.ctrl_TRecords = new OutlookStyleControls.OutlookGrid();
            this.p_MedicalCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_ClinikName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_DateForming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_CategoryTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_DoctorFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlBReportFormatPattern = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctrlCMViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlPDFViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ctrlLPatientName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1371, 39);
            this.panel1.TabIndex = 89;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.ctrlBReportAddRecord);
            this.panel3.Controls.Add(this.ctrlBReportAddPattern);
            this.panel3.Controls.Add(this.ctrlBReportFormatPattern);
            this.panel3.Controls.Add(this.ctrlBReportEdit);
            this.panel3.Controls.Add(this.ctrlBReportArchive);
            this.panel3.Controls.Add(this.ctrlBReportRating);
            this.panel3.Controls.Add(this.ctrlBReportSyncBMK);
            this.panel3.Controls.Add(this.ctrlBReportPrintDoctor);
            this.panel3.Controls.Add(this.ctrlBReportPrintPatient);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(355, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1016, 39);
            this.panel3.TabIndex = 2;
            // 
            // ctrlBReportAddRecord
            // 
            this.ctrlBReportAddRecord.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportAddRecord.Location = new System.Drawing.Point(0, 0);
            this.ctrlBReportAddRecord.Name = "ctrlBReportAddRecord";
            this.ctrlBReportAddRecord.Size = new System.Drawing.Size(99, 39);
            this.ctrlBReportAddRecord.TabIndex = 0;
            this.ctrlBReportAddRecord.Text = "добавить запись";
            this.ctrlBReportAddRecord.UseVisualStyleBackColor = true;
            this.ctrlBReportAddRecord.Click += new System.EventHandler(this.ctrlBReportAddRecord_Click);
            // 
            // ctrlBReportAddPattern
            // 
            this.ctrlBReportAddPattern.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportAddPattern.Location = new System.Drawing.Point(99, 0);
            this.ctrlBReportAddPattern.Name = "ctrlBReportAddPattern";
            this.ctrlBReportAddPattern.Size = new System.Drawing.Size(99, 39);
            this.ctrlBReportAddPattern.TabIndex = 8;
            this.ctrlBReportAddPattern.Text = "добавить паттерн";
            this.ctrlBReportAddPattern.UseVisualStyleBackColor = true;
            this.ctrlBReportAddPattern.Click += new System.EventHandler(this.ctrlBReportAddPattern_Click);
            // 
            // ctrlBReportEdit
            // 
            this.ctrlBReportEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportEdit.Location = new System.Drawing.Point(326, 0);
            this.ctrlBReportEdit.Name = "ctrlBReportEdit";
            this.ctrlBReportEdit.Size = new System.Drawing.Size(126, 39);
            this.ctrlBReportEdit.TabIndex = 4;
            this.ctrlBReportEdit.Text = "редактировать";
            this.ctrlBReportEdit.UseVisualStyleBackColor = true;
            this.ctrlBReportEdit.Visible = false;
            this.ctrlBReportEdit.Click += new System.EventHandler(this.ctrlBReportEdit_Click);
            // 
            // ctrlBReportArchive
            // 
            this.ctrlBReportArchive.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportArchive.Location = new System.Drawing.Point(452, 0);
            this.ctrlBReportArchive.Name = "ctrlBReportArchive";
            this.ctrlBReportArchive.Size = new System.Drawing.Size(120, 39);
            this.ctrlBReportArchive.TabIndex = 5;
            this.ctrlBReportArchive.Text = "архивировать";
            this.ctrlBReportArchive.UseVisualStyleBackColor = true;
            this.ctrlBReportArchive.Visible = false;
            this.ctrlBReportArchive.Click += new System.EventHandler(this.ctrlBReportArchive_Click);
            // 
            // ctrlBReportRating
            // 
            this.ctrlBReportRating.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportRating.Location = new System.Drawing.Point(572, 0);
            this.ctrlBReportRating.Name = "ctrlBReportRating";
            this.ctrlBReportRating.Size = new System.Drawing.Size(75, 39);
            this.ctrlBReportRating.TabIndex = 2;
            this.ctrlBReportRating.Text = "оценка";
            this.ctrlBReportRating.UseVisualStyleBackColor = true;
            this.ctrlBReportRating.Visible = false;
            this.ctrlBReportRating.Click += new System.EventHandler(this.ctrlBReportRating_Click);
            // 
            // ctrlBReportSyncBMK
            // 
            this.ctrlBReportSyncBMK.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportSyncBMK.Location = new System.Drawing.Point(647, 0);
            this.ctrlBReportSyncBMK.Name = "ctrlBReportSyncBMK";
            this.ctrlBReportSyncBMK.Size = new System.Drawing.Size(105, 39);
            this.ctrlBReportSyncBMK.TabIndex = 7;
            this.ctrlBReportSyncBMK.Text = "синхр. с БМК";
            this.ctrlBReportSyncBMK.UseVisualStyleBackColor = true;
            this.ctrlBReportSyncBMK.Visible = false;
            this.ctrlBReportSyncBMK.Click += new System.EventHandler(this.ctrlBReportSyncBMK_Click);
            // 
            // ctrlBReportPrintDoctor
            // 
            this.ctrlBReportPrintDoctor.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportPrintDoctor.Location = new System.Drawing.Point(752, 0);
            this.ctrlBReportPrintDoctor.Name = "ctrlBReportPrintDoctor";
            this.ctrlBReportPrintDoctor.Size = new System.Drawing.Size(132, 39);
            this.ctrlBReportPrintDoctor.TabIndex = 3;
            this.ctrlBReportPrintDoctor.Text = "печать доктора";
            this.ctrlBReportPrintDoctor.UseVisualStyleBackColor = true;
            this.ctrlBReportPrintDoctor.Visible = false;
            this.ctrlBReportPrintDoctor.Click += new System.EventHandler(this.ctrlBReportPrintDoctor_Click);
            // 
            // ctrlBReportPrintPatient
            // 
            this.ctrlBReportPrintPatient.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportPrintPatient.Location = new System.Drawing.Point(884, 0);
            this.ctrlBReportPrintPatient.Name = "ctrlBReportPrintPatient";
            this.ctrlBReportPrintPatient.Size = new System.Drawing.Size(132, 39);
            this.ctrlBReportPrintPatient.TabIndex = 6;
            this.ctrlBReportPrintPatient.Text = "печать пациента";
            this.ctrlBReportPrintPatient.UseVisualStyleBackColor = true;
            this.ctrlBReportPrintPatient.Visible = false;
            this.ctrlBReportPrintPatient.Click += new System.EventHandler(this.ctrlBReportPrintPatient_Click);
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
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 331);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1371, 13);
            this.panel2.TabIndex = 90;
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
            this.splitContainer1.Size = new System.Drawing.Size(1371, 292);
            this.splitContainer1.SplitterDistance = 775;
            this.splitContainer1.TabIndex = 91;
            // 
            // ctrlHTMLViewer
            // 
            this.ctrlHTMLViewer.ContextMenuStrip = this.ctrlCMViewer;
            this.ctrlHTMLViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlHTMLViewer.IsWebBrowserContextMenuEnabled = false;
            this.ctrlHTMLViewer.Location = new System.Drawing.Point(0, 0);
            this.ctrlHTMLViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.ctrlHTMLViewer.Name = "ctrlHTMLViewer";
            this.ctrlHTMLViewer.Size = new System.Drawing.Size(592, 292);
            this.ctrlHTMLViewer.TabIndex = 0;
            // 
            // ctrlCMViewer
            // 
            this.ctrlCMViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMIEdit,
            this.ctrlMIArchive,
            this.ctrlMIRating,
            this.ctrlMISyncBMK,
            this.ctrlMIPrint});
            this.ctrlCMViewer.Name = "ctrlCMViewer";
            this.ctrlCMViewer.Size = new System.Drawing.Size(216, 114);
            // 
            // ctrlMIEdit
            // 
            this.ctrlMIEdit.Name = "ctrlMIEdit";
            this.ctrlMIEdit.Size = new System.Drawing.Size(215, 22);
            this.ctrlMIEdit.Text = "Редактировать";
            this.ctrlMIEdit.Click += new System.EventHandler(this.ctrlMIEdit_Click);
            // 
            // ctrlMIArchive
            // 
            this.ctrlMIArchive.Name = "ctrlMIArchive";
            this.ctrlMIArchive.Size = new System.Drawing.Size(215, 22);
            this.ctrlMIArchive.Text = "Архивировать";
            this.ctrlMIArchive.Click += new System.EventHandler(this.ctrlMIArhive_Click);
            // 
            // ctrlMIRating
            // 
            this.ctrlMIRating.Name = "ctrlMIRating";
            this.ctrlMIRating.Size = new System.Drawing.Size(215, 22);
            this.ctrlMIRating.Text = "Оценка";
            this.ctrlMIRating.Click += new System.EventHandler(this.ctrlMIRating_Click);
            // 
            // ctrlMISyncBMK
            // 
            this.ctrlMISyncBMK.Name = "ctrlMISyncBMK";
            this.ctrlMISyncBMK.Size = new System.Drawing.Size(215, 22);
            this.ctrlMISyncBMK.Text = "Синхронизировать с БМК";
            this.ctrlMISyncBMK.Click += new System.EventHandler(this.ctrlMISyncBMK_Click);
            // 
            // ctrlMIPrint
            // 
            this.ctrlMIPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMIPrintDoctor,
            this.ctrlMIPrintPatient});
            this.ctrlMIPrint.Name = "ctrlMIPrint";
            this.ctrlMIPrint.Size = new System.Drawing.Size(215, 22);
            this.ctrlMIPrint.Text = "Печать";
            // 
            // ctrlMIPrintDoctor
            // 
            this.ctrlMIPrintDoctor.Name = "ctrlMIPrintDoctor";
            this.ctrlMIPrintDoctor.Size = new System.Drawing.Size(127, 22);
            this.ctrlMIPrintDoctor.Text = "Врачу";
            this.ctrlMIPrintDoctor.Click += new System.EventHandler(this.ctrlMIPrintDoctor_Click);
            // 
            // ctrlMIPrintPatient
            // 
            this.ctrlMIPrintPatient.Name = "ctrlMIPrintPatient";
            this.ctrlMIPrintPatient.Size = new System.Drawing.Size(127, 22);
            this.ctrlMIPrintPatient.Text = "Пациенту";
            this.ctrlMIPrintPatient.Click += new System.EventHandler(this.ctrlMIPrintPatient_Click);
            // 
            // ctrlPDFViewer
            // 
            this.ctrlPDFViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPDFViewer.Enabled = true;
            this.ctrlPDFViewer.Location = new System.Drawing.Point(0, 0);
            this.ctrlPDFViewer.Name = "ctrlPDFViewer";
            this.ctrlPDFViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ctrlPDFViewer.OcxState")));
            this.ctrlPDFViewer.Size = new System.Drawing.Size(592, 292);
            this.ctrlPDFViewer.TabIndex = 1;
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
            this.p_DoctorFIO});
            this.ctrl_TRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_TRecords.ExpandIcon = null;
            this.ctrl_TRecords.ItemName = "запись";
            this.ctrl_TRecords.ItemsName = "запис.";
            this.ctrl_TRecords.Location = new System.Drawing.Point(0, 0);
            this.ctrl_TRecords.Name = "ctrl_TRecords";
            this.ctrl_TRecords.ReadOnly = true;
            this.ctrl_TRecords.RowHeadersVisible = false;
            this.ctrl_TRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrl_TRecords.ShowCellErrors = false;
            this.ctrl_TRecords.ShowCellToolTips = false;
            this.ctrl_TRecords.ShowEditingIcon = false;
            this.ctrl_TRecords.ShowRowErrors = false;
            this.ctrl_TRecords.Size = new System.Drawing.Size(775, 292);
            this.ctrl_TRecords.TabIndex = 88;
            this.ctrl_TRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TRecords_CellClick);
            this.ctrl_TRecords.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TRecords_CellDoubleClick);
            // 
            // p_MedicalCardID
            // 
            this.p_MedicalCardID.HeaderText = "Карта";
            this.p_MedicalCardID.Name = "p_MedicalCardID";
            this.p_MedicalCardID.ReadOnly = true;
            this.p_MedicalCardID.Width = 60;
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
            this.p_DateForming.Width = 145;
            // 
            // p_CategoryTotal
            // 
            this.p_CategoryTotal.HeaderText = "Общая категория";
            this.p_CategoryTotal.Name = "p_CategoryTotal";
            this.p_CategoryTotal.ReadOnly = true;
            this.p_CategoryTotal.Width = 110;
            // 
            // p_Title
            // 
            this.p_Title.HeaderText = "Запись";
            this.p_Title.Name = "p_Title";
            this.p_Title.ReadOnly = true;
            this.p_Title.Width = 210;
            // 
            // p_DoctorFIO
            // 
            this.p_DoctorFIO.HeaderText = "Специалист";
            this.p_DoctorFIO.Name = "p_DoctorFIO";
            this.p_DoctorFIO.ReadOnly = true;
            this.p_DoctorFIO.Width = 130;
            // 
            // ctrlBReportFormatPattern
            // 
            this.ctrlBReportFormatPattern.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlBReportFormatPattern.Location = new System.Drawing.Point(198, 0);
            this.ctrlBReportFormatPattern.Name = "ctrlBReportFormatPattern";
            this.ctrlBReportFormatPattern.Size = new System.Drawing.Size(128, 39);
            this.ctrlBReportFormatPattern.TabIndex = 9;
            this.ctrlBReportFormatPattern.Text = "сформировать паттерн";
            this.ctrlBReportFormatPattern.UseVisualStyleBackColor = true;
            this.ctrlBReportFormatPattern.Visible = false;
            this.ctrlBReportFormatPattern.Click += new System.EventHandler(this.ctrlBReportFormatPattern_Click);
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
            this.Name = "UC_Records";
            this.Size = new System.Drawing.Size(1371, 344);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ctrlCMViewer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlPDFViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TRecords)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ctrlLPatientName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ctrlBReportAddRecord;
        private System.Windows.Forms.Button ctrlBReportAddPattern;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser ctrlHTMLViewer;
        private AxAcroPDFLib.AxAcroPDF ctrlPDFViewer;
        private OutlookStyleControls.OutlookGrid ctrl_TRecords;
        private System.Windows.Forms.ContextMenuStrip ctrlCMViewer;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIPrint;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIArchive;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIEdit;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIPrintDoctor;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIPrintPatient;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIRating;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button ctrlBReportPrintPatient;
        private System.Windows.Forms.Button ctrlBReportArchive;
        private System.Windows.Forms.Button ctrlBReportEdit;
        private System.Windows.Forms.Button ctrlBReportPrintDoctor;
        private System.Windows.Forms.Button ctrlBReportRating;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_MedicalCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_ClinikName;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_DateForming;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_CategoryTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_DoctorFIO;
        private System.Windows.Forms.ToolStripMenuItem ctrlMISyncBMK;
        private System.Windows.Forms.Button ctrlBReportSyncBMK;
        private System.Windows.Forms.Button ctrlBReportFormatPattern;
    }
}
