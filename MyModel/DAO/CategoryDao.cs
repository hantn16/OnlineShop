using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public bool Delete(long? id)
        {
            try
            {
                Category category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public long Insert(Category item)
        {
            try
            {
                db.Categories.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        public bool Update(Category item)
        {
            try
            {
                Category category = db.Categories.Find(item.ID);
                category.Name = item.Name;
                category.MetaTitle = item.MetaTitle;
                category.ModifiedBy = item.ModifiedBy;
                category.ModifiedDate = item.ModifiedDate;
                category.ParentID = item.ParentID;
                category.ShowOnHome = item.ShowOnHome;
                category.Status = item.Status;
                category.Contents = item.Contents;
                category.DisplayOrder = item.DisplayOrder;
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
