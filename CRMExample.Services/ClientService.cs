using CRMExample.DataAccess;
using CRMExample.Entities;
using CRMExample.Model;
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
    }

    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public List<Client> List()
        {
            return _repository.GetAll();
        }

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
                Description = model.Description,
                IsCorporate = model.IsCorporate,
                CreatedAt = System.DateTime.Now
            };
            _repository.Add(client);
        }
    }


}
