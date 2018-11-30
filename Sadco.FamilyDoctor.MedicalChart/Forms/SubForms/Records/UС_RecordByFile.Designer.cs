namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    partial class UС_RecordByFile
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlBAdd = new System.Windows.Forms.Button();
            this.ctrlLFilePath = new System.Windows.Forms.Label();
            this.ctrlLMKB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlBAdd
            // 
            this.ctrlBAdd.Location = new System.Drawing.Point(7, 8);
            this.ctrlBAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlBAdd.Name = "ctrlBAdd";
            this.ctrlBAdd.Size = new System.Drawing.Size(168, 22);
            this.ctrlBAdd.TabIndex = 97;
            this.ctrlBAdd.Text = "загрузить файл";
            this.ctrlBAdd.UseVisualStyleBackColor = true;
            this.ctrlBAdd.Click += new System.EventHandler(this.ctrlBAdd_Click);
            // 
            // ctrlLFilePath
            // 
            this.ctrlLFilePath.AutoSize = true;
            this.ctrlLFilePath.Location = new System.Drawing.Point(11, 36);
            this.ctrlLFilePath.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlLFilePath.Name = "ctrlLFilePath";
            this.ctrlLFilePath.Size = new System.Drawing.Size(13, 13);
            this.ctrlLFilePath.TabIndex = 96;
            this.ctrlLFilePath.Text = "-";
            // 
            // ctrlLMKB
            // 
            this.ctrlLMKB.AutoSize = true;
            this.ctrlLMKB.Location = new System.Drawing.Point(11, 64);
            this.ctrlLMKB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ctrlLMKB.Name = "ctrlLMKB";
            this.ctrlLMKB.Size = new System.Drawing.Size(0, 13);
            this.ctrlLMKB.TabIndex = 99;
            // 
            // UС_RecordByFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlLMKB);
            this.Controls.Add(this.ctrlBAdd);
            this.Controls.Add(this.ctrlLFilePath);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "UС_RecordByFile";
            this.Size = new System.Drawing.Size(671, 92);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ctrlBAdd;
        private System.Windows.Forms.Label ctrlLFilePath;
        private System.Windows.Forms.Label ctrlLMKB;
    }
}
