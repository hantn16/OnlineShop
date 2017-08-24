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

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AboutsController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/Abouts
        public ActionResult Index()
        {
            var model = new AboutDao().ListAll();
            return View(model);
        }

        // GET: Admin/Abouts/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // GET: Admin/Abouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Abouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Name,MetaTitle,Description,Image,Detail,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status")] About about)
        {
            if (ModelState.IsValid)
            {
                //Set CreatedBy, CreatedDate
                about.CreatedDate = DateTime.Now;
                UserLogin userlogin = (UserLogin)Session[CommonConstants.USER_SESSION];
                about.CreatedBy = userlogin.UserName;
                new AboutDao().Insert(about);
                return RedirectToAction("Index");
            }

            return View(about);
        }

        // GET: Admin/Abouts/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Admin/Abouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,Description,Image,Detail,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status")] About about)
        {
            if (ModelState.IsValid)
            {
                if (new AboutDao().Update(about))
                {
                    SetAlert("Cập nhật thông tin Abouts thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin Abouts thất bại");
                }
            }
            return View("Index");
        }
   
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            if (!new AboutDao().Delete(id))
            {
                ModelState.AddModelError("DeleteFailed", "Xóa thất bại!!!");
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
