using CRMExample.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using CRMExample.Common;
using NETCore.Encrypt.Extensions;
using CRMExample.Entities;

namespace CRMExample.DataAccess
{
    public interface ImockRepository
    {
        void GenerateFakeData();
    }
    public class MockRepository : ImockRepository
    {
        private readonly DatabaseContext _context;

        public MockRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void GenerateFakeData()
        {
            Faker faker = new Faker();
            //for (int i = 0; i < 10 ; i++)
            //{

            //    _context.Users.Add(new Entities.User
            //    {
            //        Email = faker.Person.Email,
            //        Password = (Constants.PasswordSalt + "12345").MD5(),
            //        Name = faker.Name.FullName(),
            //        Username = faker.Person.Email.Split('@')[0],
            //        Role = Constants.Role_User,
            //        CreatedAt = faker.Date.Between(new DateTime(2021, 1, 1), DateTime.Now),
            //    });
            //    _context.SaveChanges();
            //}
            var users = _context.Users.ToList();

            for (int i = 0; i < 10; i++)
            {
                _context.Issues.Add(new Entities.Issue
                {
                    Summary = faker.Random.String2(50),
                    DueDate = faker.Date.Between(new DateTime(2021, 1, 1), DateTime.Now),
                    Completed = faker.Random.Bool(),
                    UserId =faker.PickRandom(users).Id,
                    CreatedAt = faker.Date.Between(new DateTime(2021, 1, 1), DateTime.Now),

                });
            }
            _context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                _context.Clients.Add(new Entities.Client
                {
                    Email = faker.Person.Email,
                    Description = faker.Random.String(15),
                    Name = faker.Name.FullName(),
                    Phone = faker.Person.Phone,
                    IsCorporate = faker.Random.Bool(),
                    CreatedAt = faker.Date.Between(new DateTime(2021, 1, 1), DateTime.Now),

                });
            }
            _context.SaveChanges();

            var clients = _context.Clients.ToList();
            for (int i = 0; i < 10; i++)
            {
                _context.Leads.Add(new Entities.Lead
                {
                    Summary = faker.Random.String2(50),
                    Desc = faker.Random.String2(35),
                    Price = faker.Random.Decimal(100,5000),
                    Type = faker.PickRandom<LeadType>(),
                    ClientId = faker.PickRandom(clients).Id,
                    UserId = faker.PickRandom(users).Id,
                    ModifiedAt= faker.Date.Between(new DateTime(2021, 1, 1), DateTime.Now),
                    CreatedAt = faker.Date.Between(new DateTime(2021, 1, 1), DateTime.Now)

                });
            }
            _context.SaveChanges();
        }
    }
}
