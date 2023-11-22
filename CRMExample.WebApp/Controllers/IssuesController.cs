using CRMExample.Entities;
using CRMExample.Model;
using CRMExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using CRMExample.Common;
using System.Linq;
using CRMExample.WebApp.Filters;

namespace CRMExample.WebApp.Controllers
{
    [Auth]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly IUserService _userService;
        private readonly INotifyService _notifyService;

        public IssuesController(IIssueService issueService, IUserService userService,INotifyService notifyService)
        {
            _issueService = issueService;
            _userService = userService;
            _notifyService = notifyService;
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

        // GET: Issues
        public ActionResult Index(string search = "")
        {

            List<Issue> issues = null;

            string role = HttpContext.Session.GetString(Constants.Session_Role);
            int userid = HttpContext.Session.GetInt32(Constants.Session_Id).Value;

            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
            {
                if (role == Constants.Role_Admin)
                {
                    issues = _issueService.List();
                }
                else
                {
                    issues = _issueService.ListByUserId(userid);
                }
            }
            else
            {
                if (role == Constants.Role_Admin)
                {
                    issues = _issueService.ListBySearch(search);
                }
                else
                {
                    issues = _issueService.ListBySearch(search, userid);
                }

                ViewData["search"] = search;
            }

            return View(issues.OrderBy(x => x.Completed).ThenByDescending(x => x.CreatedAt).ToList());
        }

        // POST: Issues/Create
        [HttpPost]
        public ActionResult Create(CreateIssueModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                var issue = _issueService.Create(model);

                // todo : burada görev eklendiği için kullanıcıya bildirim eklenmeli.
                _notifyService.Create("Yeni görev atandı.", NotifyType.IssueAdded, model.UserId);
                response.Success = "Görev eklendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // GET: Issues/Details/5
        public ActionResult Details(int id)
        {
            var issue = _issueService.GetById(id);
            issue.User.Issues = null;

            return Json(new AjaxResponseModel<Issue> { Data = issue });
        }

        // POST: Issues/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EditIssueModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                var issue = _issueService.Update(id, model);

                // todo : burada görev güncellendi için kullanıcıya bildirim eklenmeli.
                if (model.Completed)
                    _notifyService.Create("Görev tamamlandı.", NotifyType.IssueCompleted, model.UserId);
                else
                    _notifyService.Create("Görev güncellendi.", NotifyType.IssueChanged, model.UserId);
                response.Success = "Görev güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // POST: Issues/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            try
            {
                _issueService.Delete(id);
                response.Success = "Görev silindi.";
            }
            catch (Exception ex)
            {
                response.AddError("ex", ex.Message);
            }

            return Json(response);
        }
    }
}

