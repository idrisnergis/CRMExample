using CRMExample.Entities;
using CRMExample.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CRMExample.Services
{
    public interface IDashboardService
    {
        DashboardModel GetDashboardModel();
    }

    public class DashboardService : IDashboardService
    {
        private readonly IIssueService _issueService;
        private readonly IClientService _clientService;
        private readonly ILeadService _leadService;

        public DashboardService(IIssueService issueService, IClientService clientService, ILeadService leadService)
        {
            _issueService = issueService;
            _clientService = clientService;
            _leadService = leadService;
        }

        public DashboardModel GetDashboardModel()
        {
            DashboardModel model = new DashboardModel();

            var issues = _issueService.List();
            var clients = _clientService.List();
            var leads = _leadService.List();

            CalculateBoxes(model, issues, clients, leads);
            CalculateLastWeekClients(model, clients);
            CalculateLastYearClients(model, clients);
            CalculateLastYearLeads(model, leads);
            CalculateCompletedLeadsForThisMonth(model, leads);

            model.Last10Leads = leads.OrderByDescending(x => x.CreatedAt).Take(10).ToList();

            return model;
        }

        private void CalculateCompletedLeadsForThisMonth(DashboardModel model, List<Lead> leads)
        {
            DateTime startDate = DateTime.Now.Date.AddMonths(-1).AddDays(1).Date;
            DateTime endDate = DateTime.Now.AddDays(1).Date;

            var completedLeadCountForThisMonth =
                leads.Where(x => x.Type == LeadType.Completed && (x.CreatedAt.Date >= startDate && x.CreatedAt.Date < endDate)).Count();

            model.CompletedLeadCountForThisMonth = completedLeadCountForThisMonth;
        }

        private void CalculateLastYearLeads(DashboardModel model, List<Lead> leads)
        {
            DateTime startDate = new DateTime(DateTime.Now.Date.AddYears(-1).Year, DateTime.Now.Date.AddYears(-1).Month, 1).Date;
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;

            var lastYearLeads = leads.Where(x => x.Type == LeadType.Completed && (x.CreatedAt.Date >= startDate && x.CreatedAt.Date < endDate)).ToList();
            var leadSumForLastYear = lastYearLeads.GroupBy(x => x.CreatedAt.Date.Month).Select(x => new { Month = x.Key, Total = x.Sum(y => y.Price) }).ToList();
            var dic = leadSumForLastYear.ToDictionary(x => x.Month, x => (int)x.Total);

            for (int i = 1; i < 13; i++)
            {
                if (!dic.ContainsKey(i))
                    dic.Add(i, 0);
            }

            model.MonthlyLeadPricesForLastYear = new Dictionary<int, int>();

            for (int i = 1; i < 13; i++)
            {
                int month = DateTime.Now.AddMonths(-1 * i).Month;
                int value = dic[month];

                model.MonthlyLeadPricesForLastYear.Add(month, value);
            }
        }

        private void CalculateLastYearClients(DashboardModel model, List<Client> clients)
        {
            DateTime startDate = new DateTime(DateTime.Now.Date.AddYears(-1).Year, DateTime.Now.Date.AddYears(-1).Month, 1).Date;
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;

            var lastYearClients = clients.Where(x => x.CreatedAt.Date >= startDate && x.CreatedAt.Date < endDate).ToList();
            var clientCountForLastYear = lastYearClients.GroupBy(x => x.CreatedAt.Date.Month).Select(x => new { Month = x.Key, Count = x.Count() }).ToList();
            var dic = clientCountForLastYear.ToDictionary(x => x.Month, x => x.Count);

            for (int i = 1; i < 13; i++)
            {
                if (!dic.ContainsKey(i))
                    dic.Add(i, 0);
            }

            model.ClientCountForLastYear = new Dictionary<int, int>();

            for (int i = 1; i < 13; i++)
            {
                int month = DateTime.Now.AddMonths(-1 * i).Month;
                int value = dic[month];

                model.ClientCountForLastYear.Add(month, value);
            }
        }

        private void CalculateLastWeekClients(DashboardModel model, List<Client> clients)
        {
            var lastWeekClients = clients.Where(x => x.CreatedAt.Date >= DateTime.Now.Date.AddDays(-7) && x.CreatedAt.Date < DateTime.Now.Date).ToList();
            var clientCountForLastWeek = lastWeekClients.GroupBy(x => x.CreatedAt.Date).Select(x => new { Date = x.Key, Count = x.Count() }).ToList();
            var dic = clientCountForLastWeek.ToDictionary(x => (int)x.Date.DayOfWeek, x => x.Count);

            for (int i = 0; i < 7; i++)
            {
                if (!dic.ContainsKey(i))
                    dic.Add(i, 0);
            }

            model.ClientCountForLastWeek = new Dictionary<string, int>();

            for (int i = 1; i < 8; i++)
            {
                DayOfWeek dayOfWeek = DateTime.Now.AddDays(-1 * i).DayOfWeek;
                int value = dic[(int)dayOfWeek];

                model.ClientCountForLastWeek.Add(
                    CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)dayOfWeek].ToUpper(), value);
            }
        }

        private void CalculateBoxes(DashboardModel model, List<Issue> issues, List<Client> clients, List<Lead> leads)
        {
            decimal yesterdayIssueCount = issues.Where(x => x.CreatedAt.Date == DateTime.Now.AddDays(-1).Date).Count();
            decimal todayIssueCount = issues.Where(x => x.CreatedAt.Date == DateTime.Now.Date).Count();
            decimal todayCompletedIssueCount = issues.Where(x => x.CreatedAt.Date == DateTime.Now.Date && x.Completed).Count();
            decimal issueRateByYesterday = todayIssueCount > 0m
                ? Math.Round((todayIssueCount - yesterdayIssueCount) / yesterdayIssueCount * 100m)
                : 0m;

            model.TodayIssueCount = (int)todayIssueCount;
            model.TodayCompletedIssueCount = (int)todayCompletedIssueCount;
            model.IssueRateByYesterday = (int)issueRateByYesterday;

            decimal yesterdayClientCount = clients.Where(x => x.CreatedAt.Date == DateTime.Now.AddDays(-1).Date).Count();
            decimal todayClientCount = clients.Where(x => x.CreatedAt.Date == DateTime.Now.Date).Count();
            decimal clientRateByYesterday = todayClientCount > 0m
                ? Math.Round((todayClientCount - yesterdayClientCount) / yesterdayClientCount * 100m)
                : 0m;

            model.TodayClientCount = (int)todayClientCount;
            model.ClientRateByYesterday = (int)clientRateByYesterday;

            decimal lastYearClientCount = clients.Where(x => x.CreatedAt.Year == DateTime.Now.Year - 1).Count();
            decimal thisYearClientCount = clients.Where(x => x.CreatedAt.Year == DateTime.Now.Year).Count();
            decimal clientCount = clients.Count;
            decimal clientRateByLastYear = thisYearClientCount > 0m
                ? Math.Round((thisYearClientCount - lastYearClientCount) / lastYearClientCount * 100m)
                : 0m;

            model.ThisYearClientCount = (int)thisYearClientCount;
            model.TotalClientCount = (int)clientCount;
            model.ClientRateByLastYear = (int)clientRateByLastYear;

            decimal yesterdayCompletedLeadSum =
                leads.Where(x => x.Type == Entities.LeadType.Completed && x.CreatedAt.Date == DateTime.Now.AddDays(-1).Date).Sum(x => x.Price);
            decimal todayCompletedLeadSum =
                leads.Where(x => x.Type == Entities.LeadType.Completed && x.CreatedAt.Date == DateTime.Now.Date).Sum(x => x.Price);
            decimal completedLeadPriceRateByYesterday = todayCompletedLeadSum > 0m
                ? Math.Round((todayCompletedLeadSum - yesterdayCompletedLeadSum) / yesterdayCompletedLeadSum * 100m)
                : 0m;

            model.TodayCompletedLeadSum = (int)todayCompletedLeadSum;
            model.CompletedLeadPriceRateByYesterday = (int)completedLeadPriceRateByYesterday;
        }
    }
}
