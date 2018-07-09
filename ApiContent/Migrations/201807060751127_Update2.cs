namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PageTemplates", newName: "ContentTemplates");
            AddColumn("dbo.Contents", "TemplateId", c => c.Int(nullable: false));
            AddColumn("dbo.ContentTemplates", "Type", c => c.String());
            CreateIndex("dbo.Contents", "TemplateId");
            AddForeignKey("dbo.Contents", "TemplateId", "dbo.ContentTemplates", "Id");
            DropColumn("dbo.Contents", "Html");
            DropColumn("dbo.Contents", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contents", "Type", c => c.String());
            AddColumn("dbo.Contents", "Html", c => c.String());
            DropForeignKey("dbo.Contents", "TemplateId", "dbo.ContentTemplates");
            DropIndex("dbo.Contents", new[] { "TemplateId" });
            DropColumn("dbo.ContentTemplates", "Type");
            DropColumn("dbo.Contents", "TemplateId");
            RenameTable(name: "dbo.ContentTemplates", newName: "PageTemplates");
        }
    }
}
