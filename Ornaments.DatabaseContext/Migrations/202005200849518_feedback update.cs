namespace Ornaments.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackupdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Feedbacks", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "ProductName", c => c.String());
        }
    }
}
