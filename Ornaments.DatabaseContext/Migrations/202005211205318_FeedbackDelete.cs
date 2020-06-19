namespace Ornaments.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedbackDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Feedbacks", "ProductId", "dbo.Products");
            DropIndex("dbo.Feedbacks", new[] { "ProductId" });
            DropTable("dbo.Feedbacks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        ProductId = c.Int(nullable: false),
                        FeedbackDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Feedbacks", "ProductId");
            AddForeignKey("dbo.Feedbacks", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
