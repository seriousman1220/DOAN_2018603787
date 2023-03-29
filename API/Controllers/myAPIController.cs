using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class myAPIController : ApiController
    {
        private List<trangthai> status_list = new List<trangthai> {
            new trangthai { id = 1, name = "Sử dụng" },
            new trangthai { id = 0, name = "Không sử dụng" }
        };

        private List<bophan> departments = new List<bophan> {
            new bophan { id = 1, name = "Kỹ thuật", bonus_mark = Convert.ToDecimal(0.5)},
            new bophan { id = 2, name = "Dự án", bonus_mark = Convert.ToDecimal(1)},
            new bophan { id = 3, name = "Test", bonus_mark = Convert.ToDecimal(1.5)}
        };

        private List<khoadt> courses = new List<khoadt> {
            new khoadt { id= "HTML, CSS", name= "HTML + CSS", from_date= new DateTime(2022, 01, 01), to_date= new DateTime(2022, 02, 01) },
            new khoadt { id= "JS", name= "Javascript", from_date= new DateTime(2022, 02, 01), to_date= new DateTime(2022, 02, 15) },
            new khoadt { id= "JQuery, AngularJs", name= "JQuery - AngularJs", from_date= new DateTime(2022, 02, 15), to_date= new DateTime(2022, 03, 01) },
            new khoadt { id= "API", name= "Kết nối Api", from_date= new DateTime(2022, 03, 02), to_date= new DateTime(2022, 04, 01) }
        };

        private List<nhanvien> ds_nhanvien = new List<nhanvien> {
            new nhanvien { id = "NV001", name = "Nguyễn Văn A" },
            new nhanvien { id = "NV002", name = "Nguyễn Thị B" },
            new nhanvien { id = "NV003", name = "Nguyễn Văn C" },
            new nhanvien { id = "NV004", name = "Nguyễn Thị D" }
        };

        private static List<dmchitieu> dmchitieus = new List<dmchitieu>();

        [HttpGet]
        public HttpResponseMessage LoadData()
        {
            dmchitieus.Clear();
            dmchitieus.Add(new dmchitieu
            {
                course = courses[0],
                employee = ds_nhanvien[0],
                department = departments[0],
                rating_date = Convert.ToDateTime("03/03/2022"),
                instructor = "Nguyễn Văn Q",
                total = 8.8M,
                status = status_list[0],
                ct = new List<dmchitieuct> {
                    new dmchitieuct { id = "CT01", name = "Khả năng tự học", he_so = 0.4M, diem = 8M, diem_qd = 3.2M },
                    new dmchitieuct { id = "CT02", name = "Khả năng logic dữ liệu", he_so = 0.2M, diem = 8M, diem_qd = 1.6M },
                    new dmchitieuct { id = "CT03", name = "Chuyên cần", he_so = 0.4M, diem = 10M, diem_qd = 4M }
                }
            });
            dmchitieus.Add(new dmchitieu
            {
                course = courses[1],
                employee = ds_nhanvien[1],
                department = departments[1],
                rating_date = Convert.ToDateTime("01/03/2022"),
                instructor = "Nguyễn Văn Q",
                total = 9,
                status = status_list[1],
                ct = new List<dmchitieuct> {
                    new dmchitieuct { id = "CT01", name = "Khả năng tự học", he_so = 0.5M, diem = 8M, diem_qd = 4M },
                    new dmchitieuct { id = "CT02", name = "Khả năng logic dữ liệu", he_so = 0.2M, diem = 5M, diem_qd = 1M },
                    new dmchitieuct { id = "CT03", name = "Chuyên cần", he_so = 0.4M, diem = 10M, diem_qd = 4M }
                }
            });
            dmchitieus.Add(new dmchitieu
            {
                course = courses[2],
                employee = ds_nhanvien[2],
                department = departments[2],
                rating_date = Convert.ToDateTime("01/03/2022"),
                instructor = "Nguyễn Văn Q",
                total = 6,
                status = status_list[0],
                ct = new List<dmchitieuct> {
                    new dmchitieuct { id = "CT01", name = "Khả năng logic dữ liệu", he_so = 0.2M, diem = 10M, diem_qd = 2M },
                    new dmchitieuct { id = "CT02", name = "Chuyên cần", he_so = 0.4M, diem = 10M, diem_qd = 4M }
                }
            });

            dscombo dscombo = new dscombo();
            dscombo.departments = departments;
            dscombo.status_list = status_list;
            dscombo.courses = courses;
            dscombo.ds_nhanvien = ds_nhanvien;
            dscombo.dmchitieus = dmchitieus;

            return Request.CreateResponse(HttpStatusCode.OK, dscombo);
        }

        [HttpGet]
        public HttpResponseMessage RemoveEmp(string id_khoadt, string id_emp)
        {
            dmchitieu dmct = dmchitieus.FirstOrDefault(d => d.course.id.Equals(id_khoadt) && d.employee.id.Equals(id_emp));
            dmchitieus.Remove(dmct);

            return Request.CreateResponse(HttpStatusCode.OK, dmchitieus);
        }

        [HttpPost]
        public HttpResponseMessage RemoveSelectedEmp(List<dmchitieu> ds_dmct)
        {
            foreach (var item in ds_dmct.ToList())
            {
                foreach (var item1 in dmchitieus.ToList())
                {
                    if (item.course.id.Equals(item1.course.id) && item.employee.id.Equals(item1.employee.id))
                    {
                        dmchitieus.Remove(item1);
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, dmchitieus);
        }

        [HttpPost]
        public HttpResponseMessage SaveChangesEmp(dmchitieu dmct)
        {
            var dmct_temp = dmchitieus.FirstOrDefault(d => d.course.id.Equals(dmct.course.id) && d.employee.id.Equals(dmct.employee.id));

            if (dmct_temp == null)
            {
                dmchitieus.Add(dmct);
            }
            else
            {
                dmct_temp.course = dmct.course;
                dmct_temp.employee = dmct.employee;
                dmct_temp.department = dmct.department;
                dmct_temp.rating_date = Convert.ToDateTime(dmct.rating_date);
                dmct_temp.instructor = dmct.instructor;
                dmct_temp.total = dmct.total;
                dmct_temp.status = dmct.status;
                dmct_temp.ct = dmct.ct;
            }

            return Request.CreateResponse(HttpStatusCode.OK, dmchitieus);
        }

        [HttpGet]
        public HttpResponseMessage Search(string key_search)
        {
            if (key_search == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, dmchitieus);
            }
            else
            {
                List<dmchitieu> ds_dmchitieu = new List<dmchitieu>();

                foreach (var item in dmchitieus)
                {
                    if (item.course.name.ToLower().Contains(key_search) || item.employee.name.ToLower().Contains(key_search)
                        || item.department.name.ToLower().Contains(key_search) || item.instructor.ToLower().Contains(key_search))
                    {
                        ds_dmchitieu.Add(item);
                    }

                }

                return Request.CreateResponse(HttpStatusCode.OK, ds_dmchitieu);
            }
        }

        [HttpGet]
        public HttpResponseMessage AdvancedSearch(string course, string department, System.DateTime rating_date, decimal from_mark, decimal to_mark)
        {
            List<dmchitieu> ds_dmct = new List<dmchitieu>();
            ds_dmct.AddRange(dmchitieus);

            if (course != null)
            {
                foreach (var item in ds_dmct.ToList())
                {
                    if (!item.course.id.Equals(course))
                    {
                        ds_dmct.Remove(item);
                    }
                }
            }

            if (department != null)
            {
                foreach (var item in ds_dmct.ToList())
                {
                    if (!item.department.id.Equals(Convert.ToInt32(department)))
                    {
                        ds_dmct.Remove(item);
                    }
                }
            }

            if (rating_date.Day != DateTime.Now.Day || rating_date.Month != DateTime.Now.Month || rating_date.Year != DateTime.Now.Year)
            {
                foreach (var item in ds_dmct.ToList())
                {
                    if (rating_date.Day != item.rating_date.Day || rating_date.Month != item.rating_date.Month
                        || rating_date.Year != item.rating_date.Year)
                    {
                        ds_dmct.Remove(item);
                    }
                }
            }

            foreach (var item in ds_dmct.ToList())
            {
                if (item.total < from_mark || item.total > to_mark)
                {
                    ds_dmct.Remove(item);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, ds_dmct);
        }

    }
}
