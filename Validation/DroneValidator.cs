using System;
using IntelligencePipeline.Models.Reports;
namespace IntelligencePipeline.Validation
{
    public class DroneValidator : BaseValidator
    {
        //Validates drone-specific report fields.
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            DroneReport drone = (DroneReport)report;
            if (drone.Altitude < 0 || drone.Altitude > 10000)
            {
                return ValidationResult.Failure("Invalid Altitude");
            }

            if (drone.ImageQuality < 0 || drone.ImageQuality > 100)
            {
                return ValidationResult.Failure("Invalid Image Quality");
            }

            return ValidationResult.Success();


        }
    }
} 