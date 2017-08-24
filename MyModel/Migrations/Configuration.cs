namespace MyModel.Migrations
{
    using MyModel.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MyTools;

    internal sealed class Configuration : DbMigrationsConfiguration<MyModel.EF.OnlineShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyModel.EF.OnlineShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Users.AddOrUpdate(x => x.ID, new User() {
                ID = 1,UserName="admin",PassWord = "adminanhhan".Encrypt2MD5(),Name= "Trịnh Ngọc Hân",Status=true
            });
        }
    }
}
