using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SPA.API.Authentication
{
    public class RoleManage: AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            string[] roles = Roles.Split(',');
            try
            {
                for (int i = 0; i < roles.Count(); i++)
                {
                    if (roles.Any(actionContext.Request.Headers.GetCookies("Type").ToString().Contains))
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response= new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

    }
}