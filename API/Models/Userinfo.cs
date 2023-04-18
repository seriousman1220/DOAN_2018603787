using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Userinfo
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string pass { get; set; }
        public string ten { get; set; }
        public int is_admin { get; set; }
        public string email { get; set; }
        public string dien_thoai { get; set; }
        public string status { get; set; }
        public DateTime date0 { get; set; }
        public int user_id0 { get; set; }
        public DateTime date2 { get; set; }
        public int user_id2 { get; set; }
        public string avatar { get; set; }
    }
}