using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class MenuDao
    {
        private OnlineShopDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopDbContext();
        }
        
        public int Insert(Menu item)
        {
            try
            {
                db.Menus.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        public bool Update(Menu item)
        {
            try
            {
                var menu = db.Menus.Find(item.ID);
                menu.Link = item.Link;
                menu.Text = item.Text;
                menu.Target = item.Target;
                menu.TypeID = item.TypeID;
                menu.Status = item.Status;
                menu.DisplayOrder = item.DisplayOrder;
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
                var menu = db.Menus.Find(id);
                db.Menus.Remove(menu);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Hàm lấy ra danh sách menu
        /// </summary>
        /// <param name="menuTypeID"> id của kiểu menu type, mặc định là -1:không lấy theo idtype</param>
        /// <param name="onlyActive"> true nếu chỉ chọn menu đang kích hoạt</param>
        /// <param name="sortDisplayOrder"> 1: sắp xếp tăng dần,-1:giảm dần, 0: không sắp xếp</param>
        /// <returns></returns>
        public List<Menu> ListMenu(int menuTypeID = -1,bool onlyActive = true,int sortDisplayOrder = 1)
        {
            try
            {
                List<Menu> listMenu = db.Menus.ToList();
                if (menuTypeID != -1)
                {
                    listMenu = listMenu.Where(x => x.TypeID == menuTypeID).ToList();
                }
                if (onlyActive)
                {
                    listMenu = listMenu.Where(x => x.Status == true).ToList();
                }
                if (sortDisplayOrder == 1)
                {
                    return listMenu.OrderBy(x => x.DisplayOrder).ToList();
                }
                else if (sortDisplayOrder == -1)
                {
                    return listMenu.OrderByDescending(x => x.DisplayOrder).ToList();
                }
                else
                {
                    return listMenu;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
