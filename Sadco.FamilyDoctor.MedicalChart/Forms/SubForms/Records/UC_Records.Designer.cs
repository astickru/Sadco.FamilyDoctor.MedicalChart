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
            this.ctrl_CMTreeElements = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrl_MIGroupNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIControlNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIControlDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_TPartNormRangeValues = new System.Windows.Forms.DataGridView();
            this.p_AgeFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_AgeTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_MaleMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlLPatientName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrlBReportAdd = new System.Windows.Forms.Button();
            this.ctrl_CMTreeElements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TPartNormRangeValues)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrl_CMTreeElements
            // 
            this.ctrl_CMTreeElements.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_MIGroupNew,
            this.ctrl_MIGroupEdit,
            this.ctrl_MIGroupDelete,
            this.ctrl_MIControlNew,
            this.ctrl_MIControlDelete});
            this.ctrl_CMTreeElements.Name = "ctrl_CMTemplate";
            this.ctrl_CMTreeElements.Size = new System.Drawing.Size(176, 114);
            // 
            // ctrl_MIGroupNew
            // 
            this.ctrl_MIGroupNew.Name = "ctrl_MIGroupNew";
            this.ctrl_MIGroupNew.Size = new System.Drawing.Size(175, 22);
            this.ctrl_MIGroupNew.Tag = "MI_GroupNew";
            this.ctrl_MIGroupNew.Text = "Добавить группу";
            // 
            // ctrl_MIGroupEdit
            // 
            this.ctrl_MIGroupEdit.Name = "ctrl_MIGroupEdit";
            this.ctrl_MIGroupEdit.Size = new System.Drawing.Size(175, 22);
            this.ctrl_MIGroupEdit.Tag = "MI_GroupEdit";
            this.ctrl_MIGroupEdit.Text = "Изменить группу";
            // 
            // ctrl_MIGroupDelete
            // 
            this.ctrl_MIGroupDelete.Name = "ctrl_MIGroupDelete";
            this.ctrl_MIGroupDelete.Size = new System.Drawing.Size(175, 22);
            this.ctrl_MIGroupDelete.Tag = "MI_GroupDelete";
            this.ctrl_MIGroupDelete.Text = "Удалить группу";
            // 
            // ctrl_MIControlNew
            // 
            this.ctrl_MIControlNew.Name = "ctrl_MIControlNew";
            this.ctrl_MIControlNew.Size = new System.Drawing.Size(175, 22);
            this.ctrl_MIControlNew.Tag = "MI_ElementNew";
            this.ctrl_MIControlNew.Text = "Добавить элемент";
            // 
            // ctrl_MIControlDelete
            // 
            this.ctrl_MIControlDelete.Name = "ctrl_MIControlDelete";
            this.ctrl_MIControlDelete.Size = new System.Drawing.Size(175, 22);
            this.ctrl_MIControlDelete.Tag = "MI_ElementDelete";
            this.ctrl_MIControlDelete.Text = "Удалить элемент";
            // 
            // ctrl_TPartNormRangeValues
            // 
            this.ctrl_TPartNormRangeValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrl_TPartNormRangeValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.p_AgeFrom,
            this.p_AgeTo,
            this.p_MaleMin});
            this.ctrl_TPartNormRangeValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_TPartNormRangeValues.Location = new System.Drawing.Point(0, 39);
            this.ctrl_TPartNormRangeValues.Name = "ctrl_TPartNormRangeValues";
            this.ctrl_TPartNormRangeValues.Size = new System.Drawing.Size(705, 305);
            this.ctrl_TPartNormRangeValues.TabIndex = 88;
            // 
            // p_AgeFrom
            // 
            this.p_AgeFrom.HeaderText = "Время";
            this.p_AgeFrom.Name = "p_AgeFrom";
            this.p_AgeFrom.Width = 110;
            // 
            // p_AgeTo
            // 
            this.p_AgeTo.HeaderText = "Специалист";
            this.p_AgeTo.Name = "p_AgeTo";
            this.p_AgeTo.Width = 150;
            // 
            // p_MaleMin
            // 
            this.p_MaleMin.HeaderText = "Описание";
            this.p_MaleMin.Name = "p_MaleMin";
            this.p_MaleMin.Width = 400;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ctrlLPatientName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 39);
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
            this.panel2.Size = new System.Drawing.Size(705, 38);
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
            // UC_Records
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ctrl_TPartNormRangeValues);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximumSize = new System.Drawing.Size(1600, 1000);
            this.Name = "UC_Records";
            this.Size = new System.Drawing.Size(705, 344);
            this.ctrl_CMTreeElements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TPartNormRangeValues)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip ctrl_CMTreeElements;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIControlNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIControlDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupEdit;
        private System.Windows.Forms.DataGridView ctrl_TPartNormRangeValues;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ctrlLPatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_AgeFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_AgeTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_MaleMin;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ctrlBReportAdd;
    }
}
