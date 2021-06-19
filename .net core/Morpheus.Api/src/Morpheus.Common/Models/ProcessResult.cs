using System.Collections.Generic;

namespace Morpheus.Common.Models
{
    public class ProcessResult
    {
        protected ProcessResult(bool isSuccess)
        {
            Reports = new List<Report>();
            IsSuccess = isSuccess;
        }

        protected ProcessResult(ICollection<Report> reports, bool isSuccess)
        {
            Reports = reports;
            IsSuccess = isSuccess;
        }

        public ICollection<Report> Reports { get; }
        public bool IsSuccess { get; }

        public static ProcessResult Success() => new ProcessResult(true);

        public static ProcessResult Failure(ICollection<Report> reports) => new ProcessResult(reports, false);
        public static ProcessResult Failure(Report report) => new ProcessResult(new List<Report>() { report }, false);
    }
}
