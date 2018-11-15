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
            ctrlCategoriesClinic.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ctrlCategoriesClinic.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ctrlCategoriesClinic.DisplayMember = "p_Name";
            ctrlCategoriesClinic.ValueMember = "p_ID";

            var categories = Cl_App.m_DataContext.p_Categories.ToArray();
            var catsTotal = categories.Where(c => c.p_Type == Entities.Cl_Category.E_CategoriesTypes.Total).ToArray();
            var catsClinic = categories.Where(c => c.p_Type == Entities.Cl_Category.E_CategoriesTypes.Clinic).ToArray();
            ctrlCategoriesTotal.AutoCompleteCustomSource.AddRange(catsTotal.Select(e => e.p_Name).ToArray());
            ctrlCategoriesClinic.AutoCompleteCustomSource.AddRange(catsClinic.Select(e => e.p_Name).ToArray());
            ctrlCategoriesTotal.Items.AddRange(catsTotal);
            ctrlCategoriesClinic.Items.AddRange(catsClinic);
        }

        /// <summary>Количество столбцов</summary>
        public int p_CountColumn {
            get {
                if (ctrlCountColumn1.Checked)
                    return 1;
                else
                    return 2;
            }
            set {
                if (value == 1)
                    ctrlCountColumn1.Checked = true;
                else
                    ctrlCountColumn2.Checked = true;
            }
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
                if (ctrlCategoriesClinic.Enabled && ctrlCategoriesClinic.SelectedItem == null)
                {
                    MonitoringStub.Message("Не выбрана клиническая категория!");
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
