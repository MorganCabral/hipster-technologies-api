namespace HipsterTechnologies.API.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Holdings",
                c => new
                    {
                        HoldingId = c.Int(nullable: false, identity: true),
                        FacebookHandle = c.String(),
                        Exchange = c.String(),
                        Symbol = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HoldingId);
            
            CreateTable(
                "dbo.Events",
                c => new 
                    { 
                        EventId = c.Int(nullable: false, identity: true),
                        FacebookHandle = c.String(),
                        EventStart = c.DateTime(),
                        EventLength = c.DateTimeOffset(),
                        EventName = c.String(),
                    })
                .PrimaryKey(t => t.EventId);

            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        FacebookHandle = c.String(),
                        PostedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.TransactionItems",
                c => new
                    {
                        TransactionItemId = c.Int(nullable: false, identity: true),
                        Exchange = c.String(),
                        Symbol = c.String(),
                        Type = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PostedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Transaction_TransactionId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionItemId)
                .ForeignKey("dbo.Transactions", t => t.Transaction_TransactionId)
                .Index(t => t.Transaction_TransactionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionItems", "Transaction_TransactionId", "dbo.Transactions");
            DropIndex("dbo.TransactionItems", new[] { "Transaction_TransactionId" });
            DropTable("dbo.Users");
            DropTable("dbo.TransactionItems");
            DropTable("dbo.Transactions");
            DropTable("dbo.Holdings");
            DropTable("dbo.Events");
        }
    }
}
