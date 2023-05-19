using CRMExample.DataAccess.Abstract;
using CRMExample.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMExample.Services.Abstract
{

    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        void Delete(int id);
        TEntity GetById(int id);
        List<TEntity> List();
    }

    public class ServiceBase<TEntity, TRepository> : IServiceBase<TEntity> where TEntity : EntityBase
                            where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _repository;

        public ServiceBase(TRepository repository)
        {
            _repository = repository;
        }

        public virtual List<TEntity> List()
        {
            return _repository.GetAll();
        }

        public virtual TEntity GetById(int id)
        {
            return _repository.Get(id);
        }

        public virtual void Delete(int id)
        {
            _repository.Remove(id);
        }
    }
}
    

