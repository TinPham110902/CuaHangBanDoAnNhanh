using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnNhanh.Models;
using Rotativa;

namespace DoAnNhanh.Controllers
{
    public class HoaDonsController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();

        // GET: HoaDons
        public ActionResult Index(string DoanhThuTheoNgay)
        {
            /*  if(DoanhThuTheoNgay !=null)
                  { 
                  DateTime ngay = DateTime.Parse(DoanhThuTheoNgay);

                     // ViewBag.select = DoanhThuTheoNgay;
                      List<HoaDon> listNgayHoaDon = new List<HoaDon>();

                      listNgayHoaDon = db.HoaDons.Where(n => n.Ngay.Equals(ngay)).ToList();
                      int valuefinal = 0;
                      foreach (var result in listNgayHoaDon)
                      {
                          valuefinal = valuefinal + 1;

                      }
                      ViewBag.total = valuefinal;
                  }*/

            List<HoaDon> hoadon = db.HoaDons.ToList();
            ViewBag.HoaDons = hoadon;
            var tonghop = db.HoaDons.GroupBy(s => s.Ngay).Select(g => new Schedule { name = g.Key, Total = (int)g.Sum(x => x.ThanhTien) }).OrderByDescending(o => o.name).ToList();
            foreach (var t in tonghop)
            {
                t.date = t.name.ToString("dd/MM/yyyy");
            }
            
            var tonghop2 = tonghop.GroupBy(s => s.date).Select(g => new Schedule { date= g.Key,Total = g.Sum(x => x.Total) });

            return View(tonghop2);
        }

        // GET: HoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          List<CT_HD> ct_hd = db.CT_HD.Where(m=>m.MaHd == id).ToList();

            HoaDon hoadon = db.HoaDons.Find(id);
           
            ViewBag.mahd = hoadon.MaHd;
            ViewBag.Tk = hoadon.Tk;
            ViewBag.tongtien = hoadon.ThanhTien;
            if (ct_hd == null)
            {
                return HttpNotFound();
            }
            return View(ct_hd);
        }

        // GET: HoaDons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHd,Ngay")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hoaDon);
        }

        // GET: HoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHd,Ngay")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hoaDon);
        }

        // GET: HoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult print(int? id)
        {
            
            Session["id"] = id;

            if (Session["id"] != null)
            {
                id = (int?)Session["id"];
               

            }
           

            List<CT_HD> ct_hd = db.CT_HD.Where(m => m.MaHd == id).ToList();

            HoaDon hoadon = db.HoaDons.Find(id);

           
            ViewBag.mahd = hoadon.MaHd;
            ViewBag.Tk = hoadon.Tk;
            ViewBag.tongtien = hoadon.ThanhTien;
           
            /*
              if (ct_hd == null)
              {
                  return HttpNotFound();
              }*/
            return PartialView(ct_hd);
        }

        public ActionResult ExportPdf(int id)
        {
            return new ActionAsPdf("print", new {id = id })
            {
                FileName = Server.MapPath("~/Content/Invoice.pdf")
            };
           
        }
    }
}
