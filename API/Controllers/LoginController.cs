using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CheckLogin(Userinfo account)
        {
            string query = "select check_login(@username, @pass)";
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=VanPhuc;User Id=postgres;Password=seriousman");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            string pwd_hash = Encrypt(account.pass);
            cmd.Parameters.AddWithValue("username", account.user_name);
            cmd.Parameters.AddWithValue("pass", Encrypt(account.pass));

            int exist_yn = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return Request.CreateResponse(HttpStatusCode.OK, exist_yn);
        }

        public HttpResponseMessage CreateAccount(Userinfo account)
        {
            var json = JsonConvert.SerializeObject(account);
            var param = new { 
                data = json
            };


            string query = "select insert_userinfo(@param)";
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=VanPhuc;User Id=postgres;Password=seriousman");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            string pwd_hash = Encrypt(account.pass);
            cmd.Parameters.AddWithValue("param", param);

            return Request.CreateResponse(HttpStatusCode.OK, "OK");

        }

        private string Encrypt(string pwd)
        {
            string hash = "Admin123!@#";
            byte[] data = UTF8Encoding.UTF8.GetBytes(pwd);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(result, 0, result.Length);
                }
            }


        }

       
    }
}
