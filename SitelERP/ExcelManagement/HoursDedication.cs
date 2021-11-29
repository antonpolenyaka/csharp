using ExcelDataReader;
using SitelCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelManagement
{
    public class HoursDedication
    {
        public const string PathWorkbook2020 = @"\\192.168.23.114\Sitel\BDades\DEDICACIONS 2020\Fulla dedicació V1.1  2020.test.xlsx";
        public const string PathWorkbook2021 = @"\\192.168.23.114\Sitel\BDades\DEDICACIONS 2021\Fulla dedicació V1.1  2021.test.xlsx";

        public async static Task<List<SitelHour>> ReadValidationsAsync(string filePath, bool ignoreBlankLines, int numSheet, int startRow)
        {
            List<SitelHour> validations = new List<SitelHour>();
            string err = null;
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        for (int i = 0; i < numSheet; i++)
                        {
                            reader.NextResult();
                        }
                        int ignoreRowCounter = 0;
                        while (reader.Read()) // Each ROW
                        {
                            ignoreRowCounter++;
                            if (ignoreRowCounter < startRow) continue;
                            List<object> columnsValues = new List<object>();
                            for (int column = 0; column < reader.FieldCount; column++)
                            {
                                object value = reader.GetValue(column);
                                columnsValues.Add(value);
                            }
                            // Check if all values is null
                            if (ignoreBlankLines && columnsValues.All(cv => cv == null || (cv is string @string && (@string == ":" || @string == "-"))))
                            {
                                continue;
                            }
                            SitelHour hour = LineToSitelHour(columnsValues, out err);
                            validations.Add(hour);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage;
                errorMessage = "Error: ";
                errorMessage = string.Concat(errorMessage, ex.Message);
                errorMessage = string.Concat(errorMessage, " Line: ");
                errorMessage = string.Concat(errorMessage, ex.Source);
                err = errorMessage;
            }
            return await Task.FromResult(validations);
        }

        private static SitelHour LineToSitelHour(List<object> columnsValues, out string err)
        {
            SitelHour hour = null;
            err = null;
            try
            {
                hour = new SitelHour
                {
                    ProjectManager = GetStringForce(columnsValues[0]),
                    Client = GetStringForce(columnsValues[1]),
                    System = GetStringForce(columnsValues[2]),
                    ProjectTitle = GetStringForce(columnsValues[3]),
                    ProjectOF = GetStringForce(columnsValues[4]),
                    InitHours = GetDouble(columnsValues[5]),
                    Status = GetStringForce(columnsValues[6]),
                    Collaborator = GetStringForce(columnsValues[7]),
                    Department = GetStringForce(columnsValues[8]),
                    DayOfYear = GetDate(columnsValues[9]),
                    Week = GetDouble(columnsValues[10]),
                    DaysBetweenEndDate = GetDouble(columnsValues[11]),
                    DeliveryDate = GetDate(columnsValues[12]),
                    HoursDedication = GetDouble(columnsValues[13]),
                    MinutesDedication = GetDouble(columnsValues[15]),
                    Task = GetStringForce(columnsValues[16]),
                    Description = GetStringForce(columnsValues[17]),
                    InvoiceNumber = GetStringForce(columnsValues[18]),
                    IncidenceId = GetDouble(columnsValues[19]),
                    Closed = GetStringForce(columnsValues[20]),
                    Solution = GetStringForce(columnsValues[21]),
                    Observation = GetStringForce(columnsValues[22])
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: ";
                errorMessage = string.Concat(errorMessage, ex.Message);
                errorMessage = string.Concat(errorMessage, " Line: ");
                errorMessage = string.Concat(errorMessage, ex.Source);
                err = errorMessage;
            }
            return hour;
        }

        private static string GetStringForce(object cellData)
        {
            if (cellData != null)
            {
                if (cellData is string value)
                {
                    return value;
                }
                else if (cellData is double value2)
                {
                    return value2.ToString();
                }
                else if (cellData is DateTime value3)
                {
                    return value3.ToString("dd/MM/yyyy");
                }
            }
            return null;
        }

        private static DateTime? GetDate(object cellData)
        {
            if (cellData != null && cellData is DateTime value)
            {
                return value;
            }
            return null;
        }

        private static double? GetDouble(object cellData)
        {
            if (cellData != null && cellData is double value)
            {
                return value;
            }
            return null;
        }
    }
}