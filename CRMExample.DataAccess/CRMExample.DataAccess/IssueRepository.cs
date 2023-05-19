using CRMExample.DataAccess.Abstract;
using CRMExample.DataAccess.Context;
using CRMExample.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SupperCRMExample.DataAccess
{
    public interface IIssueRepository : IRepository<Issue>
    {

    }

    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        private readonly DatabaseContext _context;

        public IssueRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public override List<Issue> GetAll()
        {
            return _set.Include(x => x.User).ToList();
        }

        public override List<Issue> GetAll(Expression<Func<Issue, bool>> predicate)
        {
            return _set.Include(x => x.User).Where(predicate).ToList();
        }

        public override Issue Get(int id)
        {
            return _set.Include(x => x.User).SingleOrDefault(x => x.Id == id);
        }
    }
}