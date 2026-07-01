using System;

namespace IntelligencePipeline.Models.Reports
{
    public class RadarReport : Report
    {
        //Represents an intelligence report from a radar system.

        public int Speed { get; protected set; }
        public int Direction { get; protected set; }
        public int Distance { get; protected set; }

        public RadarReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, int speed, int direction, int distance) : base(reportId, timestamp, latitude, longitude, description)
        {
            Speed = speed;  
            Direction = direction;
            Distance = distance;

        }
        public override string GetSourceType() => "Radar";

        public override string ToString()
        {
            return base.ToString() + $", Direction: {Direction}, TargetSpeed: {Speed}, Distance: {Distance}";
        }

        public override int CalculateReliabilityScore()
        {
            //Implements radar-specific reliability calculation
            int score = 6;
            if (Distance >= 500 && Distance <= 30000)
            {
                score += 2;
            }
            else if (Distance > 70000)
            {
                score -= 2;
            }
            if (Speed >= 10 && Speed <= 900)
            {
                score += 1;
            }
            else if (Speed > 1500)
            {
                score -= 2;
            }
            if (score > 10)
            {
                return 10;
            }
            else if (score < 1)
            {
                return 1;
            }
            return score;
        }


    }
    
}