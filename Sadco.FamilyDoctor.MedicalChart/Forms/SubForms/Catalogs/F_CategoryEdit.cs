using FD.dat.mon.stb.lib;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Catalogs
{
    public partial class F_CategoryEdit : Form
    {
        public F_CategoryEdit()
        {
            InitializeComponent();
        }

        private void F_CategoryEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && string.IsNullOrWhiteSpace(ctrlCategotyName.Text))
            {
                MonitoringStub.Message("Название категории не должно быть пустым!");
                e.Cancel = true;
            }
        }
    }
}
