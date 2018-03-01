namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class UC_EditorElements
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
			this.ctrl_TVTemplates = new System.Windows.Forms.TreeView();
			this.ctrl_CMTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ctrl_MIGroupNew = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MIGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MIGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MIControlNew = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MIControlDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_P_ElementProperty = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.ctrl_CMTemplate.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ctrl_TVTemplates);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AccessibleName = "";
			this.splitContainer1.Panel2.Controls.Add(this.ctrl_P_ElementProperty);
			this.splitContainer1.Size = new System.Drawing.Size(600, 300);
			this.splitContainer1.SplitterDistance = 180;
			this.splitContainer1.TabIndex = 2;
			// 
			// ctrl_TVTemplates
			// 
			this.ctrl_TVTemplates.AllowDrop = true;
			this.ctrl_TVTemplates.ContextMenuStrip = this.ctrl_CMTemplate;
			this.ctrl_TVTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_TVTemplates.Location = new System.Drawing.Point(0, 0);
			this.ctrl_TVTemplates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.ctrl_TVTemplates.Name = "ctrl_TVTemplates";
			this.ctrl_TVTemplates.Size = new System.Drawing.Size(180, 300);
			this.ctrl_TVTemplates.TabIndex = 0;
			this.ctrl_TVTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ctrl_TVTemplates_AfterSelect);
			// 
			// ctrl_CMTemplate
			// 
			this.ctrl_CMTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_MIGroupNew,
            this.ctrl_MIGroupEdit,
            this.ctrl_MIGroupDelete,
            this.ctrl_MIControlNew,
            this.ctrl_MIControlDelete});
			this.ctrl_CMTemplate.Name = "ctrl_CMTemplate";
			this.ctrl_CMTemplate.Size = new System.Drawing.Size(176, 114);
			this.ctrl_CMTemplate.Opening += new System.ComponentModel.CancelEventHandler(this.Ctrl_CMTemplate_Opening);
			// 
			// ctrl_MIGroupNew
			// 
			this.ctrl_MIGroupNew.Name = "ctrl_MIGroupNew";
			this.ctrl_MIGroupNew.Size = new System.Drawing.Size(175, 22);
			this.ctrl_MIGroupNew.Tag = "MI_GroupNew";
			this.ctrl_MIGroupNew.Text = "Добавить группу";
			this.ctrl_MIGroupNew.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_MIGroupEdit
			// 
			this.ctrl_MIGroupEdit.Name = "ctrl_MIGroupEdit";
			this.ctrl_MIGroupEdit.Size = new System.Drawing.Size(175, 22);
			this.ctrl_MIGroupEdit.Tag = "MI_GroupEdit";
			this.ctrl_MIGroupEdit.Text = "Изменить группу";
			this.ctrl_MIGroupEdit.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_MIGroupDelete
			// 
			this.ctrl_MIGroupDelete.Name = "ctrl_MIGroupDelete";
			this.ctrl_MIGroupDelete.Size = new System.Drawing.Size(175, 22);
			this.ctrl_MIGroupDelete.Tag = "MI_GroupDelete";
			this.ctrl_MIGroupDelete.Text = "Удалить группу";
			this.ctrl_MIGroupDelete.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_MIControlNew
			// 
			this.ctrl_MIControlNew.Name = "ctrl_MIControlNew";
			this.ctrl_MIControlNew.Size = new System.Drawing.Size(175, 22);
			this.ctrl_MIControlNew.Tag = "MI_ControlNew";
			this.ctrl_MIControlNew.Text = "Добавить элемент";
			this.ctrl_MIControlNew.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_MIControlDelete
			// 
			this.ctrl_MIControlDelete.Name = "ctrl_MIControlDelete";
			this.ctrl_MIControlDelete.Size = new System.Drawing.Size(175, 22);
			this.ctrl_MIControlDelete.Tag = "MI_ControlDelete";
			this.ctrl_MIControlDelete.Text = "Удалить элемент";
			this.ctrl_MIControlDelete.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_P_ElementProperty
			// 
			this.ctrl_P_ElementProperty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_P_ElementProperty.Location = new System.Drawing.Point(0, 0);
			this.ctrl_P_ElementProperty.Name = "ctrl_P_ElementProperty";
			this.ctrl_P_ElementProperty.Size = new System.Drawing.Size(416, 300);
			this.ctrl_P_ElementProperty.TabIndex = 0;
			// 
			// UC_EditorElements
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "UC_EditorElements";
			this.Size = new System.Drawing.Size(600, 300);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ctrl_CMTemplate.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView ctrl_TVTemplates;
		private System.Windows.Forms.ContextMenuStrip ctrl_CMTemplate;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIControlNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIControlDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupEdit;
		private System.Windows.Forms.Panel ctrl_P_ElementProperty;
	}
}
