using Microsoft.Data.ConnectionUI;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace Sadco.FamilyDoctor.MedicalChart
{
    public partial class F_Welcome : Form
    {
        bool tryConnectionString = false;
        string connectionString = "";

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

            f_LoadConnectionString();

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

        private string[] f_AssemblyParams()
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
            isNotValid |= ctrlConnectionString.Text == "";

            if (isNotValid) return new string[0];

            string[] startParams = new string[17];
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

            startParams[14] = ctrlConnectionString.Text;

            if (ctrlDateStart.Value != null)
                startParams[15] = ctrlDateStart.Value.ToString("dd.MM.yyyy");
            if (ctrlDateEnd.Value != null)
                startParams[16] = ctrlDateEnd.Value.ToString("dd.MM.yyyy");

            return startParams;
        }

        bool TryGetDataConnectionStringFromUser(out string outConnectionString)
        {
            using (var dialog = new DataConnectionDialog())
            {
                // If you want the user to select from any of the available data sources, do this:
                DataSource.AddStandardDataSources(dialog);

                // OR, if you want only certain data sources to be available
                // (e.g. only SQL Server), do something like this instead: 
                dialog.DataSources.Add(DataSource.SqlDataSource);
                dialog.DataSources.Add(DataSource.SqlFileDataSource);


                // The way how you show the dialog is somewhat unorthodox; `dialog.ShowDialog()`
                // would throw a `NotSupportedException`. Do it this way instead:
                DialogResult userChoice = DataConnectionDialog.Show(dialog);

                // Return the resulting connection string if a connection was selected:
                if (userChoice == DialogResult.OK)
                {
                    outConnectionString = dialog.ConnectionString;
                    return true;
                }
                else
                {
                    outConnectionString = null;
                    return false;
                }
            }
        }

        private void f_LoadConnectionString()
        {
            string appPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string cfgFile = "ConnectionString.cfg";
            string filePath = System.IO.Path.Combine(appPath, cfgFile);

            if (System.IO.File.Exists(filePath) == false)
                System.IO.File.Create(filePath);

            ctrlConnectionString.Text = connectionString = System.IO.File.ReadAllText(filePath);
        }

        private void f_SaveConnectionString()
        {
            string appPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string cfgFile = "ConnectionString.cfg";
            string filePath = System.IO.Path.Combine(appPath, cfgFile);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            System.IO.File.WriteAllText(filePath, connectionString);
        }

        #region UI
        private void button1_Click(object sender, EventArgs e)
        {
            string[] startParams = f_AssemblyParams();
            if (startParams.Length == 0) return;

            if (ctrlIsDebug.Checked)
            {
                Hide();
                f_SaveConnectionString();
                var fMain = new F_Main(startParams);
                fMain.ShowDialog();
                Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tryConnectionString = TryGetDataConnectionStringFromUser(out connectionString);
            ctrlConnectionString.Text = connectionString;
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
        #endregion
    }
}
