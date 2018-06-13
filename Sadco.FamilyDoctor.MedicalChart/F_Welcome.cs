using Sadco.FamilyDoctor.Core.Permision;
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
            ctrlPatientSex.f_SetEnum(typeof(Cl_User.E_Sex));
            f_InitValues();
        }

        private void f_InitValues()
        {
            DateTime now = DateTime.Now;
            DateTime min = now.AddDays(-now.Day + 1);
            DateTime max = min.AddMonths(1).AddDays(-1);

            ctrlDateStart.Value = min;
            ctrlDateEnd.Value = max;

            if (typeof(E_Roles).IsEnum)
            {
                m_EnumType = typeof(E_Roles);
                ctrlRoles.DisplayMember = "Description";
                ctrlRoles.ValueMember = "Value";
                ctrlRoles.DataSource = Enum.GetValues(typeof(E_Roles))
                                .Cast<Enum>()
                                .Select(value => new
                                {
                                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                                    value
                                })
                                .OrderBy(item => item.value)
                                .ToList();
                ctrlRoles.SelectedIndex = 1;
            }

#if DEBUG
            ctrlIsDebug.Visible = ctrlIsDebug.Checked = true;
#else
            ctrlIsDebug.Visible = ctrlIsDebug.Checked = false;
#endif
        }

        public T f_GetSelectedItem<T>()
        {
            return (T)Enum.Parse(typeof(T), ctrlRoles.SelectedItem.GetType().GetProperty("value").GetValue(ctrlRoles.SelectedItem, null).ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isNotValid = ctrlClinikName.Text == "";
            isNotValid |= ctrlUserID.Text == "";
            isNotValid |= ctrlUserSurName.Text == "";
            isNotValid |= ctrlUserName.Text == "";
            isNotValid |= ctrlUserLastName.Text == "";
            isNotValid |= ctrlPatientID.Text == "";
            isNotValid |= ctrlPatientSurName.Text == "";
            isNotValid |= ctrlPatientName.Text == "";
            isNotValid |= ctrlPatientLastName.Text == "";
            isNotValid |= ctrlPatientDateBirth.Text == "";
            isNotValid |= ctrlMedCardNumber.Text == "";

            if (isNotValid) return;

            string[] startParams = new string[16];
            startParams[0] = ctrlClinikName.Text;
            startParams[1] = ctrlUserID.Text;
            startParams[2] = ctrlUserSurName.Text;
            startParams[3] = ctrlUserName.Text;
            startParams[4] = ctrlUserLastName.Text;
            startParams[5] = f_GetSelectedItem<E_Roles>().ToString();
            startParams[6] = ctrlPatientID.Text;
            Guid gVal = Guid.Empty;
            if (Guid.TryParse(ctrlPatientUID.Text, out gVal))
                startParams[7] = ctrlPatientUID.Text;
            startParams[8] = ctrlPatientSurName.Text;
            startParams[9] = ctrlPatientName.Text;
            startParams[10] = ctrlPatientLastName.Text;
            startParams[11] = ctrlMedCardNumber.Text;
            startParams[12] = ((Cl_User.E_Sex)ctrlPatientSex.f_GetSelectedItem()).GetHashCode().ToString();
            startParams[13] = ctrlPatientDateBirth.Value.ToString("dd.MM.yyyy");

            if (ctrlDateStart.Value != null)
                startParams[14] = ctrlDateStart.Value.ToString("dd.MM.yyyy");
            if (ctrlDateEnd.Value != null)
                startParams[15] = ctrlDateEnd.Value.ToString("dd.MM.yyyy");

            if (ctrlIsDebug.Checked)
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
            E_Roles role = f_GetSelectedItem<E_Roles>();
            panel7.Enabled = role == E_Roles.Inspector;
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
