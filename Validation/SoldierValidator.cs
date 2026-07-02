using System;
using IntelligencePipeline.Models.Reports;
namespace IntelligencePipeline.Validation
{
    public class SignalValidator : BaseValidator
    {
        //Validates signal-specific report fields.
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            SoldierReport soldier = (SoldierReport)repore;
            if (string.IsNullOrEmpty(soldier.SoldierName) || soldier.SoldierName.Length < 2 || soldier.SoldierName.Length > 50)
            {
               return ValidationResult.Failure("Invalid SoldierName: length must be between 2 and 50 characters.");
            }
            if (string.IsNullOrEmpty(soldier.SoldierID) || soldier.SoldierID.Length != 7))
            {
                return ValidationResult.Failure("Invalid SoldierID: must be exactly 7 digits.");
            }
            if (string.IsNullOrEmpty(soldier.Unit) || soldier.Unit.Length < 2 || soldier.Unit.Length > 50)
            {
                return ValidationResult.Failure("Invalid Unit: length must be between 2 and 50 characters.");
            }
            if (soldier.ConfidenceLevel < 1 || soldier.ConfidenceLevel > 5)
            {
                return ValidationResult.Failure("Invalid ConfidenceLevel: must be between 1 and 5.");
            }
            return ValidationResult.Success();
        }




    } 













}