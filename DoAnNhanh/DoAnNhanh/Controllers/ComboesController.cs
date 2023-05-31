using DoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApplication1.Controllers
{
    public class ComboesController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();

        // GET: Comboes
        public ActionResult Index()
        {
            return View(db.Comboes.ToList());
        }

        // GET: Comboes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            return View(db.CT_Combo.Where(s => s.Macb == id).ToList());
        }

        // GET: Comboes/Create
        public ActionResult Create()
        {
            Combo combo = new Combo();
            return View(combo);
        }

        // POST: Comboes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Combo combo)
        {
            if (ModelState.IsValid)
            {
                if (combo.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(combo.ImageUpload.FileName);
                    string extension = Path.GetExtension(combo.ImageUpload.FileName);
                    fileName = fileName + extension;
                    combo.Hinh = "~/hinh/" + fileName;
                    combo.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/hinh/"), fileName));
                }
         
            db.Comboes.Add(combo);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(combo);
        }

        // GET: Comboes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Combo combo = db.Comboes.Find(id);
            if (combo == null)
            {
                return HttpNotFound();
            }
            return View(combo);
        }

        // POST: Comboes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int? id,Combo combo)
        {
            try
            {
                if (combo.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(combo.ImageUpload.FileName);
                    string extension = Path.GetExtension(combo.ImageUpload.FileName);
                    fileName = fileName + extension;
                    combo.Hinh = "~/hinh/" + fileName;
                    combo.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/hinh/"), fileName));
                }
                db.Entry(combo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(combo);
            }
        }

        // GET: Comboes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Combo combo = db.Comboes.Find(id);
            if (combo == null)
            {
                return HttpNotFound();
            }
            return View(combo);
        }

        // POST: Comboes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Combo combo = db.Comboes.Find(id);
            List<CT_Combo> ct_combo = db.CT_Combo.Where(s=>s.Macb == id).ToList();
            foreach(var item in ct_combo)
            {
                db.CT_Combo.Remove(item);
            }    
            db.Comboes.Remove(combo);
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
