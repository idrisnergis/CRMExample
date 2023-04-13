using CRMExample.DataAccess.Abstract;
using CRMExample.DataAccess.Context;
using CRMExample.Entities;

namespace CRMExample.DataAccess
{
    public interface IClientRepository : IRepository<Client>
    {
        
    }

    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly DatabaseContext _context;
        public ClientRepository(DatabaseContext context) : base(context)
        {
            _context = context; 
        }
    }
}
