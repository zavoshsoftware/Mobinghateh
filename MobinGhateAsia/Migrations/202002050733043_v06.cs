namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "HeaderImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "HeaderImageUrl");
        }
    }
}
