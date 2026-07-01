using System;
using IntelligencePipeline.Models.Reports;
namespace IntelligencePipeline.Validation
{
   public abstract class BaseValidator : IValidator
    {
        //Provides common validation logic for all report types and eliminates code duplication.

        public ValidationResult Validate(Report report)
        {
            if (report.Timestamp > DateTime.Now)
            {
                return ValidationResult.Failure("Invalid Timestamp: cannot be in the future");
            }
            if (report.Timestamp < new DateTime(2020, 1, 1))
            {     
                return ValidationResult.Failure("Invalid Timestamp: cannot be before 2020-01-01");
            {
            if (report.Latitude< 29.5 || report.Latitude> 33.5)
            {
                return ValidationResult.Failure("Invalid Latitude: must be between 29.5000 and 33.5000");
            }
            if (report.Longitude < 34.0 || report.Longitude > 36.0)
            { 
                return ValidationResult.Failure("Invalid Longitude: must be between 34.0000 and 36.0000");
            }
            if (string.IsNullOrEmpty(report.Description) || report.Description.Length < 10 || report.Description.Length > 500)
            { 
                return ValidationResult.Failure("Invalid Description: must be 10-500 characters");
            }
            return ValidationResult.Success();
        }
        protected abstract ValidationResult ValidateSpecificFields(Report report);
    }
}
