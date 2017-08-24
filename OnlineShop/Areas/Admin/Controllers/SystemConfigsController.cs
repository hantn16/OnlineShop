using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyModel.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class SystemConfigsController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/SystemConfigs
        public ActionResult Index()
        {
            return View(db.SystemConfigs.ToList());
        }

        // GET: Admin/SystemConfigs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemConfig systemConfig = db.SystemConfigs.Find(id);
            if (systemConfig == null)
            {
                return HttpNotFound();
            }
            return View(systemConfig);
        }

        // GET: Admin/SystemConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SystemConfigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Type,Value,Status")] SystemConfig systemConfig)
        {
            if (ModelState.IsValid)
            {
                db.SystemConfigs.Add(systemConfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemConfig);
        }

        // GET: Admin/SystemConfigs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemConfig systemConfig = db.SystemConfigs.Find(id);
            if (systemConfig == null)
            {
                return HttpNotFound();
            }
            return View(systemConfig);
        }

        // POST: Admin/SystemConfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Type,Value,Status")] SystemConfig systemConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemConfig);
        }

        // GET: Admin/SystemConfigs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemConfig systemConfig = db.SystemConfigs.Find(id);
            if (systemConfig == null)
            {
                return HttpNotFound();
            }
            return View(systemConfig);
        }

        // POST: Admin/SystemConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SystemConfig systemConfig = db.SystemConfigs.Find(id);
            db.SystemConfigs.Remove(systemConfig);
            db.SaveChanges();
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
