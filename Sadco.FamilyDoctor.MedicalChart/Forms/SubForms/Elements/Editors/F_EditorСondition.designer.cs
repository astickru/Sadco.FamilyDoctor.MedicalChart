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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrlBOperOr = new System.Windows.Forms.Button();
            this.ctrlBOperAnd = new System.Windows.Forms.Button();
            this.ctrlBOperNotEquals = new System.Windows.Forms.Button();
            this.ctrlBOperEquals = new System.Windows.Forms.Button();
            this.ctrlBOperLess = new System.Windows.Forms.Button();
            this.ctrlBOperMore = new System.Windows.Forms.Button();
            this.ctrlCBAddElement = new System.Windows.Forms.ComboBox();
            this.ctrlBClear = new System.Windows.Forms.Button();
            this.ctrlBCancel = new System.Windows.Forms.Button();
            this.ctrlBEdit = new System.Windows.Forms.Button();
            this.ctrlBAddValue = new System.Windows.Forms.Button();
            this.ctrlTBValue = new System.Windows.Forms.TextBox();
            this.ctrlRTBFormula = new System.Windows.Forms.RichTextBox();
            this.ctrlBDelLastAction = new System.Windows.Forms.Button();
            this.ctrlBAddTag = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(609, 330);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctrlBOperOr);
            this.panel2.Controls.Add(this.ctrlBOperAnd);
            this.panel2.Controls.Add(this.ctrlBOperNotEquals);
            this.panel2.Controls.Add(this.ctrlBOperEquals);
            this.panel2.Controls.Add(this.ctrlBOperLess);
            this.panel2.Controls.Add(this.ctrlBOperMore);
            this.panel2.Controls.Add(this.ctrlCBAddElement);
            this.panel2.Controls.Add(this.ctrlBClear);
            this.panel2.Controls.Add(this.ctrlBCancel);
            this.panel2.Controls.Add(this.ctrlBEdit);
            this.panel2.Controls.Add(this.ctrlBAddValue);
            this.panel2.Controls.Add(this.ctrlTBValue);
            this.panel2.Controls.Add(this.ctrlRTBFormula);
            this.panel2.Controls.Add(this.ctrlBDelLastAction);
            this.panel2.Controls.Add(this.ctrlBAddTag);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(599, 320);
            this.panel2.TabIndex = 2;
            // 
            // ctrlBOperOr
            // 
            this.ctrlBOperOr.Location = new System.Drawing.Point(519, 177);
            this.ctrlBOperOr.Name = "ctrlBOperOr";
            this.ctrlBOperOr.Size = new System.Drawing.Size(75, 23);
            this.ctrlBOperOr.TabIndex = 22;
            this.ctrlBOperOr.Tag = "ИЛИ";
            this.ctrlBOperOr.Text = "или";
            this.ctrlBOperOr.UseVisualStyleBackColor = true;
            this.ctrlBOperOr.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperAnd
            // 
            this.ctrlBOperAnd.Location = new System.Drawing.Point(519, 148);
            this.ctrlBOperAnd.Name = "ctrlBOperAnd";
            this.ctrlBOperAnd.Size = new System.Drawing.Size(75, 23);
            this.ctrlBOperAnd.TabIndex = 21;
            this.ctrlBOperAnd.Tag = "И";
            this.ctrlBOperAnd.Text = "и";
            this.ctrlBOperAnd.UseVisualStyleBackColor = true;
            this.ctrlBOperAnd.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperNotEquals
            // 
            this.ctrlBOperNotEquals.Location = new System.Drawing.Point(519, 119);
            this.ctrlBOperNotEquals.Name = "ctrlBOperNotEquals";
            this.ctrlBOperNotEquals.Size = new System.Drawing.Size(75, 23);
            this.ctrlBOperNotEquals.TabIndex = 20;
            this.ctrlBOperNotEquals.Tag = "!=";
            this.ctrlBOperNotEquals.Text = "не равно";
            this.ctrlBOperNotEquals.UseVisualStyleBackColor = true;
            this.ctrlBOperNotEquals.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperEquals
            // 
            this.ctrlBOperEquals.Location = new System.Drawing.Point(519, 90);
            this.ctrlBOperEquals.Name = "ctrlBOperEquals";
            this.ctrlBOperEquals.Size = new System.Drawing.Size(75, 23);
            this.ctrlBOperEquals.TabIndex = 19;
            this.ctrlBOperEquals.Tag = "=";
            this.ctrlBOperEquals.Text = "равно";
            this.ctrlBOperEquals.UseVisualStyleBackColor = true;
            this.ctrlBOperEquals.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperLess
            // 
            this.ctrlBOperLess.Location = new System.Drawing.Point(519, 61);
            this.ctrlBOperLess.Name = "ctrlBOperLess";
            this.ctrlBOperLess.Size = new System.Drawing.Size(75, 23);
            this.ctrlBOperLess.TabIndex = 18;
            this.ctrlBOperLess.Tag = "<";
            this.ctrlBOperLess.Text = "меньше";
            this.ctrlBOperLess.UseVisualStyleBackColor = true;
            this.ctrlBOperLess.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlBOperMore
            // 
            this.ctrlBOperMore.Location = new System.Drawing.Point(519, 32);
            this.ctrlBOperMore.Name = "ctrlBOperMore";
            this.ctrlBOperMore.Size = new System.Drawing.Size(75, 23);
            this.ctrlBOperMore.TabIndex = 17;
            this.ctrlBOperMore.Tag = ">";
            this.ctrlBOperMore.Text = "больше";
            this.ctrlBOperMore.UseVisualStyleBackColor = true;
            this.ctrlBOperMore.Click += new System.EventHandler(this.addAction_Click);
            // 
            // ctrlCBAddElement
            // 
            this.ctrlCBAddElement.FormattingEnabled = true;
            this.ctrlCBAddElement.Location = new System.Drawing.Point(3, 3);
            this.ctrlCBAddElement.Name = "ctrlCBAddElement";
            this.ctrlCBAddElement.Size = new System.Drawing.Size(226, 21);
            this.ctrlCBAddElement.TabIndex = 16;
            // 
            // ctrlBClear
            // 
            this.ctrlBClear.Location = new System.Drawing.Point(518, 253);
            this.ctrlBClear.Name = "ctrlBClear";
            this.ctrlBClear.Size = new System.Drawing.Size(75, 23);
            this.ctrlBClear.TabIndex = 13;
            this.ctrlBClear.Tag = "*";
            this.ctrlBClear.Text = "очистить";
            this.ctrlBClear.UseVisualStyleBackColor = true;
            this.ctrlBClear.Click += new System.EventHandler(this.ctrlBClear_Click);
            // 
            // ctrlBCancel
            // 
            this.ctrlBCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctrlBCancel.Location = new System.Drawing.Point(515, 285);
            this.ctrlBCancel.Name = "ctrlBCancel";
            this.ctrlBCancel.Size = new System.Drawing.Size(78, 28);
            this.ctrlBCancel.TabIndex = 12;
            this.ctrlBCancel.Text = "Отмена";
            this.ctrlBCancel.UseVisualStyleBackColor = true;
            // 
            // ctrlBEdit
            // 
            this.ctrlBEdit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctrlBEdit.Location = new System.Drawing.Point(431, 285);
            this.ctrlBEdit.Name = "ctrlBEdit";
            this.ctrlBEdit.Size = new System.Drawing.Size(78, 28);
            this.ctrlBEdit.TabIndex = 11;
            this.ctrlBEdit.Text = "Изменить";
            this.ctrlBEdit.UseVisualStyleBackColor = true;
            // 
            // ctrlBAddValue
            // 
            this.ctrlBAddValue.Location = new System.Drawing.Point(569, 223);
            this.ctrlBAddValue.Name = "ctrlBAddValue";
            this.ctrlBAddValue.Size = new System.Drawing.Size(24, 23);
            this.ctrlBAddValue.TabIndex = 10;
            this.ctrlBAddValue.UseVisualStyleBackColor = true;
            this.ctrlBAddValue.Click += new System.EventHandler(this.ctrlBAddValue_Click);
            // 
            // ctrlTBValue
            // 
            this.ctrlTBValue.Location = new System.Drawing.Point(519, 225);
            this.ctrlTBValue.Name = "ctrlTBValue";
            this.ctrlTBValue.Size = new System.Drawing.Size(49, 20);
            this.ctrlTBValue.TabIndex = 9;
            this.ctrlTBValue.Text = "10";
            // 
            // ctrlRTBFormula
            // 
            this.ctrlRTBFormula.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ctrlRTBFormula.Location = new System.Drawing.Point(3, 29);
            this.ctrlRTBFormula.Name = "ctrlRTBFormula";
            this.ctrlRTBFormula.ReadOnly = true;
            this.ctrlRTBFormula.Size = new System.Drawing.Size(509, 250);
            this.ctrlRTBFormula.TabIndex = 3;
            this.ctrlRTBFormula.Text = "";
            // 
            // ctrlBDelLastAction
            // 
            this.ctrlBDelLastAction.Location = new System.Drawing.Point(518, 2);
            this.ctrlBDelLastAction.Name = "ctrlBDelLastAction";
            this.ctrlBDelLastAction.Size = new System.Drawing.Size(75, 23);
            this.ctrlBDelLastAction.TabIndex = 2;
            this.ctrlBDelLastAction.Text = "Удалить";
            this.ctrlBDelLastAction.UseVisualStyleBackColor = true;
            this.ctrlBDelLastAction.Click += new System.EventHandler(this.ctrlBDelLastAction_Click);
            // 
            // ctrlBAddTag
            // 
            this.ctrlBAddTag.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.ctrlBAddTag.Location = new System.Drawing.Point(235, 2);
            this.ctrlBAddTag.Name = "ctrlBAddTag";
            this.ctrlBAddTag.Size = new System.Drawing.Size(75, 23);
            this.ctrlBAddTag.TabIndex = 1;
            this.ctrlBAddTag.Text = "Добавить";
            this.ctrlBAddTag.UseVisualStyleBackColor = true;
            this.ctrlBAddTag.Click += new System.EventHandler(this.ctrlBAddTag_Click);
            // 
            // F_EditorСondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 330);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "F_EditorСondition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_EditorСondition";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
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
    }
}

