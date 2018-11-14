using FD.dat.mon.stb.lib;
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
    public partial class UC_EditorTab : UserControl, I_EditPanel
    {
        private Cl_EntityLog m_Log = new Cl_EntityLog();

        public Cl_Element p_EditingElement { get; private set; }

        public UC_EditorTab()
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
                    el.p_ElementType = E_ElementsTypes.Tab;
                    el.p_ElementID = p_EditingElement.p_ElementID;
                    el.p_Name = ctrl_Name.Text;
                    
                    Cl_App.m_DataContext.SaveChanges();

                    if (m_Log.f_IsChanged(el) == false)
                    {
                        if (el.Equals(p_EditingElement) && el.p_Version == 1)
                        {
                            el.p_Version = 0;
                        }

                        MonitoringStub.Message("Элемент не изменялся!");
                        transaction.Rollback();
                        return null;
                    }
                    m_Log.f_SaveEntity(el);
                    f_SetElement(el);
                    transaction.Commit();

                    return el;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Editor", "При сохранении изменений произошла ошибка", ex, null, null);
                    return null;
                }
            }
        }

        public void f_SetElement(Cl_Element a_Element)
        {
            m_Log.f_SetEntity(a_Element);

            if (a_Element == null || !a_Element.f_IsTab()) return;
            p_EditingElement = a_Element;
            if (p_EditingElement.p_Version == 0)
                ctrl_Version.Text = "Черновик";
            else
                ctrl_Version.Text = p_EditingElement.p_Version.ToString();
            ctrl_Name.Text = p_EditingElement.p_Name;
        }
	}
}
