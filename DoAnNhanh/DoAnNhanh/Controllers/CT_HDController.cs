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
    public class CT_HDController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();

        // GET: CT_HD
        public ActionResult Index()
        {
            var cT_HD = db.CT_HD.Include(c => c.HoaDon).Include(c => c.MonAn);
            return View(cT_HD.ToList());
        }

        // GET: CT_HD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_HD cT_HD = db.CT_HD.Find(id);
            if (cT_HD == null)
            {
                return HttpNotFound();
            }
            return View(cT_HD);
        }

        // GET: CT_HD/Create
        public ActionResult Create()
        {
            ViewBag.MaHd = new SelectList(db.HoaDons, "MaHd", "Tk");
            ViewBag.MaMa = new SelectList(db.MonAns, "MaMa", "TenMa");
            return View();
        }

        // POST: CT_HD/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHd,MaMa,Sl")] CT_HD cT_HD)
        {
            if (ModelState.IsValid)
            {
                db.CT_HD.Add(cT_HD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHd = new SelectList(db.HoaDons, "MaHd", "Tk", cT_HD.MaHd);
            ViewBag.MaMa = new SelectList(db.MonAns, "MaMa", "TenMa", cT_HD.MaMa);
            return View(cT_HD);
        }

        // GET: CT_HD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_HD cT_HD = db.CT_HD.Find(id);
            if (cT_HD == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHd = new SelectList(db.HoaDons, "MaHd", "Tk", cT_HD.MaHd);
            ViewBag.MaMa = new SelectList(db.MonAns, "MaMa", "TenMa", cT_HD.MaMa);
            return View(cT_HD);
        }

        // POST: CT_HD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHd,MaMa,Sl")] CT_HD cT_HD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_HD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHd = new SelectList(db.HoaDons, "MaHd", "Tk", cT_HD.MaHd);
            ViewBag.MaMa = new SelectList(db.MonAns, "MaMa", "TenMa", cT_HD.MaMa);
            return View(cT_HD);
        }

        // GET: CT_HD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_HD cT_HD = db.CT_HD.Find(id);
            if (cT_HD == null)
            {
                return HttpNotFound();
            }
            return View(cT_HD);
        }

        // POST: CT_HD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_HD cT_HD = db.CT_HD.Find(id);
            db.CT_HD.Remove(cT_HD);
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
    }
}
