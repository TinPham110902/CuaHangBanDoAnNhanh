using DoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnNhanh.Controllers
{
    public class ThanhToanController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();
        // GET: ThanhToan


        public ActionResult Index()
        {
            var MonAn = db.MonAns.ToList();
            return View(MonAn.ToList());
        }
        public ActionResult ThanhToan()
        {
            var MonAn = db.MonAns.ToList();
            return View(MonAn.ToList());
        }

        public ActionResult checkout(List<CT_HD> hoadon)
        {
           
                HoaDon hoaDon = new HoaDon();
                hoaDon.Ngay=DateTime.Now;
            foreach(var item in hoadon)
            {
                CT_HD ct_hd = new CT_HD();
                ct_hd.MaMa = item.MaMa;
            }


                db.HoaDons.Add(hoaDon);
            db.SaveChanges();
            return RedirectToAction("ThanhToan", "ThanhToan");
        }
    }
}