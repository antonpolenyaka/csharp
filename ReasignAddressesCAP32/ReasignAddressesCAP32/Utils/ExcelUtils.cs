namespace ReasignAddressesCAP32.Utils
{
    public static class ExcelUtils
    {
        public static int ColumnNameToIndex(string name)
        {
            var upperCaseName = name.ToUpper();
            var number = 0;
            var pow = 1;
            for (var i = upperCaseName.Length - 1; i >= 0; i--)
            {
                number += (upperCaseName[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            number -= 1;

            return number;
        }
    }
}
