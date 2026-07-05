using System;
using System.Collections.Generic;
using System.Linq;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;

namespace IntelligencePipeline.Storage
{
    public class ReportRepository
    {
        private readonly List<Report> _reports = new List<Report>();
        public void Add(Report report)
        {
            _reports.Add(report);
        }
        public List<Report> GetAll()
        {
            return _reports;
        }

        public List<Report> GetByStatus(ReportStatus status)
        {
            List<Report> Status = _reports.Where(r => r.Status == status).ToList();
            return Status;
        }
        public List<Report> GetByPriority(Priority priority)
        {
            return _reports.Where(r => r.Priority == priority).ToList();
        }

        public List<Report> Search(string keyword)
        {
            return _reports.Where(r => r.Description.Contains(keyword)).ToList();
        }

        public Report GetById(int reportId)
        {
            return _reports.FirstOrDefault(r => r.ReportId == reportId);
        }

        public void UpdateStatus(int reportId, ReportStatus newStatus)
        {
            Report report = GetById(reportId);
            if (report != null)
            {
                report.Status = newStatus;
            }
        }

        public int GetTotalCount()
        {
            return _reports.Count;
        }

        public int GetCountByStatus(ReportStatus status)
        {
            return _reports.Count(r => r.Status == status);
        }
    }
}
