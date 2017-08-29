using MyModel.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTools;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll(true, true);
            return PartialView(model);
        }
        public ActionResult Category(long id)
        {
            var model = new ProductCategoryDao().GetById(id);
            return View(model);
        }
        public ActionResult ProductDetail(long id)
        {
            var dao = new ProductDao();
            var model = dao.GetById(id);
            if (model!=null)
            {
                ViewBag.RelatedProducts = dao.GetRelatedList(id, 4);
                ViewBag.ListImages = dao.GetById(id).MoreImages.GetListValueFromXmlString();
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
    }
}