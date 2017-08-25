using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<ProductCategory> ListAll(bool onlyActive = false,bool sort = false)
        {
            try
            {
                var listCategory = db.ProductCategories.ToList(); ;
                if (onlyActive)
                {
                    listCategory = listCategory.Where(c => c.Status == true).ToList();
                }
                if (sort)
                {
                    listCategory = listCategory.OrderBy(c => c.DisplayOrder).ToList();
                }
                return listCategory;
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// Hàm xóa một ProductCategory có id truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                ProductCategory item = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public long Insert(ProductCategory item)
        {
            try
            {
                db.ProductCategories.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        public bool Update(ProductCategory item)
        {
            try
            {
                var cat = db.ProductCategories.Find(item.ID);
                cat.ModifiedBy = item.ModifiedBy;
                cat.ModifiedDate = item.ModifiedDate;
                cat.Name = cat.Name;
                cat.DisplayOrder = item.DisplayOrder;
                cat.MetaDescription = item.MetaDescription;
                cat.MetaKeywords = item.MetaKeywords;
                cat.MetaTitle = item.MetaTitle;
                cat.ParentID = cat.ParentID;
                cat.SeoTitle = cat.SeoTitle;
                cat.ShowOnHome = cat.ShowOnHome;
                cat.Status = cat.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
