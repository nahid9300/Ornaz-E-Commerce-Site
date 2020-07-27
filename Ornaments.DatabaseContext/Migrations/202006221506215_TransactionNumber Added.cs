namespace Ornaments.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionNumberAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TransactionNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TransactionNumber");
        }
    }
}
