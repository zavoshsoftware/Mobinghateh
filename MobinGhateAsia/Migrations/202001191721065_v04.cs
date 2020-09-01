namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductGroups", "ParentId", c => c.Guid());
            CreateIndex("dbo.ProductGroups", "ParentId");
            AddForeignKey("dbo.ProductGroups", "ParentId", "dbo.ProductGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductGroups", "ParentId", "dbo.ProductGroups");
            DropIndex("dbo.ProductGroups", new[] { "ParentId" });
            DropColumn("dbo.ProductGroups", "ParentId");
        }
    }
}
