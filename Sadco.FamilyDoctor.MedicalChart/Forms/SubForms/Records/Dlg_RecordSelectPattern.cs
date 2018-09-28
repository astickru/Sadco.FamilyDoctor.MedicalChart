using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class Dlg_RecordSelectPattern : Form
    {
        public Dlg_RecordSelectPattern()
        {
            f_Init(null);
        }

        public Dlg_RecordSelectPattern(Cl_Template a_Template)
        {
            f_Init(a_Template);
        }

        public void f_Init(Cl_Template a_Template)
        {
            if (a_Template != null)
            {
                Text = string.Format("Выбор паттерна для записи v{0}", ConfigurationManager.AppSettings["Version"]);
                InitializeComponent();

                var userId = Cl_SessionFacade.f_GetInstance().p_Doctor.p_UserID;
                ctrlTablePatterns.Columns.Clear();
                var patternsDb = Cl_App.m_DataContext.p_RecordsPatterns.Include(p => p.p_Template).Include(p => p.p_CategoryClinic).Include(p => p.p_CategoryTotal)
                            .Include(p => p.p_Values).Include(r => r.p_Values.Select(v => v.p_Params)).Where(p => p.p_DoctorID == userId);
                if (a_Template != null)
                {
                    patternsDb = patternsDb.Where(p => p.p_TemplateID == a_Template.p_ID);
                }
                m_Patterns = patternsDb.ToList();
                var patterns = m_Patterns.Select(p => new { p.p_ID, p.p_Name, p_TemplateName = p.p_Template.p_Name }).ToList();
                ctrlTablePatterns.DataSource = patterns;
                ctrlTablePatterns.Columns[0].Visible = false;
                ctrlTablePatterns.Columns[1].Width = p_Name.Width;
                ctrlTablePatterns.Columns[1].HeaderText = p_Name.HeaderText;
                ctrlTablePatterns.Columns[2].Width = p_TemplateName.Width;
                ctrlTablePatterns.Columns[2].HeaderText = p_TemplateName.HeaderText;
            }
        }

        private List<Cl_RecordPattern> m_Patterns = null;

        public Cl_Template p_Template { get; set; }

        public Cl_RecordPattern p_SelectedRecordPattern {
            get {
                if (ctrlTablePatterns.SelectedRows != null && ctrlTablePatterns.SelectedRows.Count == 1)
                {
                    var id = (int)ctrlTablePatterns.SelectedRows[0].Cells[0].Value;
                    var pattern = m_Patterns.FirstOrDefault(p => p.p_ID == id);
                    if (pattern != null && pattern.p_Template != null)
                    {
                        return pattern;
                    }
                    return pattern;
                }
                return null;
            }
        }

        private void ctrlTablePatterns_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (p_SelectedRecordPattern != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
