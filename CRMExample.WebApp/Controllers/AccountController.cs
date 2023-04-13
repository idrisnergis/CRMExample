using CRMExample.Common;
using CRMExample.Model;
using CRMExample.Models;
using CRMExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMExample.WebApp.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthenticationModel model)
        {
            AjaxResponseModel<String> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                var user = _userService.Authenticate(model);

                if (user != null)
                {
                    response.Success = "Giriş işlemi başarılı şekilde yapıldı.";

                    HttpContext.Session.SetString(Constants.Session_Name,user.Name);
                    HttpContext.Session.SetString(Constants.Session_Role,user.Role);
                    HttpContext.Session.SetInt32(Constants.Session_Id,user.Id);
                }
                else
                    response.AddError(nameof(model.Username), "Hatalı Kullanıcı adı veya Şifre");

                return Json(response);

            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        public IActionResult CreateFakeUser()//Test User Create
        {
            _userService.Create(new CreateUserModel
            {
                Name = "idris",
                Email = "idris@yok.com.tr",
                Password = "565656",
                Locked =false,
                RePassword = "565656",
                Role = "admin",
                Username ="idrisnergis"
            });
            return Json("Ok");
        }
    }
}
