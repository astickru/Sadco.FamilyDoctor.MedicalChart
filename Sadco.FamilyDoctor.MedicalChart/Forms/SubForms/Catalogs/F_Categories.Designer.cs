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
            this.ctrlCategoriesTab = new System.Windows.Forms.TabControl();
            this.ctrlTabTotal = new System.Windows.Forms.TabPage();
            this.ctrlCategoriesTotal = new System.Windows.Forms.DataGridView();
            this.ctrlTabKlinik = new System.Windows.Forms.TabPage();
            this.ctrlCategoriesKlinik = new System.Windows.Forms.DataGridView();
            this.p_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlAdd = new System.Windows.Forms.Button();
            this.ctrlEdit = new System.Windows.Forms.Button();
            this.ctrlDelete = new System.Windows.Forms.Button();
            this.ctrlCategoriesTab.SuspendLayout();
            this.ctrlTabTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesTotal)).BeginInit();
            this.ctrlTabKlinik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesKlinik)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlCategoriesTab
            // 
            this.ctrlCategoriesTab.Controls.Add(this.ctrlTabTotal);
            this.ctrlCategoriesTab.Controls.Add(this.ctrlTabKlinik);
            this.ctrlCategoriesTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlCategoriesTab.Location = new System.Drawing.Point(0, 0);
            this.ctrlCategoriesTab.Name = "ctrlCategoriesTab";
            this.ctrlCategoriesTab.SelectedIndex = 0;
            this.ctrlCategoriesTab.Size = new System.Drawing.Size(602, 638);
            this.ctrlCategoriesTab.TabIndex = 0;
            // 
            // ctrlTabTotal
            // 
            this.ctrlTabTotal.Controls.Add(this.ctrlCategoriesTotal);
            this.ctrlTabTotal.Location = new System.Drawing.Point(4, 22);
            this.ctrlTabTotal.Name = "ctrlTabTotal";
            this.ctrlTabTotal.Padding = new System.Windows.Forms.Padding(3);
            this.ctrlTabTotal.Size = new System.Drawing.Size(594, 612);
            this.ctrlTabTotal.TabIndex = 0;
            this.ctrlTabTotal.Text = "Общая категория";
            this.ctrlTabTotal.UseVisualStyleBackColor = true;
            // 
            // ctrlCategoriesTotal
            // 
            this.ctrlCategoriesTotal.AllowUserToAddRows = false;
            this.ctrlCategoriesTotal.AllowUserToDeleteRows = false;
            this.ctrlCategoriesTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrlCategoriesTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlCategoriesTotal.Location = new System.Drawing.Point(3, 3);
            this.ctrlCategoriesTotal.MultiSelect = false;
            this.ctrlCategoriesTotal.Name = "ctrlCategoriesTotal";
            this.ctrlCategoriesTotal.ReadOnly = true;
            this.ctrlCategoriesTotal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrlCategoriesTotal.Size = new System.Drawing.Size(588, 606);
            this.ctrlCategoriesTotal.TabIndex = 0;
            this.ctrlCategoriesTotal.SelectionChanged += new System.EventHandler(this.ctrlCategoriesTotal_SelectionChanged);
            // 
            // ctrlTabKlinik
            // 
            this.ctrlTabKlinik.Controls.Add(this.ctrlCategoriesKlinik);
            this.ctrlTabKlinik.Location = new System.Drawing.Point(4, 22);
            this.ctrlTabKlinik.Name = "ctrlTabKlinik";
            this.ctrlTabKlinik.Padding = new System.Windows.Forms.Padding(3);
            this.ctrlTabKlinik.Size = new System.Drawing.Size(594, 612);
            this.ctrlTabKlinik.TabIndex = 1;
            this.ctrlTabKlinik.Text = "Клиническая категория";
            this.ctrlTabKlinik.UseVisualStyleBackColor = true;
            // 
            // ctrlCategoriesKlinik
            // 
            this.ctrlCategoriesKlinik.AllowUserToAddRows = false;
            this.ctrlCategoriesKlinik.AllowUserToDeleteRows = false;
            this.ctrlCategoriesKlinik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrlCategoriesKlinik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlCategoriesKlinik.Location = new System.Drawing.Point(3, 3);
            this.ctrlCategoriesKlinik.MultiSelect = false;
            this.ctrlCategoriesKlinik.Name = "ctrlCategoriesKlinik";
            this.ctrlCategoriesKlinik.ReadOnly = true;
            this.ctrlCategoriesKlinik.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrlCategoriesKlinik.Size = new System.Drawing.Size(588, 606);
            this.ctrlCategoriesKlinik.TabIndex = 1;
            this.ctrlCategoriesKlinik.SelectionChanged += new System.EventHandler(this.ctrlCategoriesKlinik_SelectionChanged);
            // 
            // p_Name
            // 
            this.p_Name.Name = "p_Name";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctrlAdd);
            this.panel1.Controls.Add(this.ctrlEdit);
            this.panel1.Controls.Add(this.ctrlDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 601);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 37);
            this.panel1.TabIndex = 1;
            // 
            // ctrlAdd
            // 
            this.ctrlAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlAdd.Location = new System.Drawing.Point(354, 0);
            this.ctrlAdd.Margin = new System.Windows.Forms.Padding(10);
            this.ctrlAdd.Name = "ctrlAdd";
            this.ctrlAdd.Size = new System.Drawing.Size(82, 37);
            this.ctrlAdd.TabIndex = 1;
            this.ctrlAdd.Text = "добавить";
            this.ctrlAdd.UseVisualStyleBackColor = true;
            this.ctrlAdd.Click += new System.EventHandler(this.ctrlAdd_Click);
            // 
            // ctrlEdit
            // 
            this.ctrlEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlEdit.Enabled = false;
            this.ctrlEdit.Location = new System.Drawing.Point(436, 0);
            this.ctrlEdit.Name = "ctrlEdit";
            this.ctrlEdit.Size = new System.Drawing.Size(83, 37);
            this.ctrlEdit.TabIndex = 2;
            this.ctrlEdit.Text = "изменить";
            this.ctrlEdit.UseVisualStyleBackColor = true;
            this.ctrlEdit.Click += new System.EventHandler(this.ctrlEdit_Click);
            // 
            // ctrlDelete
            // 
            this.ctrlDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlDelete.Enabled = false;
            this.ctrlDelete.Location = new System.Drawing.Point(519, 0);
            this.ctrlDelete.Margin = new System.Windows.Forms.Padding(10);
            this.ctrlDelete.Name = "ctrlDelete";
            this.ctrlDelete.Size = new System.Drawing.Size(83, 37);
            this.ctrlDelete.TabIndex = 0;
            this.ctrlDelete.Text = "удалить";
            this.ctrlDelete.UseVisualStyleBackColor = true;
            this.ctrlDelete.Click += new System.EventHandler(this.ctrlDelete_Click);
            // 
            // F_Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 638);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctrlCategoriesTab);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximumSize = new System.Drawing.Size(1600, 1000);
            this.Name = "F_Categories";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник категорий";
            this.ctrlCategoriesTab.ResumeLayout(false);
            this.ctrlTabTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesTotal)).EndInit();
            this.ctrlTabKlinik.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesKlinik)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ctrlCategoriesTab;
        private System.Windows.Forms.TabPage ctrlTabTotal;
        private System.Windows.Forms.TabPage ctrlTabKlinik;
        private System.Windows.Forms.DataGridView ctrlCategoriesTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Name;
        private System.Windows.Forms.DataGridView ctrlCategoriesKlinik;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ctrlAdd;
        private System.Windows.Forms.Button ctrlDelete;
        private System.Windows.Forms.Button ctrlEdit;
    }
}