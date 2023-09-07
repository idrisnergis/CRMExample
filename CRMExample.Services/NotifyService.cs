using CRMExample.DataAccess;
using CRMExample.Entities;
using CRMExample.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMExample.Services
{
    public interface INotifyService : IServiceBase<Notify>
    {
        Notify Create(string text, NotifyType type, int userId);
        Notify Update(int notifyId, bool isRead);
        List<Notify> ListByUserId(int userId, bool? isRead);
        List<Notify> ListBySearch(string search, int userId);
    }

    public class NotifyService : ServiceBase<Notify, INotifyRepository>, INotifyService
    {
        private readonly INotifyRepository _repository;

        public NotifyService(INotifyRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Notify Create(string text, NotifyType type, int userId)
        {
            Notify notify = new Notify
            {
                Text = text,
                Type = type,
                UserId = userId,
                IsRead = false,
                CreatedAt = System.DateTime.Now
            };

            _repository.Add(notify);
            return notify;
        }

        public Notify Update(int notifyId, bool isRead)
        {
            Notify notify = _repository.Get(notifyId);
            notify.IsRead = isRead;

            _repository.Update(notify);
            return notify;
        }

        public List<Notify> ListByUserId(int userId, bool? isRead)
        {
            if (isRead == null)
            {
                return _repository
                    .GetAll(x => x.UserId == userId)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();
            }
            else
            {
                return _repository
                    .GetAll(x => x.UserId == userId && x.IsRead == isRead.Value)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();
            }
        }

        public List<Notify> ListBySearch(string search, int userId)
        {
            return _repository
                .GetAll(x =>
                        x.UserId == userId &&
                        x.Text.Contains(search))
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }
    }
}
