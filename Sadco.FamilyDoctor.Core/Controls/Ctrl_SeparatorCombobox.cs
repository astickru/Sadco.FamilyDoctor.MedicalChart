using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_SeparatorCombobox : ComboBox
    {
        #region Constructor
        public Ctrl_SeparatorCombobox()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
            m_SeparatorStyle = DashStyle.Solid;
            m_Separators = new ArrayList();

            m_SeparatorStyle = DashStyle.Solid;
            m_SeparatorColor = Color.Black;
            m_SeparatorMargin = 1;
            m_SeparatorWidth = 1;
            m_AutoAdjustItemHeight = false;
        }
        #endregion

        private ArrayList m_Separators;
        private DashStyle m_SeparatorStyle;
        private Color m_SeparatorColor;
        private int m_SeparatorWidth;
        private int m_SeparatorMargin;
        private bool m_AutoAdjustItemHeight;

        #region Medthods

        public void f_AddString(string s)
        {
            Items.Add(s);
        }

        public void f_AddStringWithSeparator(string s)
        {
            Items.Add(s);
            m_Separators.Add(s);
        }

        public void f_SetSeparator(int pos)
        {
            m_Separators.Add(pos);
        }

        public void f_Clear()
        {
            Items.Clear();
            m_Separators.Clear();
        }
        #endregion

        #region Properties

        [Description("Gets or sets the Separator Style"), Category("Separator")]
        public DashStyle p_SeparatorStyle {
            get { return m_SeparatorStyle; }
            set { m_SeparatorStyle = value; }
        }

        [Description("Gets or sets the Separator Color"), Category("Separator")]
        public Color p_SeparatorColor {
            get { return m_SeparatorColor; }
            set { m_SeparatorColor = value; }
        }

        [Description("Gets or sets the Separator Width"), Category("Separator")]
        public int p_SeparatorWidth {
            get { return m_SeparatorWidth; }
            set { m_SeparatorWidth = value; }
        }

        [Description("Gets or sets the Separator Margin"), Category("Separator")]
        public int p_SeparatorMargin {
            get { return m_SeparatorMargin; }
            set { m_SeparatorMargin = value; }
        }

        [Description("Gets or sets Auto Adjust Item Height"), Category("Separator")]
        public bool p_AutoAdjustItemHeight {
            get { return m_AutoAdjustItemHeight; }
            set { m_AutoAdjustItemHeight = value; }
        }

        #endregion

        #region Overrides

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (m_AutoAdjustItemHeight)
                e.ItemHeight += m_SeparatorWidth;

            base.OnMeasureItem(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (-1 == e.Index) return;

            bool sep = false;
            object o;
            for (int i = 0; !sep && i < m_Separators.Count; i++)
            {
                o = m_Separators[i];

                if (o is string)
                {
                    if ((string)this.Items[e.Index] == o as string)
                        sep = true;
                }
                else
                {
                    int pos = (int)o;
                    if (pos < 0) pos += Items.Count;

                    if (e.Index == pos) sep = true;
                }
            }

            e.DrawBackground();
            Graphics g = e.Graphics;
            int y = e.Bounds.Location.Y + m_SeparatorWidth - 1;

            if (sep)
            {
                Pen pen = new Pen(m_SeparatorColor, m_SeparatorWidth);
                pen.DashStyle = m_SeparatorStyle;

                g.DrawLine(pen, e.Bounds.Location.X + m_SeparatorMargin, y, e.Bounds.Location.X + e.Bounds.Width - m_SeparatorMargin, y);
                y++;
            }

            Brush br = DrawItemState.Selected == (DrawItemState.Selected & e.State) ? SystemBrushes.HighlightText : new SolidBrush(e.ForeColor);
            g.DrawString((string)Items[e.Index], e.Font, br, e.Bounds.Left, y + 1);

            base.OnDrawItem(e);
        }

        #endregion
    }
}
