using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnNhanh.Models;

namespace DoAnNhanh.Controllers
{
    public class ThongKeController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();
        // GET: ThongKe
        public ActionResult Index(string DoanhThuTheoNgay="")
        {
            if (DoanhThuTheoNgay!="")
            {
                
                    ViewBag.select = DoanhThuTheoNgay;
                var ngayTaoHd = DoanhThuTheoNgay;
                List<HoaDon> listNgayHoaDon = db.HoaDons.Where(n => n.Ngay.ToString() == ngayTaoHd).ToList();
                int valuefinal = 0;
                foreach (var result in listNgayHoaDon)
                {
                    valuefinal = valuefinal + (int)result.ThanhTien;

                }
                ViewBag.total = valuefinal;
            }
          
            return View();
        }
    }
}