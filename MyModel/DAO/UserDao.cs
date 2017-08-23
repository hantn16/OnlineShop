using MyModel.EF;
using MyTools;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class UserDao
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(User entity)
        {
            try
            {
                User user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Status = entity.Status;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = entity.ModifiedDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int LogIn(string userName, string passWord)
        {
            string userpwMD5 = (userName + passWord).Encrypt2MD5();
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null) { return 0; }
            else
            {
                if (result.Status == false) return -1;
                else
                {
                    if (result.PassWord == userpwMD5) return 1; else return -2;
                }
            }
        }
        public User GetByUserName(string username)
        {
            return db.Users.SingleOrDefault(c => c.UserName == username);
        }
        public User GetById(int id) => db.Users.Find(id);
        public IPagedList<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderBy(c => c.ID).ToPagedList(page, pageSize);
        }
        public IEnumerable<User> ListAll()
        {
            return db.Users;
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
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
