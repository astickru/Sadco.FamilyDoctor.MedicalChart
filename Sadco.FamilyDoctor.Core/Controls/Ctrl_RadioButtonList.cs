using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_RadioButtonList : ListBox
    {
        private Size m_Size;
        public Ctrl_RadioButtonList()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            this.BackColor = SystemColors.Control;
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
                m_Size = RadioButtonRenderer.GetGlyphSize(Graphics.FromHwnd(IntPtr.Zero), RadioButtonState.CheckedNormal);
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var text = (Items.Count > 0) ? GetItemText(Items[e.Index]) : Name;
            Rectangle r = e.Bounds;
            Point p;
            var flags = TextFormatFlags.Default | TextFormatFlags.NoPrefix;
            var selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            var state = selected ?
                    (Enabled ? RadioButtonState.CheckedNormal :
                                         RadioButtonState.CheckedDisabled) :
                    (Enabled ? RadioButtonState.UncheckedNormal :
                                         RadioButtonState.UncheckedDisabled);
            if (RightToLeft == RightToLeft.Yes)
            {
                p = new Point(r.Right - r.Height + (ItemHeight - m_Size.Width) / 2,
                        r.Top + (ItemHeight - m_Size.Height) / 2);
                r = new Rectangle(r.Left, r.Top, r.Width - r.Height, r.Height);
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }
            else
            {
                p = new Point(r.Left + (ItemHeight - m_Size.Width) / 2,
                r.Top + (ItemHeight - m_Size.Height) / 2);
                r = new Rectangle(r.Left + r.Height, r.Top, r.Width - r.Height, r.Height);
            }
            var bc = selected ? (Enabled ? SystemColors.Highlight : SystemColors.InactiveBorder) : BackColor;
            var fc = selected ? (Enabled ? SystemColors.HighlightText : SystemColors.GrayText) : ForeColor;
            using (var b = new SolidBrush(bc))
                e.Graphics.FillRectangle(b, e.Bounds);
            RadioButtonRenderer.DrawRadioButton(e.Graphics, p, state);
            TextRenderer.DrawText(e.Graphics, text, Font, r, fc, bc, flags);
            e.DrawFocusRectangle();
            base.OnDrawItem(e);
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override SelectionMode SelectionMode {
            get { return SelectionMode.One; }
            set { }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override int ItemHeight {
            get { return (this.Font.Height + 2); }
            set { }
        }
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DrawMode DrawMode {
            get { return base.DrawMode; }
            set { base.DrawMode = DrawMode.OwnerDrawFixed; }
        }
        private Type m_EnumType = null;
        /// <summary>Установка в список значений перечислитель</summary>
        /// <param name="a_EnumType">Тип перечислителя</param>
        /// <returns>Флаг удачной операции</returns>
        public bool f_SetEnum(Type a_EnumType)
        {
            if (a_EnumType.IsEnum)
            {
                DisplayMember = "Description";
                ValueMember = "Value";
                DataSource = Enum.GetValues(a_EnumType)
                        .Cast<Enum>()
                        .Select(value => new
                        {
                            (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                            value
                        })
                        .OrderBy(item => item.value)
                        .ToList();
                m_EnumType = a_EnumType;
            }
            return false;
        }
        /// <summary>Получение выбранного элемента</summary>
        /// <returns>Выбранный элемент</returns>
        public object f_GetSelectedItem()
        {
            if (m_EnumType != null)
            {
                return Enum.Parse(m_EnumType, SelectedItem.GetType().GetProperty("value").GetValue(SelectedItem, null).ToString());
            }
            else
            {
                return SelectedItem;
            }
        }
        /// <summary>Установка выбранного элемента</summary>
        /// <param name="a_Item">Выбираемый элемент</param>
        public void f_SetSelectedItem(object a_Item)
        {
            if (m_EnumType != null && a_Item.GetType().IsEnum)
            {
                Type typeEnum = a_Item.GetType();
                foreach (var opts in Items)
                {
                    if (opts.GetType().GetProperty("value").GetValue(opts, null).ToString() == a_Item.ToString())
                    {
                        SelectedItem = opts;
                        break;
                    }
                }
            }
            else
            {
                SelectedItem = a_Item;
            }
        }
    }
}
