using Sitel.TedisNet.Common.LocalEntities.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitelUtils.Pages
{
    public partial class TagClassProcess
    {
        public string Segmentation { get; set; }
        public string NegativeSegmentation { get; set; }
        public string SegmentationResult { get; set; }
        public string ErrorProcessSegmentation { get; set; }
        public string FormatSQL { get; set; }
        public string TypeOfTagClass { get; set; }
        public string TagClassNamesExist { get; set; }
        public float DifferencePercentage { get; set; }
        public bool FormatSqlHiden { get; set; }

        protected override Task OnInitializedAsync()
        {
            Segmentation = null;
            NegativeSegmentation = null;
            SegmentationResult = null;
            ErrorProcessSegmentation = null;
            FormatSQL = null;
            TypeOfTagClass = "DI";
            DifferencePercentage = 5;
            FormatSqlHiden = false;
            return base.OnInitializedAsync();
        }

        private bool ProcessSegmentationWithoutNegativeSegmentation(bool allowDuplication, out List<string> segmentationResult, out List<string> unitsResult)
        {
            bool isOk = true;
            segmentationResult = new List<string>();
            unitsResult = new List<string>();

            string[] itemsSegmentation = Segmentation.Split('\n');
            string[] itemsNegative = NegativeSegmentation?.Split('\n');

            foreach (string segment in itemsSegmentation)
            {
                if (string.IsNullOrWhiteSpace(segment)) continue;
                string units = GetUnitsFromLine(segment);
                string description;
                if (segment.Contains(";"))
                {
                    description = segment.Split(";")[0];
                }
                else
                {
                    description = segment;
                }
                description = description.Trim().Replace("  ", " ");
                if (itemsNegative != null && itemsNegative.Length > 0)
                {
                    foreach (string negative in itemsNegative)
                    {
                        if (string.IsNullOrWhiteSpace(negative)) continue;
                        if (description.Trim().Contains(negative.Trim()))
                        {
                            description = description.Trim().Replace(negative.Trim(), " ").Replace("  ", " ");
                        }
                    }
                }
                description = description.Trim();
                if (!segmentationResult.Contains(description) || allowDuplication)
                {
                    segmentationResult.Add(description);
                    unitsResult.Add(units);
                }
            }

            return isOk;
        }

        private bool ProcessFilterToExcludeAlreadyExistingTagClasses(List<string> segmentations, List<string> unitss, out List<string> filteredSegmentation, out List<string> filteredUnits)
        {
            bool isOk = true;
            filteredSegmentation = new List<string>();
            filteredUnits = new List<string>();
            if (TagClassNamesExist != null && TagClassNamesExist.Length > 0)
            {
                string[] existingNames = TagClassNamesExist.Split('\n');
                for (int i = 0; i < segmentations.Count; i++)
                {
                    bool alreadyExistInDB = false;
                    string segmentation = segmentations[i];
                    string units = unitss[i];
                    if (existingNames != null && existingNames.Length > 0)
                    {
                        // Test 1. Exact name
                        if (existingNames.Contains(segmentation))
                        {
                            alreadyExistInDB = true;
                        }
                        // Test 2. Same string
                        if (!alreadyExistInDB)
                        {
                            // That will return the percentage of characters that are contained in your main string str2. So you can use that in an If condition.
                            foreach (string existName in existingNames)
                            {
                                if (string.IsNullOrWhiteSpace(existName)) continue;
                                float levDistance = CalcLevenshteinDistance(existName, segmentation);
                                float difference = levDistance / segmentation.Length * 100;
                                if (difference < DifferencePercentage)
                                {
                                    alreadyExistInDB = true;
                                }
                            }
                        }
                        // Test 3. Exact with type
                        if (existingNames.Contains($"DI.{segmentation}")
                            || existingNames.Contains($"CDI.{segmentation}")
                            || existingNames.Contains($"DO.{segmentation}")
                            || existingNames.Contains($"CDO.{segmentation}")
                            || existingNames.Contains($"AI.{segmentation}")
                            || existingNames.Contains($"AO.{segmentation}")
                            || existingNames.Contains($"SYS.{segmentation}"))
                        {
                            alreadyExistInDB = true;
                        }
                    }
                    // Result
                    if (!alreadyExistInDB)
                    {
                        filteredSegmentation.Add(segmentation);
                        filteredUnits.Add(units);
                    }
                }
            }
            else
            {
                filteredSegmentation = segmentations;
                filteredUnits = unitss;
            }
            return isOk;
        }

        private static int CalcLevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(a))
            {
                return b.Length;
            }
            if (string.IsNullOrEmpty(b))
            {
                return a.Length;
            }
            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (int i = 1; i <= lengthA; i++)
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    distances[i, j] = Math.Min
                        (
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost
                        );
                }
            return distances[lengthA, lengthB];
        }

        private void ProcessSegmentation()
        {
            ErrorProcessSegmentation = null;
            if (!string.IsNullOrEmpty(Segmentation))
            {
                if (ProcessSegmentationWithoutNegativeSegmentation(allowDuplication: true, out List<string> segmentationResult, out List<string> unitsResult))
                {
                    SegmentationResult = string.Join('\n', segmentationResult);
                }
            }
        }

        private bool SegmentationToTagClass(string description, string units, out TagClass tagClass)
        {
            bool isOk = true;
            tagClass = new TagClass
            {
                Comments = description,
                Description = description,
                Name = $"{TypeOfTagClass}.{description}"
            };
            switch (TypeOfTagClass)
            {
                case "DI":
                    tagClass.ValueTypeId = 101;
                    tagClass.ParentTagClassId = 1;
                    tagClass.EventTypeId = 2;
                    tagClass.Format = "######";
                    tagClass.ActivationTime = 2;
                    tagClass.SendPriorityId = 1;
                    tagClass.SourceValueTypeId = 1;
                    tagClass.SignalActivationModeId = 1;
                    break;
                case "CDI":
                    tagClass.ValueTypeId = 113;
                    tagClass.ParentTagClassId = 31;
                    tagClass.EventTypeId = 2;
                    tagClass.Format = "######";
                    tagClass.ActivationTime = 2;
                    tagClass.SourceValueTypeId = 1;
                    break;
                case "DO":
                    tagClass.ValueTypeId = 107;
                    tagClass.ParentTagClassId = 5;
                    tagClass.Format = "######";
                    tagClass.SourceValueTypeId = 1;
                    break;
                case "CDO":
                    tagClass.ValueTypeId = 6;
                    tagClass.ParentTagClassId = 33;
                    tagClass.EventTypeId = 2;
                    tagClass.Format = "######";
                    tagClass.SendPriorityId = 1;
                    tagClass.SourceValueTypeId = 1;
                    break;
                case "AI":
                    tagClass.ValueTypeId = 2;
                    tagClass.ParentTagClassId = 3;
                    tagClass.Units = units;
                    tagClass.Format = "##0.00";
                    tagClass.StoreInterval = 180;
                    tagClass.SourceValueCodecId = 1;
                    tagClass.SourceValueTypeId = 6;
                    break;
                case "AO":
                    tagClass.ValueTypeId = 2;
                    tagClass.ParentTagClassId = 36;
                    tagClass.Format = "###";
                    tagClass.SourceValueTypeId = 1;
                    break;
                case "SYS":
                default:
                    // blank
                    break;
            }
            return isOk;
        }

        private void GenerateSqlInserts()
        {
            FormatSqlHiden = false;
            ErrorProcessSegmentation = null;
            if (!string.IsNullOrEmpty(Segmentation))
            {
                List<TagClass> tagClassResult = new List<TagClass>();
                if (ProcessSegmentationWithoutNegativeSegmentation(allowDuplication: false, out List<string> segmentationResult, out List<string> unitsResult))
                {
                    if (ProcessFilterToExcludeAlreadyExistingTagClasses(segmentationResult, unitsResult, out segmentationResult, out unitsResult))
                    {
                        for (int i = 0; i < segmentationResult.Count; i++)
                        {
                            string segmentation = segmentationResult[i];
                            string units = unitsResult[i];
                            if (SegmentationToTagClass(segmentation, units, out TagClass tagClass))
                            {
                                if (!tagClassResult.Any(tc => tc.Description == tagClass.Description))
                                {
                                    tagClassResult.Add(tagClass);
                                }
                            }
                        }
                    }
                }
                FormatSQL = string.Join('\n', tagClassResult.Select(tc => TagClassToSQL(tc)));
                FormatSqlHiden = tagClassResult.Count != 0;
            }
        }

        private string GetUnitsFromLine(string line)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(line))
            {
                if (line.Contains(";"))
                {
                    string[] cells = line.Split(";");
                    if (cells.Length >= 2)
                    {
                        result = cells[1];
                    }
                }
            }
            return result;
        }

        private string TagClassToSQL(TagClass tc)
        {
            string sql;
            sql = "INSERT INTO [Lib].[TagClasses] ([Name],[Description],[Comments]";
            switch (tc.Name.Substring(0, 3))
            {
                case "DI.":
                    sql += ",[ValueTypeId],[ParentTagClassId],[EventTypeId],[Format],[ActivationTime],[SendPriorityId],[SourceValueTypeId],[SignalActivationModeId])";
                    sql += $" VALUES ('{tc.Name}','{tc.Description}','{tc.Comments}'";
                    sql += $",{tc.ValueTypeId},{tc.ParentTagClassId},{tc.EventTypeId},'{tc.Format}',{tc.ActivationTime},{tc.SendPriorityId},{tc.SourceValueTypeId},{tc.SignalActivationModeId});";
                    break;
                case "CDI":
                    sql += ",[ValueTypeId],[ParentTagClassId],[EventTypeId],[Format],[ActivationTime],[SourceValueTypeId])";
                    sql += $" VALUES ('{tc.Name}','{tc.Description}','{tc.Comments}'";
                    sql += $",{tc.ValueTypeId},{tc.ParentTagClassId},{tc.EventTypeId},'{tc.Format}',{tc.ActivationTime},{tc.SourceValueTypeId});";
                    break;
                case "DO.":
                    sql += ",[ValueTypeId],[ParentTagClassId],[Format],[SourceValueTypeId])";
                    sql += $" VALUES ('{tc.Name}','{tc.Description}','{tc.Comments}'";
                    sql += $",{tc.ValueTypeId},{tc.ParentTagClassId},'{tc.Format}',{tc.SourceValueTypeId});";
                    break;
                case "CDO":
                    sql += ",[ValueTypeId],[ParentTagClassId],[EventTypeId],[Format],[SendPriorityId],[SourceValueTypeId])";
                    sql += $" VALUES ('{tc.Name}','{tc.Description}','{tc.Comments}'";
                    sql += $",{tc.ValueTypeId},{tc.ParentTagClassId},{tc.EventTypeId},'{tc.Format}',{tc.SendPriorityId},{tc.SourceValueTypeId});";
                    break;
                case "AI.":
                    sql += ",[ValueTypeId],[ParentTagClassId],[Units],[Format],[StoreInterval],[SourceValueCodecId],[SourceValueTypeId])";
                    sql += $" VALUES ('{tc.Name}','{tc.Description}','{tc.Comments}'";
                    string unitsDBValue = !string.IsNullOrWhiteSpace(tc.Units) ? $"'{tc.Units}'" : "NULL";
                    sql += $",{tc.ValueTypeId},{tc.ParentTagClassId},{unitsDBValue},'{tc.Format}',{tc.StoreInterval},{tc.SourceValueCodecId},{tc.SourceValueTypeId});";
                    break;
                case "AO.":
                    sql += ",[ValueTypeId],[ParentTagClassId],[Format],[SourceValueTypeId])";
                    sql += $" VALUES ('{tc.Name}','{tc.Description}','{tc.Comments}'";
                    sql += $",{tc.ValueTypeId},{tc.ParentTagClassId},'{tc.Format}',{tc.SourceValueTypeId});";
                    break;
                case "SYS":
                default:
                    sql = "?";
                    // blank
                    break;
            }
            return sql;
        }
    }
}
