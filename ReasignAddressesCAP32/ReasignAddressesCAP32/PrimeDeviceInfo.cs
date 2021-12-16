using System.Collections.Generic;

namespace ReasignAddressesCAP32
{
    public class PrimeDeviceInfo
    {
        public string ConcentratorName { get; set; }
        public string ConcentratorID { get; set; }
        public int ConcentratorAddress { get; set; }
        public List<int> ConcentratorSignals { get; set; }
        public int StoryInterval { get; set; }
        public string MeterName { get; set; }
        public string MeterID { get; set; }
        public string ConcentratorWSUrl { get; set; }
        public string MeterMarca { get; set; }
        public string MeterModel { get; set; }
        public string ConcentratorPrimeVersion { get; set; }
    }
}
