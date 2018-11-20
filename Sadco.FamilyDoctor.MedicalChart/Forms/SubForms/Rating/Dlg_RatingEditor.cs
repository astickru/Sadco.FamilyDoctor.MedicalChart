using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class Dlg_RatingViewer : Form
    {
        Cl_Record curRecord = null;
        Cl_Rating firstRating = null;
        Cl_Rating selfRating = null;

        public Dlg_RatingViewer()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();

            this.Load += Dlg_RatingViewer_Load;
            ctrl_TRatings.AutoGenerateColumns = false;
        }

        public void f_LoadRating(Cl_Record p_Record)
        {
            this.curRecord = p_Record;

            int userID = Cl_SessionFacade.f_GetInstance().p_Doctor.p_UserID;
            selfRating = Cl_App.m_DataContext.p_Ratings.Where(l => l.p_RecordID == p_Record.p_RecordID && l.p_UserID == userID).OrderByDescending(l => l.p_Time).FirstOrDefault();
            firstRating = Cl_App.m_DataContext.p_Ratings.Where(l => l.p_RecordID == p_Record.p_RecordID && l.p_UserID == userID).OrderBy(l => l.p_Time).FirstOrDefault();

            // Значения по умолчанию
            ctrlLAuthor.Text = Cl_SessionFacade.f_GetInstance().p_Doctor.f_GetInitials();
            ctrlLDate.Text = DateTime.Now.ToString();

            f_FillRating(selfRating);
            f_LoadRatingTable(p_Record.p_RecordID);
        }

        private void Dlg_RatingViewer_Load(object sender, EventArgs e)
        {
            f_UpdateRateEditingState(selfRating);
        }

        private void f_FillRating(Cl_Rating rating)
        {
            ctrlLAuthor.Text = Cl_SessionFacade.f_GetInstance().p_Doctor.f_GetInitials();
            ctrlLDate.Text = DateTime.Now.ToString();
            ctrlTBComment.Text = "";

            if (rating == null) return;

            ctrlLAuthor.Text = rating.p_UserName;
            ctrlLDate.Text = rating.p_Time.ToString();
            ctrlTBComment.Text = rating.p_Comment;

            ctrlRBValue_1.Checked = rating.p_Value == 1;
            ctrlRBValue_2.Checked = rating.p_Value == 2;
            ctrlRBValue_3.Checked = rating.p_Value == 3;
            ctrlRBValue_4.Checked = rating.p_Value == 4;
            ctrlRBValue_5.Checked = rating.p_Value == 5;
        }

        private void f_LoadRatingTable(int p_RecordID)
        {
            ctrl_TRatings.DataSource = null;

            var curRatings = Cl_App.m_DataContext.p_Ratings.Where(l => l.p_RecordID == p_RecordID).OrderByDescending(l => l.p_Time);
            if (curRatings.Count() == 0) return;

            BindingSource bs = new BindingSource();
            bs.DataSource = curRatings.ToList();
            ctrl_TRatings.DataSource = bs;
        }

        private void f_UpdateRateEditingState(Cl_Rating rating)
        {
            bool allowEdit = true;
            bool visibleReRate = true;

            //allowEdit &= (rating == null);
            allowEdit &= (rating != null && Cl_SessionFacade.f_GetInstance().p_Doctor.p_UserID == rating.p_UserID);
            allowEdit &= (rating != null && firstRating != null && firstRating.p_Time.AddDays(1) > DateTime.Now);
            allowEdit &= (rating != null && selfRating != null && rating.p_Time >= selfRating.p_Time);
            allowEdit |= rating == null;

            visibleReRate &= (selfRating != null && allowEdit == false);
            visibleReRate &= (selfRating != null && firstRating != null && firstRating.p_Time.AddDays(1) > DateTime.Now);
            visibleReRate &= (selfRating != null && Cl_SessionFacade.f_GetInstance().p_Doctor.p_UserID == selfRating.p_UserID);
            visibleReRate |= selfRating == null && allowEdit == false;

            f_SetVisibleElements(allowEdit);
            ctrlBReRate.Visible = visibleReRate;
        }

        private void f_SetVisibleElements(bool isEnable)
        {
            ctrlTBComment.ReadOnly = !isEnable;
            ctrlRBValue_1.Enabled = isEnable;
            ctrlRBValue_2.Enabled = isEnable;
            ctrlRBValue_3.Enabled = isEnable;
            ctrlRBValue_4.Enabled = isEnable;
            ctrlRBValue_5.Enabled = isEnable;

            ctrlBSave.Visible = isEnable;
        }

        private void ctrlBSave_Click(object sender, EventArgs e)
        {
            if (ctrlTBComment.Text == "" && MessageBox.Show("Поле \"Комментарий\" не заполенено.\nСохранить оценку без комментария?", "Сохранение оценки", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            if (this.curRecord == null)
            {
                MessageBox.Show("Запись еще не сохранена!\nУстановка оценки невозможна.", "Оценка записи", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Cl_Rating rating = new Cl_Rating();

            rating.p_RecordID = this.curRecord.p_RecordID;
            rating.p_Time = DateTime.Now;
            rating.p_Comment = ctrlTBComment.Text;
            rating.p_UserID = Cl_SessionFacade.f_GetInstance().p_Doctor.p_UserID;
            rating.p_UserName = Cl_SessionFacade.f_GetInstance().p_Doctor.f_GetInitials();

            if (ctrlRBValue_1.Checked) rating.p_Value = 1;
            if (ctrlRBValue_2.Checked) rating.p_Value = 2;
            if (ctrlRBValue_3.Checked) rating.p_Value = 3;
            if (ctrlRBValue_4.Checked) rating.p_Value = 4;
            if (ctrlRBValue_5.Checked) rating.p_Value = 5;
            rating.p_Value = 0;

            Cl_App.m_DataContext.p_Ratings.Add(rating);
            Cl_App.m_DataContext.SaveChanges();
            Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.Rating, string.Format("Выставлена оценка {0} для записи: {1}, дата записи: {2}, клиника: {3}", rating.p_Value, this.curRecord.p_Title, this.curRecord.p_DateCreate, this.curRecord.p_ClinicName), this.curRecord.p_RecordID);

            f_LoadRatingTable(rating.p_RecordID);
            selfRating = rating;
            if (firstRating == null) firstRating = rating;
        }

        private void ctrl_TRatings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ctrl_TRatings.CurrentRow == null) return;
            if (!(ctrl_TRatings.CurrentRow is DataGridViewRow)) return;

            DataGridViewRow curRow = ctrl_TRatings.CurrentRow;

            if (!(curRow.DataBoundItem is Cl_Rating)) return;

            Cl_Rating curRating = (Cl_Rating)curRow.DataBoundItem;
            f_FillRating(curRating);
            f_UpdateRateEditingState(curRating);
        }

        private void ctrlBReRate_Click(object sender, EventArgs e)
        {
            f_FillRating(selfRating);
            f_UpdateRateEditingState(selfRating);
        }
    }
}
