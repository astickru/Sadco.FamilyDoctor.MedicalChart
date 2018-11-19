using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public partial class Ctrl_BorderedPanel : Panel, I_CtrlBordered
    {
        public Ctrl_BorderedPanel()
        {
            InitializeComponent();
            p_BorderColor = Color.Black;
            p_BorderWidth = 1;
        }

        public Ctrl_BorderedPanel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            p_BorderColor = Color.Black;
            p_BorderWidth = 1;
        }

        [DefaultValue(typeof(Color), "Black")]
        public Color p_BorderColor { get; set; }

        public int _borderWidth = 1;
        public int p_BorderWidth {
            get {
                return _borderWidth;
            }
            set {
                _borderWidth = value;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            var d = p_BorderWidth / 2;
            using (var pen = new Pen(p_BorderColor, p_BorderWidth))
                e.Graphics.DrawRectangle(pen, d, d, Width - 2 * d - 1, Height - 2 * d - 1);
        }
    }
}
