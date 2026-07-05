using System;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;

namespace IntelligencePipeline
{
    public static class ReportDisplay
    {
        public static void DisplayReport(Report report)
        {
            Console.WriteLine($"[ID: {report.ReportId}] Type: {report.GetType().Name} | Status: {report.Status}");
        }

        public static void DisplayValidatedReports(ReportRepository repository)
        {
            Console.WriteLine("\n--- רשימת דיווחים תקינים ---");
            foreach (Report r in report_reportsb.GetAll())
            {
                DisplayReport(r);
            }
        }

        public static void DisplayRejectedReports(RejectedReportRepository repository)
        {
            Console.WriteLine("\n--- רשימת דיווחים דחויים ---");
            foreach (Report r in report_reportsRejected.GetAll())
            {
                Console.WriteLine($"ID: {r.ReportId} | סיבת דחייה: {r.RejectionReason}");
            }
        }
    }
}