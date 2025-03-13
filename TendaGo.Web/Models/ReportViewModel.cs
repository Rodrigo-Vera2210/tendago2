using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class ReportViewModel
    {
        public ReportViewModel(string reportPath = null)
        {
            ReportPath = reportPath;
        }

        public string ReportPath { get; set; }
        public Dictionary<string, object> Parameters { get; private set; } = new Dictionary<string, object>();
    }
}