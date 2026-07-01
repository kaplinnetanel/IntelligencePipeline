using System;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Pipeline
{
    class Program
    {
        static void Main()
        {
            DroneReport r = new DroneReport(
                1,
                DateTime.Now,
                30.5000,
                35.0000,
                "נתנאל", 
                2500,
                85
            );
            Console.WriteLine(r.GetSourceType());
            Console.WriteLine(r.ToString());      
        }
    }
}