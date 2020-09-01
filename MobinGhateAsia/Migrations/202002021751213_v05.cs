namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Certificates", "Order", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Certificates", "Order");
        }
    }
}
