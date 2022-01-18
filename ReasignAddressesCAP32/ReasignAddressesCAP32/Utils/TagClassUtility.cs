using ReasignAddressesCAP32.Core;

namespace ReasignAddressesCAP32.Utils
{
    public static class TagClassUtility
    {
        public static (bool result, TagClassType classType, string dataType) DetectTagClassTypeByName(string tagClassName)
        {
            bool result = true;
            string dataType;
            TagClassType classType;
            if (tagClassName.StartsWith("AI."))
            {
                classType = TagClassType.AI;
                dataType = "Float32";
            }
            else if (tagClassName.StartsWith("AO."))
            {
                classType = TagClassType.AO;
                dataType = "Float32";
            }
            else if (tagClassName.StartsWith("DI."))
            {
                classType = TagClassType.DI;
                dataType = "Int32";
            }
            else if (tagClassName.StartsWith("DO."))
            {
                classType = TagClassType.DO;
                dataType = "Int32";
            }
            else if (tagClassName.StartsWith("CDO."))
            {
                classType = TagClassType.CDO;
                dataType = "Int32";
            }
            else if (tagClassName.StartsWith("CDI."))
            {
                classType = TagClassType.CDI;
                dataType = "Int32";
            }
            else
            {
                result = false;
                dataType = null;
                classType = TagClassType.Undefined;
            }
            return (result, classType, dataType);
        }
    }
}
