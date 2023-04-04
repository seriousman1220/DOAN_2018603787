using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class MainPageController : ApiController
    {
        private List<khoadt> courses = new List<khoadt> {
            new khoadt { id= "HTML, CSS", name= "HTML + CSS", from_date= new DateTime(2022, 01, 01), to_date= new DateTime(2022, 02, 01) },
            new khoadt { id= "JS", name= "Javascript", from_date= new DateTime(2022, 02, 01), to_date= new DateTime(2022, 02, 15) },
            new khoadt { id= "JQuery, AngularJs", name= "JQuery - AngularJs", from_date= new DateTime(2022, 02, 15), to_date= new DateTime(2022, 03, 01) },
            new khoadt { id= "API", name= "Kết nối Api", from_date= new DateTime(2022, 03, 02), to_date= new DateTime(2022, 04, 01) }
        };
        [HttpGet]
        public HttpResponseMessage LoadData()
        {
            dscombo dscombo = new dscombo();
            dscombo.courses = courses;

            return Request.CreateResponse(HttpStatusCode.OK, dscombo);
        }
    }
}
