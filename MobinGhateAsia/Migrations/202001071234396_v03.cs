namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v03 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Certificates", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Certificates", "Date", c => c.DateTime(nullable: false));
        }
    }
}
