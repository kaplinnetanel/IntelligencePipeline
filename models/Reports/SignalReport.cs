using System;
using IntelligencePipeline.Models.Enums;
namespace IntelligencePipeline.Models.Reports
{
    public class SignalReport : Report
    {
        //Represents an intelligence report from a signal intelligence system.
 
        public double Frequency { get; protected set; }
        public string Content { get; protected set; }
        public Language Language { get; protected set; }
        public int SignalStrength { get; protected set; }

        public SignalReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, double frequency, string content, Language language, int signalStrength) : base(reportId, timestamp, latitude, longitude, description)
        {
            Frequency = frequency;
            Content = content;
            Language = language;
            SignalStrength = signalStrength;
        }

        public override string GetSourceType() => "Signal";

        public override string ToString()
        {
            return base.ToString() + $", Frequency : {Frequency},Content : {Content},  Language: {Language},SignalStrength {SignalStrength}";
        }

        public override int CalculateReliabilityScore()
        {
            //Implements signal-specific reliability calculation:
            string[] keywords = { "attack", "target", "border", "vehicle" };
            int score = 5;
            if (SignalStrength >= -40)
            { score += 3; }
            else if (SignalStrength >= -70)
            { score += 2; }

            if (SignalStrength < -100)
            { score -= 2;}
            if (Content != null)
            {
                foreach (string word in keywords)
                {
                    if (Content.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 1;
                        break; 
                    }
                }
            }
            if (score > 10)
            { return 10; }
            else if (score < 1)
            {return 1;}
            return score;
        }
    }
}