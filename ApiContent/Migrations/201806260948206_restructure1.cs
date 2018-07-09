namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restructure1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContentAreaTypeLinks", "ChildId", "dbo.ContentAreaTypes");
            DropForeignKey("dbo.ContentAreaTypeLinks", "ParentId", "dbo.ContentAreaTypes");
            DropIndex("dbo.ContentAreaTypeLinks", new[] { "ChildId" });
            DropIndex("dbo.ContentAreaTypeLinks", new[] { "ParentId" });
            CreateTable(
                "dbo.ContentAreaLibraryItems",
                c => new
                    {
                        LibraryName = c.String(nullable: false, maxLength: 128),
                        Child_Id = c.Int(),
                    })
                .PrimaryKey(t => t.LibraryName)
                .ForeignKey("dbo.ContentAreas", t => t.Child_Id)
                .Index(t => t.Child_Id);
            
            CreateTable(
                "dbo.ContentAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Html = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContentAreaTexts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Language = c.String(),
                        Text = c.String(),
                        TextType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ContentAreaTypes");
            DropTable("dbo.ContentAreaTypeLinks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContentAreaTypeLinks",
                c => new
                    {
                        ChildId = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChildId, t.ParentId });
            
            CreateTable(
                "dbo.ContentAreaTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TemplateHtml = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ContentAreaLibraryItems", "Child_Id", "dbo.ContentAreas");
            DropIndex("dbo.ContentAreaLibraryItems", new[] { "Child_Id" });
            DropTable("dbo.ContentAreaTexts");
            DropTable("dbo.ContentAreas");
            DropTable("dbo.ContentAreaLibraryItems");
            CreateIndex("dbo.ContentAreaTypeLinks", "ParentId");
            CreateIndex("dbo.ContentAreaTypeLinks", "ChildId");
            AddForeignKey("dbo.ContentAreaTypeLinks", "ParentId", "dbo.ContentAreaTypes", "Id");
            AddForeignKey("dbo.ContentAreaTypeLinks", "ChildId", "dbo.ContentAreaTypes", "Id");
        }
    }
}
