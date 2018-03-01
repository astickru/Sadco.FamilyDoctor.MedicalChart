namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class UC_EditorImage
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
			this.ctrl_PB_Image = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.ctrl_B_Delete = new System.Windows.Forms.Button();
			this.ctrl_B_Add = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.ctrl_PB_Image)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctrl_PB_Image
			// 
			this.ctrl_PB_Image.BackColor = System.Drawing.Color.White;
			this.ctrl_PB_Image.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_PB_Image.Location = new System.Drawing.Point(0, 25);
			this.ctrl_PB_Image.Name = "ctrl_PB_Image";
			this.ctrl_PB_Image.Size = new System.Drawing.Size(404, 204);
			this.ctrl_PB_Image.TabIndex = 0;
			this.ctrl_PB_Image.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ctrl_PB_Image);
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(404, 229);
			this.panel1.TabIndex = 1;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.ctrl_B_Delete);
			this.panel4.Controls.Add(this.ctrl_B_Add);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Margin = new System.Windows.Forms.Padding(0);
			this.panel4.Name = "panel4";
			this.panel4.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.panel4.Size = new System.Drawing.Size(404, 25);
			this.panel4.TabIndex = 22;
			// 
			// ctrl_B_Delete
			// 
			this.ctrl_B_Delete.Dock = System.Windows.Forms.DockStyle.Left;
			this.ctrl_B_Delete.Location = new System.Drawing.Point(75, 2);
			this.ctrl_B_Delete.Margin = new System.Windows.Forms.Padding(0);
			this.ctrl_B_Delete.Name = "ctrl_B_Delete";
			this.ctrl_B_Delete.Size = new System.Drawing.Size(75, 21);
			this.ctrl_B_Delete.TabIndex = 5;
			this.ctrl_B_Delete.Text = "Удалить";
			this.ctrl_B_Delete.UseVisualStyleBackColor = true;
			this.ctrl_B_Delete.Click += new System.EventHandler(this.ctrl_B_Delete_Click);
			// 
			// ctrl_B_Add
			// 
			this.ctrl_B_Add.Dock = System.Windows.Forms.DockStyle.Left;
			this.ctrl_B_Add.Location = new System.Drawing.Point(0, 2);
			this.ctrl_B_Add.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
			this.ctrl_B_Add.Name = "ctrl_B_Add";
			this.ctrl_B_Add.Size = new System.Drawing.Size(75, 21);
			this.ctrl_B_Add.TabIndex = 4;
			this.ctrl_B_Add.Text = "Добавить";
			this.ctrl_B_Add.UseVisualStyleBackColor = true;
			this.ctrl_B_Add.Click += new System.EventHandler(this.ctrl_B_Add_Click);
			// 
			// UC_EditorImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "UC_EditorImage";
			this.Size = new System.Drawing.Size(404, 229);
			((System.ComponentModel.ISupportInitialize)(this.ctrl_PB_Image)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox ctrl_PB_Image;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button ctrl_B_Delete;
		private System.Windows.Forms.Button ctrl_B_Add;
	}
}
