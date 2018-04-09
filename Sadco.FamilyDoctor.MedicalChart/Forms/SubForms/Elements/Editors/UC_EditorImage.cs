using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using static Sadco.FamilyDoctor.Core.Entities.Cl_Element;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_EditorImage : UserControl, I_EditPanel
    {
        private EntityLog eLog = new EntityLog();

        public Cl_Element p_EditingElement { get; private set; }

        public UC_EditorImage()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
        }

        private bool m_ReadOnly = true;
        public bool p_ReadOnly {
            get { return m_ReadOnly; }
            set {
                m_ReadOnly = value;
                Enabled = m_ReadOnly;
            }
        }

        public object f_ConfirmChanges()
        {
            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    Cl_Element el = null;
                    if (p_EditingElement.p_Version == 0)
                    {
                        el = p_EditingElement;
                        el.p_Version = 1;
                    }
                    else
                    {
                        el = new Cl_Element();
                        el.p_Version = p_EditingElement.p_Version + 1;
                        el.p_ParentGroupID = p_EditingElement.p_ParentGroupID;
                        el.p_ParentGroup = p_EditingElement.p_ParentGroup;
                        Cl_App.m_DataContext.p_Elements.Add(el);
                    }
                    el.p_ElementType = E_ElementsTypes.Image;
                    el.p_ElementID = p_EditingElement.p_ElementID;
                    el.p_Name = ctrl_Name.Text;
                    el.p_Tag = ctrlTag.Text;
                    el.p_Image = ctrlImage.Image;
                    el.p_Help = ctrl_Hint.Text;
                    el.p_Comment = ctrl_Note.Text;

                    Cl_App.m_DataContext.SaveChanges();
                    eLog.SaveEntity(el);
                    f_SetElement(el);
                    transaction.Commit();

                    return el;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("При сохранении изменений произошла ошибка");
                    return null;
                }
            }
        }

        public void f_SetElement(Cl_Element a_Element)
        {
            eLog.SetEntity(a_Element);

            if (a_Element == null || !a_Element.f_IsImage()) return;
            p_EditingElement = a_Element;
            if (p_EditingElement.p_Version == 0)
                ctrl_Version.Text = "Черновик";
            else
                ctrl_Version.Text = p_EditingElement.p_Version.ToString();
            ctrl_Name.Text = p_EditingElement.p_Name;
            ctrlTag.Text = p_EditingElement.p_Tag;
            ctrlImage.Image = p_EditingElement.p_Image;
            ctrl_Hint.Text = p_EditingElement.p_Help;
            ctrl_Note.Text = p_EditingElement.p_Comment;
        }

        private void ctrlBAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files |*.bmp; *.gif; *.jpg; *.jpeg; *.png";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() != DialogResult.OK)
                return;
            Image result = null;
            try
            {
                result = Image.FromFile(openFile.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Выбранный файл не является изображением", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlImage.Image = result;
        }

		private void ctrlBDelete_Click(object sender, EventArgs e)
		{
			ctrlImage.Image = null;
		}

		private void ctrlTag_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = e.KeyChar != Keys.Back.GetHashCode() && e.KeyChar != Keys.Delete.GetHashCode() && (e.KeyChar < 65 || (e.KeyChar > 90 && e.KeyChar < 97) || e.KeyChar > 122);
		}
	}
}
