using System;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;
namespace IntelligencePipeline.Calculators
{
    public class PriorityCalculator
    //Calculates report priority based on business rules.
    {


        public Priority Calculate(Report report)
        {

            if (ContainsKeywords(report.Description, "missile", "explosion", "attack", "fire"))
            { return Priority.Critical; }
            if (ContainsKeywords(report.Description, " weapon", "suspicious", "border"))
            { return Priority.High; }
            if (ContainsKeywords(report.Description, "movement", "vehicle", "activity"))
            { return Priority.Medium; }

            switch (report)
            {
                case RadarReport radar:
                    if (radar.Speed >= 800) return Priority.Critical;
                    if (radar.Speed >= 400) return Priority.High;
                    if (radar.Speed >= 120) return Priority.Medium;
                    break;

                case DroneReport dorne:
                    if (dorne.Altitude < 500) return Priority.High;
                    break;

                case SignalReport signal:
                    if (ContainsKeywords(signal.Content, "missile", "explosion", "attack", "fire", "attack", "target"))
                    { return Priority.Critical; }
                    break;

                case SoldierReport soldier:
                    if (soldier.ConfidenceLevel >= 4 && ContainsKeywords(soldier.Description, "movement"))
                    { return Priority.High; }
                    break;
            }
            return Priority.Low;
        }
        private bool ContainsKeywords(string text, params string[] keywords)
        //Case-insensitive keyword search.
        {
            foreach (string word in keywords)
            {
                if (text.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                { return true; }

            }
            return false;
        }

    }





}
