using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Sadco.FamilyDoctor.Core.Facades;
using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Entities;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_MedicalCards : UserControl
    {
        private Cl_MedicalCard m_SelectedMedicalCard = null;

        public UC_MedicalCards()
        {
            Tag = string.Format("Медицинские карты v{0}", ConfigurationManager.AppSettings["Version"]);
            InitializeComponent();

            ctrlLPatientName.Text = Cl_SessionFacade.f_GetInstance().p_Patient.p_FIO;
            f_UpdateMedicalCards();
        }

        private void f_UpdateMedicalCards()
        {
            try
            {
                var patientID = Cl_SessionFacade.f_GetInstance().p_Patient.p_UserID;
                var medicalCards = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCardsByPatient(patientID);

                if (medicalCards.Count() > 0)
                {
                    BindingSource bs = new BindingSource();
                    var list = new BindingList<Cl_MedicalCard>(medicalCards);
                    ctrl_TMedicalCards.AutoGenerateColumns = false;
                    ctrl_TMedicalCards.DataSource = list;
                }
                else
                {
                    ctrl_TMedicalCards.DataSource = null;
                }
                if (ctrl_TMedicalCards.Rows.Count > 0)
                {
                    ctrl_TMedicalCards.Rows[ctrl_TMedicalCards.Rows.Count - 1].Selected = true;
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось обновить список медкарт", er, null, null);
            }
        }

        private void ctrl_TMedicalCards_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_SelectedMedicalCard = null;
            if (ctrl_TMedicalCards.CurrentRow != null && ctrl_TMedicalCards.CurrentRow.DataBoundItem is Cl_MedicalCard)
            {
                try
                {
                    m_SelectedMedicalCard = ctrl_TMedicalCards.CurrentRow.DataBoundItem as Cl_MedicalCard;
                    if (m_SelectedMedicalCard != null)
                    {
                        ctrlMCInfoValNumber.Text = m_SelectedMedicalCard.p_Number;
                        ctrlMCInfoValCreateDate.Text = m_SelectedMedicalCard.p_DateCreate.ToShortDateString();
                        ctrlMCInfoValPatient.Text = m_SelectedMedicalCard.p_PatientFIO;
                        if (m_SelectedMedicalCard.p_DateArchive != null)
                        {
                            ctrlMCInfoArchiveDate.Visible = true;
                            ctrlMCInfoValArchiveDate.Text = m_SelectedMedicalCard.p_DateArchive?.ToShortDateString();
                        }
                        else
                        {
                            ctrlMCInfoArchiveDate.Visible = false;
                            ctrlMCInfoValArchiveDate.Text = "";
                        }
                        if (m_SelectedMedicalCard.p_DateDelete != null)
                        {
                            ctrlMCInfoDeleteDate.Visible = true;
                            ctrlMCInfoValDeleteDate.Text = m_SelectedMedicalCard.p_DateDelete?.ToShortDateString();
                        }
                        else
                        {
                            ctrlMCInfoDeleteDate.Visible = false;
                            ctrlMCInfoValDeleteDate.Text = "";
                        }
                        ctrlMCInfoValComment.Text = m_SelectedMedicalCard.p_Comment;
                        ctrlPRecordInfo.Visible = true;
                    }
                    else
                    {
                        ctrlPRecordInfo.Visible = false;
                    }
                }
                catch (Exception er)
                {
                    MonitoringStub.Error("Error_Editor", "Не удалось отобразить медкарту", er, null, null);
                }
            }
        }
    }
}
