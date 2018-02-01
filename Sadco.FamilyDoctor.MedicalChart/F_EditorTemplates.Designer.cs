namespace Sadco.FamilyDoctor.MedicalChart {
	partial class F_EditorTemplates {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.ctrlMIMenu = new System.Windows.Forms.MenuStrip();
			this.ctrlMIFile = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrlMISave = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrlMIExit = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrlMIInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ctrl_TVTemplates = new System.Windows.Forms.TreeView();
			this.ctrl_CMTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ctrl_MIGroupNew = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MIGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MITemplateNew = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MITemplateDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_PSearch = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ctrl_TBSearch = new System.Windows.Forms.TextBox();
			this.ctrl_BSearch = new System.Windows.Forms.Button();
			this.ctrl_LVTemplates = new System.Windows.Forms.ListView();
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel2 = new System.Windows.Forms.Panel();
			this.ctrl_BTemplateEdit = new System.Windows.Forms.Button();
			this.ctrl_BTemplateDelete = new System.Windows.Forms.Button();
			this.ctrl_BTemplateAdd = new System.Windows.Forms.Button();
			this.ctrl_TemplateTitle = new System.Windows.Forms.Label();
			this.ctrlMIMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.ctrl_CMTemplate.SuspendLayout();
			this.ctrl_PSearch.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctrlMIMenu
			// 
			this.ctrlMIMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMIFile,
            this.ctrlMIInfo});
			this.ctrlMIMenu.Location = new System.Drawing.Point(0, 0);
			this.ctrlMIMenu.Name = "ctrlMIMenu";
			this.ctrlMIMenu.Size = new System.Drawing.Size(796, 24);
			this.ctrlMIMenu.TabIndex = 0;
			this.ctrlMIMenu.Text = "Menu";
			// 
			// ctrlMIFile
			// 
			this.ctrlMIFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMISave,
            this.ctrlMIExit});
			this.ctrlMIFile.Name = "ctrlMIFile";
			this.ctrlMIFile.Size = new System.Drawing.Size(48, 20);
			this.ctrlMIFile.Text = "Файл";
			// 
			// ctrlMISave
			// 
			this.ctrlMISave.Name = "ctrlMISave";
			this.ctrlMISave.Size = new System.Drawing.Size(132, 22);
			this.ctrlMISave.Text = "Сохранить";
			// 
			// ctrlMIExit
			// 
			this.ctrlMIExit.Name = "ctrlMIExit";
			this.ctrlMIExit.Size = new System.Drawing.Size(132, 22);
			this.ctrlMIExit.Text = "Выход";
			// 
			// ctrlMIInfo
			// 
			this.ctrlMIInfo.Name = "ctrlMIInfo";
			this.ctrlMIInfo.Size = new System.Drawing.Size(65, 20);
			this.ctrlMIInfo.Text = "Справка";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ctrl_TVTemplates);
			this.splitContainer1.Panel1.Controls.Add(this.ctrl_PSearch);
			this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ctrl_LVTemplates);
			this.splitContainer1.Panel2.Controls.Add(this.panel2);
			this.splitContainer1.Panel2.Controls.Add(this.ctrl_TemplateTitle);
			this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(15);
			this.splitContainer1.Size = new System.Drawing.Size(796, 320);
			this.splitContainer1.SplitterDistance = 265;
			this.splitContainer1.TabIndex = 1;
			// 
			// ctrl_TVTemplates
			// 
			this.ctrl_TVTemplates.AllowDrop = true;
			this.ctrl_TVTemplates.ContextMenuStrip = this.ctrl_CMTemplate;
			this.ctrl_TVTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_TVTemplates.Location = new System.Drawing.Point(10, 41);
			this.ctrl_TVTemplates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.ctrl_TVTemplates.Name = "ctrl_TVTemplates";
			this.ctrl_TVTemplates.Size = new System.Drawing.Size(245, 269);
			this.ctrl_TVTemplates.TabIndex = 3;
			this.ctrl_TVTemplates.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ctrl_TVTemplates_ItemDrag);
			this.ctrl_TVTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ctrl_TVTemplates_AfterSelect);
			this.ctrl_TVTemplates.DragDrop += new System.Windows.Forms.DragEventHandler(this.ctrl_TVTemplates_DragDrop);
			this.ctrl_TVTemplates.DragEnter += new System.Windows.Forms.DragEventHandler(this.ctrl_TVTemplates_DragEnter);
			this.ctrl_TVTemplates.DragOver += new System.Windows.Forms.DragEventHandler(this.ctrl_TVTemplates_DragOver);
			// 
			// ctrl_CMTemplate
			// 
			this.ctrl_CMTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_MIGroupNew,
            this.ctrl_MIGroupDelete,
            this.ctrl_MITemplateNew,
            this.ctrl_MITemplateDelete});
			this.ctrl_CMTemplate.Name = "ctrl_CMTemplate";
			this.ctrl_CMTemplate.Size = new System.Drawing.Size(175, 92);
			this.ctrl_CMTemplate.Opening += new System.ComponentModel.CancelEventHandler(this.Ctrl_CMTemplate_Opening);
			// 
			// ctrl_MIGroupNew
			// 
			this.ctrl_MIGroupNew.Name = "ctrl_MIGroupNew";
			this.ctrl_MIGroupNew.Size = new System.Drawing.Size(174, 22);
			this.ctrl_MIGroupNew.Tag = "MI_GroupNew";
			this.ctrl_MIGroupNew.Text = "Добавить группу";
			this.ctrl_MIGroupNew.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_MIGroupDelete
			// 
			this.ctrl_MIGroupDelete.Name = "ctrl_MIGroupDelete";
			this.ctrl_MIGroupDelete.Size = new System.Drawing.Size(174, 22);
			this.ctrl_MIGroupDelete.Tag = "MI_GroupDelete";
			this.ctrl_MIGroupDelete.Text = "Удалить группу";
			this.ctrl_MIGroupDelete.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_MITemplateNew
			// 
			this.ctrl_MITemplateNew.Name = "ctrl_MITemplateNew";
			this.ctrl_MITemplateNew.Size = new System.Drawing.Size(174, 22);
			this.ctrl_MITemplateNew.Tag = "MI_TemplateNew";
			this.ctrl_MITemplateNew.Text = "Добавить шаблон";
			this.ctrl_MITemplateNew.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_MITemplateDelete
			// 
			this.ctrl_MITemplateDelete.Name = "ctrl_MITemplateDelete";
			this.ctrl_MITemplateDelete.Size = new System.Drawing.Size(174, 22);
			this.ctrl_MITemplateDelete.Tag = "MI_TemplateDelete";
			this.ctrl_MITemplateDelete.Text = "Удалить шаблон";
			this.ctrl_MITemplateDelete.Click += new System.EventHandler(this.ctrl_MITemplates_Click);
			// 
			// ctrl_PSearch
			// 
			this.ctrl_PSearch.Controls.Add(this.panel1);
			this.ctrl_PSearch.Controls.Add(this.ctrl_BSearch);
			this.ctrl_PSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctrl_PSearch.Location = new System.Drawing.Point(10, 10);
			this.ctrl_PSearch.Name = "ctrl_PSearch";
			this.ctrl_PSearch.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.ctrl_PSearch.Size = new System.Drawing.Size(245, 31);
			this.ctrl_PSearch.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ctrl_TBSearch);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.panel1.Size = new System.Drawing.Size(176, 21);
			this.panel1.TabIndex = 4;
			// 
			// ctrl_TBSearch
			// 
			this.ctrl_TBSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_TBSearch.Location = new System.Drawing.Point(0, 0);
			this.ctrl_TBSearch.Name = "ctrl_TBSearch";
			this.ctrl_TBSearch.Size = new System.Drawing.Size(166, 20);
			this.ctrl_TBSearch.TabIndex = 3;
			// 
			// ctrl_BSearch
			// 
			this.ctrl_BSearch.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctrl_BSearch.Location = new System.Drawing.Point(176, 0);
			this.ctrl_BSearch.Name = "ctrl_BSearch";
			this.ctrl_BSearch.Size = new System.Drawing.Size(69, 21);
			this.ctrl_BSearch.TabIndex = 3;
			this.ctrl_BSearch.Text = "Поиск";
			this.ctrl_BSearch.UseVisualStyleBackColor = true;
			this.ctrl_BSearch.Click += new System.EventHandler(this.ctrl_BSearch_Click);
			// 
			// ctrl_LVTemplates
			// 
			this.ctrl_LVTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chDesc});
			this.ctrl_LVTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_LVTemplates.FullRowSelect = true;
			this.ctrl_LVTemplates.HideSelection = false;
			this.ctrl_LVTemplates.Location = new System.Drawing.Point(15, 38);
			this.ctrl_LVTemplates.MultiSelect = false;
			this.ctrl_LVTemplates.Name = "ctrl_LVTemplates";
			this.ctrl_LVTemplates.Size = new System.Drawing.Size(497, 222);
			this.ctrl_LVTemplates.TabIndex = 4;
			this.ctrl_LVTemplates.UseCompatibleStateImageBehavior = false;
			this.ctrl_LVTemplates.View = System.Windows.Forms.View.Details;
			this.ctrl_LVTemplates.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ctrl_LVTemplates_ItemSelectionChanged);
			this.ctrl_LVTemplates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ctrl_LVTemplates_MouseDoubleClick);
			// 
			// chName
			// 
			this.chName.Text = "Наименование";
			this.chName.Width = 300;
			// 
			// chDesc
			// 
			this.chDesc.Text = "Описание";
			this.chDesc.Width = 600;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ctrl_BTemplateEdit);
			this.panel2.Controls.Add(this.ctrl_BTemplateDelete);
			this.panel2.Controls.Add(this.ctrl_BTemplateAdd);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(15, 260);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(15);
			this.panel2.Size = new System.Drawing.Size(497, 45);
			this.panel2.TabIndex = 3;
			// 
			// ctrl_BTemplateEdit
			// 
			this.ctrl_BTemplateEdit.Location = new System.Drawing.Point(86, 11);
			this.ctrl_BTemplateEdit.Name = "ctrl_BTemplateEdit";
			this.ctrl_BTemplateEdit.Size = new System.Drawing.Size(75, 23);
			this.ctrl_BTemplateEdit.TabIndex = 4;
			this.ctrl_BTemplateEdit.Text = "Изменить";
			this.ctrl_BTemplateEdit.UseVisualStyleBackColor = true;
			this.ctrl_BTemplateEdit.Visible = false;
			this.ctrl_BTemplateEdit.Click += new System.EventHandler(this.ctrl_BTemplateEdit_Click);
			// 
			// ctrl_BTemplateDelete
			// 
			this.ctrl_BTemplateDelete.Location = new System.Drawing.Point(171, 11);
			this.ctrl_BTemplateDelete.Name = "ctrl_BTemplateDelete";
			this.ctrl_BTemplateDelete.Size = new System.Drawing.Size(75, 23);
			this.ctrl_BTemplateDelete.TabIndex = 3;
			this.ctrl_BTemplateDelete.Text = "Удалить";
			this.ctrl_BTemplateDelete.UseVisualStyleBackColor = true;
			this.ctrl_BTemplateDelete.Visible = false;
			this.ctrl_BTemplateDelete.Click += new System.EventHandler(this.ctrl_BTemplateDelete_Click);
			// 
			// ctrl_BTemplateAdd
			// 
			this.ctrl_BTemplateAdd.Location = new System.Drawing.Point(0, 11);
			this.ctrl_BTemplateAdd.Name = "ctrl_BTemplateAdd";
			this.ctrl_BTemplateAdd.Size = new System.Drawing.Size(75, 23);
			this.ctrl_BTemplateAdd.TabIndex = 2;
			this.ctrl_BTemplateAdd.Text = "Добавить";
			this.ctrl_BTemplateAdd.UseVisualStyleBackColor = true;
			this.ctrl_BTemplateAdd.Click += new System.EventHandler(this.ctrl_BTemplateAdd_Click);
			// 
			// ctrl_TemplateTitle
			// 
			this.ctrl_TemplateTitle.AutoSize = true;
			this.ctrl_TemplateTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctrl_TemplateTitle.Location = new System.Drawing.Point(15, 15);
			this.ctrl_TemplateTitle.Name = "ctrl_TemplateTitle";
			this.ctrl_TemplateTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.ctrl_TemplateTitle.Size = new System.Drawing.Size(108, 23);
			this.ctrl_TemplateTitle.TabIndex = 0;
			this.ctrl_TemplateTitle.Text = "Заголовок шаблона";
			// 
			// F_EditorTemplates
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(796, 344);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.ctrlMIMenu);
			this.MainMenuStrip = this.ctrlMIMenu;
			this.Name = "F_EditorTemplates";
			this.Text = "F_Main";
			this.ctrlMIMenu.ResumeLayout(false);
			this.ctrlMIMenu.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ctrl_CMTemplate.ResumeLayout(false);
			this.ctrl_PSearch.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip ctrlMIMenu;
		private System.Windows.Forms.ToolStripMenuItem ctrlMIFile;
		private System.Windows.Forms.ToolStripMenuItem ctrlMISave;
		private System.Windows.Forms.ToolStripMenuItem ctrlMIExit;
		private System.Windows.Forms.ToolStripMenuItem ctrlMIInfo;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel ctrl_PSearch;
		private System.Windows.Forms.Button ctrl_BSearch;
		private System.Windows.Forms.Label ctrl_TemplateTitle;
		private System.Windows.Forms.TreeView ctrl_TVTemplates;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox ctrl_TBSearch;
		private System.Windows.Forms.ContextMenuStrip ctrl_CMTemplate;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateNew;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ctrl_BTemplateAdd;
		private System.Windows.Forms.ListView ctrl_LVTemplates;
		private System.Windows.Forms.Button ctrl_BTemplateDelete;
		private System.Windows.Forms.Button ctrl_BTemplateEdit;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ColumnHeader chDesc;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MITemplateDelete;
	}
}