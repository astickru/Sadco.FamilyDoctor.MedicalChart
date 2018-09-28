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
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlLPatientName = new System.Windows.Forms.Label();
            this.ctrlBReportAddRecord = new System.Windows.Forms.Button();
            this.ctrlBAddRecordFromRecord = new System.Windows.Forms.Button();
            this.ctrlBReportAddPattern = new System.Windows.Forms.Button();
            this.ctrlBReportFormatPattern = new System.Windows.Forms.Button();
            this.ctrlBReportEdit = new System.Windows.Forms.Button();
            this.ctrlBReportRating = new System.Windows.Forms.Button();
            this.ctrlBReportSyncBMK = new System.Windows.Forms.Button();
            this.ctrlBReportPrintDoctor = new System.Windows.Forms.Button();
            this.ctrlBReportPrintPatient = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctrl_TRecords = new OutlookStyleControls.OutlookGrid();
            this.p_MedicalCardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_ClinikName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_DateForming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_CategoryTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_DoctorFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPDFViewer = new AxAcroPDFLib.AxAcroPDF();
            this.ctrlHTMLViewer = new System.Windows.Forms.WebBrowser();
            this.ctrlCMViewer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrlMIEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIRating = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMISyncBMK = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIPrintDoctor = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIPrintPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlPRecordInfo = new System.Windows.Forms.Panel();
            this.ctrlRecordInfo = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctrlBReportDelete = new System.Windows.Forms.Button();
            this.ctrlBReportAddRecordByFile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlPDFViewer)).BeginInit();
            this.ctrlCMViewer.SuspendLayout();
            this.ctrlPRecordInfo.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1371, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(0, 39);
            this.panel3.TabIndex = 2;
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
            // ctrlBReportAddRecord
            // 
            this.ctrlBReportAddRecord.Location = new System.Drawing.Point(210, 3);
            this.ctrlBReportAddRecord.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportAddRecord.Name = "ctrlBReportAddRecord";
            this.ctrlBReportAddRecord.Size = new System.Drawing.Size(99, 38);
            this.ctrlBReportAddRecord.TabIndex = 0;
            this.ctrlBReportAddRecord.Text = "добавить запись";
            this.ctrlBReportAddRecord.UseVisualStyleBackColor = true;
            this.ctrlBReportAddRecord.Click += new System.EventHandler(this.ctrlBReportAddRecord_Click);
            // 
            // ctrlBAddRecordFromRecord
            // 
            this.ctrlBAddRecordFromRecord.Location = new System.Drawing.Point(324, 3);
            this.ctrlBAddRecordFromRecord.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBAddRecordFromRecord.Name = "ctrlBAddRecordFromRecord";
            this.ctrlBAddRecordFromRecord.Size = new System.Drawing.Size(103, 38);
            this.ctrlBAddRecordFromRecord.TabIndex = 10;
            this.ctrlBAddRecordFromRecord.Text = "копировать запись";
            this.ctrlBAddRecordFromRecord.UseVisualStyleBackColor = true;
            this.ctrlBAddRecordFromRecord.Click += new System.EventHandler(this.ctrlBAddRecordFromRecord_Click);
            // 
            // ctrlBReportAddPattern
            // 
            this.ctrlBReportAddPattern.Location = new System.Drawing.Point(678, 3);
            this.ctrlBReportAddPattern.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportAddPattern.Name = "ctrlBReportAddPattern";
            this.ctrlBReportAddPattern.Size = new System.Drawing.Size(95, 38);
            this.ctrlBReportAddPattern.TabIndex = 8;
            this.ctrlBReportAddPattern.Text = "добавить паттерн";
            this.ctrlBReportAddPattern.UseVisualStyleBackColor = true;
            this.ctrlBReportAddPattern.Click += new System.EventHandler(this.ctrlBReportAddPattern_Click);
            // 
            // ctrlBReportFormatPattern
            // 
            this.ctrlBReportFormatPattern.Location = new System.Drawing.Point(788, 3);
            this.ctrlBReportFormatPattern.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportFormatPattern.Name = "ctrlBReportFormatPattern";
            this.ctrlBReportFormatPattern.Size = new System.Drawing.Size(116, 38);
            this.ctrlBReportFormatPattern.TabIndex = 9;
            this.ctrlBReportFormatPattern.Text = "сформировать паттерн";
            this.ctrlBReportFormatPattern.UseVisualStyleBackColor = true;
            this.ctrlBReportFormatPattern.Visible = false;
            this.ctrlBReportFormatPattern.Click += new System.EventHandler(this.ctrlBReportFormatPattern_Click);
            // 
            // ctrlBReportEdit
            // 
            this.ctrlBReportEdit.Location = new System.Drawing.Point(442, 3);
            this.ctrlBReportEdit.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportEdit.Name = "ctrlBReportEdit";
            this.ctrlBReportEdit.Size = new System.Drawing.Size(126, 38);
            this.ctrlBReportEdit.TabIndex = 4;
            this.ctrlBReportEdit.Text = "редактировать";
            this.ctrlBReportEdit.UseVisualStyleBackColor = true;
            this.ctrlBReportEdit.Visible = false;
            this.ctrlBReportEdit.Click += new System.EventHandler(this.ctrlBReportEdit_Click);
            // 
            // ctrlBReportRating
            // 
            this.ctrlBReportRating.Location = new System.Drawing.Point(1254, 3);
            this.ctrlBReportRating.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportRating.Name = "ctrlBReportRating";
            this.ctrlBReportRating.Size = new System.Drawing.Size(96, 38);
            this.ctrlBReportRating.TabIndex = 2;
            this.ctrlBReportRating.Text = "экспертиза";
            this.ctrlBReportRating.UseVisualStyleBackColor = true;
            this.ctrlBReportRating.Visible = false;
            this.ctrlBReportRating.Click += new System.EventHandler(this.ctrlBReportRating_Click);
            // 
            // ctrlBReportSyncBMK
            // 
            this.ctrlBReportSyncBMK.Location = new System.Drawing.Point(1134, 3);
            this.ctrlBReportSyncBMK.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportSyncBMK.Name = "ctrlBReportSyncBMK";
            this.ctrlBReportSyncBMK.Size = new System.Drawing.Size(105, 38);
            this.ctrlBReportSyncBMK.TabIndex = 7;
            this.ctrlBReportSyncBMK.Text = "синхр. с БМК";
            this.ctrlBReportSyncBMK.UseVisualStyleBackColor = true;
            this.ctrlBReportSyncBMK.Visible = false;
            this.ctrlBReportSyncBMK.Click += new System.EventHandler(this.ctrlBReportSyncBMK_Click);
            // 
            // ctrlBReportPrintDoctor
            // 
            this.ctrlBReportPrintDoctor.Location = new System.Drawing.Point(919, 3);
            this.ctrlBReportPrintDoctor.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportPrintDoctor.Name = "ctrlBReportPrintDoctor";
            this.ctrlBReportPrintDoctor.Size = new System.Drawing.Size(91, 38);
            this.ctrlBReportPrintDoctor.TabIndex = 3;
            this.ctrlBReportPrintDoctor.Text = "печать доктора";
            this.ctrlBReportPrintDoctor.UseVisualStyleBackColor = true;
            this.ctrlBReportPrintDoctor.Visible = false;
            this.ctrlBReportPrintDoctor.Click += new System.EventHandler(this.ctrlBReportPrintDoctor_Click);
            // 
            // ctrlBReportPrintPatient
            // 
            this.ctrlBReportPrintPatient.Location = new System.Drawing.Point(1025, 3);
            this.ctrlBReportPrintPatient.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportPrintPatient.Name = "ctrlBReportPrintPatient";
            this.ctrlBReportPrintPatient.Size = new System.Drawing.Size(94, 38);
            this.ctrlBReportPrintPatient.TabIndex = 6;
            this.ctrlBReportPrintPatient.Text = "печать пациента";
            this.ctrlBReportPrintPatient.UseVisualStyleBackColor = true;
            this.ctrlBReportPrintPatient.Visible = false;
            this.ctrlBReportPrintPatient.Click += new System.EventHandler(this.ctrlBReportPrintPatient_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.ctrlPRecordInfo);
            this.splitContainer1.Size = new System.Drawing.Size(1371, 261);
            this.splitContainer1.SplitterDistance = 695;
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
            this.p_MedicalCardNumber,
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
            this.ctrl_TRecords.MultiSelect = false;
            this.ctrl_TRecords.Name = "ctrl_TRecords";
            this.ctrl_TRecords.ReadOnly = true;
            this.ctrl_TRecords.RowHeadersVisible = false;
            this.ctrl_TRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrl_TRecords.ShowCellErrors = false;
            this.ctrl_TRecords.ShowCellToolTips = false;
            this.ctrl_TRecords.ShowEditingIcon = false;
            this.ctrl_TRecords.ShowRowErrors = false;
            this.ctrl_TRecords.Size = new System.Drawing.Size(695, 261);
            this.ctrl_TRecords.TabIndex = 88;
            this.ctrl_TRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TRecords_CellClick);
            this.ctrl_TRecords.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TRecords_CellDoubleClick);
            // 
            // p_MedicalCardNumber
            // 
            this.p_MedicalCardNumber.HeaderText = "№ медкарты";
            this.p_MedicalCardNumber.Name = "p_MedicalCardNumber";
            this.p_MedicalCardNumber.ReadOnly = true;
            this.p_MedicalCardNumber.Width = 120;
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
            this.p_DateForming.Width = 125;
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
            // ctrlPDFViewer
            // 
            this.ctrlPDFViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPDFViewer.Enabled = true;
            this.ctrlPDFViewer.Location = new System.Drawing.Point(0, 21);
            this.ctrlPDFViewer.Name = "ctrlPDFViewer";
            this.ctrlPDFViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ctrlPDFViewer.OcxState")));
            this.ctrlPDFViewer.Size = new System.Drawing.Size(672, 240);
            this.ctrlPDFViewer.TabIndex = 1;
            // 
            // ctrlHTMLViewer
            // 
            this.ctrlHTMLViewer.ContextMenuStrip = this.ctrlCMViewer;
            this.ctrlHTMLViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlHTMLViewer.IsWebBrowserContextMenuEnabled = false;
            this.ctrlHTMLViewer.Location = new System.Drawing.Point(0, 21);
            this.ctrlHTMLViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.ctrlHTMLViewer.Name = "ctrlHTMLViewer";
            this.ctrlHTMLViewer.Size = new System.Drawing.Size(672, 240);
            this.ctrlHTMLViewer.TabIndex = 0;
            // 
            // ctrlCMViewer
            // 
            this.ctrlCMViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMIEdit,
            this.ctrlMIArchive,
            this.ctrlMIDelete,
            this.ctrlMIRating,
            this.ctrlMISyncBMK,
            this.ctrlMIPrint});
            this.ctrlCMViewer.Name = "ctrlCMViewer";
            this.ctrlCMViewer.Size = new System.Drawing.Size(216, 158);
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
            // 
            // ctrlMIDelete
            // 
            this.ctrlMIDelete.Name = "ctrlMIDelete";
            this.ctrlMIDelete.Size = new System.Drawing.Size(215, 22);
            this.ctrlMIDelete.Text = "Удалить";
            this.ctrlMIDelete.Visible = false;
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
            // ctrlPRecordInfo
            // 
            this.ctrlPRecordInfo.Controls.Add(this.ctrlRecordInfo);
            this.ctrlPRecordInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlPRecordInfo.Location = new System.Drawing.Point(0, 0);
            this.ctrlPRecordInfo.Name = "ctrlPRecordInfo";
            this.ctrlPRecordInfo.Size = new System.Drawing.Size(672, 21);
            this.ctrlPRecordInfo.TabIndex = 2;
            this.ctrlPRecordInfo.Visible = false;
            // 
            // ctrlRecordInfo
            // 
            this.ctrlRecordInfo.AutoSize = true;
            this.ctrlRecordInfo.Location = new System.Drawing.Point(4, 3);
            this.ctrlRecordInfo.Name = "ctrlRecordInfo";
            this.ctrlRecordInfo.Size = new System.Drawing.Size(101, 13);
            this.ctrlRecordInfo.TabIndex = 0;
            this.ctrlRecordInfo.Text = "ctrlRecordInfo";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportRating);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportSyncBMK);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportPrintPatient);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportPrintDoctor);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportFormatPattern);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportAddPattern);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportDelete);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportEdit);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBAddRecordFromRecord);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportAddRecord);
            this.flowLayoutPanel1.Controls.Add(this.ctrlBReportAddRecordByFile);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 300);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1371, 44);
            this.flowLayoutPanel1.TabIndex = 11;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // ctrlBReportDelete
            // 
            this.ctrlBReportDelete.Location = new System.Drawing.Point(583, 3);
            this.ctrlBReportDelete.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportDelete.Name = "ctrlBReportDelete";
            this.ctrlBReportDelete.Size = new System.Drawing.Size(80, 38);
            this.ctrlBReportDelete.TabIndex = 12;
            this.ctrlBReportDelete.Text = "удалить";
            this.ctrlBReportDelete.UseVisualStyleBackColor = true;
            this.ctrlBReportDelete.Visible = false;
            this.ctrlBReportDelete.Click += new System.EventHandler(this.ctrlBReportDelete_Click);
            // 
            // ctrlBReportAddRecordByFile
            // 
            this.ctrlBReportAddRecordByFile.Location = new System.Drawing.Point(96, 3);
            this.ctrlBReportAddRecordByFile.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBReportAddRecordByFile.Name = "ctrlBReportAddRecordByFile";
            this.ctrlBReportAddRecordByFile.Size = new System.Drawing.Size(99, 38);
            this.ctrlBReportAddRecordByFile.TabIndex = 11;
            this.ctrlBReportAddRecordByFile.Text = "добавить запись";
            this.ctrlBReportAddRecordByFile.UseVisualStyleBackColor = true;
            this.ctrlBReportAddRecordByFile.Click += new System.EventHandler(this.ctrlBReportAddRecordByFile_Click);
            // 
            // UC_Records
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "UC_Records";
            this.Size = new System.Drawing.Size(1371, 344);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlPDFViewer)).EndInit();
            this.ctrlCMViewer.ResumeLayout(false);
            this.ctrlPRecordInfo.ResumeLayout(false);
            this.ctrlPRecordInfo.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ctrlLPatientName;
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
        private System.Windows.Forms.Button ctrlBReportEdit;
        private System.Windows.Forms.Button ctrlBReportPrintDoctor;
        private System.Windows.Forms.Button ctrlBReportRating;
        private System.Windows.Forms.ToolStripMenuItem ctrlMISyncBMK;
        private System.Windows.Forms.Button ctrlBReportSyncBMK;
        private System.Windows.Forms.Button ctrlBReportFormatPattern;
        private System.Windows.Forms.Panel ctrlPRecordInfo;
        private System.Windows.Forms.Label ctrlRecordInfo;
        private System.Windows.Forms.Button ctrlBAddRecordFromRecord;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_MedicalCardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_ClinikName;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_DateForming;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_CategoryTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_DoctorFIO;
        private System.Windows.Forms.Button ctrlBReportAddRecordByFile;
        private System.Windows.Forms.Button ctrlBReportDelete;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIDelete;
    }
}
