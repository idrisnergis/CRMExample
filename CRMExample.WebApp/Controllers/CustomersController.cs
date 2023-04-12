using CRMExample.DataAccess;
using CRMExample.Services;
using CRMExample.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Linq;
using System.Collections.Generic;
using CRMExample.Entities;
using CRMExample.Models;

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
            var clients = _clientService.List();
            return View(clients);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            var client = _clientService.GetById(id);
            return Json(new AjaxResponseModel<Client> { Data = client });
         }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        //Model Create Add Options
        // POST: CustomersController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CreateCustomerModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _clientService.Create(model);
                response.Success = "Müşteri Eklendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);
            return Json(response);
        }


        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var client =_clientService.GetById(id);
            return Json(new AjaxResponseModel<Client> { Data = client });

        }

        // POST: Customers/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateCustomerModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _clientService.Update(id , model);
                response.Success = "Müşteri Güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);
            return Json(response);
        }

        private void AddModelStateErrorsToAjaxResponse(AjaxResponseModel<string> response)
        {
            foreach (var key in ModelState.Keys)
            {
                var item = ModelState.GetValueOrDefault(key);

                if (item != null && item.Errors.Count > 0)
                {
                    item.Errors.ToList().ForEach(err => response.AddError("", err.ErrorMessage));
                }
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
