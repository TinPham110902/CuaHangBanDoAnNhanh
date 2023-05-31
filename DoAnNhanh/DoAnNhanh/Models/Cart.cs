using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnNhanh.Models
{
    public class Cartitem
    {
        public MonAn _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }
      

     
    }


    public class Cart
    {
        List<Cartitem> items = new List<Cartitem>();
        public IEnumerable<Cartitem> Items
        {
            get { return items; }
        }
        public Cart() {
            Hinh = "~/hinh/Add.png";
        }
        public string Hinh { get; set; }
        public void Add(MonAn _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.MaMa == _pro.MaMa);
                if(item==null)
            {
                items.Add(new Cartitem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });

            } else
            {
                item._shopping_quantity += _quantity;
            }    
        }

        public void Update_Quantity_Shopping(int id, int _quantity)
        {
            var item = items.Find(s=>s._shopping_product.MaMa==id);
            if(item!=null)
            {
                item._shopping_quantity = _quantity;
            }
        }

        public double Total_Money()
        {
            var total = items.Sum(s => s._shopping_product.Gia * s._shopping_quantity);
            return total;
        }

        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._shopping_product.MaMa == id);
        }

        public int Total_Quantity_in_Cart()
        {
            return items.Sum(s => s._shopping_quantity);
        }

        public void ClearCart()
        {
            items.Clear();
        }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}