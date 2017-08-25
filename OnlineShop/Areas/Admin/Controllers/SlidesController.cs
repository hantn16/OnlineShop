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
    public class SlidesController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/Slides
        public ActionResult Index()
        {
            var model = new SlideDao().ListAll();
            return View(model);
        }

        // GET: Admin/Slides/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Admin/Slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Image,DisplayOrder,Link,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,Status")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                //Cập nhật thời gian và người tạo
                slide.CreatedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                slide.CreatedBy = session.UserName;
                if(new SlideDao().Insert(slide) > 0)
                {
                    SetAlert("Thêm mới slide thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CreateFailed", "Thêm mới slide thất bại");
                }                
            }
            return View(slide);
        }

        // GET: Admin/Slides/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Image,DisplayOrder,Link,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,Status")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                //Cập nhật thời gian và người sửa
                slide.ModifiedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                slide.ModifiedBy = session.UserName;
                if(new SlideDao().Update(slide))
                {
                    SetAlert("Cập nhật slide thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UpdateFailed", "Cập nhật slide thất bại");
                }                
            }
            return View(slide);
        }

        // GET: Admin/Slides/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Delete/5
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            if(new SlideDao().Delete(id)) { SetAlert("Xóa slide thành công", AlertType.Success); } else { ModelState.AddModelError("DeleteFailed", "Xóa slide thất bại"); }
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
