using CRMExample.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMExample.WebApp.Filters
{
    public class AuthAttribute :Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.Session.Keys.Contains(Constants.Session_Id))
                {
                    context.Result = new RedirectResult("/Account/Login");
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Roles))
                    {
                        string role = context.HttpContext.Session.GetString(Constants.Session_Role);
                        string[] roles = Roles.Split(",");      // admin,user

                        if (!roles.Contains(role))
                        {
                            context.Result = new RedirectResult("/");
                            return;
                        }
                    }
                }
            }
    }
}
