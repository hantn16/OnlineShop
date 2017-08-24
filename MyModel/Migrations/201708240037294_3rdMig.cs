namespace MyModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3rdMig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Content", "Detail", c => c.String(nullable: false, storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Content", "Detail", c => c.String(storeType: "ntext"));
        }
    }
}
