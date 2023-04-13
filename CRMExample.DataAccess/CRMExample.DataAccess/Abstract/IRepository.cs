using CRMExample.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRMExample.DataAccess.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase//EntityBase koşulu where
    {
        TEntity Add(TEntity model);
        TEntity Get(int id);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAll();
        void Remove(int id);
        void Update(TEntity model);
    }
}
