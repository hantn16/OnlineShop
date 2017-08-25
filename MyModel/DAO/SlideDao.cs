using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class SlideDao
    {
        OnlineShopDbContext db = null;
        public SlideDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Slide> ListAll(bool onlyActive = false,bool sort = true)
        {
            try
            {
                List<Slide> listSlide = db.Slides.ToList();
                if (onlyActive) { listSlide = listSlide.Where(x => x.Status == true).ToList(); }
                if (sort) { listSlide = listSlide.OrderBy(x => x.DisplayOrder).ToList(); }
                return listSlide;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public long Insert(Slide item)
        {
            try
            {
                db.Slides.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch (Exception)
            {
                return -1;
            }
            
        }
        public bool Update(Slide item)
        {
            try
            {
                var slide = db.Slides.Find(item.ID);
                slide.Description = item.Description;
                slide.DisplayOrder = item.DisplayOrder;
                slide.Image = item.Image;
                slide.Link = item.Link;
                slide.ModifiedBy = item.ModifiedBy;
                slide.ModifiedDate = item.ModifiedDate;
                slide.Status = item.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
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
