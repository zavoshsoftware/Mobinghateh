namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V09 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "IsInHome", c => c.Boolean(nullable: false));
            AddColumn("dbo.Services", "HomeSummery", c => c.String());
            AddColumn("dbo.Services", "HomeSummeryEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "HomeSummeryEn");
            DropColumn("dbo.Services", "HomeSummery");
            DropColumn("dbo.Services", "IsInHome");
        }
    }
}
