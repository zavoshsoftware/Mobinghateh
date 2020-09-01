namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v07 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CustomerGroupId", "dbo.CustomerGroups");
            DropIndex("dbo.Customers", new[] { "CustomerGroupId" });
            DropColumn("dbo.Customers", "Place");
            DropColumn("dbo.Customers", "Subject");
            DropColumn("dbo.Customers", "CustomerGroupId");
            DropTable("dbo.CustomerGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "CustomerGroupId", c => c.Guid(nullable: false));
            AddColumn("dbo.Customers", "Subject", c => c.String());
            AddColumn("dbo.Customers", "Place", c => c.String());
            CreateIndex("dbo.Customers", "CustomerGroupId");
            AddForeignKey("dbo.Customers", "CustomerGroupId", "dbo.CustomerGroups", "Id", cascadeDelete: true);
        }
    }
}
