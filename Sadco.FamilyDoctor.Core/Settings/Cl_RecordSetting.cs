using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Settings
{
    /// <summary>
    /// Класс настроек макета записей
    /// </summary>
    public class Cl_RecordSetting
    {
        public float p_SizeH1 { get; set; } = 20;
        public Color p_RecordBackColor { get; set; } = Color.Azure;
        public Color p_RecordReadOnlyBackColor { get; set; } = Color.Green;
        public Color p_RecordCurrentEditColor { get; set; } = Color.Yellow;
        public Color p_RecordOutRangeColor { get; set; } = Color.Red;
        public Color p_RecordPatientControlBorderColor { get; set; } = Color.Orange;
        public int p_RecordPatientControlBorderWidth { get; set; } = 2;
    }
}
