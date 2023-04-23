using CRMExample.DataAccess;
using CRMExample.Entities;
using CRMExample.Model;
using CRMExample.Models;
using CRMExample.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CRMExample.Services
{
    public interface IClientService
    {
        void Create(string name, string email);

        void Create(CreateCustomerModel model);
        List<Client> List();
        Client GetById(int id);
        void Update(int id ,CreateCustomerModel model);
        
        void Delete(int id);
        List<Client> ListBySearch(string search);
    }

    public class ClientService : ServiceBase<Client, IClientRepository>, IClientService
    {
        private readonly IClientRepository _repository;


        public ClientService(IClientRepository repository) : base(repository)
        {
            _repository = repository;
        }
        //public List<Client> List()
        //{
        //    return _repository.GetAll();
        //}

        public void Create(string name, string email)
        {
            Client client = new Client
            {
                Name = name,
                Email = email,
                CreatedAt = System.DateTime.Now
            };
            _repository.Add(client);
        }

        public void Create(CreateCustomerModel model)
        {
            Client client = new Client
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Locked = model.Locked,
                IsCorporate = model.IsCorporate,
                Description = model.Description,
                CreatedAt = System.DateTime.Now
            };
            _repository.Add(client);
        }


        public void Update(int id, CreateCustomerModel model)
        {
            Client client = _repository.Get(id);
            client.Name = model.Name;
            client.Email = model.Email;
            client.Phone = model.Phone;
            client.Locked = model.Locked;
            client.IsCorporate = model.IsCorporate;
            client.Description = model.Description;

            _repository.Update(client);
        }

        public void Delete(int id)
        {
            _repository.Remove(id);
        }

        public List<Client> ListBySearch(string search)
        {
            return _repository.GetAll(x =>
                        x.Name.Contains(search) ||
                        x.Email.Contains(search) ||
                        x.Phone.Contains(search) ||
                        x.Description.Contains(search));
        }
    }


}
