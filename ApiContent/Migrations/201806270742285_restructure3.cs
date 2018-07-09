namespace ApiContent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restructure3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContentAreaLibraryItems", "Child_Id", "dbo.ContentAreas");
            DropIndex("dbo.ContentAreaLibraryItems", new[] { "Child_Id" });
            RenameColumn(table: "dbo.ContentAreaLibraryItems", name: "Child_Id", newName: "ContentAreaId");
            DropPrimaryKey("dbo.ContentAreaLibraryItems");
            CreateTable(
                "dbo.ContentAreaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seq = c.Int(nullable: false),
                        ContentArea_Id = c.Int(),
                        ContentText_Id = c.Int(),
                        ParentContentArea_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentAreas", t => t.ContentArea_Id)
                .ForeignKey("dbo.ContentAreas", t => t.ContentText_Id)
                .ForeignKey("dbo.ContentAreas", t => t.ParentContentArea_Id)
                .Index(t => t.ContentArea_Id)
                .Index(t => t.ContentText_Id)
                .Index(t => t.ParentContentArea_Id);
            
            AlterColumn("dbo.ContentAreaLibraryItems", "ContentAreaId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ContentAreaLibraryItems", new[] { "LibraryName", "ContentAreaId" });
            CreateIndex("dbo.ContentAreaLibraryItems", "ContentAreaId");
            AddForeignKey("dbo.ContentAreaLibraryItems", "ContentAreaId", "dbo.ContentAreas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContentAreaLibraryItems", "ContentAreaId", "dbo.ContentAreas");
            DropForeignKey("dbo.ContentAreaItems", "ParentContentArea_Id", "dbo.ContentAreas");
            DropForeignKey("dbo.ContentAreaItems", "ContentText_Id", "dbo.ContentAreas");
            DropForeignKey("dbo.ContentAreaItems", "ContentArea_Id", "dbo.ContentAreas");
            DropIndex("dbo.ContentAreaLibraryItems", new[] { "ContentAreaId" });
            DropIndex("dbo.ContentAreaItems", new[] { "ParentContentArea_Id" });
            DropIndex("dbo.ContentAreaItems", new[] { "ContentText_Id" });
            DropIndex("dbo.ContentAreaItems", new[] { "ContentArea_Id" });
            DropPrimaryKey("dbo.ContentAreaLibraryItems");
            AlterColumn("dbo.ContentAreaLibraryItems", "ContentAreaId", c => c.Int());
            DropTable("dbo.ContentAreaItems");
            AddPrimaryKey("dbo.ContentAreaLibraryItems", "LibraryName");
            RenameColumn(table: "dbo.ContentAreaLibraryItems", name: "ContentAreaId", newName: "Child_Id");
            CreateIndex("dbo.ContentAreaLibraryItems", "Child_Id");
            AddForeignKey("dbo.ContentAreaLibraryItems", "Child_Id", "dbo.ContentAreas", "Id");
        }
    }
}
