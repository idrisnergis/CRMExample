using CRMExample.Common;
using CRMExample.Entities;
using CRMExample.Model;
using CRMExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMExample.WebApp.Controllers
{
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _leadService;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        private readonly INotifyService _notifyService;

        public LeadsController(ILeadService leadService, IUserService userService, INotifyService notifyService, IClientService clientService)
        {
            _leadService = leadService;
            _userService = userService;
            _notifyService = notifyService;
            _clientService = clientService;
        }
        public ActionResult GetUserList()
        {
            AjaxResponseModel<List<User>> response = new AjaxResponseModel<List<User>>();

            string role = HttpContext.Session.GetString(Constants.Session_Role);

            if (role == Constants.Role_Admin)
            {
                response.Data = _userService.List();
            }
            else
            {
                int userid = HttpContext.Session.GetInt32(Constants.Session_Id).Value;

                var user = _userService.GetById(userid);
                user.Issues = null;

                response.Data = new List<User> { user };
            }

            response.Data = response.Data.OrderBy(x => x.Name).ToList();

            return Json(response);
        }

        // GET: GetClientList
        public ActionResult GetClientList()
        {
            AjaxResponseModel<List<Client>> response = new AjaxResponseModel<List<Client>>();
            response.Data = _clientService.List().OrderBy(x => x.Name).ToList();

            return Json(response);
        }

        // GET: GetLeadTypeList
        public ActionResult GetLeadTypeList()
        {
            AjaxResponseModel<List<LeadTypeModel>> response = new AjaxResponseModel<List<LeadTypeModel>>();
            response.Data = new List<LeadTypeModel>();

            response.Data.Add(new LeadTypeModel { Value = (int)LeadType.Waiting, Text = "Bekliyor" });
            response.Data.Add(new LeadTypeModel { Value = (int)LeadType.Offered, Text = "Tekliflendi" });
            response.Data.Add(new LeadTypeModel { Value = (int)LeadType.ToCall, Text = "Aranacak" });
            response.Data.Add(new LeadTypeModel { Value = (int)LeadType.OnSale, Text = "Satışta" });
            response.Data.Add(new LeadTypeModel { Value = (int)LeadType.Completed, Text = "Tamamlandı" });

            return Json(response);
        }

        // GET: Leads
        public ActionResult Index(string search = "")
        {
            List<Lead> leads = null;

            string role = HttpContext.Session.GetString(Constants.Session_Role);
            int userid = HttpContext.Session.GetInt32(Constants.Session_Id).Value;

            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
            {
                if (role == Constants.Role_Admin)
                {
                    leads = _leadService.List();
                }
                else
                {
                    leads = _leadService.ListByUserId(userid);
                }
            }
            else
            {
                if (role == Constants.Role_Admin)
                {
                    leads = _leadService.ListBySearch(search);
                }
                else
                {
                    leads = _leadService.ListBySearch(search, userid);
                }

                ViewData["search"] = search;
            }

            return View(leads);
        }

        // POST: Leads/Create
        [HttpPost]
        public ActionResult Create(CreateLeadModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                var lead = _leadService.Create(model);

                // todo : burada talep eklendiği için kullanıcıya bildirim eklenmeli.
                _notifyService.Create("Yeni talep atandı.", NotifyType.LeadAdded, model.UserId);

                response.Success = "Talep eklendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // GET: Leads/Details/5
        public ActionResult Details(int id)
        {
            var lead = _leadService.GetById(id);
            lead.User.Issues = null;

            return Json(new AjaxResponseModel<Lead> { Data = lead });
        }

        // POST: Leads/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EditLeadModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                var lead = _leadService.Update(id, model);

                // todo : burada talep güncellendi için kullanıcıya bildirim eklenmeli.
                if (model.Type == LeadType.Completed)
                    _notifyService.Create("Talep tamamlandı.", NotifyType.LeadCompleted, model.UserId);
                else
                    _notifyService.Create("Talep güncellendi.", NotifyType.LeadChanged, model.UserId);

                response.Success = "Talep güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // POST: Leads/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            try
            {
                _leadService.Delete(id);
                response.Success = "Talep silindi.";
            }
            catch (Exception ex)
            {
                response.AddError("ex", ex.Message);
            }

            return Json(response);
        }
    }
}
