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
            f_RefreshClinik();
        }

        private void f_RefreshTotal()
        {
            try
            {
                var categories = Cl_App.m_DataContext.p_Categories.Local.ToBindingList().Where(c => c.p_Type == Cl_Category.E_CategoriesTypes.Total);
                if (categories.Count() > 0)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = categories;
                    ctrlCategoriesTotal.DataSource = bs;
                    var col = ctrlCategoriesTotal.Columns["p_Name"];
                    ctrlCategoriesTotal.Columns["p_ID"].Visible = false;
                    ctrlCategoriesTotal.Columns["p_Type"].Visible = false;
                    col.HeaderText = "Название категории";
                    col.Width = 300;
                }
                else
                {
                    ctrlCategoriesTotal.DataSource = null;
                }
                if (ctrlCategoriesTotal.Rows.Count > 0)
                {
                    ctrlCategoriesTotal.Rows[ctrlCategoriesTotal.Rows.Count - 1].Selected = true;
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось обновить общую категорию", er, null, null);
            }
        }

        private void f_RefreshClinik()
        {
            var categories = Cl_App.m_DataContext.p_Categories.Local.ToBindingList().Where(c => c.p_Type == Cl_Category.E_CategoriesTypes.Clinic);
            if (categories.Count() > 0)
            {
                try
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = categories;
                    ctrlCategoriesClinik.DataSource = bs;
                    var col = ctrlCategoriesClinik.Columns["p_Name"];
                    ctrlCategoriesClinik.Columns["p_ID"].Visible = false;
                    ctrlCategoriesClinik.Columns["p_Type"].Visible = false;
                    col.HeaderText = "Название категории";
                    col.Width = 300;
                }
                catch (Exception er)
                {
                    MonitoringStub.Error("Error_Editor", "Не удалось обновить клиническую категорию", er, null, null);
                }
                if (ctrlCategoriesClinik.Rows.Count > 0)
                {
                    ctrlCategoriesClinik.Rows[ctrlCategoriesClinik.Rows.Count - 1].Selected = true;
                }
            }
            else
            {
                ctrlCategoriesClinik.DataSource = null;
            }
        }

        private void ctrlAdd_Click(object sender, EventArgs e)
        {
            var wEdit = new F_CategoryEdit();
            wEdit.Text = "Добавление новой категории";
            try
            {
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
                else if (ctrlCategoriesTab.SelectedTab == ctrlTabClinik)
                {
                    wEdit.ctrlCategoryType.Text = "Клиническая категория";
                    if (wEdit.ShowDialog() == DialogResult.OK)
                    {
                        var cat = new Cl_Category();
                        cat.p_Type = Cl_Category.E_CategoriesTypes.Clinic;
                        cat.p_Name = wEdit.ctrlCategotyName.Text;
                        Cl_App.m_DataContext.p_Categories.Add(cat);
                        Cl_App.m_DataContext.SaveChanges();
                        f_RefreshClinik();
                    }
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось добавить новую категорию", er, null, null);
            }
        }

        private void ctrlEdit_Click(object sender, EventArgs e)
        {
            var wEdit = new F_CategoryEdit();
            try
            {
                if (ctrlCategoriesTab.SelectedTab == ctrlTabTotal)
                {
                    if (ctrlCategoriesTotal.SelectedRows.Count == 1)
                    {
                        var cat = (Cl_Category)ctrlCategoriesTotal.SelectedRows[0].DataBoundItem;
                        if (cat != null)
                        {
                            wEdit.Text = string.Format("Изменение категории \"{0}\"", cat.p_Name);
                            wEdit.ctrlCategoryType.Text = "Общая категория";
                            wEdit.ctrlCategotyName.Text = cat.p_Name;
                            if (wEdit.ShowDialog() == DialogResult.OK)
                            {
                                cat.p_Name = wEdit.ctrlCategotyName.Text;
                                Cl_App.m_DataContext.SaveChanges();
                                f_RefreshTotal();
                            }
                        }
                    }
                }
                else if (ctrlCategoriesTab.SelectedTab == ctrlTabClinik)
                {
                    if (ctrlCategoriesClinik.SelectedRows.Count == 1)
                    {
                        var cat = (Cl_Category)ctrlCategoriesClinik.SelectedRows[0].DataBoundItem;
                        if (cat != null)
                        {
                            wEdit.Text = string.Format("Изменение категории \"{0}\"", cat.p_Name);
                            wEdit.ctrlCategoryType.Text = "Клиническая категория";
                            wEdit.ctrlCategotyName.Text = cat.p_Name;
                            if (wEdit.ShowDialog() == DialogResult.OK)
                            {
                                cat.p_Name = wEdit.ctrlCategotyName.Text;
                                Cl_App.m_DataContext.SaveChanges();
                                f_RefreshClinik();
                            }
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось отредактировать категорию", er, null, null);
            }
        }

        private void ctrlDelete_Click(object sender, EventArgs e)
        {
            Cl_Category cat = null;
            if (ctrlCategoriesTab.SelectedTab == ctrlTabTotal)
            {
                cat = (Cl_Category)ctrlCategoriesTotal.SelectedRows[0].DataBoundItem;
            }
            else if (ctrlCategoriesTab.SelectedTab == ctrlTabClinik)
            {
                cat = (Cl_Category)ctrlCategoriesClinik.SelectedRows[0].DataBoundItem;
            }
            if (cat != null)
            {
                if (MessageBox.Show($"Удалить категорию {cat.p_Name}?", "Удаление категории", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (ctrlCategoriesTab.SelectedTab == ctrlTabTotal)
                        {
                            if (ctrlCategoriesTotal.SelectedRows.Count == 1)
                            {
                                if (cat != null)
                                {
                                    Cl_App.m_DataContext.p_Categories.Remove(cat);
                                    Cl_App.m_DataContext.SaveChanges();
                                    transaction.Commit();
                                    f_RefreshTotal();
                                }
                            }
                        }
                        else if (ctrlCategoriesTab.SelectedTab == ctrlTabClinik)
                        {
                            if (ctrlCategoriesClinik.SelectedRows.Count == 1)
                            {
                                if (cat != null)
                                {
                                    Cl_App.m_DataContext.p_Categories.Remove(cat);
                                    Cl_App.m_DataContext.SaveChanges();
                                    transaction.Commit();
                                    f_RefreshClinik();
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
        }

        private void f_UpdateButtons()
        {
            if (ctrlCategoriesTab.SelectedTab == ctrlTabTotal)
            {
                ctrlEdit.Enabled = ctrlDelete.Enabled = ctrlCategoriesTotal.SelectedRows.Count == 1;
            }
            else if (ctrlCategoriesTab.SelectedTab == ctrlTabClinik)
            {
                ctrlEdit.Enabled = ctrlDelete.Enabled = ctrlCategoriesClinik.SelectedRows.Count == 1;
            }
        }

        private void ctrlCategoriesTotal_SelectionChanged(object sender, EventArgs e)
        {
            f_UpdateButtons();
        }

        private void ctrlCategoriesClinik_SelectionChanged(object sender, EventArgs e)
        {
            f_UpdateButtons();
        }
    }
}
