using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace API.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CheckLogin(LoginInfo account)
        {
            if (account.username.ToString() == "admin" && account.password.ToString() == "admin")
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Wrong");
            }
        }
    }
}
