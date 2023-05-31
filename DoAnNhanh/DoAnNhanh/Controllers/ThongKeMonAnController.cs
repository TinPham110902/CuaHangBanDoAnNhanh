using DoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnNhanh.Controllers
{
    public class ThongKeMonAnController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();
        // GET: ThongKeMonAn
        public ActionResult Group()
        {
         List<MonAn> monan = db.MonAns.ToList();
            ViewBag.monan = monan;


           var tonghop = db.CT_HD.GroupBy(s=>s.MaMa).Select(g=> new TotalFood {ma = g.Key, TotalFd =g.Sum(x=>x.Sl) }).OrderByDescending(o=>o.TotalFd).ToList();
            foreach(var t in tonghop)
            {
               t.name  = db.MonAns.Find(t.ma).TenMa;
            }    
                      return View(tonghop);
        }
    }
}