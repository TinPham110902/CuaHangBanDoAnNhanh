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
    public class CT_MenuController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();

        // GET: CT_Menu
        public ActionResult Index()
        {
            var cT_Menu = db.CT_Menu.Include(c => c.MonAn).Include(c => c.Menu);
            return View(cT_Menu.ToList());
        }

        // GET: CT_Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_Menu cT_Menu = db.CT_Menu.Find(id);
            if (cT_Menu == null)
            {
                return HttpNotFound();
            }
            return View(cT_Menu);
        }

        // GET: CT_Menu/Create
        public ActionResult Create()
        {
            ViewBag.MaMa = new SelectList(db.MonAns, "MaMa", "TenMa");
            ViewBag.MaMenu = new SelectList(db.Menus, "MaMenu", "MaMenu");
            return View();
        }

        // POST: CT_Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMenu,MaMa,MaCombo")] CT_Menu cT_Menu)
        {
            if (ModelState.IsValid)
            {
                db.CT_Menu.Add(cT_Menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMa = new SelectList(db.MonAns, "MaMa", "TenMa", cT_Menu.MaMa);
            ViewBag.MaMenu = new SelectList(db.Menus, "MaMenu", "MaMenu", cT_Menu.MaMenu);
            return View(cT_Menu);
        }

        // GET: CT_Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_Menu cT_Menu = db.CT_Menu.Find(id);
            if (cT_Menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMa = new SelectList(db.MonAns, "MaMa", "TenMa", cT_Menu.MaMa);
            ViewBag.MaMenu = new SelectList(db.Menus, "MaMenu", "MaMenu", cT_Menu.MaMenu);
            return View(cT_Menu);
        }

        // POST: CT_Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMenu,MaMa,MaCombo")] CT_Menu cT_Menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_Menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMa = new SelectList(db.MonAns, "MaMa", "TenMa", cT_Menu.MaMa);
            ViewBag.MaMenu = new SelectList(db.Menus, "MaMenu", "MaMenu", cT_Menu.MaMenu);
            return View(cT_Menu);
        }

        // GET: CT_Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_Menu cT_Menu = db.CT_Menu.Find(id);
            if (cT_Menu == null)
            {
                return HttpNotFound();
            }
            return View(cT_Menu);
        }

        // POST: CT_Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_Menu cT_Menu = db.CT_Menu.Find(id);
            db.CT_Menu.Remove(cT_Menu);
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
