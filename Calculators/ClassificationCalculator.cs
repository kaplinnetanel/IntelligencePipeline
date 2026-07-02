using System;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;
namespace IntelligencePipeline.Calculators
{
    class ClassificationCalculator
    {
        //Calculates security classification based on business rules.
       
        public Classification Calculate(Report report)
        {
           if (report.Priority == Priority.Critical || report is SignalReport signal && ContainsKeywords(signal.Content, "target", "attack", "missile"))
            { return Classification.TopSecret; }
            
            if (report.Priority == Priority.High || report is SignalReport || ContainsKeywords(report.Description, "weapon", "border"))
            { return Classification.Secret; }
            
            if (report.Priority == Priority.Medium || report is SoldierReport)
            {return Classification.Restricted;}

            return Classification.Unclassified;
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