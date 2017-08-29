using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }
        /// <summary>
        /// Hàm detete 1 product có id truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                Product item = db.Products.Find(id);
                db.Products.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Product GetById(long id)
        {
            return db.Products.Find(id);
        }

        public List<Product> GetList(bool onlyActive = false)
        {
            try
            {
                var listProduct = db.Products.ToList();
                if (onlyActive) { listProduct = listProduct.Where(x => x.Status == true).ToList(); }
                return listProduct;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public enum ProductFeature
        {
            TopHot = 1,
            New = 2,
            Promoted  = 3,
            TopView = 4
        }
        /// <summary>
        /// Hàm lấy list sản phẩm theo tiêu chí
        /// </summary>
        /// <param name="feature">Tiêu chí lấy sản phẩm</param>
        /// <param name="top"> số lượng cần lấy</param>
        /// <returns></returns>
        public List<Product> GetFeaturedList(ProductFeature feature,int top)
        {
            var listPro = GetList(true);
            switch (feature)
            {
                case ProductFeature.TopHot:
                    return listPro.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).Take(top).ToList();
                case ProductFeature.New:
                    return listPro.OrderByDescending(x=>x.CreatedDate).Take(top).ToList();
                case ProductFeature.Promoted:
                    return listPro.Where(x => x.PromotionPrice != null&&x.Price!=null).OrderByDescending(x=>(1-x.PromotionPrice/x.Price)).Take(top).ToList();
                case ProductFeature.TopView:
                    return listPro.Where(x => x.ViewCount != null).OrderByDescending(x=>x.ViewCount).Take(top).ToList();
                default:
                    return listPro.Take(top).ToList();
            }
        }
        /// <summary>
        /// Hàm lấy tất cả sản phẩm cùng danh mục với sản phẩm có id truyền vào
        /// </summary>
        /// <param name="productId">ID của sản phẩm truyền vào</param>
        /// <returns></returns>
        public List<Product> GetRelatedList(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }
        /// <summary>
        /// Hàm lấy top n sản phẩm cùng danh mục với sản phẩm có id truyền vào
        /// </summary>
        /// <param name="productId">ID của sản phẩm truyền vào</param>
        /// <param name="top">số lượng sản phẩm lấy ra</param>
        /// <returns></returns>
        public List<Product> GetRelatedList(long productId,int top)
        {
            return GetRelatedList(productId).Take(top).ToList();
        }
        public long Insert(Product item)
        {
            try
            {
                db.Products.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public bool Update(Product item)
        {
            try
            {
                var product = db.Products.Find(item.ID);
                product.CategoryID = item.CategoryID;
                product.Code = item.Code;
                product.Description = item.Description;
                product.Detail = item.Detail;
                product.Image = item.Image;
                product.IncludeVAT = item.IncludeVAT;
                product.MetaDescription = item.MetaDescription;
                product.MetaKeywords = item.MetaKeywords;
                product.MetaTitle = item.MetaTitle;
                product.ModifiedBy = item.ModifiedBy;
                product.ModifiedDate = item.ModifiedDate;
                product.MoreImages = item.MoreImages;
                product.Name = item.Name;
                product.Price = item.Price;
                product.PromotionPrice = item.PromotionPrice;
                product.Quantity = item.Quantity;
                product.Status = item.Status;
                product.TopHot = item.TopHot;
                product.Waranty = item.Waranty;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void UpdateImages(long id,string images)
        {
            Product product = db.Products.Find(id);
            product.MoreImages = images;
            db.SaveChanges();
        }
    }
}
