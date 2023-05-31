using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnNhanh.Models;
using System.IO;

namespace DoAnNhanh.Controllers
{
    public class MonAnsController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();

        // GET: MonAns
        public ActionResult Index(string searchBy = "" , string sortOrder="")
        {
            

            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SortByPrice = (sortOrder == "dongia" ? "dongia_desc" : "dongia");
            var monAns = db.MonAns.Include(m => m.Loai);
            ViewBag.Cate = db.Loais.ToList();



            switch(sortOrder)
            {
                case "ten_desc":
                    monAns = monAns.OrderByDescending(s => s.TenMa);
                    break;
                case "dongia_desc":
                    monAns = monAns.OrderByDescending(s => s.Gia);
                    break;
                case "dongia":
                    monAns = monAns.OrderBy(s => s.Gia);
                    break;
                default:
                    monAns = monAns.OrderBy(s => s.TenMa);
                    break;
            }    
            if (searchBy != "")
            {
                ViewBag.select = searchBy;
                return View(monAns.Where(s => s.Loai.TenLoai == searchBy).ToList());
               
            }

            return View(monAns.ToList());
        }



        // GET: MonAns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            return View(monAn);
        }

        // GET: MonAns/Create
        public ActionResult Create()
        {
            ViewBag.MaKm = new SelectList(db.KhuyenMais, "MaKm", "MaKm");
            MonAn Monan = new MonAn();
            return View(Monan);
        }

        // POST: MonAns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MonAn monAn)
        {
            if (ModelState.IsValid)
            {
                if (monAn.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(monAn.ImageUpload.FileName);
                    string extension = Path.GetExtension(monAn.ImageUpload.FileName);
                    fileName = fileName + extension;
                    monAn.Hinh = "~/hinh/" + fileName;
                    monAn.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/hinh/"), fileName));
                }
                db.MonAns.Add(monAn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKm = new SelectList(db.KhuyenMais, "MaKm", "MaKm", monAn.MaKm);
            return View(monAn);
        }

        // GET: MonAns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKm = new SelectList(db.KhuyenMais, "MaKm", "MaKm", monAn.MaKm);
            return View(monAn);
        }

        // POST: MonAns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,MonAn monAn)
        {
            try
            {
                if (monAn.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(monAn.ImageUpload.FileName);
                    string extension = Path.GetExtension(monAn.ImageUpload.FileName);
                    fileName = fileName + extension;
                    monAn.Hinh = "~/hinh/" + fileName;
                    monAn.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/hinh/"), fileName));
                }
                db.Entry(monAn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(monAn);
            }

        }

        // GET: MonAns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            return View(monAn);
        }

        // POST: MonAns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonAn monAn = db.MonAns.Find(id);
            db.MonAns.Remove(monAn);
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

        public ActionResult ChooseMaLoai()
        {
            Loai loai = new Loai();
            loai.LoaiCollection = db.Loais.ToList<Loai>();
            return PartialView(loai);
        }
    }
}
