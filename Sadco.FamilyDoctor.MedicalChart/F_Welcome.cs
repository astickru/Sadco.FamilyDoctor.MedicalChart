using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
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
        private Type m_EnumType = null;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();
        private Cl_MedicalCard _currentMedCard = null;

        public F_Welcome()
        {
            InitializeComponent();
            if (Cl_App.Initialize())
            {
                f_InitValues();
            }
            else
            {
                Application.Exit();
            }
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

        private string[] f_AssemblyParams()
        {
            var role = f_GetSelectedItem<E_Roles>();

            bool isNotValid = ctrlClinikName.Text == "";
            isNotValid |= ctrlUserID.Text == "";
            isNotValid |= ctrlUserSurName.Text == "";
            isNotValid |= ctrlUserName.Text == "";
            isNotValid |= ctrlUserLastName.Text == "";
            isNotValid |= _currentMedCard == null;

            isNotValid |= role == E_Roles.Assistant && ctrlUserID.Text == "";
            isNotValid |= role == E_Roles.Assistant && ctrlDoctorSurName.Text == "";
            isNotValid |= role == E_Roles.Assistant && ctrlDoctorName.Text == "";
            isNotValid |= role == E_Roles.Assistant && ctrlDoctorLastName.Text == "";

            if (isNotValid) return new string[0];

            string[] startParams = new string[14];
            startParams[0] = ctrlClinikName.Text;
            startParams[1] = ctrlUserID.Text;
            startParams[2] = ctrlUserSurName.Text;
            startParams[3] = ctrlUserName.Text;
            startParams[4] = ctrlUserLastName.Text;
            startParams[5] = role.ToString();
            if (_currentMedCard != null)
            {
                startParams[6] = _currentMedCard.p_Number;
                startParams[7] = _currentMedCard.p_PatientID.ToString();
            }
            if (ctrlDateStart.Value != null)
                startParams[8] = ctrlDateStart.Value.ToString("dd.MM.yyyy");
            if (ctrlDateEnd.Value != null)
                startParams[9] = ctrlDateEnd.Value.ToString("dd.MM.yyyy");

            if (role == E_Roles.Assistant)
            {
                startParams[10] = ctrlDoctorID.Text;
                startParams[11] = ctrlDoctorSurName.Text;
                startParams[12] = ctrlDoctorName.Text;
                startParams[13] = ctrlDoctorLastName.Text;
            }

            return startParams;
        }

        #region UI
        private void button1_Click(object sender, EventArgs e)
        {
            string[] startParams = f_AssemblyParams();
            if (startParams.Length == 0) return;

            if (ctrlIsDebug.Checked)
            {
                Hide();
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != Keys.Back.GetHashCode() && e.KeyChar != Keys.Delete.GetHashCode() && !(e.KeyChar > 47 && e.KeyChar < 58);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            E_Roles role = f_GetSelectedItem<E_Roles>();
            ctrlPParentDoctor.Visible = role == E_Roles.Assistant;
            ctrlPPeriod.Enabled = role == E_Roles.Inspector;
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

        private void ctrlBLoadMedicalCard_Click(object sender, EventArgs e)
        {
            int patientId = 0;
            Guid patientUid = Guid.Empty;
            ctrlMedcardInfo.Text = "";
            _currentMedCard = null;
            if (int.TryParse(ctrlTBPatientID.Text, out patientId))
            {
                var medCard = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCard(ctrlTBMedCardNumber.Text, patientId);
                if (medCard != null)
                {
                    ctrlMedcardInfo.Text = $"№{medCard.p_Number}, {medCard.p_PatientFIO}, {medCard.p_PatientDateBirth}";
                    _currentMedCard = medCard;
                }
                else
                {
                    MonitoringStub.Warning("Не найдена медицинская карта");
                }
            }
            else if (Guid.TryParse(ctrlTBPatientID.Text, out patientUid))
            {
                var medCard = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCard(ctrlTBMedCardNumber.Text, patientUid);
                if (medCard != null)
                {
                    ctrlMedcardInfo.Text = $"№{medCard.p_Number}, {medCard.p_PatientFIO}, {medCard.p_PatientDateBirth}";
                    _currentMedCard = medCard;
                }
                else
                {
                    MonitoringStub.Warning("Не найдена медицинская карта");
                }
            }
            else
            {
                MonitoringStub.Error("Error_Welcome", "Не верно указан Id пациента", null, null, null);
            }
        }

        private void ctrlBAddMK_Click(object sender, EventArgs e)
        {
            var fAddMK = new F_AddMK();
            fAddMK.ShowDialog();
        }
    }
}
