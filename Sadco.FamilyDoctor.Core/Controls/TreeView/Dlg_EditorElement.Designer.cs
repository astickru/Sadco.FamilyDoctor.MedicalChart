namespace Sadco.FamilyDoctor.Core.Controls
{
    partial class Dlg_EditorElement
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
            this.ctrl_CB_ControlType = new Sadco.FamilyDoctor.Core.Controls.Ctrl_RadioButtonList();
            this.L_TypeDescription = new System.Windows.Forms.Label();
            this.ctrl_LGroupValue = new System.Windows.Forms.Label();
            this.ctrl_LTitleGroup = new System.Windows.Forms.Label();
            this.ctrl_TBName = new System.Windows.Forms.TextBox();
            this.ctrl_LTitleName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrl_TBDecs = new System.Windows.Forms.TextBox();
            this.ctrl_LTitleDesc = new System.Windows.Forms.Label();
            this.ctrl_BCancel = new System.Windows.Forms.Button();
            this.ctrl_BOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.ctrl_CB_ControlType);
            this.panel1.Controls.Add(this.L_TypeDescription);
            this.panel1.Controls.Add(this.ctrl_LGroupValue);
            this.panel1.Controls.Add(this.ctrl_LTitleGroup);
            this.panel1.Controls.Add(this.ctrl_TBName);
            this.panel1.Controls.Add(this.ctrl_LTitleName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 122);
            this.panel1.TabIndex = 63;
            // 
            // ctrl_CB_ControlType
            // 
            this.ctrl_CB_ControlType.BackColor = System.Drawing.SystemColors.Control;
            this.ctrl_CB_ControlType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctrl_CB_ControlType.FormattingEnabled = true;
            this.ctrl_CB_ControlType.Location = new System.Drawing.Point(162, 74);
            this.ctrl_CB_ControlType.Name = "ctrl_CB_ControlType";
            this.ctrl_CB_ControlType.Size = new System.Drawing.Size(192, 45);
            this.ctrl_CB_ControlType.TabIndex = 68;
            // 
            // L_TypeDescription
            // 
            this.L_TypeDescription.AutoSize = true;
            this.L_TypeDescription.Location = new System.Drawing.Point(12, 74);
            this.L_TypeDescription.Name = "L_TypeDescription";
            this.L_TypeDescription.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.L_TypeDescription.Size = new System.Drawing.Size(138, 16);
            this.L_TypeDescription.TabIndex = 67;
            this.L_TypeDescription.Text = "Вид текстового элемента";
            // 
            // ctrl_LGroupValue
            // 
            this.ctrl_LGroupValue.AutoSize = true;
            this.ctrl_LGroupValue.Location = new System.Drawing.Point(159, 17);
            this.ctrl_LGroupValue.Name = "ctrl_LGroupValue";
            this.ctrl_LGroupValue.Size = new System.Drawing.Size(68, 13);
            this.ctrl_LGroupValue.TabIndex = 66;
            this.ctrl_LGroupValue.Text = "Неизвестно";
            // 
            // ctrl_LTitleGroup
            // 
            this.ctrl_LTitleGroup.AutoSize = true;
            this.ctrl_LTitleGroup.Location = new System.Drawing.Point(12, 17);
            this.ctrl_LTitleGroup.Name = "ctrl_LTitleGroup";
            this.ctrl_LTitleGroup.Size = new System.Drawing.Size(42, 13);
            this.ctrl_LTitleGroup.TabIndex = 65;
            this.ctrl_LTitleGroup.Text = "Группа";
            // 
            // ctrl_TBName
            // 
            this.ctrl_TBName.Location = new System.Drawing.Point(162, 43);
            this.ctrl_TBName.Name = "ctrl_TBName";
            this.ctrl_TBName.Size = new System.Drawing.Size(192, 20);
            this.ctrl_TBName.TabIndex = 64;
            // 
            // ctrl_LTitleName
            // 
            this.ctrl_LTitleName.AutoSize = true;
            this.ctrl_LTitleName.Location = new System.Drawing.Point(12, 46);
            this.ctrl_LTitleName.Name = "ctrl_LTitleName";
            this.ctrl_LTitleName.Size = new System.Drawing.Size(83, 13);
            this.ctrl_LTitleName.TabIndex = 63;
            this.ctrl_LTitleName.Text = "Наименование";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctrl_TBDecs);
            this.panel2.Controls.Add(this.ctrl_LTitleDesc);
            this.panel2.Controls.Add(this.ctrl_BCancel);
            this.panel2.Controls.Add(this.ctrl_BOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 89);
            this.panel2.TabIndex = 64;
            // 
            // ctrl_TBDecs
            // 
            this.ctrl_TBDecs.Location = new System.Drawing.Point(162, 8);
            this.ctrl_TBDecs.Name = "ctrl_TBDecs";
            this.ctrl_TBDecs.Size = new System.Drawing.Size(192, 20);
            this.ctrl_TBDecs.TabIndex = 25;
            // 
            // ctrl_LTitleDesc
            // 
            this.ctrl_LTitleDesc.AutoSize = true;
            this.ctrl_LTitleDesc.Location = new System.Drawing.Point(19, 11);
            this.ctrl_LTitleDesc.Name = "ctrl_LTitleDesc";
            this.ctrl_LTitleDesc.Size = new System.Drawing.Size(57, 13);
            this.ctrl_LTitleDesc.TabIndex = 24;
            this.ctrl_LTitleDesc.Text = "Описание";
            // 
            // ctrl_BCancel
            // 
            this.ctrl_BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctrl_BCancel.Location = new System.Drawing.Point(268, 50);
            this.ctrl_BCancel.Name = "ctrl_BCancel";
            this.ctrl_BCancel.Size = new System.Drawing.Size(75, 23);
            this.ctrl_BCancel.TabIndex = 23;
            this.ctrl_BCancel.Text = "Отмена";
            this.ctrl_BCancel.UseVisualStyleBackColor = true;
            // 
            // ctrl_BOk
            // 
            this.ctrl_BOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctrl_BOk.Location = new System.Drawing.Point(179, 50);
            this.ctrl_BOk.Name = "ctrl_BOk";
            this.ctrl_BOk.Size = new System.Drawing.Size(75, 23);
            this.ctrl_BOk.TabIndex = 22;
            this.ctrl_BOk.Text = "ОК";
            this.ctrl_BOk.UseVisualStyleBackColor = true;
            // 
            // Dlg_EditorElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(370, 211);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Dlg_EditorElement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dlg_EditorElement";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label L_TypeDescription;
        protected internal System.Windows.Forms.Label ctrl_LGroupValue;
        private System.Windows.Forms.Label ctrl_LTitleGroup;
        protected internal System.Windows.Forms.TextBox ctrl_TBName;
        private System.Windows.Forms.Label ctrl_LTitleName;
        private System.Windows.Forms.Panel panel2;
        protected internal System.Windows.Forms.TextBox ctrl_TBDecs;
        private System.Windows.Forms.Label ctrl_LTitleDesc;
        private System.Windows.Forms.Button ctrl_BCancel;
        private System.Windows.Forms.Button ctrl_BOk;
        public Ctrl_RadioButtonList ctrl_CB_ControlType;
    }
}