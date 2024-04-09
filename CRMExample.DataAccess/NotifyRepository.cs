using CRMExample.DataAccess.Abstract;
using CRMExample.DataAccess.Context;
using CRMExample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMExample.DataAccess
{
    public interface INotifyRepository : IRepository<Notify>
    {

    }
    public class NotifyRepository : Repository<Notify>, INotifyRepository
    {
        private readonly DatabaseContext _context;

        public NotifyRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

    }
}
