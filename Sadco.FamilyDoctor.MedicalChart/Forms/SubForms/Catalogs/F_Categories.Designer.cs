namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Catalogs
{
    partial class F_Categories
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ctrlCategoriesTotal = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.p_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlSave = new System.Windows.Forms.Button();
            this.ctrlReset = new System.Windows.Forms.Button();
            this.ctrlCategoriesKlinik = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesTotal)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesKlinik)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(602, 638);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ctrlCategoriesTotal);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(594, 612);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Общая категория";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrlCategoriesTotal
            // 
            this.ctrlCategoriesTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrlCategoriesTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlCategoriesTotal.Location = new System.Drawing.Point(3, 3);
            this.ctrlCategoriesTotal.Name = "ctrlCategoriesTotal";
            this.ctrlCategoriesTotal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrlCategoriesTotal.Size = new System.Drawing.Size(588, 606);
            this.ctrlCategoriesTotal.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ctrlCategoriesKlinik);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(594, 612);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Клиническая категория";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // p_Name
            // 
            this.p_Name.Name = "p_Name";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctrlSave);
            this.panel1.Controls.Add(this.ctrlReset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 601);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 37);
            this.panel1.TabIndex = 1;
            // 
            // ctrlSave
            // 
            this.ctrlSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlSave.Location = new System.Drawing.Point(409, 0);
            this.ctrlSave.Margin = new System.Windows.Forms.Padding(10);
            this.ctrlSave.Name = "ctrlSave";
            this.ctrlSave.Size = new System.Drawing.Size(110, 37);
            this.ctrlSave.TabIndex = 1;
            this.ctrlSave.Text = "сохранить";
            this.ctrlSave.UseVisualStyleBackColor = true;
            this.ctrlSave.Click += new System.EventHandler(this.ctrlSave_Click);
            // 
            // ctrlReset
            // 
            this.ctrlReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlReset.Location = new System.Drawing.Point(519, 0);
            this.ctrlReset.Margin = new System.Windows.Forms.Padding(10);
            this.ctrlReset.Name = "ctrlReset";
            this.ctrlReset.Size = new System.Drawing.Size(83, 37);
            this.ctrlReset.TabIndex = 0;
            this.ctrlReset.Text = "сбросить";
            this.ctrlReset.UseVisualStyleBackColor = true;
            this.ctrlReset.Click += new System.EventHandler(this.ctrlReset_Click);
            // 
            // ctrlCategoriesKlinik
            // 
            this.ctrlCategoriesKlinik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrlCategoriesKlinik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlCategoriesKlinik.Location = new System.Drawing.Point(3, 3);
            this.ctrlCategoriesKlinik.Name = "ctrlCategoriesKlinik";
            this.ctrlCategoriesKlinik.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrlCategoriesKlinik.Size = new System.Drawing.Size(588, 606);
            this.ctrlCategoriesKlinik.TabIndex = 1;
            // 
            // F_Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 638);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximumSize = new System.Drawing.Size(1600, 1000);
            this.Name = "F_Categories";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник категорий";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_Categories_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesTotal)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesKlinik)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView ctrlCategoriesTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Name;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ctrlSave;
        private System.Windows.Forms.Button ctrlReset;
        private System.Windows.Forms.DataGridView ctrlCategoriesKlinik;
    }
}