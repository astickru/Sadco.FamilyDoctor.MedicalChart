using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    partial class Ctrl_Template
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
            this.ctrlContent = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // ctrlContent
            // 
            this.ctrlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlContent.Location = new System.Drawing.Point(0, 0);
            this.ctrlContent.Name = "ctrlContent";
            this.ctrlContent.SelectedIndex = 0;
            this.ctrlContent.Size = new System.Drawing.Size(354, 260);
            this.ctrlContent.TabIndex = 0;
            // 
            // Ctrl_Template
            // 
            this.AutoSize = true;
            this.Controls.Add(this.ctrlContent);
            this.Name = "Ctrl_Template";
            this.Size = new System.Drawing.Size(354, 260);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl ctrlContent;
    }
}
