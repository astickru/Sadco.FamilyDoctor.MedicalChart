namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class UC_EditorTemplates
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctrl_CMTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrl_MIGroupNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.P_ = new System.Windows.Forms.Panel();
            this.ctrl_LVTemplates = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctrl_TreeTemplates = new Sadco.FamilyDoctor.Core.Controls.Ctrl_TreeTemplates();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctrl_CMTemplate.SuspendLayout();
            this.P_.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ctrl_TreeTemplates);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.P_);
            this.splitContainer1.Size = new System.Drawing.Size(692, 329);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.TabIndex = 2;
            // 
            // ctrl_CMTemplate
            // 
            this.ctrl_CMTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_MIGroupNew,
            this.ctrl_MIGroupEdit,
            this.ctrl_MIGroupDelete,
            this.ctrl_MITemplateNew,
            this.ctrl_MITemplateEdit,
            this.ctrl_MITemplateDelete});
            this.ctrl_CMTemplate.Name = "ctrl_CMTemplate";
            this.ctrl_CMTemplate.Size = new System.Drawing.Size(177, 136);
            // 
            // ctrl_MIGroupNew
            // 
            this.ctrl_MIGroupNew.Name = "ctrl_MIGroupNew";
            this.ctrl_MIGroupNew.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MIGroupNew.Tag = "MI_GroupNew";
            this.ctrl_MIGroupNew.Text = "Добавить группу";
            // 
            // ctrl_MIGroupEdit
            // 
            this.ctrl_MIGroupEdit.Name = "ctrl_MIGroupEdit";
            this.ctrl_MIGroupEdit.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MIGroupEdit.Tag = "MI_GroupEdit";
            this.ctrl_MIGroupEdit.Text = "Изменить группу";
            // 
            // ctrl_MIGroupDelete
            // 
            this.ctrl_MIGroupDelete.Name = "ctrl_MIGroupDelete";
            this.ctrl_MIGroupDelete.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MIGroupDelete.Tag = "MI_GroupDelete";
            this.ctrl_MIGroupDelete.Text = "Удалить группу";
            // 
            // ctrl_MITemplateNew
            // 
            this.ctrl_MITemplateNew.Name = "ctrl_MITemplateNew";
            this.ctrl_MITemplateNew.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MITemplateNew.Tag = "MI_TemplateNew";
            this.ctrl_MITemplateNew.Text = "Добавить шаблон";
            // 
            // ctrl_MITemplateEdit
            // 
            this.ctrl_MITemplateEdit.Name = "ctrl_MITemplateEdit";
            this.ctrl_MITemplateEdit.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MITemplateEdit.Tag = "MI_TemplateEdit";
            this.ctrl_MITemplateEdit.Text = "Изменить шаблон";
            // 
            // ctrl_MITemplateDelete
            // 
            this.ctrl_MITemplateDelete.Name = "ctrl_MITemplateDelete";
            this.ctrl_MITemplateDelete.Size = new System.Drawing.Size(176, 22);
            this.ctrl_MITemplateDelete.Tag = "MI_TemplateDelete";
            this.ctrl_MITemplateDelete.Text = "Удалить шаблон";
            // 
            // P_
            // 
            this.P_.Controls.Add(this.ctrl_LVTemplates);
            this.P_.Dock = System.Windows.Forms.DockStyle.Fill;
            this.P_.Location = new System.Drawing.Point(0, 0);
            this.P_.Margin = new System.Windows.Forms.Padding(0);
            this.P_.Name = "P_";
            this.P_.Size = new System.Drawing.Size(481, 329);
            this.P_.TabIndex = 0;
            // 
            // ctrl_LVTemplates
            // 
            this.ctrl_LVTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chDesc});
            this.ctrl_LVTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_LVTemplates.FullRowSelect = true;
            this.ctrl_LVTemplates.HideSelection = false;
            this.ctrl_LVTemplates.Location = new System.Drawing.Point(0, 0);
            this.ctrl_LVTemplates.MultiSelect = false;
            this.ctrl_LVTemplates.Name = "ctrl_LVTemplates";
            this.ctrl_LVTemplates.Size = new System.Drawing.Size(481, 329);
            this.ctrl_LVTemplates.TabIndex = 7;
            this.ctrl_LVTemplates.UseCompatibleStateImageBehavior = false;
            this.ctrl_LVTemplates.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Наименование";
            this.chName.Width = 200;
            // 
            // chDesc
            // 
            this.chDesc.Text = "Описание";
            this.chDesc.Width = 210;
            // 
            // ctrl_TreeTemplates
            // 
            this.ctrl_TreeTemplates.AllowDrop = true;
            this.ctrl_TreeTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_TreeTemplates.ImageKey = "FOLDER_16";
            this.ctrl_TreeTemplates.Location = new System.Drawing.Point(0, 0);
            this.ctrl_TreeTemplates.Name = "ctrl_TreeTemplates";
            this.ctrl_TreeTemplates.SelectedImageKey = "FOLDER_16";
            this.ctrl_TreeTemplates.Size = new System.Drawing.Size(207, 329);
            this.ctrl_TreeTemplates.TabIndex = 0;
            // 
            // UC_EditorTemplates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UC_EditorTemplates";
            this.Size = new System.Drawing.Size(692, 329);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ctrl_CMTemplate.ResumeLayout(false);
            this.P_.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ContextMenuStrip ctrl_CMTemplate;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateDelete;
		private System.Windows.Forms.Panel P_;
		private System.Windows.Forms.ListView ctrl_LVTemplates;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ColumnHeader chDesc;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupEdit;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateEdit;
        private Core.Controls.Ctrl_TreeTemplates ctrl_TreeTemplates;
    }
}
