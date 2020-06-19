namespace Ornaments.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Product_Id", "dbo.Products");
            DropIndex("dbo.Reviews", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Reviews", name: "Product_Id", newName: "productId");
            AlterColumn("dbo.Reviews", "productId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "productId");
            AddForeignKey("dbo.Reviews", "productId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "productId", "dbo.Products");
            DropIndex("dbo.Reviews", new[] { "productId" });
            AlterColumn("dbo.Reviews", "productId", c => c.Int());
            RenameColumn(table: "dbo.Reviews", name: "productId", newName: "Product_Id");
            CreateIndex("dbo.Reviews", "Product_Id");
            AddForeignKey("dbo.Reviews", "Product_Id", "dbo.Products", "Id");
        }
    }
}
