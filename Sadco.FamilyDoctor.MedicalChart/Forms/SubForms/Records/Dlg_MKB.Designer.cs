namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    partial class Dlg_MKB
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
            this.ctrlTBMKB1 = new System.Windows.Forms.TextBox();
            this.ctrlTBMKB2 = new System.Windows.Forms.TextBox();
            this.ctrlTBMKB4 = new System.Windows.Forms.TextBox();
            this.ctrlTBMKB3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlTBMKB1
            // 
            this.ctrlTBMKB1.Location = new System.Drawing.Point(16, 12);
            this.ctrlTBMKB1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlTBMKB1.Name = "ctrlTBMKB1";
            this.ctrlTBMKB1.Size = new System.Drawing.Size(316, 21);
            this.ctrlTBMKB1.TabIndex = 0;
            // 
            // ctrlTBMKB2
            // 
            this.ctrlTBMKB2.Location = new System.Drawing.Point(16, 38);
            this.ctrlTBMKB2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlTBMKB2.Name = "ctrlTBMKB2";
            this.ctrlTBMKB2.Size = new System.Drawing.Size(316, 21);
            this.ctrlTBMKB2.TabIndex = 1;
            // 
            // ctrlTBMKB4
            // 
            this.ctrlTBMKB4.Location = new System.Drawing.Point(16, 90);
            this.ctrlTBMKB4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlTBMKB4.Name = "ctrlTBMKB4";
            this.ctrlTBMKB4.Size = new System.Drawing.Size(316, 21);
            this.ctrlTBMKB4.TabIndex = 2;
            // 
            // ctrlTBMKB3
            // 
            this.ctrlTBMKB3.Location = new System.Drawing.Point(16, 64);
            this.ctrlTBMKB3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlTBMKB3.Name = "ctrlTBMKB3";
            this.ctrlTBMKB3.Size = new System.Drawing.Size(316, 21);
            this.ctrlTBMKB3.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(183, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "ok";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(263, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "отмена";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Dlg_MKB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 166);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrlTBMKB3);
            this.Controls.Add(this.ctrlTBMKB4);
            this.Controls.Add(this.ctrlTBMKB2);
            this.Controls.Add(this.ctrlTBMKB1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dlg_MKB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "МКБ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox ctrlTBMKB1;
        public System.Windows.Forms.TextBox ctrlTBMKB2;
        public System.Windows.Forms.TextBox ctrlTBMKB4;
        public System.Windows.Forms.TextBox ctrlTBMKB3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}