using FD.dat.mon.stb.lib;
using System.Linq;
using System.Windows.Forms;
using static Sadco.FamilyDoctor.Core.Entities.Cl_Template;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public partial class Dlg_EditorTemplate : Form
    {
        public Dlg_EditorTemplate()
        {
            InitializeComponent();
            ctrlCategoriesTotal.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ctrlCategoriesTotal.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ctrlCategoriesTotal.DisplayMember = "p_Name";
            ctrlCategoriesTotal.ValueMember = "p_ID";
            ctrlCategoriesKlinik.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ctrlCategoriesKlinik.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ctrlCategoriesKlinik.DisplayMember = "p_Name";
            ctrlCategoriesKlinik.ValueMember = "p_ID";

            var categories = Cl_App.m_DataContext.p_Categories.ToArray();
            var catsTotal = categories.Where(c => c.p_Type == Entities.Cl_Category.E_CategoriesTypes.Total).ToArray();
            var catsKlinik = categories.Where(c => c.p_Type == Entities.Cl_Category.E_CategoriesTypes.Klinik).ToArray();
            ctrlCategoriesTotal.AutoCompleteCustomSource.AddRange(catsTotal.Select(e => e.p_Name).ToArray());
            ctrlCategoriesKlinik.AutoCompleteCustomSource.AddRange(catsKlinik.Select(e => e.p_Name).ToArray());
            ctrlCategoriesTotal.Items.AddRange(catsTotal);
            ctrlCategoriesKlinik.Items.AddRange(catsKlinik);
        }

        private void Dlg_EditorTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(ctrl_TBName.Text))
                {
                    MonitoringStub.Message("Наименование шаблона пустое!");
                    e.Cancel = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(ctrlTitle.Text))
                {
                    MonitoringStub.Message("Заголовок шаблона пустое!");
                    e.Cancel = true;
                    return;
                }
                if (ctrlCategoriesTotal.SelectedItem == null)
                {
                    MonitoringStub.Message("Не выбрана общая категория!");
                    e.Cancel = true;
                    return;
                }
                if (ctrlCategoriesKlinik.SelectedItem == null)
                {
                    MonitoringStub.Message("Не выбрана клиническая категория!");
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
