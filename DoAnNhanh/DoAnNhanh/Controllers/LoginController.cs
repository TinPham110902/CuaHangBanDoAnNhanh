using DoAnNhanh.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnNhanh.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();
        public ActionResult Index()
        {
            return View();
        }

        //method login
        [HttpPost]
        public ActionResult Authen(Tk taikhoan) 
        {
            var check = db.Tks.SingleOrDefault(s => s.Tk1.Equals(taikhoan.Tk1)
            && s.Mk.Equals(taikhoan.Mk));
            if(check == null)
            {
                taikhoan.LoginErrorMessage = "Sai tài khoản hoặc mật khẩu!";
                return View("Index",taikhoan);
            }
            else
            {
                Session["user"] = check;
                Log_login login = new Log_login();
                login.Login=System.DateTime.Now;
                login.TK = check.Tk1;
                db.Log_login.Add(login);
                db.SaveChanges();
                Session["ID"] = login.ID;
                return RedirectToAction("Index","Tks");
            }    
        
        
        }

        public ActionResult LogOut()
        {

            Log_login login = db.Log_login.Find(Session["ID"]);

            login.Logout = System.DateTime.Now;

            db.Entry(login).State = EntityState.Modified;
            db.SaveChanges();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

       
    }
}