namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentAreaTypeUpd1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContentAreaTypes", "ParentAreaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContentAreaTypes", "ParentAreaId", c => c.Int(nullable: false));
        }
    }
}
