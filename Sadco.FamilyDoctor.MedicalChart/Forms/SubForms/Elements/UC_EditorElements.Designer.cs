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
            this.ctrl_TreeElements = new Sadco.FamilyDoctor.Core.Controls.Ctrl_TreeElements();
            this.ctrl_P_ElementProperty = new System.Windows.Forms.Panel();
            this.ctrl_CMTreeElements = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrl_MIGroupNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIControlNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_MIControlDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctrl_CMTreeElements.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.ctrl_TreeElements);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleName = "";
            this.splitContainer1.Panel2.Controls.Add(this.ctrl_P_ElementProperty);
            this.splitContainer1.Size = new System.Drawing.Size(328, 191);
            this.splitContainer1.SplitterDistance = 104;
            this.splitContainer1.TabIndex = 2;
            // 
            // ctrl_TreeElements
            // 
            this.ctrl_TreeElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_TreeElements.ImageKey = "folder";
            this.ctrl_TreeElements.Location = new System.Drawing.Point(0, 0);
            this.ctrl_TreeElements.Name = "ctrl_TreeElements";
            this.ctrl_TreeElements.SelectedImageKey = "folder";
            this.ctrl_TreeElements.Size = new System.Drawing.Size(104, 191);
            this.ctrl_TreeElements.TabIndex = 0;
            // 
            // ctrl_P_ElementProperty
            // 
            this.ctrl_P_ElementProperty.AutoScroll = true;
            this.ctrl_P_ElementProperty.AutoSize = true;
            this.ctrl_P_ElementProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_P_ElementProperty.Location = new System.Drawing.Point(0, 0);
            this.ctrl_P_ElementProperty.Name = "ctrl_P_ElementProperty";
            this.ctrl_P_ElementProperty.Size = new System.Drawing.Size(220, 191);
            this.ctrl_P_ElementProperty.TabIndex = 0;
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
            // UC_EditorElements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UC_EditorElements";
            this.Size = new System.Drawing.Size(328, 191);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ctrl_CMTreeElements.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ContextMenuStrip ctrl_CMTreeElements;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIControlNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIControlDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupEdit;
		private System.Windows.Forms.Panel ctrl_P_ElementProperty;
        private Core.Controls.Ctrl_TreeElements ctrl_TreeElements;
    }
}
