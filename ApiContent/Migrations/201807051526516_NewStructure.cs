namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewStructure : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ContentAreas", newName: "Contents");
            DropForeignKey("dbo.ContentAreaItems", "ContentAreaId", "dbo.ContentAreas");
            DropForeignKey("dbo.ContentAreaItems", "ContentTextId", "dbo.ContentTexts");
            DropForeignKey("dbo.ContentAreaItems", "ParentContentAreaId", "dbo.ContentAreas");
            DropForeignKey("dbo.ContentAreaLibraryItems", "ContentAreaId", "dbo.ContentAreas");
            DropForeignKey("dbo.ContentTextLangs", "Id", "dbo.ContentTexts");
            DropForeignKey("dbo.Pages", "ContentId", "dbo.ContentAreas");
            DropIndex("dbo.ContentAreaItems", new[] { "ParentContentAreaId" });
            DropIndex("dbo.ContentAreaItems", new[] { "ContentAreaId" });
            DropIndex("dbo.ContentAreaItems", new[] { "ContentTextId" });
            DropIndex("dbo.ContentAreaLibraryItems", new[] { "ContentAreaId" });
            DropIndex("dbo.ContentTextLangs", new[] { "Id" });
            DropIndex("dbo.Pages", new[] { "ContentId" });
            CreateTable(
                "dbo.ContentLibraryItems",
                c => new
                    {
                        LibraryName = c.String(nullable: false, maxLength: 128),
                        ContentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LibraryName, t.ContentId })
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.Texts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TextType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PageContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seq = c.Int(nullable: false),
                        ParentPageId = c.Int(nullable: false),
                        ContentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .ForeignKey("dbo.Pages", t => t.ParentPageId, cascadeDelete: true)
                .Index(t => t.ParentPageId)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.PageTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Html = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TextLangs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Language = c.String(nullable: false, maxLength: 128),
                        Html = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.Language })
                .ForeignKey("dbo.Texts", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            AddColumn("dbo.Contents", "Type", c => c.String());
            AddColumn("dbo.ContentTexts", "Seq", c => c.Int(nullable: false));
            AddColumn("dbo.ContentTexts", "ParentContentId", c => c.Int(nullable: false));
            AddColumn("dbo.ContentTexts", "ContentTextId", c => c.Int(nullable: false));
            AddColumn("dbo.Pages", "TemplateId", c => c.Int(nullable: false));
            CreateIndex("dbo.ContentTexts", "ParentContentId");
            CreateIndex("dbo.ContentTexts", "ContentTextId");
            CreateIndex("dbo.Pages", "TemplateId");
            AddForeignKey("dbo.ContentTexts", "ParentContentId", "dbo.Contents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContentTexts", "ContentTextId", "dbo.Texts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Pages", "TemplateId", "dbo.PageTemplates", "Id", cascadeDelete: true);
            DropColumn("dbo.ContentTexts", "Name");
            DropColumn("dbo.ContentTexts", "TextType");
            DropColumn("dbo.Pages", "ContentId");
            DropTable("dbo.ContentAreaItems");
            DropTable("dbo.ContentAreaLibraryItems");
            DropTable("dbo.ContentTextLangs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContentTextLangs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Language = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.Language });
            
            CreateTable(
                "dbo.ContentAreaLibraryItems",
                c => new
                    {
                        LibraryName = c.String(nullable: false, maxLength: 128),
                        ContentAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LibraryName, t.ContentAreaId });
            
            CreateTable(
                "dbo.ContentAreaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seq = c.Int(nullable: false),
                        ParentContentAreaId = c.Int(nullable: false),
                        ContentAreaId = c.Int(),
                        ContentTextId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Pages", "ContentId", c => c.Int(nullable: false));
            AddColumn("dbo.ContentTexts", "TextType", c => c.String());
            AddColumn("dbo.ContentTexts", "Name", c => c.String());
            DropForeignKey("dbo.TextLangs", "Id", "dbo.Texts");
            DropForeignKey("dbo.PageContents", "ParentPageId", "dbo.Pages");
            DropForeignKey("dbo.Pages", "TemplateId", "dbo.PageTemplates");
            DropForeignKey("dbo.PageContents", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.ContentTexts", "ContentTextId", "dbo.Texts");
            DropForeignKey("dbo.ContentTexts", "ParentContentId", "dbo.Contents");
            DropForeignKey("dbo.ContentLibraryItems", "ContentId", "dbo.Contents");
            DropIndex("dbo.TextLangs", new[] { "Id" });
            DropIndex("dbo.Pages", new[] { "TemplateId" });
            DropIndex("dbo.PageContents", new[] { "ContentId" });
            DropIndex("dbo.PageContents", new[] { "ParentPageId" });
            DropIndex("dbo.ContentTexts", new[] { "ContentTextId" });
            DropIndex("dbo.ContentTexts", new[] { "ParentContentId" });
            DropIndex("dbo.ContentLibraryItems", new[] { "ContentId" });
            DropColumn("dbo.Pages", "TemplateId");
            DropColumn("dbo.ContentTexts", "ContentTextId");
            DropColumn("dbo.ContentTexts", "ParentContentId");
            DropColumn("dbo.ContentTexts", "Seq");
            DropColumn("dbo.Contents", "Type");
            DropTable("dbo.TextLangs");
            DropTable("dbo.PageTemplates");
            DropTable("dbo.PageContents");
            DropTable("dbo.Texts");
            DropTable("dbo.ContentLibraryItems");
            CreateIndex("dbo.Pages", "ContentId");
            CreateIndex("dbo.ContentTextLangs", "Id");
            CreateIndex("dbo.ContentAreaLibraryItems", "ContentAreaId");
            CreateIndex("dbo.ContentAreaItems", "ContentTextId");
            CreateIndex("dbo.ContentAreaItems", "ContentAreaId");
            CreateIndex("dbo.ContentAreaItems", "ParentContentAreaId");
            AddForeignKey("dbo.Pages", "ContentId", "dbo.ContentAreas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContentTextLangs", "Id", "dbo.ContentTexts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContentAreaLibraryItems", "ContentAreaId", "dbo.ContentAreas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContentAreaItems", "ParentContentAreaId", "dbo.ContentAreas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContentAreaItems", "ContentTextId", "dbo.ContentTexts", "Id");
            AddForeignKey("dbo.ContentAreaItems", "ContentAreaId", "dbo.ContentAreas", "Id");
            RenameTable(name: "dbo.Contents", newName: "ContentAreas");
        }
    }
}
