namespace Ornaments.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceNumberAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "invoiceNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "invoiceNumber");
        }
    }
}
