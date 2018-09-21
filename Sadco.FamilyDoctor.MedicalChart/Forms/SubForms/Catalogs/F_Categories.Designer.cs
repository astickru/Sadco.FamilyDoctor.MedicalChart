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
            this.ctrlTabClinik = new System.Windows.Forms.TabPage();
            this.ctrlCategoriesClinik = new System.Windows.Forms.DataGridView();
            this.p_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlAdd = new System.Windows.Forms.Button();
            this.ctrlEdit = new System.Windows.Forms.Button();
            this.ctrlDelete = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctrlCategoriesTab.SuspendLayout();
            this.ctrlTabTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesTotal)).BeginInit();
            this.ctrlTabClinik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesClinik)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlCategoriesTab
            // 
            this.ctrlCategoriesTab.Controls.Add(this.ctrlTabTotal);
            this.ctrlCategoriesTab.Controls.Add(this.ctrlTabClinik);
            this.ctrlCategoriesTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlCategoriesTab.Location = new System.Drawing.Point(0, 0);
            this.ctrlCategoriesTab.Name = "ctrlCategoriesTab";
            this.ctrlCategoriesTab.SelectedIndex = 0;
            this.ctrlCategoriesTab.Size = new System.Drawing.Size(688, 638);
            this.ctrlCategoriesTab.TabIndex = 0;
            // 
            // ctrlTabTotal
            // 
            this.ctrlTabTotal.Controls.Add(this.ctrlCategoriesTotal);
            this.ctrlTabTotal.Location = new System.Drawing.Point(4, 22);
            this.ctrlTabTotal.Name = "ctrlTabTotal";
            this.ctrlTabTotal.Padding = new System.Windows.Forms.Padding(3);
            this.ctrlTabTotal.Size = new System.Drawing.Size(680, 612);
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
            this.ctrlCategoriesTotal.Size = new System.Drawing.Size(674, 606);
            this.ctrlCategoriesTotal.TabIndex = 0;
            this.ctrlCategoriesTotal.SelectionChanged += new System.EventHandler(this.ctrlCategoriesTotal_SelectionChanged);
            // 
            // ctrlTabClinik
            // 
            this.ctrlTabClinik.Controls.Add(this.ctrlCategoriesClinik);
            this.ctrlTabClinik.Location = new System.Drawing.Point(4, 22);
            this.ctrlTabClinik.Name = "ctrlTabClinik";
            this.ctrlTabClinik.Padding = new System.Windows.Forms.Padding(3);
            this.ctrlTabClinik.Size = new System.Drawing.Size(680, 612);
            this.ctrlTabClinik.TabIndex = 1;
            this.ctrlTabClinik.Text = "Клиническая категория";
            this.ctrlTabClinik.UseVisualStyleBackColor = true;
            // 
            // ctrlCategoriesClinik
            // 
            this.ctrlCategoriesClinik.AllowUserToAddRows = false;
            this.ctrlCategoriesClinik.AllowUserToDeleteRows = false;
            this.ctrlCategoriesClinik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrlCategoriesClinik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlCategoriesClinik.Location = new System.Drawing.Point(3, 3);
            this.ctrlCategoriesClinik.MultiSelect = false;
            this.ctrlCategoriesClinik.Name = "ctrlCategoriesClinik";
            this.ctrlCategoriesClinik.ReadOnly = true;
            this.ctrlCategoriesClinik.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrlCategoriesClinik.Size = new System.Drawing.Size(674, 606);
            this.ctrlCategoriesClinik.TabIndex = 1;
            this.ctrlCategoriesClinik.SelectionChanged += new System.EventHandler(this.ctrlCategoriesClinik_SelectionChanged);
            // 
            // p_Name
            // 
            this.p_Name.Name = "p_Name";
            // 
            // ctrlAdd
            // 
            this.ctrlAdd.Location = new System.Drawing.Point(364, 3);
            this.ctrlAdd.Margin = new System.Windows.Forms.Padding(0, 0, 17, 0);
            this.ctrlAdd.Name = "ctrlAdd";
            this.ctrlAdd.Size = new System.Drawing.Size(94, 25);
            this.ctrlAdd.TabIndex = 1;
            this.ctrlAdd.Text = "добавить";
            this.ctrlAdd.UseVisualStyleBackColor = true;
            this.ctrlAdd.Click += new System.EventHandler(this.ctrlAdd_Click);
            // 
            // ctrlEdit
            // 
            this.ctrlEdit.Enabled = false;
            this.ctrlEdit.Location = new System.Drawing.Point(475, 3);
            this.ctrlEdit.Margin = new System.Windows.Forms.Padding(0, 0, 17, 0);
            this.ctrlEdit.Name = "ctrlEdit";
            this.ctrlEdit.Size = new System.Drawing.Size(95, 25);
            this.ctrlEdit.TabIndex = 2;
            this.ctrlEdit.Text = "изменить";
            this.ctrlEdit.UseVisualStyleBackColor = true;
            this.ctrlEdit.Click += new System.EventHandler(this.ctrlEdit_Click);
            // 
            // ctrlDelete
            // 
            this.ctrlDelete.Enabled = false;
            this.ctrlDelete.Location = new System.Drawing.Point(587, 3);
            this.ctrlDelete.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlDelete.Name = "ctrlDelete";
            this.ctrlDelete.Size = new System.Drawing.Size(95, 25);
            this.ctrlDelete.TabIndex = 0;
            this.ctrlDelete.Text = "удалить";
            this.ctrlDelete.UseVisualStyleBackColor = true;
            this.ctrlDelete.Click += new System.EventHandler(this.ctrlDelete_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ctrlDelete);
            this.flowLayoutPanel1.Controls.Add(this.ctrlEdit);
            this.flowLayoutPanel1.Controls.Add(this.ctrlAdd);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 606);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(688, 32);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // F_Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 638);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ctrlCategoriesTab);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.MaximumSize = new System.Drawing.Size(1826, 1000);
            this.Name = "F_Categories";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник категорий";
            this.ctrlCategoriesTab.ResumeLayout(false);
            this.ctrlTabTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesTotal)).EndInit();
            this.ctrlTabClinik.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlCategoriesClinik)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ctrlCategoriesTab;
        private System.Windows.Forms.TabPage ctrlTabTotal;
        private System.Windows.Forms.TabPage ctrlTabClinik;
        private System.Windows.Forms.DataGridView ctrlCategoriesTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Name;
        private System.Windows.Forms.DataGridView ctrlCategoriesClinik;
        private System.Windows.Forms.Button ctrlAdd;
        private System.Windows.Forms.Button ctrlDelete;
        private System.Windows.Forms.Button ctrlEdit;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}