using Newtonsoft.Json;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MyTools;
using MyModel.EF;
using MyModel.DAO;

namespace OnlineShop.Controllers
{
    public class CartsController : Controller
    {
        // GET: Carts
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            List<CartItem> listItem = (List<CartItem>)cart;
            return View(listItem);
        }
        public JsonResult AddItem(long productID, long quantity)
        {
            try
            {
                var cart = Session[CommonConstants.CartSession];
                List<CartItem> listItem = new List<CartItem>();
                if (cart != null)
                {
                    listItem = (List<CartItem>)cart;
                }
                if (cart != null)
                {
                    CartItem item = listItem.SingleOrDefault(c => c.Product.ID == productID);
                    if (item != null)
                    {
                        item.Quantity += quantity;
                    }
                    else
                    {
                        listItem.Add(new CartItem(productID, quantity));
                    }
                }
                else
                {
                    var item = new CartItem(productID, quantity);
                    listItem.Add(item);
                }
                //Pass to Session
                Session[CommonConstants.CartSession] = listItem;
                //Return
                string tongsl = listItem.Sum(c => c.Quantity).ToString("N0");
                double tongtien = (double)listItem.Sum(c => c.Quantity * c.Product.Price.GetValueOrDefault(0));
                string tongtienstr = tongtien.ToString("N0");
                string tongtienbc = tongtien.DocSo();
                var listItemJsonString = JsonConvert.SerializeObject(listItem, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return Json(new
                {
                    status = true,
                    data = listItemJsonString,
                    tongtienbangchu = tongtienbc,
                    tongsl = tongsl,
                    tongtien = tongtienstr
                });
            }
            catch (Exception ex)
            {
                var text = ex.Message;
                return Json(new { status = false });
            }

        }
        [HttpPost]
        public JsonResult Delete(long productid)
        {
            try
            {
                var listItem = (List<CartItem>)Session[CommonConstants.CartSession];
                CartItem item = listItem.SingleOrDefault(c => c.Product.ID == productid);
                listItem.Remove(item);
                Session[CommonConstants.CartSession] = listItem;
                var listItemJsonString = JsonConvert.SerializeObject(listItem, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                decimal tongsoluong = 0;
                double tongtien = 0;
                string tongtienbc = "không đồng";
                if (listItem.Count != 0)
                {
                    tongsoluong = listItem.Sum(c => c.Quantity);
                    tongtien = (double)listItem.Sum(c => c.Quantity * c.Product.Price.GetValueOrDefault(0));
                    tongtienbc = tongtien.DocSo();
                }
                return Json(new
                {
                    status = true,
                    data = listItemJsonString,
                    tongtienbangchu = tongtienbc,
                    tongsl = tongsoluong.ToString("N0"),
                    tongtien = tongtien.ToString("N0")
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }

        }
        public class CartShortItem
        {
            public long productid { get; set; }
            public long quantity { get; set; }
        }
        [HttpPost]
        public JsonResult Update(string items)
        {
            try
            {
                List<CartShortItem> listitem = JsonConvert.DeserializeObject<List<CartShortItem>>(items);
                List<CartItem> fullListItem = new List<CartItem>();
                foreach (var item in listitem)
                {
                    fullListItem.Add(new CartItem(item.productid, item.quantity));
                }
                Session[CommonConstants.CartSession] = fullListItem;
                var listItemJsonString = JsonConvert.SerializeObject(fullListItem, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return Json(new
                {
                    status = true,
                    data = listItemJsonString
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }

        }
    }
}