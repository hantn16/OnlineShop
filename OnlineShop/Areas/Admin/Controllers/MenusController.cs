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
    public class MenusController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/Menus
        public ActionResult Index()
        {
            var model = new MenuDao().ListMenu();
            return View(model);
        }

        // GET: Admin/Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menus/Create
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.MenuTypes, "ID", "Name");
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Text,Link,DisplayOrder,Target,Status,TypeID")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (new MenuDao().Insert(menu)>0)
                {
                    SetAlert("Tạo mới menu thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CreateFailed", "Tạo mới menu thất bại");
                }                
            }

            ViewBag.TypeID = new SelectList(db.MenuTypes, "ID", "Name", menu.TypeID);
            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.MenuTypes, "ID", "Name", menu.TypeID);
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Text,Link,DisplayOrder,Target,Status,TypeID")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (new MenuDao().Update(menu))
                {
                    SetAlert("Cập nhật menu thành công", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UpdateFailed", "Cập nhật menu thất bại");
                }               
            }
            ViewBag.TypeID = new SelectList(db.MenuTypes, "ID", "Name", menu.TypeID);
            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (!new MenuDao().Delete(id))
            {
                ModelState.AddModelError("DeleteFailed", "Xóa menu thất bại");
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
