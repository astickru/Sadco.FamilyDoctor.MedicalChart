using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_BorderedTextBox : TextBox, I_CtrlBordered
    {
        public Color p_BorderColor { get; set; } = Color.Black;
        public int p_BorderWidth { get; set; } = 1;

        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        private const int WM_NCPAINT = 0x85;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCPAINT)
            {
                var dc = GetWindowDC(Handle);
                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawRectangle(new Pen(p_BorderColor, p_BorderWidth), p_BorderWidth, p_BorderWidth, Width - p_BorderWidth, Height - p_BorderWidth);
                }
            }
        }
    }
}
