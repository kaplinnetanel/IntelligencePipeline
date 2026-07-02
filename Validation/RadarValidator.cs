using System;
using IntelligencePipeline.Models.Reports;
namespace IntelligencePipeline.Validation
{ 
    public class RadarValidator : BaseValidator
    {
        //Validates radar-specific report fields.
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            RadarReport radar = (RadarReport)report;

            if (radar.Speed < 0 || radar.Speed > 2000)
            {
                return ValidationResult.Failure("The speed is not in the appropriate range.");
            }
            if (radar.Direction < 0 || radar.Direction > 360)
            {
                return ValidationResult.Failure("Direction of movement not in range");
            }
            if (radar.Distance < 100 || radar.Distance > 100000)
            {
                return ValidationResult.Failure("Distance not in range");
            }
            return ValidationResult.Success(); 
        }

    }