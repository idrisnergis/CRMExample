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

    public interface ILeadRepository : IRepository<Lead>
    {

    }

    public class LeadRepository : Repository<Lead>, ILeadRepository
    {
        private readonly DatabaseContext _context;

        public LeadRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public override List<Lead> GetAll()
        {
            return _set.Include(x => x.User).Include(x => x.Client).ToList();
        }

        public override List<Lead> GetAll(Expression<Func<Lead, bool>> predicate)
        {
            return _set.Include(x => x.User).Include(x => x.Client).Where(predicate).ToList();
        }

        public override Lead Get(int id)
        {
            return _set.Include(x => x.User).Include(x => x.Client).SingleOrDefault(x => x.Id == id);
        }
    }
}
