using System;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
namespace IntelligencePipeline
{
    public static class ReportInputHandler
    {
        private static  (DateTime time, double lat, double lon, string desc) GetCommonInput()
        {
            Console.WriteLine("Enter Timestamp (yyyy-MM-dd HH:mm:ss):");
            DateTime time = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter Latitude:");
            double lat = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter Longitude:");
            double lon = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter Description:");
            string desc = Console.ReadLine();

            return (0,time, lat, lon, desc);
        }

        public static Report CreateDroneReport()
        {
            var common = GetCommonInput();
            Console.WriteLine("Enter Altitude:");
            int alt = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Image Quality:");
            int qual = int.Parse(Console.ReadLine());

            return new DroneReport(common.time, common.lat, common.lon, common.desc, alt, qual);
        }

        public static Report CreateSoldierReport()
        {
            var common = GetCommonInput();
            Console.WriteLine("Enter Soldier Name:");
            string name = Console.ReadLine();
            return new SoldierReport(common.time, common.lat, common.lon, common.desc, name);
        }
        public static RadarReport CreateRadarReport()
        {
            var common = GetCommonInput();

            Console.WriteLine("Enter Detection Range:");
            double range = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter Frequency:");
            double freq = double.Parse(Console.ReadLine());

            return new RadarReport(common.time, common.lat, common.lon, common.desc, range, freq);
        }
        public static SignalReport CreateSignalReport()
        {
            var common = GetCommonInput();

            Console.WriteLine("Enter Signal Strength:");
            int strength = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Encryption Type:");
            string encryption = Console.ReadLine();

            return new SignalReport(common.time, common.lat, common.lon, common.desc, strength, encryption);
        }
    }




}