using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Entities.Controls;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Data
{
	public class Cl_DataContextMegaTemplate : DbContext
	{
		public Cl_DataContextMegaTemplate()
			: base("MedicalChart") {
		}
		public Cl_DataContextMegaTemplate(string a_ConnectionPath)
		: base(a_ConnectionPath) {
		}

		public DbSet<Cl_GroupsTemplate> p_GroupsTemplate { get; set; }
		public DbSet<Cl_Template> p_Templates { get; set; }
		public DbSet<Cl_TemplateControl> p_TemplateControls { get; set; }

		public DbSet<Cl_GroupsControl> p_GroupsControl { get; set; }
		public DbSet<Cl_Control> p_BaseControls { get; set; }
		public DbSet<Cl_CtrlTextual> p_Elms_Textual { get; set; }
		public DbSet<Cl_CtrlImage> p_Elms_Image { get; set; }

		private Dictionary<string, Type> m_GetAvailableControls { get; set; }
		public Dictionary<string, Type> f_GetAvailableControls() {
			return new Dictionary<string, Type>(m_GetAvailableControls);
		}

		public void f_Init() {
			p_GroupsTemplate.Load();
			if (!p_GroupsTemplate.Any()) p_GroupsTemplate.Add(new Cl_GroupsTemplate() { p_Name = "Root" });
			if (!p_GroupsControl.Any()) p_GroupsControl.Add(new Cl_GroupsControl() { p_Name = "Root" });

			m_GetAvailableControls = new Dictionary<string, Type>();
			Type type = typeof(I_BaseControl);
			IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
				 .SelectMany(s => s.GetTypes())
				 .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract && p.CustomAttributes.Count() > 0);

			foreach (Type item in types) {
				m_GetAvailableControls.Add(item.Name, item);
			}

			base.SaveChanges();
		}

		public I_BaseControl f_GetControlByType(int controlID, string typeName) {
			if (typeof(Cl_CtrlTextual).Name.ToLower() == typeName.ToLower()) {
				return Cl_App.m_DataContext.p_Elms_Textual.Include(el => el.p_BaseControl).FirstOrDefault(e => e.p_ID == controlID);
			} else if (typeof(Cl_CtrlImage).Name.ToLower() == typeName.ToLower()) {
				return Cl_App.m_DataContext.p_Elms_Image.Include(el => el.p_BaseControl).FirstOrDefault(e => e.p_ID == controlID);
			} else { return null; }
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
		}
	}
}
