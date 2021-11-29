using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var res1 = GetScaledValue(-32768);
            var res2 = GetScaledValue(32767);
            var res3 = GetScaledValue(0);
            var res4 = GetScaledValue(-1);
        }

        class TagScale
        {
            public double InputValue { get; set; }
            public double ResultValue { get; set; }
        }

        public static double GetScaledValue(double inputValue)
        {
            double result = short.MinValue;

            List<TagScale> scales = new List<TagScale>()
            {
                { new TagScale(){ InputValue = 32767, ResultValue = 120} },
                { new TagScale(){ InputValue = -32768, ResultValue = 0} },

            }.OrderBy(o => o.InputValue).ToList();

            if(scales.Count == 2)
            {
                double? valMin = scales.FirstOrDefault(s => s.InputValue == short.MinValue)?.ResultValue;
                double? valMax = scales.FirstOrDefault(s => s.InputValue == short.MaxValue)?.ResultValue;

                if (valMin.HasValue && valMax.HasValue)
                {
                    // Copy from TMANDO caso SCHNEIDER ELECTRIC Lersa (Ripoll)
                    double maxByProtocol = 65534;
                    double minByProtocol = 0;
                    double newValue = inputValue + 32768; // With range (-32768;32767), elevate all to positive sign by +32768
                    double rate = (newValue / (maxByProtocol - minByProtocol)); // 0..1
                    double newRange = valMax.Value - valMin.Value;
                    result = rate * newRange + valMin.Value;
                }
            }

            return result;
        }

        public static double GetScaledValue2(double inputValue)
        {
            bool isOk = false;
            bool applySign = true;  // if true, operation is made using module, and then sign is applied at the end. Not used in case of offset
            bool limit1Found = false;
            bool limit2Found = false;
            double moduleX = Math.Abs(inputValue);
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double result = 0;

            // find place of the inputvalue in all tag scales input values
            List<TagScale> scales = new List<TagScale>()
            {
                { new TagScale(){ InputValue = 32767, ResultValue = 120} },
                { new TagScale(){ InputValue = -32768, ResultValue = 0} },

            }.OrderBy(o => o.InputValue).ToList();
            int scaleCount = scales.Count;
            for (int i = 0; i < scaleCount; i++)
            {
                if (limit1Found && moduleX <= scales[i].InputValue)
                {
                    // max limit of interval found
                    x2 = scales[i].InputValue;
                    y2 = scales[i].ResultValue;
                    limit2Found = true;
                    break;
                }
                else if (moduleX >= scales[i].InputValue)
                {
                    // min limit of interval found
                    x1 = scales[i].InputValue;
                    y1 = scales[i].ResultValue;
                    limit1Found = true;
                }
            }

            if (limit1Found && limit2Found)
            {
                if (y1 == y2)
                {
                    // "4-20 mA style" in the 0-4 mA interval : invalid measure indicated as -999. In the future we may have a quality detail for this
                    result = -999;
                    isOk = true;
                }
                else
                {
                    // offset + factor
                    if (x1 != x2)
                    {
                        double factor = (moduleX - x1) / (x2 - x1);
                        result = y1 + factor * (y2 - y1);
                        isOk = true;
                    }
                }
            }
            else
            {
                // limit 1 or 2 not found: inputValue outside the intervals -> use highest scale factor if found (if input != 0) or use offset (if input==0)
                if (scaleCount > 0)
                {
                    if (scales[scaleCount - 1].InputValue == 0)
                    {
                        // scale input==0 -> add result value as offset
                        result = inputValue + scales[scaleCount - 1].ResultValue;
                        applySign = false;
                    }
                    else
                    {
                        // scale input!=0 -> use scale
                        x2 = scales[scaleCount - 1].InputValue;
                        y2 = scales[scaleCount - 1].ResultValue;
                        result = moduleX * y2 / x2;
                    }
                    isOk = true;
                }
            }

            if (isOk)
            {
                if (applySign && inputValue < 0)
                {
                    result = (-1) * result;
                }
            }
            else
            {
                result = inputValue;
            }

            return result;
        }


    }
}
