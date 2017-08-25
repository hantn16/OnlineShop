using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class MenuTypeDao
    {
        private OnlineShopDbContext db = null;
        public MenuTypeDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<MenuType> ListAll()
        {
            return db.MenuTypes.ToList();
        }
        public int Insert(MenuType item)
        {
            try
            {
                db.MenuTypes.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        public bool Update(MenuType item)
        {
            try
            {
                var menuType = db.MenuTypes.Find(item.ID);
                menuType.Name = item.Name;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var menuType = db.MenuTypes.Find(id);
                db.MenuTypes.Remove(menuType);
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
