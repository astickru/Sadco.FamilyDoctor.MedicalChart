using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class Dlg_HistoryViewer : Form
    {
        DataTable dtLogs = new DataTable();

        public Dlg_HistoryViewer()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
            foreach (DataGridViewColumn col in ctrl_DGLogs.Columns)
            {
                dtLogs.Columns.Add(col.Name);
                col.DataPropertyName = col.Name;
            }
        }

        public void LoadHistory(int LoadForID)
        {
            LoadRow(LoadForID);

            ctrl_DGLogs.DataSource = dtLogs;
        }

        private void LoadRow(int LoadForID)
        {
            Cl_Log log = Cl_App.m_DataContext.p_Logs.Where(l => l.p_ElementID == LoadForID).FirstOrDefault();
            if (log != null)
            {
                var row = dtLogs.NewRow();
                row["cl_date"] = log.p_ChangeTime;
                row["cl_event"] = log.p_Event;
                row["cl_user"] = log.p_UserName;
                dtLogs.Rows.Add(row);

                if (log.p_PrevElementID != 0)
                    LoadRow(log.p_PrevElementID);
            }
        }
    }
}
