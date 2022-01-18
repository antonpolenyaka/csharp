using System.Collections.Generic;

namespace ReasignAddressesCAP32.IEC104
{
    public class Info104
    {
        public string DeviceName { get; set; }
        public int FirstTagInfoRow { get; set; }
        public int DeviceType { get; set; }
        public string ColumnTagClassName { get; set; }
        public string ColumnTagIOA { get; set; }
        public string ColumnTagUnits { get; set; }
        public string ColumnTagScale { get; set; }
        public List<TagInfo> TagInfos { get; set; }
        public string ExcelSheetName { get; set; }
        public string IoaFormat { get; set; }

        public Info104()
        {
            TagInfos = new List<TagInfo>();
        }
    }
}
