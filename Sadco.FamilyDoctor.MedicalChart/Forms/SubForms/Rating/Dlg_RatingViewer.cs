using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class Dlg_RatingViewer : Form
    {
        int recordID = 0;
        Cl_Rating curRating = null;

        public Dlg_RatingViewer()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();

            this.Load += Dlg_RatingViewer_Load;
        }

        private void Dlg_RatingViewer_Load(object sender, EventArgs e)
        {
            ctrlRBValue_5.Checked = true;
            
            if (curRating == null) return;
            if (DateTime.Now < curRating.p_Time.AddDays(1)) return;

            ctrlTBComment.Enabled = false;
            ctrlRBValue_1.Enabled = false;
            ctrlRBValue_2.Enabled = ctrlRBValue_1.Enabled;
            ctrlRBValue_3.Enabled = ctrlRBValue_1.Enabled;
            ctrlRBValue_4.Enabled = ctrlRBValue_1.Enabled;
            ctrlRBValue_5.Enabled = ctrlRBValue_1.Enabled;

            ctrlBSave.Enabled = false;
        }

        internal void LoadRating(int p_RecordID)
        {
            int userID = Cl_SessionFacade.f_GetInstance().p_User.p_UserID;
            recordID = p_RecordID;
            curRating = Cl_App.m_DataContext.p_Ratings.Where(l => l.p_RecordID == p_RecordID && l.p_UserID == userID).FirstOrDefault();

            ctrlLAuthor.Text = Cl_SessionFacade.f_GetInstance().p_User.f_GetInitials();
            ctrlLDate.Text = DateTime.Now.ToString();

            if (curRating == null) return;

            ctrlLDate.Text = curRating.p_Time.ToString();
            ctrlTBComment.Text = curRating.p_Comment;

            ctrlRBValue_1.Checked = curRating.p_Value == 1;
            ctrlRBValue_2.Checked = curRating.p_Value == 2;
            ctrlRBValue_3.Checked = curRating.p_Value == 3;
            ctrlRBValue_4.Checked = curRating.p_Value == 4;
            ctrlRBValue_5.Checked = curRating.p_Value == 5;
        }

        private void ctrlBSave_Click(object sender, EventArgs e)
        {
            Cl_Rating rating = null;

            if (curRating == null)
                rating = new Cl_Rating();
            else
                rating = curRating;

            rating.p_RecordID = recordID;
            rating.p_Time = DateTime.Now;
            rating.p_Comment = ctrlTBComment.Text;
            rating.p_UserID = Cl_SessionFacade.f_GetInstance().p_User.p_UserID;
            rating.p_UserName = Cl_SessionFacade.f_GetInstance().p_User.f_GetInitials();

            if (ctrlRBValue_1.Checked) rating.p_Value = 1;
            if (ctrlRBValue_2.Checked) rating.p_Value = 2;
            if (ctrlRBValue_3.Checked) rating.p_Value = 3;
            if (ctrlRBValue_4.Checked) rating.p_Value = 4;
            if (ctrlRBValue_5.Checked) rating.p_Value = 5;

            if (curRating == null)
                Cl_App.m_DataContext.p_Ratings.Add(rating);

            Cl_App.m_DataContext.SaveChanges();
        }
    }
}
