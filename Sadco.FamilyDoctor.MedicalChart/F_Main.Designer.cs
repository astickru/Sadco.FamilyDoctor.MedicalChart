namespace Sadco.FamilyDoctor.MedicalChart
{
	partial class F_Main
	{
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
            this.ctrlMIMenu = new System.Windows.Forms.MenuStrip();
            this.ctrlMIFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMISave = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIMedicalCards = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCatalogs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMegaTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPatterns = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMegaTemplateDeleted = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMISettingsPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlMIInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ctrl_CustomControls = new System.Windows.Forms.Panel();
            this.ctrlPFooter = new System.Windows.Forms.Panel();
            this.ctrlSessionInfo = new System.Windows.Forms.Label();
            this.ctrlMIMenu.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.ctrlPFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlMIMenu
            // 
            this.ctrlMIMenu.BackColor = System.Drawing.SystemColors.Control;
            this.ctrlMIMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMIFile,
            this.ctrlMIMedicalCards,
            this.ctrlMIRecord,
            this.ctrlMIEditor,
            this.настройкиToolStripMenuItem,
            this.ctrlMIInfo});
            this.ctrlMIMenu.Location = new System.Drawing.Point(0, 0);
            this.ctrlMIMenu.Name = "ctrlMIMenu";
            this.ctrlMIMenu.Size = new System.Drawing.Size(984, 24);
            this.ctrlMIMenu.TabIndex = 1;
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
            this.ctrlMIExit.Click += new System.EventHandler(this.ctrlMIExit_Click);
            // 
            // ctrlMIMedicalCards
            // 
            this.ctrlMIMedicalCards.Name = "ctrlMIMedicalCards";
            this.ctrlMIMedicalCards.Size = new System.Drawing.Size(75, 20);
            this.ctrlMIMedicalCards.Text = "Медкарты";
            this.ctrlMIMedicalCards.Click += new System.EventHandler(this.ctrlMIMedicalCards_Click);
            // 
            // ctrlMIRecord
            // 
            this.ctrlMIRecord.Name = "ctrlMIRecord";
            this.ctrlMIRecord.Size = new System.Drawing.Size(59, 20);
            this.ctrlMIRecord.Text = "Записи";
            this.ctrlMIRecord.Click += new System.EventHandler(this.ctrlMIRecord_Click);
            // 
            // ctrlMIEditor
            // 
            this.ctrlMIEditor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCatalogs,
            this.menuMegaTemplate,
            this.menuTemplate,
            this.menuPatterns,
            this.menuMegaTemplateDeleted});
            this.ctrlMIEditor.Name = "ctrlMIEditor";
            this.ctrlMIEditor.Size = new System.Drawing.Size(69, 20);
            this.ctrlMIEditor.Text = "Редактор";
            // 
            // menuCatalogs
            // 
            this.menuCatalogs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCategories});
            this.menuCatalogs.Name = "menuCatalogs";
            this.menuCatalogs.Size = new System.Drawing.Size(231, 22);
            this.menuCatalogs.Text = "Справочники";
            // 
            // menuCategories
            // 
            this.menuCategories.Name = "menuCategories";
            this.menuCategories.Size = new System.Drawing.Size(131, 22);
            this.menuCategories.Text = "Категории";
            this.menuCategories.Click += new System.EventHandler(this.menuCategories_Click);
            // 
            // menuTemplate
            // 
            this.menuTemplate.Image = global::Sadco.FamilyDoctor.MedicalChart.Properties.Resources.data_sort;
            this.menuTemplate.Name = "menuTemplate";
            this.menuTemplate.Size = new System.Drawing.Size(231, 22);
            this.menuTemplate.Text = "Шаблоны";
            this.menuTemplate.Click += new System.EventHandler(this.ctrl_MenuShowTemplates_Click);
            // 
            // menuMegaTemplate
            // 
            this.menuMegaTemplate.Image = global::Sadco.FamilyDoctor.MedicalChart.Properties.Resources.data_table;
            this.menuMegaTemplate.Name = "menuMegaTemplate";
            this.menuMegaTemplate.Size = new System.Drawing.Size(231, 22);
            this.menuMegaTemplate.Text = "Элементы";
            this.menuMegaTemplate.Click += new System.EventHandler(this.ctrl_MenuShowElements_Click);
            // 
            // menuPatterns
            // 
            this.menuPatterns.Name = "menuPatterns";
            this.menuPatterns.Size = new System.Drawing.Size(231, 22);
            this.menuPatterns.Text = "Паттерны";
            this.menuPatterns.Click += new System.EventHandler(this.menuPatterns_Click);
            // 
            // menuMegaTemplateDeleted
            // 
            this.menuMegaTemplateDeleted.Image = global::Sadco.FamilyDoctor.MedicalChart.Properties.Resources.data_table_deleted;
            this.menuMegaTemplateDeleted.Name = "menuMegaTemplateDeleted";
            this.menuMegaTemplateDeleted.Size = new System.Drawing.Size(231, 22);
            this.menuMegaTemplateDeleted.Text = "Показ удаленных элементов";
            this.menuMegaTemplateDeleted.Click += new System.EventHandler(this.menuMegaTemplateDeleted_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlMISettingsPrint});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // ctrlMISettingsPrint
            // 
            this.ctrlMISettingsPrint.CheckOnClick = true;
            this.ctrlMISettingsPrint.Name = "ctrlMISettingsPrint";
            this.ctrlMISettingsPrint.Size = new System.Drawing.Size(198, 22);
            this.ctrlMISettingsPrint.Text = "Печать с настройками";
            this.ctrlMISettingsPrint.CheckedChanged += new System.EventHandler(this.ctrlMISettingsPrint_CheckedChanged);
            // 
            // ctrlMIInfo
            // 
            this.ctrlMIInfo.Name = "ctrlMIInfo";
            this.ctrlMIInfo.Size = new System.Drawing.Size(65, 20);
            this.ctrlMIInfo.Text = "Справка";
            // 
            // MainPanel
            // 
            this.MainPanel.AutoSize = true;
            this.MainPanel.Controls.Add(this.ctrl_CustomControls);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.MainPanel.Size = new System.Drawing.Size(984, 829);
            this.MainPanel.TabIndex = 2;
            // 
            // ctrl_CustomControls
            // 
            this.ctrl_CustomControls.AutoSize = true;
            this.ctrl_CustomControls.BackColor = System.Drawing.SystemColors.Control;
            this.ctrl_CustomControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_CustomControls.Location = new System.Drawing.Point(5, 0);
            this.ctrl_CustomControls.Margin = new System.Windows.Forms.Padding(0);
            this.ctrl_CustomControls.Name = "ctrl_CustomControls";
            this.ctrl_CustomControls.Size = new System.Drawing.Size(974, 824);
            this.ctrl_CustomControls.TabIndex = 0;
            // 
            // ctrlPFooter
            // 
            this.ctrlPFooter.Controls.Add(this.ctrlSessionInfo);
            this.ctrlPFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlPFooter.Location = new System.Drawing.Point(0, 853);
            this.ctrlPFooter.Name = "ctrlPFooter";
            this.ctrlPFooter.Size = new System.Drawing.Size(984, 28);
            this.ctrlPFooter.TabIndex = 0;
            // 
            // ctrlSessionInfo
            // 
            this.ctrlSessionInfo.AutoSize = true;
            this.ctrlSessionInfo.Location = new System.Drawing.Point(9, 5);
            this.ctrlSessionInfo.Name = "ctrlSessionInfo";
            this.ctrlSessionInfo.Size = new System.Drawing.Size(76, 13);
            this.ctrlSessionInfo.TabIndex = 0;
            this.ctrlSessionInfo.Text = "ctrlSessionInfo";
            // 
            // F_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(984, 881);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.ctrlPFooter);
            this.Controls.Add(this.ctrlMIMenu);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximumSize = new System.Drawing.Size(1600, 1200);
            this.MinimumSize = new System.Drawing.Size(650, 350);
            this.Name = "F_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_Main";
            this.ctrlMIMenu.ResumeLayout(false);
            this.ctrlMIMenu.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ctrlPFooter.ResumeLayout(false);
            this.ctrlPFooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip ctrlMIMenu;
		private System.Windows.Forms.ToolStripMenuItem ctrlMIFile;
		private System.Windows.Forms.ToolStripMenuItem ctrlMISave;
		private System.Windows.Forms.ToolStripMenuItem ctrlMIExit;
		private System.Windows.Forms.ToolStripMenuItem ctrlMIEditor;
		private System.Windows.Forms.ToolStripMenuItem menuTemplate;
		private System.Windows.Forms.ToolStripMenuItem menuMegaTemplate;
		private System.Windows.Forms.ToolStripMenuItem ctrlMIInfo;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.Panel ctrl_CustomControls;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIRecord;
        private System.Windows.Forms.ToolStripMenuItem menuMegaTemplateDeleted;
        private System.Windows.Forms.ToolStripMenuItem menuCatalogs;
        private System.Windows.Forms.ToolStripMenuItem menuCategories;
        private System.Windows.Forms.ToolStripMenuItem menuPatterns;
		private System.Windows.Forms.Panel ctrlPFooter;
		private System.Windows.Forms.Label ctrlSessionInfo;
        private System.Windows.Forms.ToolStripMenuItem ctrlMIMedicalCards;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ctrlMISettingsPrint;
    }
}