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
    public class TksController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();

        // GET: Tks
        public ActionResult Index()
        {
            var tks = db.Tks.Include(t => t.Quyen);
            return View(tks.ToList());
        }

        // GET: Tks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tk tk = db.Tks.Find(id);
            if (tk == null)
            {
                return HttpNotFound();
            }
            return View(tk);
        }

        // GET: Tks/Create
        public ActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }

        // POST: Tks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tk1,Mk,MaQuyen")] Tk tk)
        {
            if (ModelState.IsValid)
            {
                db.Tks.Add(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", tk.MaQuyen);
            return View(tk);
        }

        // GET: Tks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tk tk = db.Tks.Find(id);
            if (tk == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", tk.MaQuyen);
            return View(tk);
        }

        // POST: Tks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tk1,Mk,MaQuyen")] Tk tk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", tk.MaQuyen);
            return View(tk);
        }

        // GET: Tks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tk tk = db.Tks.Find(id);
            if (tk == null)
            {
                return HttpNotFound();
            }
            return View(tk);
        }

        // POST: Tks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tk tk = db.Tks.Find(id);
            db.Tks.Remove(tk);
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

        public ActionResult LsLogin(string id)
        {
            List<Log_login> log_Logins = db.Log_login.Where(s => s.TK.Equals(id)).ToList();

            return View(log_Logins);
        }
    }
}
