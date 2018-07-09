namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restructure6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageMenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seq = c.Int(nullable: false),
                        DisplayName = c.String(),
                        ParentMenuId = c.Int(nullable: false),
                        PageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pages", t => t.PageId)
                .ForeignKey("dbo.PageMenuItems", t => t.ParentMenuId)
                .Index(t => t.ParentMenuId)
                .Index(t => t.PageId);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Url = c.String(),
                        ContentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentAreas", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageMenuItems", "ParentMenuId", "dbo.PageMenuItems");
            DropForeignKey("dbo.PageMenuItems", "PageId", "dbo.Pages");
            DropForeignKey("dbo.Pages", "ContentId", "dbo.ContentAreas");
            DropIndex("dbo.Pages", new[] { "ContentId" });
            DropIndex("dbo.PageMenuItems", new[] { "PageId" });
            DropIndex("dbo.PageMenuItems", new[] { "ParentMenuId" });
            DropTable("dbo.Pages");
            DropTable("dbo.PageMenuItems");
        }
    }
}
