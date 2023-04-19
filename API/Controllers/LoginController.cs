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
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        private readonly Random _random = new Random();

        [HttpPost]
        public HttpResponseMessage CheckLogin(Userinfo account)
        {
            string query = "select check_login(@username, @pass)";
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=VanPhuc;User Id=postgres;Password=seriousman");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            string pwd_hash = Encrypt(account.pass.ToString());
            cmd.Parameters.AddWithValue("username", account.user_name);
            cmd.Parameters.AddWithValue("pass", Encrypt(account.pass));

            int exist_yn = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return Request.CreateResponse(HttpStatusCode.OK, exist_yn);
        }

        public HttpResponseMessage CreateAccount(Userinfo account)
        {
            account.pass = Encrypt(account.pass.ToString());
            var json = JsonConvert.SerializeObject(account);

            string query = "select insert_userinfo(@param)";
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=VanPhuc;User Id=postgres;Password=seriousman");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            string pwd_hash = Encrypt(account.pass.ToString());
            cmd.Parameters.AddWithValue("param", json);
            var result = Convert.ToInt32(cmd.ExecuteScalar());
            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Error");

            }

        }

        public HttpResponseMessage ResetPassword(Userinfo account)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=VanPhuc;User Id=postgres;Password=seriousman");
            #region Check email tồn tại
            string query = "select 1 from userinfo where email = @email";
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("email", account.email);
            int exist_yn = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            if (exist_yn == 1)
            {
                string pwd_send = RandomPassword();
                string pwd_update = Encrypt(pwd_send.ToString());
                #region Gửi email 
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("CTy TNHH Vạn Phúc", "tuananh603787@email.com"));
                email.To.Add(new MailboxAddress("Khách hàng", account.email));

                email.Subject = "Cấp lại mật khẩu";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "<p>Xác nhận email thành công. Mật khẩu mới của bạn là: <b>" + pwd_send + "</b></p>"
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("tuananh603787@gmail.com", "limilklgwhevbqri");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                #endregion

                //Update password
                string query_update = "update userinfo set pass = @pass, date2 = current_date where email = @email";
                conn.Open();
                NpgsqlCommand cmd_update = new NpgsqlCommand(query_update, conn);
                cmd_update.Parameters.AddWithValue("email", account.email);
                cmd_update.Parameters.AddWithValue("pass", pwd_update);
                cmd_update.ExecuteNonQuery();
                conn.Close();

                return Request.CreateResponse(HttpStatusCode.OK, "OK");


            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ERROR");

            }
            #endregion


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

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        private string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }


    }
}
