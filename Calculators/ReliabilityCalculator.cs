using System;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;
namespace IntelligencePipeline.Calculators
{
    public class ReliabilityCalculator
    {
        //Centralized location for reliability score calculation logic.
        public int Calculate(Report report)
        {
            int score = report.CalculateReliabilityScore();
            if (score < 1) return 1;
            if (score > 10) return 10;
            return score;
        }
    }

}