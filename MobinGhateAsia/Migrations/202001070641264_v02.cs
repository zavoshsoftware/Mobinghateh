namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceItems", "ServiceId", "dbo.Services");
            DropIndex("dbo.ServiceItems", new[] { "ServiceId" });
            AddColumn("dbo.Services", "Body", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Services", "BodyEn", c => c.String(storeType: "ntext"));
            DropTable("dbo.ServiceItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, storeType: "ntext"),
                        ServiceId = c.Guid(nullable: false),
                        Priority = c.Int(),
                        TitleEn = c.String(nullable: false, storeType: "ntext"),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Services", "BodyEn");
            DropColumn("dbo.Services", "Body");
            CreateIndex("dbo.ServiceItems", "ServiceId");
            AddForeignKey("dbo.ServiceItems", "ServiceId", "dbo.Services", "Id", cascadeDelete: true);
        }
    }
}
