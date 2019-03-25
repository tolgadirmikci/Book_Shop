namespace Book_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_orderbookpropertytocustomermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "OrderBook", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "OrderBook");
        }
    }
}
