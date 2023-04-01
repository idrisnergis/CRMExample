using CRMExample.Entities.Abstract;
using CRMExaple.DataAccess.Context;
using CRMExaple.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMExaple.DataAccess
{
    public interface IRepository<TEntitiy>
        where TEntitiy : EntityBase//EntityBase sartı 
    {
        TEntitiy Add(TEntitiy model);
        TEntitiy Get(int id);
        List<TEntitiy> GetAll();
        void Remove(int id);
        void Update(TEntitiy model);
    }



    public class ClientRepository : IRepository<Client>
    {
        private readonly DatabaseContext _context;

        public ClientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public Client Get(int id)
        {
            return _context.Clients.Find(id);
        }

        public Client Add(Client model)
        {
            _context.Clients.Add(model);

            if (_context.SaveChanges() > 0)
                return model;

            return null;
        }

        public void Update(Client model)
        {
            if (model.Id == 0)
                throw new ArgumentNullException(nameof(model.Id));

            var entity = _context.Entry(model);
            entity.State = EntityState.Modified;

            if (_context.SaveChanges() == 0)
                throw new Exception("Güncelleme işlemi yapılamadı.");
        }

        public void Remove(int id)
        {
            _context.Clients.Remove(Get(id));

            if (_context.SaveChanges() == 0)
                throw new Exception("Silme işlemi yapılamadı.");
        }
    }
}
