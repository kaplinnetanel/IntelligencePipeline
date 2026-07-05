using System;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.Storage;

namespace IntelligencePipeline
{
    public class Program
    {
        private static ReportRepository _activeRepo = new ReportRepository();
        private static RejectedReportRepository _rejectedRepo = new RejectedReportRepository();
        private static ReportPipeline _pipeline = new ReportPipeline(_activeRepo, _rejectedRepo);

        public static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Intelligence Pipeline Management System ---");
                Console.WriteLine("1. Create New Report (Drone/Soldier/Radar/Signal)");
                Console.WriteLine("2. View Validated Reports");
                Console.WriteLine("3. Search Reports by Description");
                Console.WriteLine("4. Filter Reports (Source, Priority, Classification, Status, Date)");
                Console.WriteLine("5. Sort Reports (Timestamp, Priority, ReliabilityScore)");
                Console.WriteLine("6. Update Report Status (InProgress/Completed)");
                Console.WriteLine("7. View Full Report Details");
                Console.WriteLine("8. View Rejected Reports (Including Rejection Reason)");
                Console.WriteLine("9. Show Statistics (Percentage of Validated Reports & Distribution)");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                switch (choice)
                {

                    case "1":
                        Console.WriteLine("****Creating reports****");
                        Console.WriteLine("1 => Drone");
                        Console.WriteLine("2 => Soldier");
                        Console.WriteLine("3 => Radar");
                        Console.WriteLine("4 => Signal");
                        Console.WriteLine("5 => stop");

                        string choice = Console.ReadLine();

                        if (choice == "5")
                        {
                            running = false;
                            continue;
                        }

                        Report newReport = null;

                        switch (choice)
                        {
                            case "1":
                                newReport = ReportInputHandler.CreateDroneReport();
                                break;
                            case "2":
                                newReport = ReportInputHandler.CreateSoldierReport();
                                break;
                            case "3":
                                newReport = ReportInputHandler.CreateRadarReport();
                                break;
                            case "4":
                                newReport = ReportInputHandler.CreateSignalReport();
                                break;
                            default:
                                Console.WriteLine("Invalid choice!");
                                continue;
                        }

                        if (newReport != null)
                        {
                            _pipeline.ProcessReport(newReport);
                            Console.WriteLine("Report successfully sent to pipeline.");
                            break;

                        }

                    case "2":
                        Console.WriteLine("\n--- Validated Reports ---");
                        List<Report> validatedReports = _activeRepo.GetAll();
                        if (validatedReports.Count == 0)
                        {
                            Console.WriteLine("No validated reports found.");
                        }
                        else
                        {
                            foreach (Report report in validatedReports)
                            {
                                Console.WriteLine(report.ToString());
                            }
                        }
                        break;

                    case "3":
                        Console.Write("Enter search keyword: ");
                        string keyword = Console.ReadLine();

                        List<Report> searchResults = _activeRepo.Search(keyword);

                        if (searchResults.Count == 0)
                        {
                            Console.WriteLine($"No reports found containing '{keyword}'.");
                        }
                        else
                        {
                            Console.WriteLine($"\n--- Found {searchResults.Count} reports ---");
                            foreach (Report report in searchResults)
                            {
                                Console.WriteLine(report.ToString());
                            }
                        }
                        break;
                    case "4":
                        List<Report> filtered = _activeRepo.FilterReports(null, null, null, null, null, null);
                        filtered.ForEach(r => Console.WriteLine(r.ToString()));
                        break;

                    case "5":
                        Console.Write("Sort by (Timestamp/Priority/Reliability): ");
                        string sortField = Console.ReadLine();
                        _activeRepo.GetSortedReports(sortField).ForEach(r => Console.WriteLine(r.ToString()));
                        break;

                    case "6":
                        Console.Write("Enter Report ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter new status (InProgress/Completed): ");
                        ReportStatus newStatus = Enum.Parse<ReportStatus>(Console.ReadLine());
                        _activeRepo.UpdateStatus(id, newStatus);
                        break;

                    case "7":
                        Console.Write("Enter Report ID: ");
                        int reportId = int.Parse(Console.ReadLine());
                        Report r = _activeRepo.GetById(reportId);
                        DisplayReport(r);
                        break;

                    case "8":
                        DisplayRejectedReports(_rejectedRepo);
                        break;

                    case "9":
                        DisplayStatistics();
                        break;

                }

            }
        }


        private static void DisplayReport(Report report)
        {
            if (report != null) Console.WriteLine(report.ToString());
            else Console.WriteLine("Report not found.");
        }


        private static void DisplayValidatedReports(ReportRepository repository)
        {
            repository.GetAll().ForEach(r => Console.WriteLine(r.ToString()));
        }



        private static void DisplayRejectedReports(RejectedReportRepository repository)
        {
            foreach (Report r in repository.GetAll())
            {
                Console.WriteLine($"{r} | Reason: {r.RejectionReason}");
            }
        }


        private static void DisplayStatistics()
        {
            int valid = _activeRepo.GetTotalCount();
            int rejected = _rejectedRepo.GetTotalCount();
            double total = valid + rejected;
            Console.WriteLine($"Validated: {valid}, Rejected: {rejected}, Success Rate: {(valid / total) * 100:F2}%");
        }

    }

}












