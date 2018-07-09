namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restructure4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContentAreaItems", "ParentContentArea_Id", "dbo.ContentAreas");
            DropIndex("dbo.ContentAreaItems", new[] { "ParentContentArea_Id" });
            RenameColumn(table: "dbo.ContentAreaItems", name: "ContentArea_Id", newName: "ContentAreaId");
            RenameColumn(table: "dbo.ContentAreaItems", name: "ContentText_Id", newName: "ContentTextId");
            RenameColumn(table: "dbo.ContentAreaItems", name: "ParentContentArea_Id", newName: "ParentContentAreaId");
            RenameIndex(table: "dbo.ContentAreaItems", name: "IX_ContentArea_Id", newName: "IX_ContentAreaId");
            RenameIndex(table: "dbo.ContentAreaItems", name: "IX_ContentText_Id", newName: "IX_ContentTextId");
            AlterColumn("dbo.ContentAreaItems", "ParentContentAreaId", c => c.Int(nullable: false));
            CreateIndex("dbo.ContentAreaItems", "ParentContentAreaId");
            AddForeignKey("dbo.ContentAreaItems", "ParentContentAreaId", "dbo.ContentAreas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContentAreaItems", "ParentContentAreaId", "dbo.ContentAreas");
            DropIndex("dbo.ContentAreaItems", new[] { "ParentContentAreaId" });
            AlterColumn("dbo.ContentAreaItems", "ParentContentAreaId", c => c.Int());
            RenameIndex(table: "dbo.ContentAreaItems", name: "IX_ContentTextId", newName: "IX_ContentText_Id");
            RenameIndex(table: "dbo.ContentAreaItems", name: "IX_ContentAreaId", newName: "IX_ContentArea_Id");
            RenameColumn(table: "dbo.ContentAreaItems", name: "ParentContentAreaId", newName: "ParentContentArea_Id");
            RenameColumn(table: "dbo.ContentAreaItems", name: "ContentTextId", newName: "ContentText_Id");
            RenameColumn(table: "dbo.ContentAreaItems", name: "ContentAreaId", newName: "ContentArea_Id");
            CreateIndex("dbo.ContentAreaItems", "ParentContentArea_Id");
            AddForeignKey("dbo.ContentAreaItems", "ParentContentArea_Id", "dbo.ContentAreas", "Id");
        }
    }
}
