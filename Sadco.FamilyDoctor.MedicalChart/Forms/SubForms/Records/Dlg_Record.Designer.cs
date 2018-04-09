namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class Dlg_Record
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
            this.ctrl_CMTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrl_MIGroupNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MITemplateDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlPContent = new System.Windows.Forms.Panel();
            this.ctrl_CMTemplate.SuspendLayout();
            this.SuspendLayout();
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
            // ctrlPContent
            // 
            this.ctrlPContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPContent.Location = new System.Drawing.Point(0, 0);
            this.ctrlPContent.Name = "ctrlPContent";
            this.ctrlPContent.Size = new System.Drawing.Size(550, 507);
            this.ctrlPContent.TabIndex = 1;
            // 
            // Dlg_Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(550, 507);
            this.Controls.Add(this.ctrlPContent);
            this.Name = "Dlg_Record";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ctrl_CMTemplate.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip ctrl_CMTemplate;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupEdit;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateEdit;
        private System.Windows.Forms.Panel ctrlPContent;
    }
}
