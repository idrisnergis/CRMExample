using CRMExample.DataAccess.Abstract;
using CRMExample.DataAccess.Context;
using CRMExample.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRMExample.DataAccess
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        private readonly DatabaseContext _context;

        public LogRepository(DatabaseContext context): base(context)
        {
            _context = context;
        }
        public override Log Get(int id)
        {
            return _set.Include(x => x.User).SingleOrDefault(x=> x.Id == id);
        }

        public override List<Log> GetAll()
        {
            return _set.Include(x => x.User).ToList();
        }

        public override List<Log> GetAll(Expression<Func<Log, bool>> predicate)
        {
            return _set.Include(x => x.User).Where(predicate).ToList();
        }
    }

    public interface ILogRepository : IRepository<Log>
    {
    }
}
