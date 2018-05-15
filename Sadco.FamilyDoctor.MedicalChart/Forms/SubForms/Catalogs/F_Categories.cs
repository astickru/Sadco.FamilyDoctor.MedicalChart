using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Catalogs
{
    public partial class F_Categories : Form
    {
        public F_Categories()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();

            Cl_App.m_DataContext.p_Categories.Load();


            //BindingSource bs = new BindingSource();
            var query = Cl_App.m_DataContext.p_Categories.Local.Where(c => c.p_Type == Cl_Category.E_CategoriesTypes.Total).ToList();
            var binding = new BindingList<Cl_Category>(query);
            ctrlCategoriesTotal.DataSource = binding;



            //bs.DataSource = Cl_App.m_DataContext.p_Categories.Local.ToBindingList();
            //bs.Filter = "2 = 1" + Cl_Category.E_CategoriesTypes.Total.GetHashCode().ToString();

            //ctrlCategoriesTotal.DataSource = bs;


            ctrlCategoriesTotal.Columns["p_ID"].Visible = false;
            ctrlCategoriesTotal.Columns["p_Type"].Visible = false;
            var col = ctrlCategoriesTotal.Columns["p_Name"];
            col.HeaderText = "Название категории";
            col.Width = 300;

            //ctrlCategoriesKlinik.DataSource = Cl_App.m_DataContext.p_Categories.Local.ToBindingList();
            //ctrlCategoriesKlinik.Columns["p_ID"].Visible = false;
            //ctrlCategoriesKlinik.Columns["p_Type"].Visible = false;
            //col = ctrlCategoriesKlinik.Columns["p_Name"];
            //col.HeaderText = "Название категории";
            //col.Width = 300;
        }

        private void ctrlSave_Click(object sender, EventArgs e)
        {
            //var binding = (BindingList<Cl_Category>)ctrlCategoriesTotal.DataSource;
            //Cl_App.m_DataContext.p_Categories..Where(c => c.p_Type == Cl_Category.E_CategoriesTypes.Total).Union(binding);

            var binding = (BindingList<Cl_Category>)ctrlCategoriesTotal.DataSource;
            Cl_App.m_DataContext.p_Categories.Where(c => c.p_Type == Cl_Category.E_CategoriesTypes.Total).Union(binding);


            Cl_App.m_DataContext.SaveChanges();
            ctrlCategoriesTotal.Refresh();
        }

        private void f_Reset()
        {
            //Cl_App.m_DataContext.p_Categories.Load();
            //var query = Cl_App.m_DataContext.p_Categories.Local.Where(c => c.p_Type == Cl_Category.E_CategoriesTypes.Total).ToList();
            //var binding = new BindingList<Cl_Category>(query);
            //ctrlCategoriesTotal.DataSource = binding;



            //foreach (var entityEntry in Cl_App.m_DataContext.ChangeTracker.Entries().ToArray())
            //{
            //    if (entityEntry.Entity != null)
            //    {
            //        entityEntry.State = EntityState.Detached;
            //    }
            //}
            //Cl_App.m_DataContext.p_Categories.Load();

            //ctrlCategoriesTotal.Refresh();
        }

        private void ctrlReset_Click(object sender, EventArgs e)
        {
            f_Reset();
        }

        private void F_Categories_FormClosing(object sender, FormClosingEventArgs e)
        {
            f_Reset();
        }
    }
}
