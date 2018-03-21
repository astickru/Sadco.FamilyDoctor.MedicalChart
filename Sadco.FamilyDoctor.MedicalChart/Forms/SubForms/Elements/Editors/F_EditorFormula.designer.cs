namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    partial class F_EditorFormula
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
			this.ctrlCBAddElement = new System.Windows.Forms.ComboBox();
			this.ctrlBClear = new System.Windows.Forms.Button();
			this.ctrlBCancel = new System.Windows.Forms.Button();
			this.ctrlBEdit = new System.Windows.Forms.Button();
			this.ctrlBAddValue = new System.Windows.Forms.Button();
			this.ctrlTBValue = new System.Windows.Forms.TextBox();
			this.ctrlBMultiply = new System.Windows.Forms.Button();
			this.ctrlBCarve = new System.Windows.Forms.Button();
			this.ctrlBMinus = new System.Windows.Forms.Button();
			this.ctrlBPlus = new System.Windows.Forms.Button();
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
			this.panel2.Controls.Add(this.ctrlCBAddElement);
			this.panel2.Controls.Add(this.ctrlBClear);
			this.panel2.Controls.Add(this.ctrlBCancel);
			this.panel2.Controls.Add(this.ctrlBEdit);
			this.panel2.Controls.Add(this.ctrlBAddValue);
			this.panel2.Controls.Add(this.ctrlTBValue);
			this.panel2.Controls.Add(this.ctrlBMultiply);
			this.panel2.Controls.Add(this.ctrlBCarve);
			this.panel2.Controls.Add(this.ctrlBMinus);
			this.panel2.Controls.Add(this.ctrlBPlus);
			this.panel2.Controls.Add(this.ctrlRTBFormula);
			this.panel2.Controls.Add(this.ctrlBDelLastAction);
			this.panel2.Controls.Add(this.ctrlBAddTag);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(5, 5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(599, 320);
			this.panel2.TabIndex = 2;
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
			this.ctrlBClear.Location = new System.Drawing.Point(518, 176);
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
			this.ctrlBAddValue.Location = new System.Drawing.Point(569, 146);
			this.ctrlBAddValue.Name = "ctrlBAddValue";
			this.ctrlBAddValue.Size = new System.Drawing.Size(24, 23);
			this.ctrlBAddValue.TabIndex = 10;
			this.ctrlBAddValue.UseVisualStyleBackColor = true;
			this.ctrlBAddValue.Click += new System.EventHandler(this.ctrlBAddValue_Click);
			// 
			// ctrlTBValue
			// 
			this.ctrlTBValue.Location = new System.Drawing.Point(519, 148);
			this.ctrlTBValue.Name = "ctrlTBValue";
			this.ctrlTBValue.Size = new System.Drawing.Size(49, 20);
			this.ctrlTBValue.TabIndex = 9;
			this.ctrlTBValue.Text = "10";
			// 
			// ctrlBMultiply
			// 
			this.ctrlBMultiply.Location = new System.Drawing.Point(518, 118);
			this.ctrlBMultiply.Name = "ctrlBMultiply";
			this.ctrlBMultiply.Size = new System.Drawing.Size(75, 23);
			this.ctrlBMultiply.TabIndex = 7;
			this.ctrlBMultiply.Tag = "";
			this.ctrlBMultiply.Text = "умножить";
			this.ctrlBMultiply.UseVisualStyleBackColor = true;
			this.ctrlBMultiply.Click += new System.EventHandler(this.addAction_Click);
			// 
			// ctrlBCarve
			// 
			this.ctrlBCarve.Location = new System.Drawing.Point(518, 89);
			this.ctrlBCarve.Name = "ctrlBCarve";
			this.ctrlBCarve.Size = new System.Drawing.Size(75, 23);
			this.ctrlBCarve.TabIndex = 6;
			this.ctrlBCarve.Tag = "";
			this.ctrlBCarve.Text = "разделить";
			this.ctrlBCarve.UseVisualStyleBackColor = true;
			this.ctrlBCarve.Click += new System.EventHandler(this.addAction_Click);
			// 
			// ctrlBMinus
			// 
			this.ctrlBMinus.Location = new System.Drawing.Point(518, 60);
			this.ctrlBMinus.Name = "ctrlBMinus";
			this.ctrlBMinus.Size = new System.Drawing.Size(75, 23);
			this.ctrlBMinus.TabIndex = 5;
			this.ctrlBMinus.Tag = "";
			this.ctrlBMinus.Text = "минус";
			this.ctrlBMinus.UseVisualStyleBackColor = true;
			this.ctrlBMinus.Click += new System.EventHandler(this.addAction_Click);
			// 
			// ctrlBPlus
			// 
			this.ctrlBPlus.Location = new System.Drawing.Point(518, 31);
			this.ctrlBPlus.Name = "ctrlBPlus";
			this.ctrlBPlus.Size = new System.Drawing.Size(75, 23);
			this.ctrlBPlus.TabIndex = 4;
			this.ctrlBPlus.Tag = "";
			this.ctrlBPlus.Text = "плюс";
			this.ctrlBPlus.UseVisualStyleBackColor = true;
			this.ctrlBPlus.Click += new System.EventHandler(this.addAction_Click);
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
			// F_EditorFormula
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(609, 330);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.Name = "F_EditorFormula";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "F_EditorFormula";
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
        private System.Windows.Forms.Button ctrlBMultiply;
        private System.Windows.Forms.Button ctrlBCarve;
        private System.Windows.Forms.Button ctrlBMinus;
        private System.Windows.Forms.Button ctrlBPlus;
        private System.Windows.Forms.RichTextBox ctrlRTBFormula;
        private System.Windows.Forms.Button ctrlBDelLastAction;
        private System.Windows.Forms.Button ctrlBAddTag;
    }
}

