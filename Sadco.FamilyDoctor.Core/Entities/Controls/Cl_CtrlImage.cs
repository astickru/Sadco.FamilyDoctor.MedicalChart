using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace Sadco.FamilyDoctor.Core.Entities.Controls
{
	[Table("T_CONTROLS_IMAGE")]
	[Description("Картинка")]
	public class Cl_CtrlImage : I_BaseControl
	{
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		[Column("F_CONTROL_ID")]
		//[ForeignKey("p_BaseControl")]
		public int p_BaseControlID { get; set; }
		public Cl_Control p_BaseControl { get; set; }

		/// <summary>Текст-шаблон</summary>
		[Column("F_IMAGE")]
		public byte[] p_ImageBytes { get; set; }

		public Image f_GetImage(Image newImage) {
			MemoryStream ms = null;

			if (newImage == null) {
				if (p_ImageBytes == null || p_ImageBytes.Length == 0) {
					return null;
				}
			} else {
				using (ms = new MemoryStream()) {
					newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
					p_ImageBytes = ms.ToArray();
				}
			}

			ms = new MemoryStream(p_ImageBytes);
			return Image.FromStream(ms);
		}

		public string p_IconName { get { return this.p_BaseControl.p_IconName; } }
		public Bitmap p_Icon { get { return this.p_BaseControl.p_Icon; } }

		public Cl_CtrlImage() {
			p_BaseControl = new Cl_Control();
			p_BaseControl.p_IconName = "image";
		}
	}
}
