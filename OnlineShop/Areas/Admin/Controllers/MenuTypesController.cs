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

namespace OnlineShop.Areas.Admin.Controllers
{
    public class MenuTypesController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/MenuTypes
        public ActionResult Index()
        {
            var model = new MenuTypeDao().ListAll();
            return View(model);
        }

        // GET: Admin/MenuTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuType menuType = db.MenuTypes.Find(id);
            if (menuType == null)
            {
                return HttpNotFound();
            }
            return View(menuType);
        }

        // GET: Admin/MenuTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MenuTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] MenuType menuType)
        {
            if (ModelState.IsValid)
            {
                if (new MenuTypeDao().Insert(menuType)>0)
                {
                    SetAlert("Thêm mới kiểu menu thành công", AlertType.Success);
                }
                else
                {
                    ModelState.AddModelError("CreateFailed", "Thêm mới kiểu menu thất bại");
                }
                return RedirectToAction("Index");
            }

            return View(menuType);
        }

        // GET: Admin/MenuTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuType menuType = db.MenuTypes.Find(id);
            if (menuType == null)
            {
                return HttpNotFound();
            }
            return View(menuType);
        }

        // POST: Admin/MenuTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] MenuType menuType)
        {
            if (ModelState.IsValid)
            {
                if (!new MenuTypeDao().Update(menuType))
                {
                    ModelState.AddModelError("UpdateFailed", "Cập nhật thông tin kiểu menu thất bại");
                }
                return RedirectToAction("Index");
            }
            return View(menuType);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (!new MenuTypeDao().Delete(id))
            {
                ModelState.AddModelError("DeleteFailed", "Xóa kiểu menu thất bại");
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
