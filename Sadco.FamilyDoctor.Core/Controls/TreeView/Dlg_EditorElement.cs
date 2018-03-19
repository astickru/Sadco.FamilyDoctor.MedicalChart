using System.Windows.Forms;
using static Sadco.FamilyDoctor.Core.Entities.Cl_Element;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public partial class Dlg_EditorElement : Form
    {
        public Dlg_EditorElement()
        {
            InitializeComponent();
            ctrl_CB_ControlType.f_SetEnum(typeof(E_TextTypes));
        }
    }
}
