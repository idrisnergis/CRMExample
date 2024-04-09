using CRMExample.DataAccess.Abstract;
using CRMExample.DataAccess.Context;
using CRMExample.Entities;

namespace CRMExample.DataAccess
{
    public interface IUserRepository : IRepository<User>
    {

    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
