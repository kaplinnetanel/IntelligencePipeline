using System;
using System.Collections.Generic;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Validation; // הוספנו את ה-Namespace של הולידציות

namespace IntelligencePipeline.Tests
{
    public class ReportTester
    {
        public static void Main(string[] args)
        {
            var reports = new List<Report>
            {
                new RadarReport(1, DateTime.Now, 31.0, 35.0, "Fast object detected", 850, 45, 5000),
                new DroneReport(2, DateTime.Now, 31.0, 35.0, "Suspicious drone", 300, 90),
                new SoldierReport(3, DateTime.Now, 31.0, 35.0, "Movement near border", "John Doe", "1234567", "Unit101", 5)
            };

            var priorityCalc = new PriorityCalculator();
            var classificationCalc = new ClassificationCalculator();
            var reliabilityCalc = new ReliabilityCalculator();

            Console.WriteLine("--- Starting Report Analysis Tests (With Validation) ---\n");

            foreach (var report in reports)
            {
                // 1. קביעת הולידטור המתאים לסוג הדוח
                IValidator validator = GetValidator(report);

                // 2. ביצוע האימות
                ValidationResult result = validator.Validate(report);

                Console.WriteLine($"Testing Report Type: {report.GetSourceType()}");

                if (result.IsValid)
                {
                    // 3. אם תקין - מבצעים חישובים
                    report.Status = ReportStatus.Validated;

                    Priority priority = priorityCalc.Calculate(report);
                    Classification classification = classificationCalc.Calculate(report);
                    int reliability = reliabilityCalc.Calculate(report);

                    Console.WriteLine($"Status: Validated. Priority: {priority}, Classification: {classification}, Reliability: {reliability}/10");
                }
                else
                {
                    // 4. אם לא תקין - מעדכנים סטטוס וסיבת דחייה
                    report.Status = ReportStatus.Rejected;
                    report.RejectionReason = result.ErrorMessage;
                    Console.WriteLine($"Status: Rejected. Reason: {result.ErrorMessage}");
                }
                Console.WriteLine("--------------------------------------");
            }
        }

        // עזר לבחירת הולידטור המתאים לפי סוג הדוח
        private static IValidator GetValidator(Report report)
        {
            return report switch
            {
                DroneReport => new DroneValidator(),
                SoldierReport => new SoldierValidator(),
                RadarReport => new RadarValidator(),
                SignalReport => new SignalValidator(),
                _ => throw new Exception("Unknown report type")
            };
        }
    }
}