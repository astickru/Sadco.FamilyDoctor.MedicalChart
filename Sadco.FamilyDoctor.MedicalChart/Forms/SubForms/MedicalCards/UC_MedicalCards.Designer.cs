namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    partial class UC_MedicalCards
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlLPatientName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctrl_TMedicalCards = new System.Windows.Forms.DataGridView();
            this.p_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_DateCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_PatientFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPRecordInfo = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlMCInfoValCreateDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlMCInfoValNumber = new System.Windows.Forms.Label();
            this.ctrlMCInfoArchiveDate = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.ctrlMCInfoValArchiveDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ctrlMCInfoValPatient = new System.Windows.Forms.Label();
            this.ctrlMCInfoDeleteDate = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrlMCInfoValDeleteDate = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.ctrlMCInfoValComment = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TMedicalCards)).BeginInit();
            this.ctrlPRecordInfo.SuspendLayout();
            this.panel4.SuspendLayout();
            this.ctrlMCInfoArchiveDate.SuspendLayout();
            this.ctrlMCInfoDeleteDate.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ctrlLPatientName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1828, 39);
            this.panel1.TabIndex = 90;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1828, 0);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1828, 371);
            this.panel2.TabIndex = 89;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ctrl_TMedicalCards);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ctrlPRecordInfo);
            this.splitContainer1.Size = new System.Drawing.Size(1828, 371);
            this.splitContainer1.SplitterDistance = 695;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 93;
            // 
            // ctrl_TMedicalCards
            // 
            this.ctrl_TMedicalCards.AllowUserToAddRows = false;
            this.ctrl_TMedicalCards.AllowUserToResizeRows = false;
            this.ctrl_TMedicalCards.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ctrl_TMedicalCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrl_TMedicalCards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.p_Number,
            this.p_DateCreate,
            this.p_PatientFIO,
            this.p_Comment});
            this.ctrl_TMedicalCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_TMedicalCards.Location = new System.Drawing.Point(0, 0);
            this.ctrl_TMedicalCards.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrl_TMedicalCards.MultiSelect = false;
            this.ctrl_TMedicalCards.Name = "ctrl_TMedicalCards";
            this.ctrl_TMedicalCards.ReadOnly = true;
            this.ctrl_TMedicalCards.RowHeadersVisible = false;
            this.ctrl_TMedicalCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrl_TMedicalCards.ShowCellErrors = false;
            this.ctrl_TMedicalCards.ShowCellToolTips = false;
            this.ctrl_TMedicalCards.ShowEditingIcon = false;
            this.ctrl_TMedicalCards.ShowRowErrors = false;
            this.ctrl_TMedicalCards.Size = new System.Drawing.Size(695, 371);
            this.ctrl_TMedicalCards.TabIndex = 88;
            this.ctrl_TMedicalCards.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrl_TMedicalCards_CellClick);
            // 
            // p_Number
            // 
            this.p_Number.DataPropertyName = "p_Number";
            this.p_Number.HeaderText = "№ медкарты";
            this.p_Number.Name = "p_Number";
            this.p_Number.ReadOnly = true;
            this.p_Number.Width = 110;
            // 
            // p_DateCreate
            // 
            this.p_DateCreate.DataPropertyName = "p_DateCreate";
            this.p_DateCreate.HeaderText = "Дата создания";
            this.p_DateCreate.Name = "p_DateCreate";
            this.p_DateCreate.ReadOnly = true;
            this.p_DateCreate.Width = 135;
            // 
            // p_PatientFIO
            // 
            this.p_PatientFIO.DataPropertyName = "p_PatientFIO";
            this.p_PatientFIO.HeaderText = "Пациент";
            this.p_PatientFIO.Name = "p_PatientFIO";
            this.p_PatientFIO.ReadOnly = true;
            this.p_PatientFIO.Width = 200;
            // 
            // p_Comment
            // 
            this.p_Comment.DataPropertyName = "p_Comment";
            this.p_Comment.HeaderText = "Комментарий";
            this.p_Comment.Name = "p_Comment";
            this.p_Comment.ReadOnly = true;
            this.p_Comment.Width = 245;
            // 
            // ctrlPRecordInfo
            // 
            this.ctrlPRecordInfo.Controls.Add(this.panel5);
            this.ctrlPRecordInfo.Controls.Add(this.ctrlMCInfoDeleteDate);
            this.ctrlPRecordInfo.Controls.Add(this.ctrlMCInfoArchiveDate);
            this.ctrlPRecordInfo.Controls.Add(this.panel4);
            this.ctrlPRecordInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPRecordInfo.Location = new System.Drawing.Point(0, 0);
            this.ctrlPRecordInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlPRecordInfo.Name = "ctrlPRecordInfo";
            this.ctrlPRecordInfo.Size = new System.Drawing.Size(1128, 371);
            this.ctrlPRecordInfo.TabIndex = 2;
            this.ctrlPRecordInfo.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.ctrlMCInfoValPatient);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.ctrlMCInfoValCreateDate);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.ctrlMCInfoValNumber);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1128, 70);
            this.panel4.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Дата создания:";
            // 
            // ctrlMCInfoValCreateDate
            // 
            this.ctrlMCInfoValCreateDate.AutoSize = true;
            this.ctrlMCInfoValCreateDate.Location = new System.Drawing.Point(166, 26);
            this.ctrlMCInfoValCreateDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ctrlMCInfoValCreateDate.Name = "ctrlMCInfoValCreateDate";
            this.ctrlMCInfoValCreateDate.Size = new System.Drawing.Size(167, 13);
            this.ctrlMCInfoValCreateDate.TabIndex = 6;
            this.ctrlMCInfoValCreateDate.Text = "ctrlMCInfoValCreateDate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Номер медкарты:";
            // 
            // ctrlMCInfoValNumber
            // 
            this.ctrlMCInfoValNumber.AutoSize = true;
            this.ctrlMCInfoValNumber.Location = new System.Drawing.Point(166, 0);
            this.ctrlMCInfoValNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ctrlMCInfoValNumber.Name = "ctrlMCInfoValNumber";
            this.ctrlMCInfoValNumber.Size = new System.Drawing.Size(145, 13);
            this.ctrlMCInfoValNumber.TabIndex = 4;
            this.ctrlMCInfoValNumber.Text = "ctrlMCInfoValNumber";
            // 
            // ctrlMCInfoArchiveDate
            // 
            this.ctrlMCInfoArchiveDate.Controls.Add(this.label4);
            this.ctrlMCInfoArchiveDate.Controls.Add(this.ctrlMCInfoValArchiveDate);
            this.ctrlMCInfoArchiveDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlMCInfoArchiveDate.Location = new System.Drawing.Point(0, 70);
            this.ctrlMCInfoArchiveDate.Name = "ctrlMCInfoArchiveDate";
            this.ctrlMCInfoArchiveDate.Size = new System.Drawing.Size(1128, 28);
            this.ctrlMCInfoArchiveDate.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Дата архивирования:";
            // 
            // ctrlMCInfoValArchiveDate
            // 
            this.ctrlMCInfoValArchiveDate.AutoSize = true;
            this.ctrlMCInfoValArchiveDate.Location = new System.Drawing.Point(166, 11);
            this.ctrlMCInfoValArchiveDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ctrlMCInfoValArchiveDate.Name = "ctrlMCInfoValArchiveDate";
            this.ctrlMCInfoValArchiveDate.Size = new System.Drawing.Size(174, 13);
            this.ctrlMCInfoValArchiveDate.TabIndex = 6;
            this.ctrlMCInfoValArchiveDate.Text = "ctrlMCInfoValArchiveDate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Пациент:";
            // 
            // ctrlMCInfoValPatient
            // 
            this.ctrlMCInfoValPatient.AutoSize = true;
            this.ctrlMCInfoValPatient.Location = new System.Drawing.Point(166, 53);
            this.ctrlMCInfoValPatient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ctrlMCInfoValPatient.Name = "ctrlMCInfoValPatient";
            this.ctrlMCInfoValPatient.Size = new System.Drawing.Size(140, 13);
            this.ctrlMCInfoValPatient.TabIndex = 8;
            this.ctrlMCInfoValPatient.Text = "ctrlMCInfoValPatient";
            // 
            // ctrlMCInfoDeleteDate
            // 
            this.ctrlMCInfoDeleteDate.Controls.Add(this.label6);
            this.ctrlMCInfoDeleteDate.Controls.Add(this.ctrlMCInfoValDeleteDate);
            this.ctrlMCInfoDeleteDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlMCInfoDeleteDate.Location = new System.Drawing.Point(0, 98);
            this.ctrlMCInfoDeleteDate.Name = "ctrlMCInfoDeleteDate";
            this.ctrlMCInfoDeleteDate.Size = new System.Drawing.Size(1128, 28);
            this.ctrlMCInfoDeleteDate.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Дата удаления:";
            // 
            // ctrlMCInfoValDeleteDate
            // 
            this.ctrlMCInfoValDeleteDate.AutoSize = true;
            this.ctrlMCInfoValDeleteDate.Location = new System.Drawing.Point(166, 11);
            this.ctrlMCInfoValDeleteDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ctrlMCInfoValDeleteDate.Name = "ctrlMCInfoValDeleteDate";
            this.ctrlMCInfoValDeleteDate.Size = new System.Drawing.Size(166, 13);
            this.ctrlMCInfoValDeleteDate.TabIndex = 6;
            this.ctrlMCInfoValDeleteDate.Text = "ctrlMCInfoValDeleteDate";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.ctrlMCInfoValComment);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 126);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1128, 28);
            this.panel5.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Комментарий:";
            // 
            // ctrlMCInfoValComment
            // 
            this.ctrlMCInfoValComment.AutoSize = true;
            this.ctrlMCInfoValComment.Location = new System.Drawing.Point(166, 11);
            this.ctrlMCInfoValComment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ctrlMCInfoValComment.Name = "ctrlMCInfoValComment";
            this.ctrlMCInfoValComment.Size = new System.Drawing.Size(155, 13);
            this.ctrlMCInfoValComment.TabIndex = 6;
            this.ctrlMCInfoValComment.Text = "ctrlMCInfoValComment";
            // 
            // UC_MedicalCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "UC_MedicalCards";
            this.Size = new System.Drawing.Size(1828, 410);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TMedicalCards)).EndInit();
            this.ctrlPRecordInfo.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ctrlMCInfoArchiveDate.ResumeLayout(false);
            this.ctrlMCInfoArchiveDate.PerformLayout();
            this.ctrlMCInfoDeleteDate.ResumeLayout(false);
            this.ctrlMCInfoDeleteDate.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ctrlLPatientName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView ctrl_TMedicalCards;
        private System.Windows.Forms.Panel ctrlPRecordInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_DateCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_PatientFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Comment;
        private System.Windows.Forms.Panel ctrlMCInfoArchiveDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ctrlMCInfoValArchiveDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ctrlMCInfoValCreateDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ctrlMCInfoValNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ctrlMCInfoValPatient;
        private System.Windows.Forms.Panel ctrlMCInfoDeleteDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ctrlMCInfoValDeleteDate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label ctrlMCInfoValComment;
    }
}
