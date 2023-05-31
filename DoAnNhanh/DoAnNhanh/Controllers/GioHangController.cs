using DoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnNhanh.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();


     
        public ActionResult ThanhToan()
        {
            var MonAn = db.MonAns.ToList();
            return View(MonAn.ToList());
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart==null || Session["Cart"]==null  )
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var pro = db.MonAns.SingleOrDefault(s=> s.MaMa == id);
            if(pro!= null )
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "GioHang");
        }

        public ActionResult ShowToCart()
        {

            var MonAn = db.MonAns.ToList();
     ViewBag.DsMonAn = MonAn;
/*            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "GioHang");*/
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "GioHang");
        }
         public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "GioHang");
        }

        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if(cart != null)        
                total_item = cart.Total_Quantity_in_Cart();
                ViewBag.QuantityCart = total_item;
                return PartialView("BagCart");
    
        }

        public ActionResult Shopping_success()
        {
            return View();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                HoaDon _order = new HoaDon();
                _order.Ngay = DateTime.Now;
                _order.Tk = "TinPham";
                _order.ThanhTien = int.Parse(form["TongTien"]);
                db.HoaDons.Add(_order);
                foreach(var item in cart.Items){
                    CT_HD _order_Detail = new CT_HD();
                    _order_Detail.MaHd = _order.MaHd;
                    _order_Detail.MaMa = item._shopping_product.MaMa;
                    _order_Detail.Sl = item._shopping_quantity;
                    db.CT_HD.Add(_order_Detail);
                }

                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Shopping_success", "GioHang");
            }
            catch
            {
                return Content("Error CheckOut, Please Check information....");
            }
        }
    }
}