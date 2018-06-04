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
            ctrlCategoriesClinik.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ctrlCategoriesClinik.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ctrlCategoriesClinik.DisplayMember = "p_Name";
            ctrlCategoriesClinik.ValueMember = "p_ID";

            var categories = Cl_App.m_DataContext.p_Categories.ToArray();
            var catsTotal = categories.Where(c => c.p_Type == Entities.Cl_Category.E_CategoriesTypes.Total).ToArray();
            var catsClinik = categories.Where(c => c.p_Type == Entities.Cl_Category.E_CategoriesTypes.Clinik).ToArray();
            ctrlCategoriesTotal.AutoCompleteCustomSource.AddRange(catsTotal.Select(e => e.p_Name).ToArray());
            ctrlCategoriesClinik.AutoCompleteCustomSource.AddRange(catsClinik.Select(e => e.p_Name).ToArray());
            ctrlCategoriesTotal.Items.AddRange(catsTotal);
            ctrlCategoriesClinik.Items.AddRange(catsClinik);
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
                if (ctrlCategoriesTotal.Enabled && ctrlCategoriesTotal.SelectedItem == null)
                {
                    MonitoringStub.Message("Не выбрана общая категория!");
                    e.Cancel = true;
                    return;
                }
                if (ctrlCategoriesClinik.Enabled && ctrlCategoriesClinik.SelectedItem == null)
                {
                    MonitoringStub.Message("Не выбрана клиническая категория!");
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
