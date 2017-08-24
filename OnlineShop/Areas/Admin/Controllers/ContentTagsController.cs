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
    public class ContentTagsController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/ContentTags
        public ActionResult Index()
        {
            var contentTags = db.ContentTags.Include(c => c.Content).Include(c => c.Tag);
            return View(contentTags.ToList());
        }

        // GET: Admin/ContentTags/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentTag contentTag = db.ContentTags.Find(id);
            if (contentTag == null)
            {
                return HttpNotFound();
            }
            return View(contentTag);
        }

        // GET: Admin/ContentTags/Create
        public ActionResult Create()
        {
            ViewBag.ContentID = new SelectList(db.Contents, "ID", "Name");
            ViewBag.TagID = new SelectList(db.Tags, "ID", "Name");
            return View();
        }

        // POST: Admin/ContentTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContentID,TagID")] ContentTag contentTag)
        {
            if (ModelState.IsValid)
            {
                db.ContentTags.Add(contentTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContentID = new SelectList(db.Contents, "ID", "Name", contentTag.ContentID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "Name", contentTag.TagID);
            return View(contentTag);
        }

        // GET: Admin/ContentTags/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentTag contentTag = db.ContentTags.Find(id);
            if (contentTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContentID = new SelectList(db.Contents, "ID", "Name", contentTag.ContentID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "Name", contentTag.TagID);
            return View(contentTag);
        }

        // POST: Admin/ContentTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContentID,TagID")] ContentTag contentTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contentTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContentID = new SelectList(db.Contents, "ID", "Name", contentTag.ContentID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "Name", contentTag.TagID);
            return View(contentTag);
        }

        // GET: Admin/ContentTags/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentTag contentTag = db.ContentTags.Find(id);
            if (contentTag == null)
            {
                return HttpNotFound();
            }
            return View(contentTag);
        }

        // POST: Admin/ContentTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ContentTag contentTag = db.ContentTags.Find(id);
            db.ContentTags.Remove(contentTag);
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
