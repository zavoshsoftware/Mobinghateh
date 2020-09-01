namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Summery = c.String(maxLength: 500),
                        Detail = c.String(storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 200),
                        VisitNumber = c.Int(nullable: false),
                        TitleEn = c.String(nullable: false, maxLength: 200),
                        SummeryEn = c.String(maxLength: 500),
                        DetailEn = c.String(storeType: "ntext"),
                        Order = c.Int(),
                        MetaDescription = c.String(maxLength: 320),
                        MetaDescriptionEn = c.String(maxLength: 320),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Careers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        CellNumber = c.String(nullable: false, maxLength: 25),
                        Email = c.String(maxLength: 300),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Catalogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        File = c.String(maxLength: 200),
                        Description = c.String(storeType: "ntext"),
                        IsEn = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Company = c.String(nullable: false, maxLength: 200),
                        CompanyEn = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false),
                        TitleEn = c.String(nullable: false),
                        ImageUrl = c.String(maxLength: 200),
                        Subject = c.String(nullable: false),
                        SubjectEn = c.String(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactusForms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Fullname = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Message = c.String(nullable: false, maxLength: 100),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Place = c.String(),
                        Subject = c.String(),
                        ImageUrl = c.String(),
                        CustomerGroupId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerGroups", t => t.CustomerGroupId, cascadeDelete: true)
                .Index(t => t.CustomerGroupId);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        ImageUrl = c.String(maxLength: 200),
                        TitleEn = c.String(maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        TitleEn = c.String(maxLength: 200),
                        ImageUrl = c.String(maxLength: 200),
                        GalleryId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.GalleryId, cascadeDelete: true)
                .Index(t => t.GalleryId);
            
            CreateTable(
                "dbo.NewsLetters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProducImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageUrl = c.String(maxLength: 200),
                        ProductId = c.Guid(nullable: false),
                        ProductImageTypeId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductImageTypes", t => t.ProductImageTypeId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProductImageTypeId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductGroupId = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        Summary = c.String(maxLength: 500),
                        Body = c.String(storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 200),
                        FlashImageUrl = c.String(maxLength: 200),
                        TitleEn = c.String(maxLength: 200),
                        SummaryEn = c.String(maxLength: 500),
                        BodyEn = c.String(storeType: "ntext"),
                        IsInHome = c.Boolean(nullable: false),
                        MetaDescription = c.String(maxLength: 320),
                        MetaDescriptionEn = c.String(maxLength: 320),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        TitleEn = c.String(maxLength: 200),
                        Description = c.String(maxLength: 4000),
                        DescriptionEn = c.String(maxLength: 2000),
                        Priority = c.Int(nullable: false),
                        ImageUrl = c.String(maxLength: 200),
                        MetaDescription = c.String(maxLength: 320),
                        MetaDescriptionEn = c.String(maxLength: 320),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductImageTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResumeForms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        NationalCode = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 15),
                        Email = c.String(maxLength: 260),
                        ResumeFile = c.String(maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LastLoginDate = c.DateTime(),
                        RoleId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Priority = c.Int(),
                        TitleEn = c.String(nullable: false, maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        TitleEn = c.String(maxLength: 200),
                        Summary = c.String(maxLength: 200),
                        SummaryEn = c.String(maxLength: 200),
                        LinkTitle = c.String(maxLength: 200),
                        LinkTitleEn = c.String(maxLength: 200),
                        LinkAddress = c.String(maxLength: 200),
                        ImageUrl = c.String(maxLength: 200),
                        Priority = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TextTypeItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        Summary1 = c.String(maxLength: 1000),
                        Summary2 = c.String(maxLength: 500),
                        Body = c.String(storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 200),
                        TextTypeId = c.Guid(nullable: false),
                        TitleEn = c.String(maxLength: 200),
                        Summary1En = c.String(maxLength: 200),
                        Summary2En = c.String(maxLength: 200),
                        BodyEn = c.String(storeType: "ntext"),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TextTypes", t => t.TextTypeId, cascadeDelete: true)
                .Index(t => t.TextTypeId);
            
            CreateTable(
                "dbo.TextTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextTypeItems", "TextTypeId", "dbo.TextTypes");
            DropForeignKey("dbo.ServiceItems", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ProducImages", "ProductImageTypeId", "dbo.ProductImageTypes");
            DropForeignKey("dbo.ProducImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.GalleryImages", "GalleryId", "dbo.Galleries");
            DropForeignKey("dbo.Customers", "CustomerGroupId", "dbo.CustomerGroups");
            DropIndex("dbo.TextTypeItems", new[] { "TextTypeId" });
            DropIndex("dbo.ServiceItems", new[] { "ServiceId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropIndex("dbo.ProducImages", new[] { "ProductImageTypeId" });
            DropIndex("dbo.ProducImages", new[] { "ProductId" });
            DropIndex("dbo.GalleryImages", new[] { "GalleryId" });
            DropIndex("dbo.Customers", new[] { "CustomerGroupId" });
            DropTable("dbo.TextTypes");
            DropTable("dbo.TextTypeItems");
            DropTable("dbo.Sliders");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceItems");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.ResumeForms");
            DropTable("dbo.ProductImageTypes");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.Products");
            DropTable("dbo.ProducImages");
            DropTable("dbo.NewsLetters");
            DropTable("dbo.GalleryImages");
            DropTable("dbo.Galleries");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerGroups");
            DropTable("dbo.ContactusForms");
            DropTable("dbo.Certificates");
            DropTable("dbo.Catalogs");
            DropTable("dbo.Careers");
            DropTable("dbo.Blogs");
        }
    }
}
