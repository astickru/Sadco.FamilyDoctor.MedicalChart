namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	partial class UC_EditorTextual
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
			this.ctrl_LVTemplates = new System.Windows.Forms.ListView();
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel4 = new System.Windows.Forms.Panel();
			this.ctrl_B_Delete = new System.Windows.Forms.Button();
			this.ctrl_B_Add = new System.Windows.Forms.Button();
			this.ctrl_P_TableText = new System.Windows.Forms.Panel();
			this.ctrl_P_Text = new System.Windows.Forms.Panel();
			this.ctrl_TB_Text = new System.Windows.Forms.TextBox();
			this.TopPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SelectTypePanel = new System.Windows.Forms.Panel();
			this.ctrl_CB_ControlType = new System.Windows.Forms.ComboBox();
			this.L_TypeDescription = new System.Windows.Forms.Label();
			this.panel4.SuspendLayout();
			this.ctrl_P_TableText.SuspendLayout();
			this.ctrl_P_Text.SuspendLayout();
			this.TopPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SelectTypePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctrl_LVTemplates
			// 
			this.ctrl_LVTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
			this.ctrl_LVTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_LVTemplates.FullRowSelect = true;
			this.ctrl_LVTemplates.HideSelection = false;
			this.ctrl_LVTemplates.Location = new System.Drawing.Point(0, 25);
			this.ctrl_LVTemplates.MultiSelect = false;
			this.ctrl_LVTemplates.Name = "ctrl_LVTemplates";
			this.ctrl_LVTemplates.Size = new System.Drawing.Size(404, 179);
			this.ctrl_LVTemplates.TabIndex = 20;
			this.ctrl_LVTemplates.UseCompatibleStateImageBehavior = false;
			this.ctrl_LVTemplates.View = System.Windows.Forms.View.Details;
			// 
			// chName
			// 
			this.chName.Text = "Наименование";
			this.chName.Width = 300;
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
			this.panel4.TabIndex = 21;
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
			// ctrl_P_TableText
			// 
			this.ctrl_P_TableText.Controls.Add(this.ctrl_LVTemplates);
			this.ctrl_P_TableText.Controls.Add(this.panel4);
			this.ctrl_P_TableText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_P_TableText.Location = new System.Drawing.Point(0, 0);
			this.ctrl_P_TableText.Name = "ctrl_P_TableText";
			this.ctrl_P_TableText.Size = new System.Drawing.Size(404, 204);
			this.ctrl_P_TableText.TabIndex = 22;
			this.ctrl_P_TableText.Visible = false;
			// 
			// ctrl_P_Text
			// 
			this.ctrl_P_Text.Controls.Add(this.ctrl_TB_Text);
			this.ctrl_P_Text.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_P_Text.Location = new System.Drawing.Point(0, 0);
			this.ctrl_P_Text.Name = "ctrl_P_Text";
			this.ctrl_P_Text.Size = new System.Drawing.Size(404, 204);
			this.ctrl_P_Text.TabIndex = 0;
			this.ctrl_P_Text.Visible = false;
			// 
			// ctrl_TB_Text
			// 
			this.ctrl_TB_Text.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_TB_Text.Location = new System.Drawing.Point(0, 0);
			this.ctrl_TB_Text.Multiline = true;
			this.ctrl_TB_Text.Name = "ctrl_TB_Text";
			this.ctrl_TB_Text.Size = new System.Drawing.Size(404, 204);
			this.ctrl_TB_Text.TabIndex = 22;
			// 
			// TopPanel
			// 
			this.TopPanel.Controls.Add(this.panel1);
			this.TopPanel.Controls.Add(this.SelectTypePanel);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TopPanel.Location = new System.Drawing.Point(0, 0);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(404, 229);
			this.TopPanel.TabIndex = 23;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ctrl_P_TableText);
			this.panel1.Controls.Add(this.ctrl_P_Text);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(404, 204);
			this.panel1.TabIndex = 22;
			// 
			// SelectTypePanel
			// 
			this.SelectTypePanel.Controls.Add(this.ctrl_CB_ControlType);
			this.SelectTypePanel.Controls.Add(this.L_TypeDescription);
			this.SelectTypePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.SelectTypePanel.Location = new System.Drawing.Point(0, 0);
			this.SelectTypePanel.Name = "SelectTypePanel";
			this.SelectTypePanel.Size = new System.Drawing.Size(404, 25);
			this.SelectTypePanel.TabIndex = 22;
			// 
			// ctrl_CB_ControlType
			// 
			this.ctrl_CB_ControlType.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctrl_CB_ControlType.FormattingEnabled = true;
			this.ctrl_CB_ControlType.Location = new System.Drawing.Point(78, 0);
			this.ctrl_CB_ControlType.Name = "ctrl_CB_ControlType";
			this.ctrl_CB_ControlType.Size = new System.Drawing.Size(326, 21);
			this.ctrl_CB_ControlType.TabIndex = 1;
			this.ctrl_CB_ControlType.SelectedValueChanged += new System.EventHandler(this.ctrl_CB_ControlType_SelectedValueChanged);
			// 
			// L_TypeDescription
			// 
			this.L_TypeDescription.AutoSize = true;
			this.L_TypeDescription.Dock = System.Windows.Forms.DockStyle.Left;
			this.L_TypeDescription.Location = new System.Drawing.Point(0, 0);
			this.L_TypeDescription.Name = "L_TypeDescription";
			this.L_TypeDescription.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.L_TypeDescription.Size = new System.Drawing.Size(78, 16);
			this.L_TypeDescription.TabIndex = 0;
			this.L_TypeDescription.Text = "Тип элемента";
			// 
			// UC_EditorTextual
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TopPanel);
			this.Name = "UC_EditorTextual";
			this.Size = new System.Drawing.Size(404, 229);
			this.panel4.ResumeLayout(false);
			this.ctrl_P_TableText.ResumeLayout(false);
			this.ctrl_P_Text.ResumeLayout(false);
			this.ctrl_P_Text.PerformLayout();
			this.TopPanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.SelectTypePanel.ResumeLayout(false);
			this.SelectTypePanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView ctrl_LVTemplates;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button ctrl_B_Delete;
		private System.Windows.Forms.Button ctrl_B_Add;
		private System.Windows.Forms.Panel ctrl_P_TableText;
		private System.Windows.Forms.Panel ctrl_P_Text;
		private System.Windows.Forms.TextBox ctrl_TB_Text;
		private System.Windows.Forms.Panel TopPanel;
		private System.Windows.Forms.Panel SelectTypePanel;
		private System.Windows.Forms.ComboBox ctrl_CB_ControlType;
		private System.Windows.Forms.Label L_TypeDescription;
		private System.Windows.Forms.Panel panel1;
	}
}
