using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TextBoxAutoHeight : TextBox
    {
        public Ctrl_TextBoxAutoHeight()
        {
            Multiline = true;
            DoubleBuffered = true;
            f_SetHeight();
        }

        private int m_MinLines = 3;
        public int p_MinLines {
            get {
                return m_MinLines;
            }
            set {
                m_MinLines = value;
                m_BlockSize = true;
                Height = f_GetHeightLine() * p_MinLines;
                m_BlockSize = false;
            }
        }

        private bool m_BlockSize = false;
        private void f_SetHeight()
        {
            m_BlockSize = true;
            Height = f_GetHeightLine() * p_MinLines;
            m_BlockSize = false;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!m_BlockSize)
            {
                f_SetHeight();
            }
        }

        private int f_GetHeightLine()
        {
            using (Graphics g = CreateGraphics())
            {
                return TextRenderer.MeasureText(g, "1", Font).Height;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            int countLines = Text.Split('\n').Length + 1;
            if (p_MinLines > countLines)
                countLines = p_MinLines;
            m_BlockSize = true;
            Height = f_GetHeightLine() * countLines;
            m_BlockSize = false;
        }
    }
}
