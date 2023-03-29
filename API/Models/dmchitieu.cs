using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class dmchitieu
    {
        public khoadt course { get; set; }
        public nhanvien employee { get; set; }
        public bophan department { get; set; }
        public System.DateTime rating_date { get; set; }
        public string instructor { get; set; }
        public decimal total { get; set; }
        public trangthai status { get; set; }
        public List<dmchitieuct> ct { get; set; }

        public decimal Total()
        {
            foreach (var item in ct)
            {
                total += item.diem_qd;
            }
            return total;
        }
    }
}