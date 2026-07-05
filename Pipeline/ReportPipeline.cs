using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using IntelligencePipeline.Calculators;
namespace IntelligencePipeline.Pipeline
{
    public class ReportPipeline
    {
        private readonly ReportRepository _activeRepo;
        private readonly RejectedReportRepository _rejectedRepo;
        private int _nextReportId = 1;

        private readonly ReliabilityCalculator _reliabilityCalculator = new ReliabilityCalculator();
        private readonly PriorityCalculator _priorityCalculator = new PriorityCalculator();
        private readonly ClassificationCalculator _classificationCalculator = new ClassificationCalculator();

        public ReportPipeline(ReportRepository activeRepo, RejectedReportRepository rejectedRepo)
        {
            _activeRepo = activeRepo;
            _rejectedRepo = rejectedRepo;
        }
        public void ProcessReport(Report report)
        {
            report.ReportId = _nextReportId++;
            report.Status = ReportStatus.Validating;
            ValidationResult result = GetValidator(report).Validate(report);
            if (! result.IsValid)
            {
                report.Status = ReportStatus.Rejected;
                report.RejectionReason = result.ErrorMessage;
                _rejectedRepo.Add(report);
                return;
            }
            report.Status = ReportStatus.Validated;
            report.ReliabilityScore = _reliabilityCalculator.Calculate(report);
            report.Priority = _priorityCalculator.Calculate(report);
            report.Classification = _classificationCalculator.Calculate(report);
            _activeRepo.Add(report);
      
        }
       
        private IValidator GetValidator(Report report);
        {
            if (report is DroneReport) return new DroneValidator();
            if (report is SoldierReport) return new SoldierValidator();
            if (report is RadarReport) return new RadarValidator();
            if (report is SignalReport) return new SignalValidator();

            return null; 
        }

    }



}