namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class Dlg_RatingViewer
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrlBReRate = new System.Windows.Forms.Button();
            this.ctrlBSave = new System.Windows.Forms.Button();
            this.p_Elements = new System.Windows.Forms.Panel();
            this.ctrl_TRatings = new System.Windows.Forms.DataGridView();
            this.p_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlTBComment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ctrlRBValue_1 = new System.Windows.Forms.RadioButton();
            this.ctrlRBValue_2 = new System.Windows.Forms.RadioButton();
            this.ctrlRBValue_3 = new System.Windows.Forms.RadioButton();
            this.ctrlRBValue_4 = new System.Windows.Forms.RadioButton();
            this.ctrlRBValue_5 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlLDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlLAuthor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.p_Buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2.SuspendLayout();
            this.p_Elements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TRatings)).BeginInit();
            this.p_Buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.p_Buttons);
            this.panel2.Controls.Add(this.p_Elements);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(571, 574);
            this.panel2.TabIndex = 67;
            // 
            // ctrlBReRate
            // 
            this.ctrlBReRate.Location = new System.Drawing.Point(475, 3);
            this.ctrlBReRate.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlBReRate.Name = "ctrlBReRate";
            this.ctrlBReRate.Size = new System.Drawing.Size(90, 25);
            this.ctrlBReRate.TabIndex = 18;
            this.ctrlBReRate.Text = "оценить";
            this.ctrlBReRate.UseVisualStyleBackColor = true;
            // 
            // ctrlBSave
            // 
            this.ctrlBSave.Location = new System.Drawing.Point(370, 3);
            this.ctrlBSave.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ctrlBSave.Name = "ctrlBSave";
            this.ctrlBSave.Size = new System.Drawing.Size(90, 25);
            this.ctrlBSave.TabIndex = 17;
            this.ctrlBSave.Text = "сохранить";
            this.ctrlBSave.UseVisualStyleBackColor = true;
            // 
            // p_Elements
            // 
            this.p_Elements.Controls.Add(this.ctrl_TRatings);
            this.p_Elements.Controls.Add(this.ctrlTBComment);
            this.p_Elements.Controls.Add(this.label4);
            this.p_Elements.Controls.Add(this.label5);
            this.p_Elements.Controls.Add(this.label6);
            this.p_Elements.Controls.Add(this.label7);
            this.p_Elements.Controls.Add(this.label8);
            this.p_Elements.Controls.Add(this.label9);
            this.p_Elements.Controls.Add(this.ctrlRBValue_1);
            this.p_Elements.Controls.Add(this.ctrlRBValue_2);
            this.p_Elements.Controls.Add(this.ctrlRBValue_3);
            this.p_Elements.Controls.Add(this.ctrlRBValue_4);
            this.p_Elements.Controls.Add(this.ctrlRBValue_5);
            this.p_Elements.Controls.Add(this.label3);
            this.p_Elements.Controls.Add(this.ctrlLDate);
            this.p_Elements.Controls.Add(this.label2);
            this.p_Elements.Controls.Add(this.ctrlLAuthor);
            this.p_Elements.Controls.Add(this.label1);
            this.p_Elements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_Elements.Location = new System.Drawing.Point(0, 0);
            this.p_Elements.Margin = new System.Windows.Forms.Padding(0);
            this.p_Elements.Name = "p_Elements";
            this.p_Elements.Size = new System.Drawing.Size(571, 574);
            this.p_Elements.TabIndex = 20;
            // 
            // ctrl_TRatings
            // 
            this.ctrl_TRatings.AllowUserToAddRows = false;
            this.ctrl_TRatings.AllowUserToDeleteRows = false;
            this.ctrl_TRatings.AllowUserToResizeColumns = false;
            this.ctrl_TRatings.AllowUserToResizeRows = false;
            this.ctrl_TRatings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrl_TRatings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.p_Value,
            this.p_Time,
            this.p_Comment,
            this.p_UserName});
            this.ctrl_TRatings.Location = new System.Drawing.Point(3, 231);
            this.ctrl_TRatings.Name = "ctrl_TRatings";
            this.ctrl_TRatings.ReadOnly = true;
            this.ctrl_TRatings.RowHeadersVisible = false;
            this.ctrl_TRatings.ShowCellErrors = false;
            this.ctrl_TRatings.ShowCellToolTips = false;
            this.ctrl_TRatings.ShowEditingIcon = false;
            this.ctrl_TRatings.ShowRowErrors = false;
            this.ctrl_TRatings.Size = new System.Drawing.Size(566, 298);
            this.ctrl_TRatings.TabIndex = 17;
            // 
            // p_Value
            // 
            this.p_Value.DataPropertyName = "p_Value";
            this.p_Value.HeaderText = "Экспертиза";
            this.p_Value.Name = "p_Value";
            this.p_Value.ReadOnly = true;
            this.p_Value.Width = 95;
            // 
            // p_Time
            // 
            this.p_Time.DataPropertyName = "p_Time";
            this.p_Time.HeaderText = "Дата";
            this.p_Time.Name = "p_Time";
            this.p_Time.ReadOnly = true;
            this.p_Time.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.p_Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // p_Comment
            // 
            this.p_Comment.DataPropertyName = "p_Comment";
            this.p_Comment.HeaderText = "Комментарий";
            this.p_Comment.Name = "p_Comment";
            this.p_Comment.ReadOnly = true;
            this.p_Comment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.p_Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.p_Comment.Width = 227;
            // 
            // p_UserName
            // 
            this.p_UserName.DataPropertyName = "p_UserName";
            this.p_UserName.HeaderText = "Автор";
            this.p_UserName.Name = "p_UserName";
            this.p_UserName.ReadOnly = true;
            this.p_UserName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.p_UserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.p_UserName.Width = 140;
            // 
            // ctrlTBComment
            // 
            this.ctrlTBComment.Location = new System.Drawing.Point(3, 99);
            this.ctrlTBComment.Multiline = true;
            this.ctrlTBComment.Name = "ctrlTBComment";
            this.ctrlTBComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ctrlTBComment.Size = new System.Drawing.Size(566, 115);
            this.ctrlTBComment.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Комментарий";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(118, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(138, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "4";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(158, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "5";
            // 
            // ctrlRBValue_1
            // 
            this.ctrlRBValue_1.AutoSize = true;
            this.ctrlRBValue_1.Location = new System.Drawing.Point(79, 44);
            this.ctrlRBValue_1.Name = "ctrlRBValue_1";
            this.ctrlRBValue_1.Size = new System.Drawing.Size(14, 13);
            this.ctrlRBValue_1.TabIndex = 7;
            this.ctrlRBValue_1.TabStop = true;
            this.ctrlRBValue_1.UseVisualStyleBackColor = true;
            // 
            // ctrlRBValue_2
            // 
            this.ctrlRBValue_2.AutoSize = true;
            this.ctrlRBValue_2.Location = new System.Drawing.Point(99, 44);
            this.ctrlRBValue_2.Name = "ctrlRBValue_2";
            this.ctrlRBValue_2.Size = new System.Drawing.Size(14, 13);
            this.ctrlRBValue_2.TabIndex = 8;
            this.ctrlRBValue_2.TabStop = true;
            this.ctrlRBValue_2.UseVisualStyleBackColor = true;
            // 
            // ctrlRBValue_3
            // 
            this.ctrlRBValue_3.AutoSize = true;
            this.ctrlRBValue_3.Location = new System.Drawing.Point(119, 44);
            this.ctrlRBValue_3.Name = "ctrlRBValue_3";
            this.ctrlRBValue_3.Size = new System.Drawing.Size(14, 13);
            this.ctrlRBValue_3.TabIndex = 9;
            this.ctrlRBValue_3.TabStop = true;
            this.ctrlRBValue_3.UseVisualStyleBackColor = true;
            // 
            // ctrlRBValue_4
            // 
            this.ctrlRBValue_4.AutoSize = true;
            this.ctrlRBValue_4.Location = new System.Drawing.Point(139, 44);
            this.ctrlRBValue_4.Name = "ctrlRBValue_4";
            this.ctrlRBValue_4.Size = new System.Drawing.Size(14, 13);
            this.ctrlRBValue_4.TabIndex = 10;
            this.ctrlRBValue_4.TabStop = true;
            this.ctrlRBValue_4.UseVisualStyleBackColor = true;
            // 
            // ctrlRBValue_5
            // 
            this.ctrlRBValue_5.AutoSize = true;
            this.ctrlRBValue_5.Location = new System.Drawing.Point(159, 44);
            this.ctrlRBValue_5.Name = "ctrlRBValue_5";
            this.ctrlRBValue_5.Size = new System.Drawing.Size(14, 13);
            this.ctrlRBValue_5.TabIndex = 11;
            this.ctrlRBValue_5.TabStop = true;
            this.ctrlRBValue_5.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Оценка:";
            // 
            // ctrlLDate
            // 
            this.ctrlLDate.AutoSize = true;
            this.ctrlLDate.Location = new System.Drawing.Point(80, 27);
            this.ctrlLDate.Name = "ctrlLDate";
            this.ctrlLDate.Size = new System.Drawing.Size(0, 13);
            this.ctrlLDate.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата:";
            // 
            // ctrlLAuthor
            // 
            this.ctrlLAuthor.AutoSize = true;
            this.ctrlLAuthor.Location = new System.Drawing.Point(80, 9);
            this.ctrlLAuthor.Name = "ctrlLAuthor";
            this.ctrlLAuthor.Size = new System.Drawing.Size(0, 13);
            this.ctrlLAuthor.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Автор:";
            // 
            // p_Buttons
            // 
            this.p_Buttons.Controls.Add(this.ctrlBReRate);
            this.p_Buttons.Controls.Add(this.ctrlBSave);
            this.p_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.p_Buttons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.p_Buttons.Location = new System.Drawing.Point(0, 542);
            this.p_Buttons.Margin = new System.Windows.Forms.Padding(0);
            this.p_Buttons.Name = "p_Buttons";
            this.p_Buttons.Padding = new System.Windows.Forms.Padding(3);
            this.p_Buttons.Size = new System.Drawing.Size(571, 32);
            this.p_Buttons.TabIndex = 19;
            // 
            // Dlg_RatingViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(591, 594);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "Dlg_RatingViewer";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Экспертиза";
            this.panel2.ResumeLayout(false);
            this.p_Elements.ResumeLayout(false);
            this.p_Elements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_TRatings)).EndInit();
            this.p_Buttons.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ctrlBReRate;
		private System.Windows.Forms.Button ctrlBSave;
		private System.Windows.Forms.Panel p_Elements;
		private System.Windows.Forms.DataGridView ctrl_TRatings;
		private System.Windows.Forms.TextBox ctrlTBComment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.RadioButton ctrlRBValue_1;
		private System.Windows.Forms.RadioButton ctrlRBValue_2;
		private System.Windows.Forms.RadioButton ctrlRBValue_3;
		private System.Windows.Forms.RadioButton ctrlRBValue_4;
		private System.Windows.Forms.RadioButton ctrlRBValue_5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label ctrlLDate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label ctrlLAuthor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn p_Value;
		private System.Windows.Forms.DataGridViewTextBoxColumn p_Time;
		private System.Windows.Forms.DataGridViewTextBoxColumn p_Comment;
		private System.Windows.Forms.DataGridViewTextBoxColumn p_UserName;
        private System.Windows.Forms.FlowLayoutPanel p_Buttons;
    }
}