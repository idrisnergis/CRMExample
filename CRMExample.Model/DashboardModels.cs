using CRMExample.Entities;
using System.Collections.Generic;

namespace CRMExample.Models
{
    public class DashboardModel
    {
        public int TodayIssueCount { get; set; }
        public int TodayCompletedIssueCount { get; set; }
        public int IssueRateByYesterday { get; set; }

        public int TodayClientCount { get; set; }
        public int ClientRateByYesterday { get; set; }

        public int TotalClientCount { get; set; }
        public int ThisYearClientCount { get; set; }
        public int ClientRateByLastYear { get; set; }

        public int TodayCompletedLeadSum { get; set; }
        public int CompletedLeadPriceRateByYesterday { get; set; }

        public Dictionary<string, int> ClientCountForLastWeek { get; set; }
        public Dictionary<int, int> ClientCountForLastYear { get; set; }
        public Dictionary<int, int> MonthlyLeadPricesForLastYear { get; set; }

        public List<Lead> Last10Leads { get; set; }
        public int CompletedLeadCountForThisMonth { get; set; }
    }
}
