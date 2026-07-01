using System;
using IntelligencePipeline.Models.Enums;
namespace IntelligencePipeline.Models.Reports
{
    public abstract class Report
    {
        public int ReportId { get; protected set; }
        public DateTime Timestamp { get; protected set; }
        public double Latitude { get; protected set; }
        public double Longitude { get; protected set; }
        public string Description { get; protected set; }
        public ReportStatus Status { get; set; }
        public Priority Priority { get; set; }
        public Classification Classification { get; set; }
        public int ReliabilityScore { get; set; }
        public string RejectionReason { get; set; }

        protected Report(int reportId, DateTime timestamp, double latitude, double longitude, string description)
        {
            ReportId = reportId;
            Timestamp = timestamp;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
            Status = ReportStatus.New;
        }

        public abstract string GetSourceType();
        public abstract int CalculateReliabilityScore();

        public virtual string GetSummary()
       //Returns a formatted summary of the report. Derived classes can override for source-specific formatting. 
        {
            return "Basic report";
        }
        public override string ToString()
        //Returns a complete string representation of the report. 
        {
            return $"[ReportId: {ReportId}, Status: {Status}]";
        }
    }
}

       

