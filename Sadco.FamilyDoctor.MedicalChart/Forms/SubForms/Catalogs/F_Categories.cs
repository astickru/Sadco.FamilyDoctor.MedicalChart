using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
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

            f_RefreshTotal();
            f_RefreshKlinik();

            ctrlCategoriesTotal.Columns["p_ID"].Visible = false;
            ctrlCategoriesTotal.Columns["p_Type"].Visible = false;
            var col = ctrlCategoriesTotal.Columns["p_Name"];
            col.HeaderText = "Название категории";
            col.Width = 300;

            ctrlCategoriesKlinik.Columns["p_ID"].Visible = false;
            ctrlCategoriesKlinik.Columns["p_Type"].Visible = false;
            col = ctrlCategoriesKlinik.Columns["p_Name"];
            col.HeaderText = "Название категории";
            col.Width = 300;
        }

        private void f_RefreshTotal()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = Cl_App.m_DataContext.p_Categories.Local.ToBindingList().Where(c => c.p_Type == Cl_Category.E_CategoriesTypes.Total);
            ctrlCategoriesTotal.DataSource = bs;
        }

        private void f_RefreshKlinik()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = Cl_App.m_DataContext.p_Categories.Local.ToBindingList().Where(c => c.p_Type == Cl_Category.E_CategoriesTypes.Klinik);
            ctrlCategoriesKlinik.DataSource = bs;
        }

        private void ctrlAdd_Click(object sender, EventArgs e)
        {
            var wEdit = new F_CategoryEdit();
            wEdit.Text = "Добавление новой категории";
            if (ctrlCategoriesTab.SelectedTab == ctrlTabTotal)
            {
                wEdit.ctrlCategoryType.Text = "Общая категория";
                if (wEdit.ShowDialog() == DialogResult.OK)
                {
                    var cat = new Cl_Category();
                    cat.p_Type = Cl_Category.E_CategoriesTypes.Total;
                    cat.p_Name = wEdit.ctrlCategotyName.Text;
                    Cl_App.m_DataContext.p_Categories.Add(cat);
                    Cl_App.m_DataContext.SaveChanges();
                    f_RefreshTotal();
                }
            }
            else if (ctrlCategoriesTab.SelectedTab == ctrlTabKlinik)
            {
                wEdit.ctrlCategoryType.Text = "Клиническая категория";
                if (wEdit.ShowDialog() == DialogResult.OK)
                {
                    var cat = new Cl_Category();
                    cat.p_Type = Cl_Category.E_CategoriesTypes.Klinik;
                    cat.p_Name = wEdit.ctrlCategotyName.Text;
                    Cl_App.m_DataContext.p_Categories.Add(cat);
                    Cl_App.m_DataContext.SaveChanges();
                    f_RefreshKlinik();
                }
            }
        }

        private void ctrlEdit_Click(object sender, EventArgs e)
        {
            var wEdit = new F_CategoryEdit();
            if (ctrlCategoriesTab.SelectedTab == ctrlTabTotal)
            {
                if (ctrlCategoriesTotal.SelectedRows.Count == 1)
                {
                    var cat = (Cl_Category)ctrlCategoriesTotal.SelectedRows[0].DataBoundItem;
                    if (cat != null)
                    {
                        wEdit.Text = string.Format("Изменение категории \"{0}\"", cat.p_Name);
                        wEdit.ctrlCategoryType.Text = "Общая категория";
                        if (wEdit.ShowDialog() == DialogResult.OK)
                        {
                            cat.p_Name = wEdit.ctrlCategotyName.Text;
                            Cl_App.m_DataContext.SaveChanges();
                            f_RefreshTotal();
                        }
                    }
                }
            }
            else if (ctrlCategoriesTab.SelectedTab == ctrlTabKlinik)
            {
                if (ctrlCategoriesKlinik.SelectedRows.Count == 1)
                {
                    var cat = (Cl_Category)ctrlCategoriesKlinik.SelectedRows[0].DataBoundItem;
                    if (cat != null)
                    {
                        wEdit.Text = string.Format("Изменение категории \"{0}\"", cat.p_Name);
                        wEdit.ctrlCategoryType.Text = "Клиническая категория";
                        if (wEdit.ShowDialog() == DialogResult.OK)
                        {
                            cat.p_Name = wEdit.ctrlCategotyName.Text;
                            Cl_App.m_DataContext.SaveChanges();
                            f_RefreshKlinik();
                        }
                    }
                }
            }
        }

        private void ctrlDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить категорию?", "Удаление категории", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    if (ctrlCategoriesTab.SelectedTab == ctrlTabTotal)
                    {
                        if (ctrlCategoriesTotal.SelectedRows.Count == 1)
                        {
                            var cat = (Cl_Category)ctrlCategoriesTotal.SelectedRows[0].DataBoundItem;
                            if (cat != null)
                            {
                                Cl_App.m_DataContext.p_Categories.Remove(cat);
                                Cl_App.m_DataContext.SaveChanges();
                                transaction.Commit();
                                f_RefreshTotal();
                            }
                        }
                    }
                    else if (ctrlCategoriesTab.SelectedTab == ctrlTabKlinik)
                    {
                        if (ctrlCategoriesKlinik.SelectedRows.Count == 1)
                        {
                            var cat = (Cl_Category)ctrlCategoriesKlinik.SelectedRows[0].DataBoundItem;
                            if (cat != null)
                            {
                                Cl_App.m_DataContext.p_Categories.Remove(cat);
                                Cl_App.m_DataContext.SaveChanges();
                                transaction.Commit();
                                f_RefreshKlinik();
                            }
                        }
                    }
                }
                catch
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Tree", "Нельзя удалить категорию", null, null, null);
                }
            }
        }

        private void f_UpdateButtons()
        {
            if (ctrlCategoriesTab.SelectedTab == ctrlTabTotal)
            {
                ctrlEdit.Enabled = ctrlDelete.Enabled = ctrlCategoriesTotal.SelectedRows.Count == 1;
            }
            else if (ctrlCategoriesTab.SelectedTab == ctrlTabKlinik)
            {
                ctrlEdit.Enabled = ctrlDelete.Enabled = ctrlCategoriesKlinik.SelectedRows.Count == 1;
            }
        }

        private void ctrlCategoriesTotal_SelectionChanged(object sender, EventArgs e)
        {
            f_UpdateButtons();
        }

        private void ctrlCategoriesKlinik_SelectionChanged(object sender, EventArgs e)
        {
            f_UpdateButtons();
        }
    }
}
