using CRMExample.DataAccess;
using CRMExample.Services;
using CRMExample.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace CRMExample.WebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IClientService _clientService;

        public CustomersController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: CustomersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }



        //Model Create Add Options
        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                _clientService.Create(model);
                return RedirectToAction(nameof(Index));
            }
                return View(model);
            
        }

        public ActionResult FakeInsert()
        {
            CreateCustomerModel model = new CreateCustomerModel
            {
                Name = "John Doe",
                IsCorporate = false,
                Email = "john@doe.com",
                Phone = "5556667788",
                Description = "Lorem ipsum dolor sit a met.",
                Locked = false
            };

            _clientService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
