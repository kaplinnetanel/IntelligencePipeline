using System;
using IntelligencePipeline.Models.Enums; 

namespace IntelligencePipeline.Models.Reports
{
    class DroneReport : Report
    {
        //Represents an intelligence report from a drone source. 

        public int Altitude { get; protected set; }
        public int ImageQuality { get; protected set; }

        public DroneReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, int altitude, int imageQuality) : base(0, timestamp, latitude, longitude, description)
        {
            Altitude = altitude;
            ImageQuality = imageQuality;
        }

        public override string GetSourceType() => "Drone";


        public override string ToString()
        {
            return base.ToString() + $", Altitude: {Altitude}, ImageQuality {ImageQuality}%";
        }

        public override int CalculateReliabilityScore()
        {
            //Implements drone-specific reliability calculation
            int score = 5;
            if (ImageQuality >= 80)
            {
                score += 3;
            }
            else if (ImageQuality >= 50) 
            {
                score += 2;
            }
            if (Altitude >= 500 && Altitude <= 3000)
            {
                score += 2;
            }
            else if (Altitude > 7000)
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