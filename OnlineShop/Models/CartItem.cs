using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [Serializable]
    public class CartItem
    {   
        private OnlineShopDbContext db = new OnlineShopDbContext();
        public Product Product { get; set; }
        public long Quantity { get; set; }

        //Constructor function
        public CartItem(Product product=null,long quantity = 1)
        {
            Product = product;Quantity = quantity;
        }
        public CartItem(long productID,long quantity = 1)
        {
            var product = db.Products.Find(productID);
            Product = product;Quantity = quantity;
        }
    }
}