﻿using Sadco.FamilyDoctor.Core;
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
			IOrderedQueryable<Cl_Log> logs = Cl_App.m_DataContext.p_Logs.Where(l => l.p_ElementID == LoadForID).OrderByDescending(d => d.p_ChangeTime);
			if (logs.Count() == 0) return;

            foreach(Cl_Log log in logs)
            {
                var row = dtLogs.NewRow();
                row["cl_date"] = log.p_ChangeTime;
                row["cl_version"] = log.p_Version == 0 ? "Черновик" : log.p_Version.ToString();
                row["cl_event"] = log.p_Event;
                row["cl_user"] = log.p_UserName;
                dtLogs.Rows.Add(row);
            }

			ctrl_DGLogs.DataSource = dtLogs;
		}
	}
}
