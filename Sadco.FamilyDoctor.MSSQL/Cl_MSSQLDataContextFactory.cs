using Sadco.FamilyDoctor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadco.FamilyDoctor.MSSQL {
	public class Cl_MSSQLDataContextFactory : IDbContextFactory<Cl_DataContextMegaTemplate> {
		public Cl_DataContextMegaTemplate Create(DbContextFactoryOptions options) {
			var builder = new DbContextOptionsBuilder<DataContext>();
			builder.UseSqlite("Data Source = findexpert.db", b => b.MigrationsAssembly("FindExpert.Data.EF.Sqlite"));
			return new Cl_DataContextMegaTemplate(builder.Options);
		}
	}
}
