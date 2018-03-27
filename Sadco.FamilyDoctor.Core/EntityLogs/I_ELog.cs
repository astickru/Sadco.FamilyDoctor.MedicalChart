using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
	/// <summary>
	/// Интерфейс ведения логов объекта
	/// </summary>
	public interface I_ELog
	{
		/// <summary>
		/// ИД объекта
		/// </summary>
		int p_ID { get; set; }
	}
}
