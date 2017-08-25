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

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductCategoriesController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/ProductCategories
        public ActionResult Index()
        {
            var model = new ProductCategoryDao().ListAll(sort:true);
            return View(model);
        }

        // GET: Admin/ProductCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.ProductCategories, "ID", "Name");
            return View();
        }

        // POST: Admin/ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Name,MetaTitle,ParentID,DisplayOrder,SeoTitle,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status,ShowOnHome")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                //Set CreateBy, CreateDate
                productCategory.CreatedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                productCategory.CreatedBy = session.UserName;
                //Update MetaTitle theo Name
                productCategory.MetaTitle = productCategory.Name.ToLower().ConvertToUnSign();
                if (new ProductCategoryDao().Insert(productCategory)>0)
                {
                    SetAlert("Tạo mới danh mục sản phẩm thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CreateFailed", "Tạo mới danh mục sản phẩm thất bại");
                }               
            }

            ViewBag.ParentID = new SelectList(db.ProductCategories, "ID", "Name", productCategory.ParentID);
            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.ProductCategories, "ID", "Name", productCategory.ParentID);
            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,ParentID,DisplayOrder,SeoTitle,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status,ShowOnHome")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                //Set ModifiedBy, ModifiedDate
                productCategory.ModifiedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                productCategory.ModifiedBy = session.UserName;
                //Update MetaTitle theo Name
                productCategory.MetaTitle = productCategory.Name.ToLower().ConvertToUnSign();
                if (new ProductCategoryDao().Update(productCategory))
                {
                    SetAlert("Cập nhật danh mục sản phẩm thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UpdateFailed", "Cập nhật danh mục sản phẩm thất bại");
                }
                
            }
            ViewBag.ParentID = new SelectList(db.ProductCategories, "ID", "Name", productCategory.ParentID);
            return View(productCategory);
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            if (new ProductCategoryDao().Delete(id))
            {
                ModelState.AddModelError("DeleteFailed", "Xóa danh mục sản phẩm thất bại");
            }
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
