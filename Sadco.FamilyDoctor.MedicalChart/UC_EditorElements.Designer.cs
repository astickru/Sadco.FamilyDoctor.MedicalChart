namespace Sadco.FamilyDoctor.MedicalChart
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
			this.ctrl_MIGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MIControlNew = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_MIControlDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ctrl_PSearch = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ctrl_TBSearch = new System.Windows.Forms.TextBox();
			this.ctrl_BSearch = new System.Windows.Forms.Button();
			this.ctrl_Pnl_Properties = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panel3 = new System.Windows.Forms.Panel();
			this.ctrl_CB_IsSymmentry = new System.Windows.Forms.CheckBox();
			this.ctrl_TB_Symmetry1 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.ctrl_TB_Symmetry2 = new System.Windows.Forms.TextBox();
			this.ctrl_TB_Note = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.ctrl_CB_IsVisible = new System.Windows.Forms.CheckBox();
			this.ctrl_CB_IsEditing = new System.Windows.Forms.CheckBox();
			this.ctrl_CB_IsRequiredFIeld = new System.Windows.Forms.CheckBox();
			this.ctrl_TB_Hint = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.ctrl_TB_Name = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.ctrl_LVTemplates = new System.Windows.Forms.ListView();
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel4 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.ctrl_B_CBAddElement = new System.Windows.Forms.Button();
			this.ctrl_TB_TextItem = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ctrl_BTemplateEdit = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.ctrl_CMTemplate.SuspendLayout();
			this.ctrl_PSearch.SuspendLayout();
			this.panel1.SuspendLayout();
			this.ctrl_Pnl_Properties.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.ctrl_TVTemplates);
			this.splitContainer1.Panel1.Controls.Add(this.ctrl_PSearch);
			this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AccessibleName = "";
			this.splitContainer1.Panel2.Controls.Add(this.ctrl_Pnl_Properties);
			this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(7, 7, 11, 7);
			this.splitContainer1.Size = new System.Drawing.Size(796, 318);
			this.splitContainer1.SplitterDistance = 219;
			this.splitContainer1.TabIndex = 2;
			// 
			// ctrl_TVTemplates
			// 
			this.ctrl_TVTemplates.AllowDrop = true;
			this.ctrl_TVTemplates.ContextMenuStrip = this.ctrl_CMTemplate;
			this.ctrl_TVTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_TVTemplates.Location = new System.Drawing.Point(10, 41);
			this.ctrl_TVTemplates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.ctrl_TVTemplates.Name = "ctrl_TVTemplates";
			this.ctrl_TVTemplates.Size = new System.Drawing.Size(199, 267);
			this.ctrl_TVTemplates.TabIndex = 0;
			this.ctrl_TVTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ctrl_TVTemplates_AfterSelect);
			// 
			// ctrl_CMTemplate
			// 
			this.ctrl_CMTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_MIGroupNew,
            this.ctrl_MIGroupDelete,
            this.ctrl_MIControlNew,
            this.ctrl_MIControlDelete});
			this.ctrl_CMTemplate.Name = "ctrl_CMTemplate";
			this.ctrl_CMTemplate.Size = new System.Drawing.Size(176, 92);
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
			this.ctrl_MIControlNew.Tag = "MI_ControlNew";
			this.ctrl_MIControlNew.Text = "Добавить элемент";
			// 
			// ctrl_MIControlDelete
			// 
			this.ctrl_MIControlDelete.Name = "ctrl_MIControlDelete";
			this.ctrl_MIControlDelete.Size = new System.Drawing.Size(175, 22);
			this.ctrl_MIControlDelete.Tag = "MI_ControlDelete";
			this.ctrl_MIControlDelete.Text = "Удалить элемент";
			// 
			// ctrl_PSearch
			// 
			this.ctrl_PSearch.Controls.Add(this.panel1);
			this.ctrl_PSearch.Controls.Add(this.ctrl_BSearch);
			this.ctrl_PSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctrl_PSearch.Location = new System.Drawing.Point(10, 10);
			this.ctrl_PSearch.Name = "ctrl_PSearch";
			this.ctrl_PSearch.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.ctrl_PSearch.Size = new System.Drawing.Size(199, 31);
			this.ctrl_PSearch.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ctrl_TBSearch);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.panel1.Size = new System.Drawing.Size(130, 21);
			this.panel1.TabIndex = 4;
			// 
			// ctrl_TBSearch
			// 
			this.ctrl_TBSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_TBSearch.Location = new System.Drawing.Point(0, 0);
			this.ctrl_TBSearch.Name = "ctrl_TBSearch";
			this.ctrl_TBSearch.Size = new System.Drawing.Size(120, 20);
			this.ctrl_TBSearch.TabIndex = 3;
			// 
			// ctrl_BSearch
			// 
			this.ctrl_BSearch.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctrl_BSearch.Location = new System.Drawing.Point(130, 0);
			this.ctrl_BSearch.Name = "ctrl_BSearch";
			this.ctrl_BSearch.Size = new System.Drawing.Size(69, 21);
			this.ctrl_BSearch.TabIndex = 3;
			this.ctrl_BSearch.Text = "Поиск";
			this.ctrl_BSearch.UseVisualStyleBackColor = true;
			// 
			// ctrl_Pnl_Properties
			// 
			this.ctrl_Pnl_Properties.Controls.Add(this.tabControl1);
			this.ctrl_Pnl_Properties.Controls.Add(this.panel2);
			this.ctrl_Pnl_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_Pnl_Properties.Location = new System.Drawing.Point(7, 7);
			this.ctrl_Pnl_Properties.Name = "ctrl_Pnl_Properties";
			this.ctrl_Pnl_Properties.Size = new System.Drawing.Size(555, 304);
			this.ctrl_Pnl_Properties.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(555, 266);
			this.tabControl1.TabIndex = 17;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panel3);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(547, 240);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Основные";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.ctrl_CB_IsSymmentry);
			this.panel3.Controls.Add(this.ctrl_TB_Symmetry1);
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.ctrl_TB_Symmetry2);
			this.panel3.Controls.Add(this.ctrl_TB_Note);
			this.panel3.Controls.Add(this.label9);
			this.panel3.Controls.Add(this.ctrl_CB_IsVisible);
			this.panel3.Controls.Add(this.ctrl_CB_IsEditing);
			this.panel3.Controls.Add(this.ctrl_CB_IsRequiredFIeld);
			this.panel3.Controls.Add(this.ctrl_TB_Hint);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.ctrl_TB_Name);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 3);
			this.panel3.Margin = new System.Windows.Forms.Padding(2);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(541, 237);
			this.panel3.TabIndex = 18;
			// 
			// ctrl_CB_IsSymmentry
			// 
			this.ctrl_CB_IsSymmentry.AutoSize = true;
			this.ctrl_CB_IsSymmentry.Location = new System.Drawing.Point(6, 102);
			this.ctrl_CB_IsSymmentry.Name = "ctrl_CB_IsSymmentry";
			this.ctrl_CB_IsSymmentry.Size = new System.Drawing.Size(158, 17);
			this.ctrl_CB_IsSymmentry.TabIndex = 37;
			this.ctrl_CB_IsSymmentry.Text = "Признак симметричности";
			this.ctrl_CB_IsSymmentry.UseVisualStyleBackColor = true;
			// 
			// ctrl_TB_Symmetry1
			// 
			this.ctrl_TB_Symmetry1.Location = new System.Drawing.Point(118, 124);
			this.ctrl_TB_Symmetry1.Name = "ctrl_TB_Symmetry1";
			this.ctrl_TB_Symmetry1.Size = new System.Drawing.Size(420, 20);
			this.ctrl_TB_Symmetry1.TabIndex = 34;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 127);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(102, 13);
			this.label7.TabIndex = 33;
			this.label7.Text = "Симметричность 1";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(3, 153);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(102, 13);
			this.label8.TabIndex = 35;
			this.label8.Text = "Симметричность 2";
			// 
			// ctrl_TB_Symmetry2
			// 
			this.ctrl_TB_Symmetry2.Location = new System.Drawing.Point(118, 150);
			this.ctrl_TB_Symmetry2.Name = "ctrl_TB_Symmetry2";
			this.ctrl_TB_Symmetry2.Size = new System.Drawing.Size(420, 20);
			this.ctrl_TB_Symmetry2.TabIndex = 36;
			// 
			// ctrl_TB_Note
			// 
			this.ctrl_TB_Note.AcceptsReturn = true;
			this.ctrl_TB_Note.Location = new System.Drawing.Point(118, 176);
			this.ctrl_TB_Note.Multiline = true;
			this.ctrl_TB_Note.Name = "ctrl_TB_Note";
			this.ctrl_TB_Note.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ctrl_TB_Note.Size = new System.Drawing.Size(420, 58);
			this.ctrl_TB_Note.TabIndex = 32;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(3, 179);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(70, 13);
			this.label9.TabIndex = 31;
			this.label9.Text = "Примечание";
			// 
			// ctrl_CB_IsVisible
			// 
			this.ctrl_CB_IsVisible.AutoSize = true;
			this.ctrl_CB_IsVisible.Location = new System.Drawing.Point(6, 85);
			this.ctrl_CB_IsVisible.Name = "ctrl_CB_IsVisible";
			this.ctrl_CB_IsVisible.Size = new System.Drawing.Size(98, 17);
			this.ctrl_CB_IsVisible.TabIndex = 30;
			this.ctrl_CB_IsVisible.Text = "Видимое поле";
			this.ctrl_CB_IsVisible.UseVisualStyleBackColor = true;
			// 
			// ctrl_CB_IsEditing
			// 
			this.ctrl_CB_IsEditing.AutoSize = true;
			this.ctrl_CB_IsEditing.Location = new System.Drawing.Point(6, 68);
			this.ctrl_CB_IsEditing.Name = "ctrl_CB_IsEditing";
			this.ctrl_CB_IsEditing.Size = new System.Drawing.Size(132, 17);
			this.ctrl_CB_IsEditing.TabIndex = 29;
			this.ctrl_CB_IsEditing.Text = "Редактируемое поле";
			this.ctrl_CB_IsEditing.UseVisualStyleBackColor = true;
			// 
			// ctrl_CB_IsRequiredFIeld
			// 
			this.ctrl_CB_IsRequiredFIeld.AutoSize = true;
			this.ctrl_CB_IsRequiredFIeld.Location = new System.Drawing.Point(6, 51);
			this.ctrl_CB_IsRequiredFIeld.Name = "ctrl_CB_IsRequiredFIeld";
			this.ctrl_CB_IsRequiredFIeld.Size = new System.Drawing.Size(204, 17);
			this.ctrl_CB_IsRequiredFIeld.TabIndex = 28;
			this.ctrl_CB_IsRequiredFIeld.Text = "Обязательно поле для заполнения";
			this.ctrl_CB_IsRequiredFIeld.UseVisualStyleBackColor = true;
			// 
			// ctrl_TB_Hint
			// 
			this.ctrl_TB_Hint.Location = new System.Drawing.Point(118, 25);
			this.ctrl_TB_Hint.Name = "ctrl_TB_Hint";
			this.ctrl_TB_Hint.Size = new System.Drawing.Size(420, 20);
			this.ctrl_TB_Hint.TabIndex = 21;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 28);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 20;
			this.label4.Text = "Подсказка";
			// 
			// ctrl_TB_Name
			// 
			this.ctrl_TB_Name.Location = new System.Drawing.Point(118, 0);
			this.ctrl_TB_Name.Name = "ctrl_TB_Name";
			this.ctrl_TB_Name.Size = new System.Drawing.Size(420, 20);
			this.ctrl_TB_Name.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(109, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Название элемента";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.ctrl_LVTemplates);
			this.tabPage2.Controls.Add(this.panel4);
			this.tabPage2.Controls.Add(this.ctrl_TB_TextItem);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(547, 240);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Параметры элемента";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// ctrl_LVTemplates
			// 
			this.ctrl_LVTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
			this.ctrl_LVTemplates.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctrl_LVTemplates.FullRowSelect = true;
			this.ctrl_LVTemplates.HideSelection = false;
			this.ctrl_LVTemplates.Location = new System.Drawing.Point(3, 35);
			this.ctrl_LVTemplates.MultiSelect = false;
			this.ctrl_LVTemplates.Name = "ctrl_LVTemplates";
			this.ctrl_LVTemplates.Size = new System.Drawing.Size(541, 205);
			this.ctrl_LVTemplates.TabIndex = 19;
			this.ctrl_LVTemplates.UseCompatibleStateImageBehavior = false;
			this.ctrl_LVTemplates.View = System.Windows.Forms.View.Details;
			// 
			// chName
			// 
			this.chName.Text = "Наименование";
			this.chName.Width = 300;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.button1);
			this.panel4.Controls.Add(this.ctrl_B_CBAddElement);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(3, 3);
			this.panel4.Margin = new System.Windows.Forms.Padding(0);
			this.panel4.Name = "panel4";
			this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
			this.panel4.Size = new System.Drawing.Size(541, 32);
			this.panel4.TabIndex = 18;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Left;
			this.button1.Location = new System.Drawing.Point(75, 0);
			this.button1.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 25);
			this.button1.TabIndex = 5;
			this.button1.Text = "Удалить";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ctrl_B_CBAddElement
			// 
			this.ctrl_B_CBAddElement.Dock = System.Windows.Forms.DockStyle.Left;
			this.ctrl_B_CBAddElement.Location = new System.Drawing.Point(0, 0);
			this.ctrl_B_CBAddElement.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
			this.ctrl_B_CBAddElement.Name = "ctrl_B_CBAddElement";
			this.ctrl_B_CBAddElement.Size = new System.Drawing.Size(75, 25);
			this.ctrl_B_CBAddElement.TabIndex = 4;
			this.ctrl_B_CBAddElement.Text = "Добавить";
			this.ctrl_B_CBAddElement.UseVisualStyleBackColor = true;
			this.ctrl_B_CBAddElement.Click += new System.EventHandler(this.ctrl_B_CBAddElement_Click);
			// 
			// ctrl_TB_TextItem
			// 
			this.ctrl_TB_TextItem.Location = new System.Drawing.Point(3, 3);
			this.ctrl_TB_TextItem.Multiline = true;
			this.ctrl_TB_TextItem.Name = "ctrl_TB_TextItem";
			this.ctrl_TB_TextItem.Size = new System.Drawing.Size(495, 234);
			this.ctrl_TB_TextItem.TabIndex = 20;
			this.ctrl_TB_TextItem.Visible = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ctrl_BTemplateEdit);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 263);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(7);
			this.panel2.Size = new System.Drawing.Size(555, 41);
			this.panel2.TabIndex = 16;
			// 
			// ctrl_BTemplateEdit
			// 
			this.ctrl_BTemplateEdit.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctrl_BTemplateEdit.Location = new System.Drawing.Point(473, 7);
			this.ctrl_BTemplateEdit.Name = "ctrl_BTemplateEdit";
			this.ctrl_BTemplateEdit.Size = new System.Drawing.Size(75, 27);
			this.ctrl_BTemplateEdit.TabIndex = 4;
			this.ctrl_BTemplateEdit.Text = "Сохранить";
			this.ctrl_BTemplateEdit.UseVisualStyleBackColor = true;
			this.ctrl_BTemplateEdit.Click += new System.EventHandler(this.ctrl_BTemplateSave_Click);
			// 
			// UC_EditorElements
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "UC_EditorElements";
			this.Size = new System.Drawing.Size(796, 318);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ctrl_CMTemplate.ResumeLayout(false);
			this.ctrl_PSearch.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ctrl_Pnl_Properties.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView ctrl_TVTemplates;
		private System.Windows.Forms.Panel ctrl_PSearch;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox ctrl_TBSearch;
		private System.Windows.Forms.Button ctrl_BSearch;
		private System.Windows.Forms.Panel ctrl_Pnl_Properties;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.CheckBox ctrl_CB_IsSymmentry;
		private System.Windows.Forms.TextBox ctrl_TB_Symmetry1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox ctrl_TB_Symmetry2;
		private System.Windows.Forms.TextBox ctrl_TB_Note;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox ctrl_CB_IsVisible;
		private System.Windows.Forms.CheckBox ctrl_CB_IsEditing;
		private System.Windows.Forms.CheckBox ctrl_CB_IsRequiredFIeld;
		private System.Windows.Forms.TextBox ctrl_TB_Hint;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox ctrl_TB_Name;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView ctrl_LVTemplates;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button ctrl_B_CBAddElement;
		private System.Windows.Forms.TextBox ctrl_TB_TextItem;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ctrl_BTemplateEdit;
		private System.Windows.Forms.ContextMenuStrip ctrl_CMTemplate;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIControlNew;
		private System.Windows.Forms.ToolStripMenuItem ctrl_MIControlDelete;
	}
}
