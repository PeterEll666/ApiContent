namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentAreaTypeUpd2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContentAreaTypes", "ContentAreaType_Id", "dbo.ContentAreaTypes");
            DropIndex("dbo.ContentAreaTypes", new[] { "ContentAreaType_Id" });
            CreateTable(
                "dbo.ContentAreaTypeLinks",
                c => new
                    {
                        ChildId = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChildId, t.ParentId })
                .ForeignKey("dbo.ContentAreaTypes", t => t.ChildId)
                .ForeignKey("dbo.ContentAreaTypes", t => t.ParentId)
                .Index(t => t.ChildId)
                .Index(t => t.ParentId);
            
            DropColumn("dbo.ContentAreaTypes", "ContentAreaType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContentAreaTypes", "ContentAreaType_Id", c => c.Int());
            DropForeignKey("dbo.ContentAreaTypeLinks", "ParentId", "dbo.ContentAreaTypes");
            DropForeignKey("dbo.ContentAreaTypeLinks", "ChildId", "dbo.ContentAreaTypes");
            DropIndex("dbo.ContentAreaTypeLinks", new[] { "ParentId" });
            DropIndex("dbo.ContentAreaTypeLinks", new[] { "ChildId" });
            DropTable("dbo.ContentAreaTypeLinks");
            CreateIndex("dbo.ContentAreaTypes", "ContentAreaType_Id");
            AddForeignKey("dbo.ContentAreaTypes", "ContentAreaType_Id", "dbo.ContentAreaTypes", "Id");
        }
    }
}
