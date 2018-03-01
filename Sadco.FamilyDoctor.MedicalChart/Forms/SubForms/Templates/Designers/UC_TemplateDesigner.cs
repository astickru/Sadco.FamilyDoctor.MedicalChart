using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.MedicalChart.Entities.Controls;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities.Controls;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	public partial class UC_TemplateDesigner : UserControl
	{
		internal Cl_Template p_ActiveTemplate { get; set; }

		private List<Cl_TemplateControl> m_newConstrols = new List<Cl_TemplateControl>();
		private List<Cl_TemplateControl> m_delConstrols = new List<Cl_TemplateControl>();

		private int deltaX = 0;
		private int deltaY = 0;

		public UC_TemplateDesigner() {
			InitializeComponent();

			this.Load += UC_TemplateDesigner_Load;
		}

		private void UC_TemplateDesigner_Load(object sender, EventArgs e) {
			if (p_ActiveTemplate.p_TemplateControls == null || p_ActiveTemplate.p_TemplateControls.Count == 0) return;

			foreach (Cl_TemplateControl item in p_ActiveTemplate.p_TemplateControls) {
				//activeConstrols.Add(item);

				if (item.p_Control == null) continue;

				Control uiControl = f_CreateUIControl(item.p_Control);
				if (uiControl == null) return;

				ctrl_P_Designer.Controls.Add(uiControl);

				uiControl.Size = new Size(item.p_Width, item.p_Height);
				uiControl.Location = new Point(item.p_PositionX, item.p_PositionY);

				uiControl.Tag = item;
			}
		}

		private void ctrl_MouseDown(object sender, MouseEventArgs e) {
			Control c = sender as Control;

			if (e.Button == MouseButtons.Left) {

				deltaX = e.X;
				deltaY = e.Y;

				c.DoDragDrop(c, DragDropEffects.Move);
			} else {
				MenuItem delMenu = new MenuItem("Удалить");
				delMenu.Click += (msender, me) => {
					Control control = (Control)((MenuItem)msender).Tag;
					Cl_TemplateControl templateControl = (Cl_TemplateControl)control.Tag;

					if (m_newConstrols.Contains(templateControl)) {
						m_newConstrols.Remove(templateControl);
					} else {
						m_delConstrols.Add(templateControl);
					}

					control.Parent.Controls.Remove(control);
				};

				delMenu.Tag = c;

				ContextMenu buttonMenu = new ContextMenu();
				buttonMenu.MenuItems.Add(delMenu);
				buttonMenu.Show(c, new Point(e.X, e.Y));
			}
		}

		private void ctrl_B_Save_Click(object sender, EventArgs e) {
			foreach (Control item in ctrl_P_Designer.Controls) {
				Cl_TemplateControl templateControl = (Cl_TemplateControl)item.Tag;
				templateControl.p_Width = item.Size.Width;
				templateControl.p_Height = item.Size.Height;
				templateControl.p_PositionX = item.Location.X;
				templateControl.p_PositionY = item.Location.Y;

				if (m_newConstrols.Contains(templateControl)) {
					m_newConstrols.Remove(templateControl);
					Cl_App.m_DataContext.p_TemplateControls.Add(templateControl);
				}

				foreach (Cl_TemplateControl delItem in m_delConstrols) {
					Cl_App.m_DataContext.p_TemplateControls.Remove(delItem);
				}
			}

			Cl_App.m_DataContext.SaveChanges();
			m_delConstrols.Clear();
		}

		private void ctrl_P_Designer_DragEnter(object sender, DragEventArgs e) {
			e.Effect = e.AllowedEffect;
		}

		private void ctrl_P_Designer_DragDrop(object sender, DragEventArgs e) {
			if (e.Data.GetData(e.Data.GetFormats()[0]) is Control) {
				f_DragControl(e.Data.GetData(e.Data.GetFormats()[0]) as Control, e.X, e.Y);
			} else if (e.Data.GetData(e.Data.GetFormats()[0]) is Cl_CtrlControlNode) {
				f_DragNewControl(e.Data.GetData(e.Data.GetFormats()[0]) as Cl_CtrlControlNode, e.X, e.Y);
			}
		}

		private void f_DragControl(Control control, int x, int y) {
			if (control == null) return;

			control.Location = ctrl_P_Designer.PointToClient(new Point(x - deltaX, y - deltaY));
		}

		private void f_DragNewControl(Cl_CtrlControlNode controlNode, int posX, int posY) {
			if (controlNode == null) return;
			if (controlNode.p_Control == null) return;

			Control uiControl = f_CreateUIControl(controlNode.p_Control);
			if (uiControl == null) return;

			ctrl_P_Designer.Controls.Add(uiControl);
			uiControl.Location = ctrl_P_Designer.PointToClient(new Point(posX - (uiControl.Size.Width / 2), posY - (uiControl.Size.Height / 2)));

			Cl_TemplateControl templateControl = new Cl_TemplateControl();
			templateControl.p_Control = controlNode.p_Control;
			templateControl.p_Template = p_ActiveTemplate;

			m_newConstrols.Add(templateControl);

			uiControl.Tag = templateControl;
		}

		private Control f_CreateUIControl(I_BaseControl control) {
			Control uiControl = null;

			if (control is Cl_CtrlImage) {
				PictureBox pick = new PictureBox();
				pick.Image = ((Cl_CtrlImage)control).f_GetImage(null);
				pick.Size = pick.Image.Size;
				pick.BorderStyle = BorderStyle.FixedSingle;

				uiControl = pick;
			} else if (control is Cl_CtrlTextual) {
				Cl_CtrlTextual ctrlTextual = (Cl_CtrlTextual)control;

				switch (ctrlTextual.p_ControlType) {
					case TextControlTypes.ComboBox:
						uiControl = new ComboBox();
						foreach (string item in ctrlTextual.p_Elements) {
							((ComboBox)uiControl).Items.Add(item);
						}
						if (((ComboBox)uiControl).Items.Count > 0)
							((ComboBox)uiControl).SelectedIndex = 0;
						break;
					case TextControlTypes.List:
						uiControl = new ListBox();
						foreach (string item in ctrlTextual.p_Elements) {
							((ListBox)uiControl).Items.Add(item);
						}
						break;
					case TextControlTypes.CheckBox:
						uiControl = new CheckBox() {
							Checked = true
						};
						uiControl.Text = ctrlTextual.p_Text;
						break;
					case TextControlTypes.Text:
						uiControl = new TextBox();
						uiControl.Text = ctrlTextual.p_Text;
						break;
					default:
						throw new NotSupportedException("Указанный тип " + ctrlTextual.p_ControlType.ToString() + " не реализован");
				}
			} else {
				throw new NotImplementedException("Обработка класса " + control.GetType().Name + " не реализована");
			}

			// Установка минимального размера
			//int mixXY = 10;

			//uiControl.Size = new Size(control.p_Width < mixXY ? mixXY : control.p_Width, control.p_Height < mixXY ? mixXY : control.p_Height);
			uiControl.MouseDown += ctrl_MouseDown;
			return uiControl;
		}
	}
}
