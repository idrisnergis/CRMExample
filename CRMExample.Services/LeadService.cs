using CRMExample.DataAccess;
using CRMExample.Entities;
using CRMExample.Model;
using CRMExample.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMExample.Services
{

    public interface ILeadService : IServiceBase<Lead>
    {
        Lead Create(CreateLeadModel model);
        Lead Update(int id, EditLeadModel model);
        List<Lead> ListBySearch(string search);
        List<Lead> ListBySearch(string search, int userId);
        List<Lead> ListByUserId(int userId);
    }

    public class LeadService : ServiceBase<Lead, ILeadRepository>, ILeadService
    {
        private readonly ILeadRepository _repository;

        public LeadService(ILeadRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Lead Create(CreateLeadModel model)
        {
            Lead lead = new Lead
            {
                Summary = model.Summary,
                Desc = model.Desc,
                Price = model.Price,
                Type = LeadType.Waiting,
                UserId = model.UserId,
                ClientId = model.ClientId,
                CreatedAt = DateTime.Now
            };

            _repository.Add(lead);
            return lead;
        }

        public Lead Update(int id, EditLeadModel model)
        {
            Lead lead = _repository.Get(id);
            lead.Summary = model.Summary;
            lead.Desc = model.Desc;
            lead.Price = model.Price;
            lead.UserId = model.UserId;
            lead.ClientId = model.ClientId;
            lead.Type = model.Type;
            lead.ModifiedAt = DateTime.Now;

            _repository.Update(lead);
            return lead;
        }

        public List<Lead> ListBySearch(string search)
        {
            return _repository.GetAll(x =>
                        x.Summary.Contains(search) ||
                        x.Desc.Contains(search));
        }

        public List<Lead> ListBySearch(string search, int userId)
        {
            return _repository.GetAll(x =>
                        x.UserId == userId &&
                        (x.Summary.Contains(search) ||
                        x.Desc.Contains(search)));
        }

        public List<Lead> ListByUserId(int userId)
        {
            return _repository.GetAll(x => x.UserId == userId);
        }
    }
}
