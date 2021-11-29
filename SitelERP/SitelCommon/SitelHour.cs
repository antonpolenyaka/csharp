using System;

namespace SitelCommon
{
    public class SitelHour
    {
        /// <summary>
        /// First letters of full name of Project Manager
        /// </summary>
        public string ProjectManager { get; set; }
        public string Client { get; set; }
        public string System { get; set; }
        public string ProjectTitle { get; set; }
        /// <summary>
        /// Number of principal OF
        /// </summary>
        public string ProjectOF { get; set; }
        /// <summary>
        /// OB - Obert, TA - Tancat
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Init reserved hours in project
        /// </summary>
        public double? InitHours { get; set; }
        /// <summary>
        /// First letters of full name
        /// </summary>
        public string Collaborator { get; set; }
        /// <summary>
        /// E - enginering, O - tecnical office
        /// </summary>
        public string Department { get; set; }
        public DateTime? DayOfYear { get; set; }
        /// <summary>
        /// Num of week of dedication
        /// </summary>
        public double? Week { get; set; }
        public double? DaysBetweenEndDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double? HoursDedication { get; set; }
        public double? MinutesDedication { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
        public string InvoiceNumber { get; set; }
        public double? IncidenceId { get; set; }
        public string Closed { get; set; }
        public string Solution { get; set; }
        public string Observation { get; set; }
    }
}
