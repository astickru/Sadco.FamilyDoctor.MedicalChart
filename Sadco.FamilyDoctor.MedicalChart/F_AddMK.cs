using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
    public partial class F_AddMK : Form
    {
        public F_AddMK()
        {
            InitializeComponent();
            ctrlPatientSex.f_SetEnum(typeof(Cl_User.E_Sex));
        }

        private void ctrlBCreateMK_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ctrlMedCardNumber.Text))
            {
                MonitoringStub.Message("Поле \"Номер медкарты\" пустое!");
                return;
            }
            if (string.IsNullOrWhiteSpace(ctrlPatientID.Text))
            {
                MonitoringStub.Message("Поле \"ID пациента\" пустое!");
                return;
            }
            int patientId = 0;
            if (!int.TryParse(ctrlPatientID.Text, out patientId))
            {
                MonitoringStub.Message("Некорректное значения поля \"ID пациента\" пустое!");
                return;
            }
            if (string.IsNullOrWhiteSpace(ctrlPatientSurName.Text))
            {
                MonitoringStub.Message("Поле \"Фамилия пациента\" пустое!");
                return;
            }
            if (string.IsNullOrWhiteSpace(ctrlPatientSurName.Text))
            {
                MonitoringStub.Message("Поле \"Имя пациента\" пустое!");
                return;
            }
            if (string.IsNullOrWhiteSpace(ctrlPatientSurName.Text))
            {
                MonitoringStub.Message("Поле \"Отчество пациента\" пустое!");
                return;
            }
            if (ctrlPatientDateBirth.Value == null)
            {
                MonitoringStub.Message("Поле \"Дата рождения\" пустое!");
                return;
            }
            Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard(ctrlMedCardNumber.Text, patientId, (Cl_User.E_Sex)ctrlPatientSex.f_GetSelectedItem()
                , ctrlPatientSurName.Text, ctrlPatientName.Text, ctrlPatientLastName.Text, ctrlPatientDateBirth.Value, ctrlTBComment.Text);
        }
    }
}
