namespace Ornaments.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productidInFeedBack : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Feedbacks", "Product_Id", "dbo.Products");
            DropIndex("dbo.Feedbacks", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Feedbacks", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.Feedbacks", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Feedbacks", "ProductId");
            AddForeignKey("dbo.Feedbacks", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "ProductId", "dbo.Products");
            DropIndex("dbo.Feedbacks", new[] { "ProductId" });
            AlterColumn("dbo.Feedbacks", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.Feedbacks", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.Feedbacks", "Product_Id");
            AddForeignKey("dbo.Feedbacks", "Product_Id", "dbo.Products", "Id");
        }
    }
}
