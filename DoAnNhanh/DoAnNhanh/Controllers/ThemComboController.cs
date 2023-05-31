using DoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnNhanh.Controllers
{
    public class ThemComboController : Controller
    {
        private FoodWebNcEntities6 db = new FoodWebNcEntities6();
        // GET: ThemCombo
        public ActionResult ThanhToan()
        {
            var MonAn = db.MonAns.ToList();
            return View(MonAn.ToList());
        }
        public Cart GetCart()
        {
            Cart cart = Session["combo"] as Cart;
            if (cart == null || Session["combo"] == null)
            {
                cart = new Cart();
                Session["combo"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var pro = db.MonAns.SingleOrDefault(s => s.MaMa == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ThemCombo");
        }
        public ActionResult ShowToCart()
        {

            var MonAn = db.MonAns.ToList();
            ViewBag.DsMonAn = MonAn;
            /*            if (Session["Cart"] == null)
                            return RedirectToAction("ShowToCart", "GioHang");*/
            Cart cart = Session["combo"] as Cart;
            return View(cart);
        }

        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["combo"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ThemCombo");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["combo"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ThemCombo");
        }

        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["combo"] as Cart;
            if (cart != null)
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
            
                Cart cart = Session["combo"] as Cart;
                Combo _order = new Combo();

                _order.TenCb = form["TenCb"];
                    
                    if (cart.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(cart.ImageUpload.FileName);
                    string extension = Path.GetExtension(cart.ImageUpload.FileName);
                    fileName = fileName + extension;
                    _order.Hinh = "~/hinh/" + fileName;
                    cart.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/hinh/"), fileName));
                }

                _order.TongTien = int.Parse(form["TongTien"]);
                db.Comboes.Add(_order);
       
            foreach (var item in cart.Items)
                {
                    CT_Combo _order_Detail = new CT_Combo();
                    _order_Detail.Macb = _order.MaCb;
                    _order_Detail.MaMa = item._shopping_product.MaMa;
                    _order_Detail.sl = item._shopping_quantity;
                    db.CT_Combo.Add(_order_Detail);
                }

                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Index", "Comboes");
            
        }

    }
}