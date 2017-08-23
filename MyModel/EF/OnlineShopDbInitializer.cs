using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTools;

namespace MyModel.EF
{
    public class OnlineShopDbInitializer : DropCreateDatabaseIfModelChanges<OnlineShopDbContext>
    {
        protected override void Seed(OnlineShopDbContext context)
        {
            string pwMD5 = "adminanhhan".Encrypt2MD5();
            string pwMD52 = "user01anhhan".Encrypt2MD5();
            var admin = new User()
            {
                UserName = "admin",
                PassWord = pwMD5,
                Name = "Trịnh Ngọc Hân",
                CreatedDate = DateTime.Now,
                Status = true
            };
            context.Users.Add(admin);
            var user01 = new User()
            {
                UserName = "user01",
                PassWord = pwMD52,
                Name = "user01",
                CreatedDate = DateTime.Now,
                Status = true
            };
            context.Users.Add(user01);
            context.SaveChanges();
        }
    }
}
