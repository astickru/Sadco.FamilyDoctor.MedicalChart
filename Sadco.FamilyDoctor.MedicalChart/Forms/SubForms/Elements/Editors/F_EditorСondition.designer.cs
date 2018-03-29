namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    partial class F_EditorСondition
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Main = new System.Windows.Forms.Panel();
            this.panel_Editor = new System.Windows.Forms.Panel();
            this.ctrlRTBFormula = new System.Windows.Forms.RichTextBox();
            this.panel_Actions = new System.Windows.Forms.Panel();
            this.ctrlStandartValues = new Sadco.FamilyDoctor.Core.Controls.Ctrl_SeparatorCombobox();
            this.ctrlBDelLastAction = new System.Windows.Forms.Button();
            this.ctrlCBAddElement = new System.Windows.Forms.ComboBox();
            this.ctrlBAddTag = new System.Windows.Forms.Button();
            this.ctrlBClear = new System.Windows.Forms.Button();
            this.panel_Operators = new System.Windows.Forms.Panel();
            this.ctrlBOperEquals = new System.Windows.Forms.Button();
            this.ctrlBOperNotEquals = new System.Windows.Forms.Button();
            this.ctrlBOperAnd = new System.Windows.Forms.Button();
            this.ctrlBOperMore = new System.Windows.Forms.Button();
            this.ctrlBAddValue = new System.Windows.Forms.Button();
            this.ctrlTBValue = new System.Windows.Forms.TextBox();
            this.ctrlBOperLess = new System.Windows.Forms.Button();
            this.ctrlBOperOr = new System.Windows.Forms.Button();
            this.panel_Result = new System.Windows.Forms.Panel();
            this.ctrlBEdit = new System.Windows.Forms.Button();
            this.ctrlBCancel = new System.Windows.Forms.Button();
            this.panel_Main.SuspendLayout();
            this.panel_Editor.SuspendLayout();
            this.panel_Actions.SuspendLayout();
            this.panel_Operators.SuspendLayout();
            this.panel_Result.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Main
            // 
            this.panel_Main.Controls.Add(this.panel_Editor);
            this.panel_Main.Controls.Add(this.panel_Actions);
            this.panel_Main.Controls.Add(this.panel_Operators);
            this.panel_Main.Controls.Add(this.panel_Result);
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(7, 5);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.panel_Main.Size = new System.Drawing.Size(764, 251);
            this.panel_Main.TabIndex = 1;
            // 
            // panel_Editor
            // 
            this.panel_Editor.Controls.Add(this.ctrlRTBFormula);
            this.panel_Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Editor.Location = new System.Drawing.Point(7, 34);
            this.panel_Editor.Name = "panel_Editor";
            this.panel_Editor.Size = new System.Drawing.Size(750, 151);
            this.panel_Editor.TabIndex = 17;
            // 
            // ctrlRTBFormula
            // 
            this.ctrlRTBFormula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlRTBFormula.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ctrlRTBFormula.Location = new System.Drawing.Point(0, 0);
            this.ctrlRTBFormula.Name = "ctrlRTBFormula";
            this.ctrlRTBFormula.ReadOnly = true;
            this.ctrlRTBFormula.Size = new System.Drawing.Size(750, 151);
            this.ctrlRTBFormula.TabIndex = 3;
            this.ctrlRTBFormula.Text = "";
            // 
            // panel_Actions
            // 
            this.panel_Actions.Controls.Add(this.ctrlStandartValues);
            this.panel_Actions.Controls.Add(this.ctrlBDelLastAction);
            this.panel_Actions.Controls.Add(this.ctrlCBAddElement);
            this.panel_Actions.Controls.Add(this.ctrlBAddTag);
            this.panel_Actions.Controls.Add(this.ctrlBClear);
            this.panel_Actions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Actions.Location = new System.Drawing.Point(7, 5);
            this.panel_Actions.Name = "panel_Actions";
            this.panel_Actions.Size = new System.Drawing.Size(750, 29);
            this.panel_Actions.TabIndex = 2;
            // 
            // ctrlStandartValues
            // 
            this.ctrlStandartValues.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ctrlStandartValues.FormattingEnabled = true;
            this.ctrlStandartValues.Location = new System.Drawing.Point(340, 3);
            this.ctrlStandartValues.Name = "ctrlStandartValues";
            this.ctrlStandartValues.p_AutoAdjustItemHeight = false;
            this.ctrlStandartValues.p_SeparatorColor = System.Drawing.Color.Black;
            this.ctrlStandartValues.p_SeparatorMargin = 1;
            this.ctrlStandartValues.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.ctrlStandartValues.p_SeparatorWidth = 1;
            this.ctrlStandartValues.Size = new System.Drawing.Size(192, 22);
            this.ctrlStandartValues.TabIndex = 80;
            this.ctrlStandartValues.SelectedIndexChanged += new System.EventHandler(this.ctrlStandartValues_SelectedIndexChanged);
            // 
            // ctrlBDelLastAction
            // 
            this.ctrlBDelLastAction.Location = new System.Drawing.Point(538, 3);
            this.ctrlBDelLastAction.Name = "ctrlBDelLastAction";
            this.ctrlBDelLastAction.Size = new System.Drawing.Size(99, 23);
            this.ctrlBDelLastAction.TabIndex = 2;
            this.ctrlBDelLastAction.Text = "удалить";
            this.ctrlBDelLastAction.UseVisualStyleBackColor = true;
            this.ctrlBDelLastAction.Click += new System.EventHandler(this.ctrlBDelLastAction_Click);
            // 
            // ctrlCBAddElement
            // 
            this.ctrlCBAddElement.FormattingEnabled = true;
            this.ctrlCBAddElement.Location = new System.Drawing.Point(3, 4);
            this.ctrlCBAddElement.Name = "ctrlCBAddElement";
            this.ctrlCBAddElement.Size = new System.Drawing.Size(226, 21);
            this.ctrlCBAddElement.TabIndex = 16;
            // 
            // ctrlBAddTag
            // 
            this.ctrlBAddTag.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.ctrlBAddTag.Location = new System.Drawing.Point(235, 3);
            this.ctrlBAddTag.Name = "ctrlBAddTag";
            this.ctrlBAddTag.Size = new System.Drawing.Size(99, 23);
            this.ctrlBAddTag.TabIndex = 1;
            this.ctrlBAddTag.Text = "добавить";
            this.ctrlBAddTag.UseVisualStyleBackColor = true;
            this.ctrlBAddTag.Click += new System.EventHandler(this.ctrlBAddTag_Click);
            // 
            // ctrlBClear
            // 
            this.ctrlBClear.Location = new System.Drawing.Point(647, 3);
            this.ctrlBClear.Name = "ctrlBClear";
            this.ctrlBClear.Size = new System.Drawing.Size(99, 23);
            this.ctrlBClear.TabIndex = 13;
            this.ctrlBClear.Tag = "*";
            this.ctrlBClear.Text = "очистить";
            this.ctrlBClear.UseVisualStyleBackColor = true;
            this.ctrlBClear.Click += new System.EventHandler(this.ctrlBClear_Click);
            // 
            // panel_Operators
            // 
            this.panel_Operators.Controls.Add(this.ctrlBOperEquals);
            this.panel_Operators.Controls.Add(this.ctrlBOperNotEquals);
            this.panel_Operators.Controls.Add(this.ctrlBOperAnd);
            this.panel_Operators.Controls.Add(this.ctrlBOperMore);
            this.panel_Operators.Controls.Add(this.ctrlBAddValue);
            this.panel_Operators.Controls.Add(this.ctrlTBValue);
            this.panel_Operators.Controls.Add(this.ctrlBOperLess);
            this.panel_Operators.Controls.Add(this.ctrlBOperOr);
            this.panel_Operators.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Operators.Location = new System.Drawing.Point(7, 185);
            this.panel_Operators.Name = "panel_Operators";
            this.panel_Operators.Size = new System.Drawing.Size(750, 30);
            this.panel_Operators.TabIndex = 4;
            // 
            // ctrlBOperEquals
            // 
            this.ctrlBOperEquals.Location = new System.Drawing.Point(0, 0);
            this.ctrlBOperEquals.Name = "ctrlBOperEquals";
            this.ctrlBOperEquals.Size = new System.Drawing.Size(80, 23);
            this.ctrlBOperEquals.TabIndex = 19;
            this.ctrlBOperEquals.Tag = "";
            this.ctrlBOperEquals.Text = "равно";
            this.ctrlBOperEquals.UseVisualStyleBackColor = true;
            this.ctrlBOperEquals.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperNotEquals
            // 
            this.ctrlBOperNotEquals.Location = new System.Drawing.Point(88, 0);
            this.ctrlBOperNotEquals.Name = "ctrlBOperNotEquals";
            this.ctrlBOperNotEquals.Size = new System.Drawing.Size(80, 23);
            this.ctrlBOperNotEquals.TabIndex = 20;
            this.ctrlBOperNotEquals.Tag = "";
            this.ctrlBOperNotEquals.Text = "не равно";
            this.ctrlBOperNotEquals.UseVisualStyleBackColor = true;
            this.ctrlBOperNotEquals.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperAnd
            // 
            this.ctrlBOperAnd.Location = new System.Drawing.Point(497, 0);
            this.ctrlBOperAnd.Name = "ctrlBOperAnd";
            this.ctrlBOperAnd.Size = new System.Drawing.Size(80, 23);
            this.ctrlBOperAnd.TabIndex = 21;
            this.ctrlBOperAnd.Tag = "";
            this.ctrlBOperAnd.Text = "и";
            this.ctrlBOperAnd.UseVisualStyleBackColor = true;
            this.ctrlBOperAnd.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperMore
            // 
            this.ctrlBOperMore.Location = new System.Drawing.Point(176, 0);
            this.ctrlBOperMore.Name = "ctrlBOperMore";
            this.ctrlBOperMore.Size = new System.Drawing.Size(80, 23);
            this.ctrlBOperMore.TabIndex = 17;
            this.ctrlBOperMore.Tag = "";
            this.ctrlBOperMore.Text = "больше";
            this.ctrlBOperMore.UseVisualStyleBackColor = true;
            this.ctrlBOperMore.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBAddValue
            // 
            this.ctrlBAddValue.Location = new System.Drawing.Point(667, 0);
            this.ctrlBAddValue.Name = "ctrlBAddValue";
            this.ctrlBAddValue.Size = new System.Drawing.Size(80, 23);
            this.ctrlBAddValue.TabIndex = 10;
            this.ctrlBAddValue.Text = "число";
            this.ctrlBAddValue.UseVisualStyleBackColor = true;
            this.ctrlBAddValue.Click += new System.EventHandler(this.ctrlBAddValue_Click);
            // 
            // ctrlTBValue
            // 
            this.ctrlTBValue.Location = new System.Drawing.Point(610, 1);
            this.ctrlTBValue.Name = "ctrlTBValue";
            this.ctrlTBValue.Size = new System.Drawing.Size(52, 21);
            this.ctrlTBValue.TabIndex = 9;
            this.ctrlTBValue.Text = "10";
            // 
            // ctrlBOperLess
            // 
            this.ctrlBOperLess.Location = new System.Drawing.Point(264, 0);
            this.ctrlBOperLess.Name = "ctrlBOperLess";
            this.ctrlBOperLess.Size = new System.Drawing.Size(80, 23);
            this.ctrlBOperLess.TabIndex = 18;
            this.ctrlBOperLess.Tag = "";
            this.ctrlBOperLess.Text = "меньше";
            this.ctrlBOperLess.UseVisualStyleBackColor = true;
            this.ctrlBOperLess.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperOr
            // 
            this.ctrlBOperOr.Location = new System.Drawing.Point(409, 0);
            this.ctrlBOperOr.Name = "ctrlBOperOr";
            this.ctrlBOperOr.Size = new System.Drawing.Size(80, 23);
            this.ctrlBOperOr.TabIndex = 22;
            this.ctrlBOperOr.Tag = "";
            this.ctrlBOperOr.Text = "или";
            this.ctrlBOperOr.UseVisualStyleBackColor = true;
            this.ctrlBOperOr.Click += new System.EventHandler(this.addAction_Click);
            // 
            // panel_Result
            // 
            this.panel_Result.AutoSize = true;
            this.panel_Result.Controls.Add(this.ctrlBEdit);
            this.panel_Result.Controls.Add(this.ctrlBCancel);
            this.panel_Result.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Result.Location = new System.Drawing.Point(7, 215);
            this.panel_Result.Name = "panel_Result";
            this.panel_Result.Size = new System.Drawing.Size(750, 31);
            this.panel_Result.TabIndex = 17;
            // 
            // ctrlBEdit
            // 
            this.ctrlBEdit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctrlBEdit.Location = new System.Drawing.Point(530, 0);
            this.ctrlBEdit.Name = "ctrlBEdit";
            this.ctrlBEdit.Size = new System.Drawing.Size(104, 28);
            this.ctrlBEdit.TabIndex = 11;
            this.ctrlBEdit.Text = "изменить";
            this.ctrlBEdit.UseVisualStyleBackColor = true;
            // 
            // ctrlBCancel
            // 
            this.ctrlBCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctrlBCancel.Location = new System.Drawing.Point(642, 0);
            this.ctrlBCancel.Name = "ctrlBCancel";
            this.ctrlBCancel.Size = new System.Drawing.Size(104, 28);
            this.ctrlBCancel.TabIndex = 12;
            this.ctrlBCancel.Text = "отмена";
            this.ctrlBCancel.UseVisualStyleBackColor = true;
            // 
            // F_EditorСondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 261);
            this.Controls.Add(this.panel_Main);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2128, 1000);
            this.Name = "F_EditorСondition";
            this.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_EditorСondition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_EditorСondition_FormClosing);
            this.panel_Main.ResumeLayout(false);
            this.panel_Main.PerformLayout();
            this.panel_Editor.ResumeLayout(false);
            this.panel_Actions.ResumeLayout(false);
            this.panel_Operators.ResumeLayout(false);
            this.panel_Operators.PerformLayout();
            this.panel_Result.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.Panel panel_Actions;
        private System.Windows.Forms.ComboBox ctrlCBAddElement;
        private System.Windows.Forms.Button ctrlBClear;
        private System.Windows.Forms.Button ctrlBCancel;
        private System.Windows.Forms.Button ctrlBEdit;
        private System.Windows.Forms.Button ctrlBAddValue;
        private System.Windows.Forms.TextBox ctrlTBValue;
        private System.Windows.Forms.RichTextBox ctrlRTBFormula;
        private System.Windows.Forms.Button ctrlBDelLastAction;
        private System.Windows.Forms.Button ctrlBAddTag;
        private System.Windows.Forms.Button ctrlBOperOr;
        private System.Windows.Forms.Button ctrlBOperAnd;
        private System.Windows.Forms.Button ctrlBOperNotEquals;
        private System.Windows.Forms.Button ctrlBOperEquals;
        private System.Windows.Forms.Button ctrlBOperLess;
        private System.Windows.Forms.Button ctrlBOperMore;
		private System.Windows.Forms.Panel panel_Result;
		private System.Windows.Forms.Panel panel_Editor;
		private System.Windows.Forms.Panel panel_Operators;
        private Core.Controls.Ctrl_SeparatorCombobox ctrlStandartValues;
    }
}

