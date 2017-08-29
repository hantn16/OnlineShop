using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyModel.EF;
using MyModel.DAO;
using OnlineShop.Common;
using MyTools;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategory);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Code,Name,MetaTitle,Description,Image,MoreImages,Price,PromotionPrice,IncludeVAT,Quantity,CategoryID,Detail,Waranty,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status,TopHot,ViewCount")] Product product)
        {
            if (ModelState.IsValid)
            {
                //Cập nhật thời gian và người tạo
                product.CreatedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.CreatedBy = session.UserName;
                //Set gía trị mặc định
                product.ViewCount = 0;
                product.MetaTitle = product.Name.ToLower().ConvertToUnSign();
                if(new ProductDao().Insert(product) > 0)
                {
                    SetAlert("Tạo mới sản phẩm thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CreateFailed", "Tạo mới sản phẩm thất bại");
                }               
            }

            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Code,Name,MetaTitle,Description,Image,MoreImages,Price,PromotionPrice,IncludeVAT,Quantity,CategoryID,Detail,Waranty,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status,TopHot,ViewCount")] Product product)
        {
            if (ModelState.IsValid)
            {
                //Cập nhật ngày sửa và người sửa
                product.ModifiedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.ModifiedBy = session.UserName;
                //Set gía trị mặc định
                product.MetaTitle = product.Name.ToLower().ConvertToUnSign();
                if (new ProductDao().Update(product))
                {
                    SetAlert("Cập nhật sản phẩm thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UpdateFailed", "Cập nhật sản phẩm thất bại");
                }               
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            if (!new ProductDao().Delete(id))
            {
                ModelState.AddModelError("DeleteFailed","Xóa sản phẩm thất bại");
            }
            return RedirectToAction("Index");
        }
        public JsonResult SaveImages(long id,string images)
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            var listImage = serial.Deserialize<List<string>>(images);
            XElement xElement = new XElement("Images");
            foreach(var item in listImage)
            {
                string kq = item.Substring(item.IndexOf("/", 7));
                xElement.Add(new XElement("Image", kq));
            }
            ProductDao dao = new ProductDao();
            try
            {
                dao.UpdateImages(id, xElement.ToString());
                return Json(new { status = true});
            }
            catch (Exception)
            {

                return Json(new { status = false });
            }

            
        }
        public JsonResult LoadImages(long id)
        {
            try
            {
                Product product = db.Products.Find(id);
                var images = product.MoreImages;
                List<string> listImage = images.GetListValueFromXmlString();
                return Json(new { data = listImage,status = true},JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { status = false},JsonRequestBehavior.AllowGet); ;
            }

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
