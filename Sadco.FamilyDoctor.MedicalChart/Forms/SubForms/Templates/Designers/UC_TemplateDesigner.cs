using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_TemplateDesigner : UserControl
    {
        internal Cl_Template p_ActiveTemplate { get; set; }

        private List<Cl_TemplatesElements> m_newConstrols = new List<Cl_TemplatesElements>();
        private List<Cl_TemplatesElements> m_delConstrols = new List<Cl_TemplatesElements>();

        //private int deltaX = 0;
        //private int deltaY = 0;

        public UC_TemplateDesigner()
        {
            InitializeComponent();

            ctrl_EditorPanel.p_AllowItemDrag = true;
            //this.Load += UC_TemplateDesigner_Load;
        }

        public void f_SetToolboxService(Ctrl_ToolboxService a_ToolboxService)
        {
            ctrl_EditorPanel.p_ToolboxService = a_ToolboxService;
            ctrl_EditorPanel.p_ToolboxService.p_ReadOnly = true;
        }

        //private void UC_TemplateDesigner_Load(object sender, EventArgs e)
        //{
        //    if (p_ActiveTemplate.p_TemplateControls == null || p_ActiveTemplate.p_TemplateControls.Count == 0)
        //        return;
        //    foreach (Cl_TemplatesElements item in p_ActiveTemplate.p_TemplateControls)
        //    {
        //        if (item.p_Element == null)
        //            continue;
        //        Control uiControl = f_CreateUIControl(item.p_Element);
        //        if (uiControl == null)
        //            return;
        //        ctrl_P_Designer.Controls.Add(uiControl);
        //        uiControl.Size = new Size(item.p_Width, item.p_Height);
        //        uiControl.Location = new Point(item.p_PositionX, item.p_PositionY);
        //        uiControl.Tag = item;
        //    }
        //}

        private void ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            //Control c = sender as Control;

            //if (e.Button == MouseButtons.Left)
            //{

            //    deltaX = e.X;
            //    deltaY = e.Y;

            //    c.DoDragDrop(c, DragDropEffects.Move);
            //}
            //else
            //{
            //    MenuItem delMenu = new MenuItem("Удалить");
            //    delMenu.Click += (msender, me) =>
            //    {
            //        Control control = (Control)((MenuItem)msender).Tag;
            //        Cl_TemplatesElements templateControl = (Cl_TemplatesElements)control.Tag;

            //        if (m_newConstrols.Contains(templateControl))
            //        {
            //            m_newConstrols.Remove(templateControl);
            //        }
            //        else
            //        {
            //            m_delConstrols.Add(templateControl);
            //        }

            //        control.Parent.Controls.Remove(control);
            //    };

            //    delMenu.Tag = c;

            //    ContextMenu buttonMenu = new ContextMenu();
            //    buttonMenu.MenuItems.Add(delMenu);
            //    buttonMenu.Show(c, new Point(e.X, e.Y));
            //}
        }

        private void ctrl_B_Save_Click(object sender, EventArgs e)
        {
            //foreach (Control item in ctrl_P_Designer.Controls)
            //{
            //    Cl_TemplatesElements templateControl = (Cl_TemplatesElements)item.Tag;
            //    templateControl.p_Width = item.Size.Width;
            //    templateControl.p_Height = item.Size.Height;
            //    templateControl.p_PositionX = item.Location.X;
            //    templateControl.p_PositionY = item.Location.Y;

            //    if (m_newConstrols.Contains(templateControl))
            //    {
            //        m_newConstrols.Remove(templateControl);
            //        Cl_App.m_DataContext.p_TemplatesElements.Add(templateControl);
            //    }

            //    foreach (Cl_TemplatesElements delItem in m_delConstrols)
            //    {
            //        Cl_App.m_DataContext.p_TemplatesElements.Remove(delItem);
            //    }
            //}

            //Cl_App.m_DataContext.SaveChanges();
            //m_delConstrols.Clear();
        }

        //private void ctrl_P_Designer_DragEnter(object sender, DragEventArgs e)
        //{
        //    e.Effect = e.AllowedEffect;
        //}

        //private void ctrl_P_Designer_DragDrop(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetData(e.Data.GetFormats()[0]) is Control)
        //    {
        //        f_DragControl(e.Data.GetData(e.Data.GetFormats()[0]) as Control, e.X, e.Y);
        //    }
        //    else if (e.Data.GetData(e.Data.GetFormats()[0]) is Ctrl_TreeNodeElement)
        //    {
        //        f_DragNewControl(e.Data.GetData(e.Data.GetFormats()[0]) as Ctrl_TreeNodeElement, e.X, e.Y);
        //    }
        //}

        //private void f_DragControl(Control control, int x, int y)
        //{
        //    if (control == null)
        //        return;

        //    control.Location = ctrl_P_Designer.PointToClient(new Point(x - deltaX, y - deltaY));
        //}

        //private void f_DragNewControl(Ctrl_TreeNodeElement controlNode, int posX, int posY)
        //{
        //    if (controlNode == null)
        //        return;
        //    if (controlNode.p_Element == null)
        //        return;

        //    Control uiControl = f_CreateUIControl(controlNode.p_Element);
        //    if (uiControl == null)
        //        return;

        //    ctrl_P_Designer.Controls.Add(uiControl);
        //    uiControl.Location = ctrl_P_Designer.PointToClient(new Point(posX - (uiControl.Size.Width / 2), posY - (uiControl.Size.Height / 2)));

        //    Cl_TemplatesElements templateControl = new Cl_TemplatesElements();
        //    templateControl.p_Element = controlNode.p_Element;
        //    templateControl.p_Template = p_ActiveTemplate;

        //    m_newConstrols.Add(templateControl);

        //    uiControl.Tag = templateControl;
        //}

        //private Control f_CreateUIControl(Cl_Element control)
        //{
        //    Control uiControl = null;

        //    //if (control is Cl_CtrlImage) {
        //    //	PictureBox pick = new PictureBox();
        //    //	pick.Image = ((Cl_CtrlImage)control).f_GetImage(null);
        //    //	pick.Size = pick.Image.Size;
        //    //	pick.BorderStyle = BorderStyle.FixedSingle;

        //    //	uiControl = pick;
        //    //} else if (control is Cl_CtrlTextual) {
        //    //	Cl_CtrlTextual ctrlTextual = (Cl_CtrlTextual)control;

        //    //	switch (ctrlTextual.p_ControlType) {
        //    //		case E_TextControlTypes.Float:
        //    //		if (ctrlTextual.p_BaseControl.p_Editing) {
        //    //			uiControl = new TextBox();
        //    //			uiControl.Text = ctrlTextual.p_BaseControl.p_Default;
        //    //		} else {
        //    //			uiControl = new Label();
        //    //			uiControl.BackColor = Color.Azure;
        //    //			uiControl.Text = ctrlTextual.p_BaseControl.p_Default;
        //    //		}
        //    //		break;
        //    //		case E_TextControlTypes.Line:
        //    //		uiControl = new TextBox();
        //    //		uiControl.Text = ctrlTextual.p_BaseControl.p_Default;
        //    //		break;
        //    //		case E_TextControlTypes.Bigbox:
        //    //		uiControl = new TextBox();
        //    //		uiControl.Text = ctrlTextual.p_BaseControl.p_Default;
        //    //		break;

        //    //		default:
        //    //		throw new NotSupportedException("Указанный тип " + ctrlTextual.p_ControlType.ToString() + " не реализован");
        //    //	}

        //    //} else {
        //    //	throw new NotImplementedException("Обработка класса " + control.GetType().Name + " не реализована");
        //    //}

        //    // Установка минимального размера
        //    //int mixXY = 10;

        //    //uiControl.Size = new Size(control.p_Width < mixXY ? mixXY : control.p_Width, control.p_Height < mixXY ? mixXY : control.p_Height);
        //    uiControl.MouseDown += ctrl_MouseDown;
        //    return uiControl;
        //}
    }
}
