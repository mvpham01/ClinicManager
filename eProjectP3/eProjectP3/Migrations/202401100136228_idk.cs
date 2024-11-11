namespace eProjectP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.business_application",
                c => new
                    {
                        Transaction_ID = c.Int(nullable: false, identity: true),
                        Sector = c.String(maxLength: 50),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Date = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Transaction_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.business_application");
        }
    }
}
