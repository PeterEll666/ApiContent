namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentAreaType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentAreaTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ParentAreaId = c.Int(nullable: false),
                        TemplateHtml = c.String(),
                        ContentAreaType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentAreaTypes", t => t.ContentAreaType_Id)
                .Index(t => t.ContentAreaType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContentAreaTypes", "ContentAreaType_Id", "dbo.ContentAreaTypes");
            DropIndex("dbo.ContentAreaTypes", new[] { "ContentAreaType_Id" });
            DropTable("dbo.ContentAreaTypes");
        }
    }
}
