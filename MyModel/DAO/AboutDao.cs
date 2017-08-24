using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class AboutDao
    {
        private OnlineShopDbContext db = null;
        public AboutDao()
        {
            db = new OnlineShopDbContext();
        }
        /// <summary>
        /// Hàm liệt kê toàn bộ about trong database
        /// </summary>
        /// <param name="onlyActive"></param>
        /// <returns></returns>
        public List<About> ListAll(bool onlyActive = false)
        {
            if (onlyActive)
            {
                return db.Abouts.Where(x => x.Status == true).ToList();
            }
            else
            {
                return db.Abouts.ToList();
            }
        }
        /// <summary>
        /// Hàm thêm mới một thực thể about
        /// </summary>
        /// <param name="about"></param>
        /// <returns></returns>
        public long Insert(About about)
        {
            try
            {
                db.Abouts.Add(about);
                db.SaveChanges();
                return about.ID;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        /// <summary>
        /// Hàm cập nhật about
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(About entity)
        {
            try
            {
                About about = db.Abouts.Find(entity.ID);
                about.Image = entity.Image;
                about.MetaDescription = entity.MetaDescription;
                about.MetaKeywords = entity.MetaKeywords;
                about.MetaTitle = entity.MetaTitle;
                about.Name = entity.Name;
                about.Status = entity.Status;
                about.ModifiedBy = entity.ModifiedBy;
                about.ModifiedDate = entity.ModifiedDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// Hàm xóa about
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                About about = db.Abouts.Find(id);
                db.Abouts.Remove(about);
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
