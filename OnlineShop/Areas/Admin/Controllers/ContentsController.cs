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
    public class ContentsController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/Contents
        public ActionResult Index()
        {
            var contents = db.Contents.Include(c => c.Category);
            return View(contents.ToList());
        }

        // GET: Admin/Contents/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // GET: Admin/Contents/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Admin/Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Name,MetaTitle,Description,Image,CategoryID,Detail,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status,TopHot,ViewCount,Tagstring")] Content content)
        {
            if (ModelState.IsValid)
            {
                //Set CreatedBy, CreatedDate
                content.CreatedDate = DateTime.Now;
                UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
                content.CreatedBy = userLogin.UserName;

                //Set default values
                content.ViewCount = 0;

                //Add content
                new ContentDao().Insert(content);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", content.CategoryID);
            return View(content);
        }

        // GET: Admin/Contents/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", content.CategoryID);
            return View(content);
        }

        // POST: Admin/Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,Description,Image,CategoryID,Detail,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status,TopHot,ViewCount,Tagstring")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                //Set ModifiedBy, ModifiedDate
                content.ModifiedDate = DateTime.Now;
                UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
                content.ModifiedBy = userLogin.UserName;
                //Update entity
                if (new ContentDao().Update(content))
                {
                    SetAlert("Cập nhật thông tin bài viết thành công", AlertType.Success);
                    return RedirectToAction("Index", "Contents");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin bài viết thất bại!!!");
                }
                return View("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", content.CategoryID);
            return View(content);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (!new ContentDao().Delete(id))
            {
                ModelState.AddModelError("", "Xóa bài viết thất bại");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            try
            {
                Content content = db.Contents.Find(id);
                if (content.ChangeBoolValue("Status"))
                {
                    db.SaveChanges();
                    return Json(new
                    {
                        status = true,
                        value = content.Status,
                        message = "Thay đổi thuộc tính status thành công"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception("Thay đổi thuộc tính status thất bại");
                }

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
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
