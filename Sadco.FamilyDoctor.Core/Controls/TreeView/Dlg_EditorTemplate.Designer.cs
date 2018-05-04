namespace Sadco.FamilyDoctor.Core.Controls
{
    partial class Dlg_EditorTemplate
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrlCategoriesKlinik = new System.Windows.Forms.ComboBox();
            this.ctrlCategoriesTotal = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrl_LGroupValue = new System.Windows.Forms.Label();
            this.ctrl_LTitleGroup = new System.Windows.Forms.Label();
            this.ctrl_TBName = new System.Windows.Forms.TextBox();
            this.ctrl_LTitleName = new System.Windows.Forms.Label();
            this.ctrl_BCancel = new System.Windows.Forms.Button();
            this.ctrl_BOk = new System.Windows.Forms.Button();
            this.ctrlTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctrlTitle);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.ctrlCategoriesKlinik);
            this.panel2.Controls.Add(this.ctrlCategoriesTotal);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.ctrl_LGroupValue);
            this.panel2.Controls.Add(this.ctrl_LTitleGroup);
            this.panel2.Controls.Add(this.ctrl_TBName);
            this.panel2.Controls.Add(this.ctrl_LTitleName);
            this.panel2.Controls.Add(this.ctrl_BCancel);
            this.panel2.Controls.Add(this.ctrl_BOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 185);
            this.panel2.TabIndex = 64;
            // 
            // ctrlCategoriesKlinik
            // 
            this.ctrlCategoriesKlinik.FormattingEnabled = true;
            this.ctrlCategoriesKlinik.Location = new System.Drawing.Point(164, 115);
            this.ctrlCategoriesKlinik.Name = "ctrlCategoriesKlinik";
            this.ctrlCategoriesKlinik.Size = new System.Drawing.Size(192, 21);
            this.ctrlCategoriesKlinik.TabIndex = 75;
            // 
            // ctrlCategoriesTotal
            // 
            this.ctrlCategoriesTotal.FormattingEnabled = true;
            this.ctrlCategoriesTotal.Location = new System.Drawing.Point(164, 90);
            this.ctrlCategoriesTotal.Name = "ctrlCategoriesTotal";
            this.ctrlCategoriesTotal.Size = new System.Drawing.Size(192, 21);
            this.ctrlCategoriesTotal.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Общая группа";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Клиническая группа";
            // 
            // ctrl_LGroupValue
            // 
            this.ctrl_LGroupValue.AutoSize = true;
            this.ctrl_LGroupValue.Location = new System.Drawing.Point(161, 17);
            this.ctrl_LGroupValue.Name = "ctrl_LGroupValue";
            this.ctrl_LGroupValue.Size = new System.Drawing.Size(68, 13);
            this.ctrl_LGroupValue.TabIndex = 70;
            this.ctrl_LGroupValue.Text = "Неизвестно";
            // 
            // ctrl_LTitleGroup
            // 
            this.ctrl_LTitleGroup.AutoSize = true;
            this.ctrl_LTitleGroup.Location = new System.Drawing.Point(14, 17);
            this.ctrl_LTitleGroup.Name = "ctrl_LTitleGroup";
            this.ctrl_LTitleGroup.Size = new System.Drawing.Size(42, 13);
            this.ctrl_LTitleGroup.TabIndex = 69;
            this.ctrl_LTitleGroup.Text = "Группа";
            // 
            // ctrl_TBName
            // 
            this.ctrl_TBName.Location = new System.Drawing.Point(164, 42);
            this.ctrl_TBName.Name = "ctrl_TBName";
            this.ctrl_TBName.Size = new System.Drawing.Size(192, 20);
            this.ctrl_TBName.TabIndex = 68;
            // 
            // ctrl_LTitleName
            // 
            this.ctrl_LTitleName.AutoSize = true;
            this.ctrl_LTitleName.Location = new System.Drawing.Point(14, 45);
            this.ctrl_LTitleName.Name = "ctrl_LTitleName";
            this.ctrl_LTitleName.Size = new System.Drawing.Size(83, 13);
            this.ctrl_LTitleName.TabIndex = 67;
            this.ctrl_LTitleName.Text = "Наименование";
            // 
            // ctrl_BCancel
            // 
            this.ctrl_BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctrl_BCancel.Location = new System.Drawing.Point(268, 151);
            this.ctrl_BCancel.Name = "ctrl_BCancel";
            this.ctrl_BCancel.Size = new System.Drawing.Size(75, 23);
            this.ctrl_BCancel.TabIndex = 23;
            this.ctrl_BCancel.Text = "Отмена";
            this.ctrl_BCancel.UseVisualStyleBackColor = true;
            // 
            // ctrl_BOk
            // 
            this.ctrl_BOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctrl_BOk.Location = new System.Drawing.Point(179, 151);
            this.ctrl_BOk.Name = "ctrl_BOk";
            this.ctrl_BOk.Size = new System.Drawing.Size(75, 23);
            this.ctrl_BOk.TabIndex = 22;
            this.ctrl_BOk.Text = "ОК";
            this.ctrl_BOk.UseVisualStyleBackColor = true;
            // 
            // ctrlTitle
            // 
            this.ctrlTitle.Location = new System.Drawing.Point(164, 66);
            this.ctrlTitle.Name = "ctrlTitle";
            this.ctrlTitle.Size = new System.Drawing.Size(192, 20);
            this.ctrlTitle.TabIndex = 77;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "Заголовок";
            // 
            // Dlg_EditorTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(370, 185);
            this.Controls.Add(this.panel2);
            this.Name = "Dlg_EditorTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dlg_EditorTemplate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dlg_EditorTemplate_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ctrl_BCancel;
        private System.Windows.Forms.Button ctrl_BOk;
        protected internal System.Windows.Forms.Label ctrl_LGroupValue;
        private System.Windows.Forms.Label ctrl_LTitleGroup;
        protected internal System.Windows.Forms.TextBox ctrl_TBName;
        private System.Windows.Forms.Label ctrl_LTitleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox ctrlCategoriesKlinik;
        public System.Windows.Forms.ComboBox ctrlCategoriesTotal;
        public System.Windows.Forms.TextBox ctrlTitle;
        private System.Windows.Forms.Label label3;
    }
}