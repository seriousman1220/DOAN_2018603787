using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class dscombo
    {
        public List<nhanvien> ds_nhanvien { get; set; }
        public List<bophan> departments { get; set; }
        public List<khoadt> courses { get; set; }
        public List<trangthai> status_list { get; set; }
        public List<dmchitieu> dmchitieus { get; set; }
    }
}