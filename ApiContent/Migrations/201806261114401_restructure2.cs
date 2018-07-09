namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restructure2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentTextLangs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Language = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.Language })
                .ForeignKey("dbo.ContentTexts", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ContentTexts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TextType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ContentAreaTexts");
        }
        
        public override void Down()
        {
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
            
            DropForeignKey("dbo.ContentTextLangs", "Id", "dbo.ContentTexts");
            DropIndex("dbo.ContentTextLangs", new[] { "Id" });
            DropTable("dbo.ContentTexts");
            DropTable("dbo.ContentTextLangs");
        }
    }
}
