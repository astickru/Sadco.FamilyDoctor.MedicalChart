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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Records));
            this.ctrl_TPartNormRangeValues = new System.Windows.Forms.DataGridView();
            this.p_ClinikName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_DateForming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_CategoryTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_UserFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlLPatientName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrlBReportAdd = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctrlHTMLViewer = new System.Windows.Forms.WebBrowser();
            this.ctrlPDFViewer = new AxAcroPDFLib.AxAcroPDF();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TPartNormRangeValues)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlPDFViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrl_TPartNormRangeValues
            // 
            this.ctrl_TPartNormRangeValues.AllowUserToAddRows = false;
            this.ctrl_TPartNormRangeValues.AllowUserToResizeRows = false;
            this.ctrl_TPartNormRangeValues.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ctrl_TPartNormRangeValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrl_TPartNormRangeValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.p_ClinikName,
            this.p_DateForming,
            this.p_CategoryTotal,
            this.p_Title,
            this.p_UserFIO});
            this.ctrl_TPartNormRangeValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_TPartNormRangeValues.Location = new System.Drawing.Point(0, 0);
            this.ctrl_TPartNormRangeValues.Name = "ctrl_TPartNormRangeValues";
            this.ctrl_TPartNormRangeValues.ReadOnly = true;
            this.ctrl_TPartNormRangeValues.RowHeadersVisible = false;
            this.ctrl_TPartNormRangeValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrl_TPartNormRangeValues.ShowCellErrors = false;
            this.ctrl_TPartNormRangeValues.ShowCellToolTips = false;
            this.ctrl_TPartNormRangeValues.ShowEditingIcon = false;
            this.ctrl_TPartNormRangeValues.ShowRowErrors = false;
            this.ctrl_TPartNormRangeValues.Size = new System.Drawing.Size(480, 267);
            this.ctrl_TPartNormRangeValues.TabIndex = 88;
            this.ctrl_TPartNormRangeValues.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TPartNormRangeValues_CellClick);
            this.ctrl_TPartNormRangeValues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TPartNormRangeValues_CellDoubleClick);
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
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Записи пациента:";
            // 
            // ctrlLPatientName
            // 
            this.ctrlLPatientName.AutoSize = true;
            this.ctrlLPatientName.Location = new System.Drawing.Point(129, 13);
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
            this.splitContainer1.Panel1.Controls.Add(this.ctrl_TPartNormRangeValues);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ctrlPDFViewer);
            this.splitContainer1.Panel2.Controls.Add(this.ctrlHTMLViewer);
            this.splitContainer1.Size = new System.Drawing.Size(789, 267);
            this.splitContainer1.SplitterDistance = 480;
            this.splitContainer1.TabIndex = 91;
            // 
            // ctrlViewer
            // 
            this.ctrlHTMLViewer.AllowNavigation = true;
            this.ctrlHTMLViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlHTMLViewer.IsWebBrowserContextMenuEnabled = false;
            this.ctrlHTMLViewer.Location = new System.Drawing.Point(0, 0);
            this.ctrlHTMLViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.ctrlHTMLViewer.Name = "ctrlViewer";
            this.ctrlHTMLViewer.Size = new System.Drawing.Size(305, 267);
            this.ctrlHTMLViewer.TabIndex = 0;
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
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TPartNormRangeValues)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlPDFViewer)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.DataGridView ctrl_TPartNormRangeValues;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ctrlLPatientName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ctrlBReportAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_AgeFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_AgeTo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_ClinikName;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_DateForming;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_CategoryTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_UserFIO;
        private System.Windows.Forms.WebBrowser ctrlHTMLViewer;
        private AxAcroPDFLib.AxAcroPDF ctrlPDFViewer;
    }
}
