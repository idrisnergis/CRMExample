using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMExample.DataAccess;

namespace CRMExample.Services
{
    public interface IMockService
    {
        void RunFakeGenerator();
    }
    public class MockService : IMockService
    {
        private readonly ImockRepository _repository;

        public MockService (ImockRepository repository)
        {
            _repository = repository;
        }
        public void RunFakeGenerator()
        {
            _repository.GenerateFakeData();
        }
    }
}
