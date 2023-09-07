using CRMExample.DataAccess;
using CRMExample.Entities;
using CRMExample.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace CRMExample.Services
{
    public interface IlogService : IServiceBase<Log>
    {
        Log Create(string text, LogType type, int? userid);
        List<Log> ListByUserId(int userId, int count = 10);
    }
    public class LogService : ServiceBase<Log, ILogRepository>, IlogService
    {
        private readonly ILogRepository _repository;

        public LogService(ILogRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public  Log Create(string text, LogType type, int? userid)
        {
            Log log = new Log
            {
                Text = text,
                Type = type,
                UserId = userid,
                CreatedAt = System.DateTime.Now
            };
            _repository.Add(log);
            return log;
        }

        public List<Log> ListByUserId(int userId, int count = 10)
        {
            if (count == -1)
            {
                return _repository
                    .GetAll(x => x.UserId == userId)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();
            }
            else
            {
                return _repository
                    .GetAll(x => x.UserId == userId)
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(10)
                    .ToList();
            }
        }
    }
}