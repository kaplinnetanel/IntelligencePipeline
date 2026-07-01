using System;

namespace IntelligencePipeline.Validation
{
    abstract Class BaseValidator : IValidator
    { 
    //Template method that validates both common and specific fields:
    public ValidationResult Validate(Report report)


    protected ValidationResult ValidateCommonFields(Report report)
        if (report.Timestamp
        if (report.Latitude >= 29.5000 && report.Latitude <= 33.5000)
            {ValidationResult.Success()}
        else
            {ValidationResult.Failure(message) }
        if (report.Longitude
        
        if (report.Descriptionh

        {
        get => _latitude;
        set
            {
            if (value > 29.5000 && value < 33.5000)
            { _latitude = value; }
      
        }
    }



public double Longitude
{
    get => _longitude;
    set
    {
        if (value > 34.000 && value < 36.000)
        { _longitude = value; }
    }
}
public string Description
{
    get => _description;
    set
    {
        if (value.)
            }


    protected abstract ValidationResult ValidateSpecificFields(Report report);
    //protected abstract ValidationResult ValidateSpecificFields(Report report);