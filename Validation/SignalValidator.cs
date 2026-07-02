using System;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;

namespace IntelligencePipeline.Validation
{
    public class SignalValidator : BaseValidator
    {
        //Validates signal-specific report fields.
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            SignalReport signal = (SignalReport)report;
            if (signal.Frequency < 1.0 || signal.Frequency > 3000.0)
            {
                return ValidationResult.Failure("Invalid Frequency: must be between 1.0 and 3000.0");
            }
            if (string.IsNullOrEmpty(signal.Content) || signal.Content.Length < 5 || signal.Content.Length > 1000)
            {
                return ValidationResult.Failure("Invalid Content: must be between 5 and 1000 characters");
            }
            if (!Enum.IsDefined(typeof(Language), signal.Language))
            {
                return ValidationResult.Failure("Invalid Language: value not allowed");
            }
            if (signal.SignalStrength < -120 || signal.SignalStrength > 0)
            {
                return ValidationResult.Failure("Invalid SignalStrength: must be between -120 and 0");
            }
            return ValidationResult.Success();
        }
    }
}