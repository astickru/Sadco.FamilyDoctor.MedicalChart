using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public partial class Ctrl_MacrosEditor : Control
    {
        public Ctrl_MacrosEditor()
        {
            InitializeComponent();
            f_InitializePanels();
            f_InitializeMenu();
        }

        private void f_InitializePanels()
        {
            this.MinimumSize = new Size(300, 150);

            this.FBDEditorPanel.Parent = this;
            this.FBDEditorPanel.Dock = DockStyle.Fill;
            this.FBDEditorPanel.BackColor = Color.LightYellow;
            this.FBDEditorPanel.BorderStyle = BorderStyle.Fixed3D;

            this.InputValuesPanel.Parent = this;
            this.InputValuesPanel.Dock = DockStyle.Left;
            this.InputValuesPanel.BackColor = Color.LightGray;
            this.InputValuesPanel.Width = 40;

            this.ResultValuePanel.Parent = this;
            this.ResultValuePanel.Dock = DockStyle.Right;
            this.ResultValuePanel.BackColor = Color.LightGray;
            this.ResultValuePanel.Width = 40;

            this.FBDToolStrip.Parent = this;
            this.FBDToolStrip.Dock = DockStyle.Top;
        }

        private void f_InitializeMenu()
        {
            this.FBDToolStrip.Items.Add("asas");
        }

        protected override Size DefaultSize {
            get {
                return new Size(300, 150);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
