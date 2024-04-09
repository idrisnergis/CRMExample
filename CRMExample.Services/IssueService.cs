using CRMExample.Entities;
using CRMExample.Model;
using CRMExample.Services.Abstract;
using CRMExample.DataAccess;
using System;
using System.Collections.Generic;

namespace CRMExample.Services
{
    public interface IIssueService : IServiceBase<Issue>
    {
        Issue Create(CreateIssueModel model);
        Issue Update(int id, EditIssueModel model);
        List<Issue> ListBySearch(string search);
        List<Issue> ListBySearch(string search, int userId);
        List<Issue> ListByUserId(int userId);
    }
    public class IssueService : ServiceBase<Issue, IIssueRepository>, IIssueService
    {
        private readonly IIssueRepository _repository;

        public IssueService(IIssueRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Issue Create(CreateIssueModel model)
        {
            Issue issue = new Issue
            {
                Summary = model.Summary,
                DueDate = model.DueDate,
                UserId = model.UserId,
                CreatedAt = System.DateTime.Now
            };

            _repository.Add(issue);
            return issue;
        }

        public Issue Update(int id, EditIssueModel model)
        {
            Issue issue = _repository.Get(id);
            issue.Summary = model.Summary;
            issue.DueDate = model.DueDate;
            issue.UserId = model.UserId;
            issue.Completed = model.Completed;

            _repository.Update(issue);
            return issue;
        }

        public List<Issue> ListBySearch(string search)
        {
            return _repository.GetAll(x =>
                        x.Summary.Contains(search));
        }

        public List<Issue> ListBySearch(string search, int userId)
        {
            return _repository.GetAll(x =>
                        x.UserId == userId &&
                        x.Summary.Contains(search));
        }

        public List<Issue> ListByUserId(int userId)
        {
            return _repository.GetAll(x => x.UserId == userId);
        }
    }
}
