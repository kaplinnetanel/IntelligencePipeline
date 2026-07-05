using System;
using System.Collections.Generic;
using System.Linq;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;


namespace IntelligencePipeline.Storage

{
    public class RejectedReportRepository
    //Stores and manages rejected reports separately.
    {

        private List<Report> _reports = new List<Report>();
        public void Add(Report report)
        {
            _reportsRejected.Add(report); 
        }
        public List<Report> GetAll()
        {
            return _reportsRejected;
        }
        public int GetTotalCount()
        {
            return _reportsRejected.Count;
        }
        public List<Report> GetByReason(string reasonKeyword)
        {
            return _reportsRejected.Where(r => r.RejectionReason.Contains(reasonKeyword)).ToList();

        }
        public List<Report> Filter(string source, Priority? priority, ReportStatus? status)
        {
            var query = _reports.AsQueryable();

            if (!string.IsNullOrEmpty(source))
                query = query.Where(r => r.GetType().Name.Contains(source, StringComparison.OrdinalIgnoreCase));

            if (priority.HasValue)
                query = query.Where(r => r.Priority == priority.Value);

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            return query.ToList();


        }
        public List<Report> Sort(string criterion)
        {
            return criterion.ToLower() switch
            {
                "timestamp" => _reports.OrderByDescending(r => r.Timestamp).ToList(),
                "priority" => _reports.OrderByDescending(r => r.Priority).ToList(),
                "reliability" => _reports.OrderByDescending(r => r.ReliabilityScore).ToList(),
                _ => _reports
            };
       
        }
        public Report GetById(int id) => _reports.FirstOrDefault(r => r.ReportId == id);

        public void UpdateStatus(int id, ReportStatus newStatus)
        {
            var report = GetById(id);
            if (report != null) report.Status = newStatus;
        }


    }

}