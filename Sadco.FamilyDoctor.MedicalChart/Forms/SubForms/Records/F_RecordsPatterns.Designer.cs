namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Catalogs
{
    partial class F_RecordsPatterns
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlAdd = new System.Windows.Forms.Button();
            this.ctrlDelete = new System.Windows.Forms.Button();
            this.ctrlTablePatterns = new System.Windows.Forms.DataGridView();
            this.p_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_TemplateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlTablePatterns)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctrlAdd);
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
            this.ctrlAdd.Location = new System.Drawing.Point(437, 0);
            this.ctrlAdd.Margin = new System.Windows.Forms.Padding(10);
            this.ctrlAdd.Name = "ctrlAdd";
            this.ctrlAdd.Size = new System.Drawing.Size(82, 37);
            this.ctrlAdd.TabIndex = 1;
            this.ctrlAdd.Text = "добавить";
            this.ctrlAdd.UseVisualStyleBackColor = true;
            this.ctrlAdd.Click += new System.EventHandler(this.ctrlAdd_Click);
            // 
            // ctrlDelete
            // 
            this.ctrlDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrlDelete.Location = new System.Drawing.Point(519, 0);
            this.ctrlDelete.Margin = new System.Windows.Forms.Padding(10);
            this.ctrlDelete.Name = "ctrlDelete";
            this.ctrlDelete.Size = new System.Drawing.Size(83, 37);
            this.ctrlDelete.TabIndex = 0;
            this.ctrlDelete.Text = "удалить";
            this.ctrlDelete.UseVisualStyleBackColor = true;
            this.ctrlDelete.Click += new System.EventHandler(this.ctrlDelete_Click);
            // 
            // ctrlTablePatterns
            // 
            this.ctrlTablePatterns.AllowUserToAddRows = false;
            this.ctrlTablePatterns.AllowUserToDeleteRows = false;
            this.ctrlTablePatterns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrlTablePatterns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.p_ID,
            this.p_Name,
            this.p_TemplateName});
            this.ctrlTablePatterns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTablePatterns.Location = new System.Drawing.Point(0, 0);
            this.ctrlTablePatterns.MultiSelect = false;
            this.ctrlTablePatterns.Name = "ctrlTablePatterns";
            this.ctrlTablePatterns.ReadOnly = true;
            this.ctrlTablePatterns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrlTablePatterns.Size = new System.Drawing.Size(602, 601);
            this.ctrlTablePatterns.TabIndex = 2;
            this.ctrlTablePatterns.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctrlTablePatterns_CellDoubleClick);
            // 
            // p_ID
            // 
            this.p_ID.HeaderText = "p_ID";
            this.p_ID.Name = "p_ID";
            this.p_ID.ReadOnly = true;
            this.p_ID.Visible = false;
            // 
            // p_Name
            // 
            this.p_Name.HeaderText = "Название";
            this.p_Name.Name = "p_Name";
            this.p_Name.ReadOnly = true;
            this.p_Name.Width = 250;
            // 
            // p_TemplateName
            // 
            this.p_TemplateName.HeaderText = "Шаблон";
            this.p_TemplateName.Name = "p_TemplateName";
            this.p_TemplateName.ReadOnly = true;
            this.p_TemplateName.Width = 250;
            // 
            // F_RecordsPatterns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 638);
            this.Controls.Add(this.ctrlTablePatterns);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximumSize = new System.Drawing.Size(1600, 1000);
            this.Name = "F_RecordsPatterns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Паттерны";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlTablePatterns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ctrlAdd;
        private System.Windows.Forms.Button ctrlDelete;
        private System.Windows.Forms.DataGridView ctrlTablePatterns;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_TemplateName;
    }
}