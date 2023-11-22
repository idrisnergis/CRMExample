using CRMExample.Common;
using CRMExample.Entities;
using CRMExample.Model;
using CRMExample.Models;
using CRMExample.Services;
using CRMExample.WebApp.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMExample.WebApp.Controllers
{
    [Auth(Roles = Constants.Role_Admin)]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index(string search = "")
        {
            List<User> users = null;
            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
            {
                users = _userService.List();
            }
            else
            {
                users = _userService.ListBySearch(search);
                ViewData["search"] = search;
            }
            return View(users);
        }

        public ActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            return Json(new AjaxResponseModel<User> { Data = user });
        }


        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _userService.Create(model);
                response.Success = "Müşteri Eklendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);
            return Json(response);
        }

        [HttpPost]
        public ActionResult Edit(int id, UpdateUserModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _userService.Update(id, model);
                response.Success = "Kullanıcı Güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);
            return Json(response);
        }

        public ActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            return Json(new AjaxResponseModel<User> { Data = user });
        }

        public ActionResult Delete(int id)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            try
            {
                _userService.Delete(id);
                response.Success = "Kullanıcı Silindi.";
            }
            catch (Exception ex)
            {
                response.AddError("ex", ex.Message);
            }
            return Json(response);
        }

        // POST: Customers/ChangePassword/5
        [HttpPost]
        public ActionResult ChangePassword(int id, PasswordUserModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                _userService.ChangePassword(id, model);

                response.Success = "Kullanıcı şifre değiştirildi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        [HttpPost]
        public ActionResult ChangeUsername(int id, ChangeUsernameModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.ChangeUsername(id, model);

                    response.Success = "Kullanıcı adı değişitirildi.";
                    return Json(response);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(model.Username), ex.Message);
                }
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

    }
}
