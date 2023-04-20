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

    public class MainPageController : ApiController
    {
        private readonly NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=VanPhuc;User Id=postgres;Password=seriousman");
        [HttpGet]
        public HttpResponseMessage LoadData()
        {
            conn.Open();
            string query = "select level, ma_nhom, ten_nhom from dmnhvt where level = 1";
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            List<dmnhvt> ds_nh_vt1 = new List<dmnhvt>();
            using (NpgsqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    ds_nh_vt1.Add(ReadNhomVT(dr));
                }
            }
            conn.Close();
            var data_return = new
            {
                ds_nh_vt1 = ds_nh_vt1,

            };
            return Request.CreateResponse(HttpStatusCode.OK, data_return);
        }

        private static dmnhvt ReadNhomVT(NpgsqlDataReader reader)
        {
            string ma_nhom = reader["ma_nhom"] as string;
            string ten_nhom = reader["ten_nhom"] as string;
            dmnhvt nhvt = new dmnhvt
            {
                ma_nhom = ma_nhom,
                ten_nhom = ten_nhom,
            };
            return nhvt;
        }


    }
}
