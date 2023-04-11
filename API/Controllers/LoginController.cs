using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using System.Data;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CheckLogin(LoginInfo account)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=seriousman");
            conn.Open();
            NpgsqlCommand query = new NpgsqlCommand();
            query.Connection = conn;
            query.CommandType = CommandType.Text;
            query.CommandText = "select * from dmnhvt";
            NpgsqlDataAdapter nda = new NpgsqlDataAdapter(query);
            DataTable dt = new DataTable();
            nda.Fill(dt);
            query.Dispose();
            conn.Close();
            if (account.username.ToString() == "admin" && account.password.ToString() == "admin")
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Wrong");
            }
        }
    }
}
