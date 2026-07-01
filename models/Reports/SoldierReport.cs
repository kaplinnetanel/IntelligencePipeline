using System;

namespace IntelligencePipeline.Models.Reports
{
    public class SoldierReport : Report
    {
        public string SoldierName { get; protected set; }
        public string SoldierID { get; protected set; }
        public string Unit { get; protected set; }
        public int ConfidenceLevel { get; protected set; }

        public SoldierReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, string soldierName, string soldierID, string unit, int confidenceLevel) : base(reportId, timestamp, latitude, longitude, description)
        {
            SoldierName = soldierName;
            SoldierID = soldierID;
            Unit = unit;
            ConfidenceLevel = confidenceLevel;

        }
        public override string GetSourceType() => "Soldier";
      
        public override string ToString()
        {
            return base.ToString() + $", Soldier: {SoldierName}, Unit: {Unit}, Confidence: {ConfidenceLevel}";
        }
        public override int CalculateReliabilityScore()
        {
            int score = 4 + ConfidenceLevel;
            string[] keywords = { "weapon", "vehicle", "movement", "explosion" };
            if (Description != null)
            {
                foreach (string word in keywords)
                {
                    if (Description.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 1;
                        break;
                    }
                }
            }

           if (score > 10)
            { return 10; }
           else if (score < 1)
            { return 1;}
           return score;
        }
    }
}
