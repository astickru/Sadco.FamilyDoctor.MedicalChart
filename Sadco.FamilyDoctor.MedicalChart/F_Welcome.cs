using Sadco.FamilyDoctor.Core.Permision.Enums;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
	public partial class F_Welcome : Form
	{
		private Type m_EnumType = null;
		private const int WM_NCLBUTTONDOWN = 0xA1;
		private const int HT_CAPTION = 0x2;

		[DllImportAttribute("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		private static extern bool ReleaseCapture();

		public F_Welcome()
		{
			InitializeComponent();
			InitValues();
		}

		private void InitValues()
		{
			DateTime now = DateTime.Now;
			DateTime min = now.AddDays(-now.Day + 1);
			DateTime max = min.AddMonths(1).AddDays(-1);

			dateTimePicker2.Value = min;
			dateTimePicker1.Value = max;
			dateTimePicker3.Value = min;

			if (typeof(Roles).IsEnum)
			{
				m_EnumType = typeof(Roles);
				comboBox1.DisplayMember = "Description";
				comboBox1.ValueMember = "Value";
				comboBox1.DataSource = Enum.GetValues(typeof(Roles))
								.Cast<Enum>()
								.Select(value => new
								{
									(Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
									value
								})
								.OrderBy(item => item.value)
								.ToList();
                comboBox1.SelectedIndex = 1;
            }
		}

		public T f_GetSelectedItem<T>()
		{
			return (T)Enum.Parse(typeof(T), comboBox1.SelectedItem.GetType().GetProperty("value").GetValue(comboBox1.SelectedItem, null).ToString());
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			bool isNotValid = textBox1.Text == "";
			isNotValid |= textBox2.Text == "";
			isNotValid |= textBox4.Text == "";
			isNotValid |= textBox5.Text == "";

			if (isNotValid) return;

			string[] startParams = new string[8];
			startParams[0] = textBox1.Text;
			startParams[1] = textBox2.Text;
			startParams[2] = f_GetSelectedItem<Roles>().ToString();
			startParams[3] = textBox4.Text;
			startParams[4] = textBox5.Text;
			startParams[5] = dateTimePicker3.Value.ToString("dd.MM.yyyy");
			startParams[6] = dateTimePicker2.Value.ToString("dd.MM.yyyy");
			startParams[7] = dateTimePicker1.Value.ToString("dd.MM.yyyy");

			if (checkBox1.Checked)
			{
				this.Hide();
				new F_Main(startParams).ShowDialog();
				this.Close();
			}
			else
			{
				startParams[1] = "\"" + startParams[1] + "\"";
				startParams[4] = "\"" + startParams[4] + "\"";
				string param = String.Join(" ", startParams);
				Process proc = Process.Start(Assembly.GetExecutingAssembly().Location, param);
				if (proc != null) this.Close();
			}
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = e.KeyChar != Keys.Back.GetHashCode() && e.KeyChar != Keys.Delete.GetHashCode() && !(e.KeyChar > 47 && e.KeyChar < 58);
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			Roles role = f_GetSelectedItem<Roles>();
			panel7.Enabled = role == Roles.Inspector;
		}

		private void comboBox1_Leave(object sender, EventArgs e)
		{
			ComboBox cb = (ComboBox)sender;
			if (cb.SelectedIndex != -1) return;
			if (cb.Items.Count == 0) return;

			cb.SelectedIndex = 0;
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}
	}
}
