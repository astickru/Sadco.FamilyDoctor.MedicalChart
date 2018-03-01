using Sadco.FamilyDoctor.Core.Entities.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	public partial class UC_EditorImage : UserControl, I_EditPanel
	{
		Cl_CtrlImage editingControl = null;

		public UC_EditorImage() {
			InitializeComponent();
		}

		public void ConfirmChanges() {
			editingControl.f_GetImage(ctrl_PB_Image.Image);
		}

		public void SetControl(I_BaseControl p_Control) {
			if (!(p_Control is Cl_CtrlImage)) return;

			editingControl = (Cl_CtrlImage)p_Control;

			SetImage(editingControl.f_GetImage(null));
		}

		private void SetImage(Image bitmap) {
			if (bitmap == null)
				return;

			ctrl_PB_Image.Image = bitmap;
		}

		private void ctrl_B_Add_Click(object sender, EventArgs e) {
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = "Image Files |*.bmp; *.gif; *.jpg; *.jpeg; *.png";
			openFile.FilterIndex = 1;

			if (openFile.ShowDialog() != DialogResult.OK) return;

			Image result = null;
			try {
				result = Image.FromFile(openFile.FileName);
			} catch(Exception) {
				MessageBox.Show("Выбранный файл не является изображением", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			ctrl_PB_Image.Image = result;
		}

		private void ctrl_B_Delete_Click(object sender, EventArgs e) {
			ctrl_PB_Image.Image = null;
		}
	}
}
