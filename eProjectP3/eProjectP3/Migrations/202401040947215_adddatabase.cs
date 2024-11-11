namespace eProjectP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.client",
                c => new
                    {
                        Client_ID = c.Int(nullable: false, identity: true),
                        Clinic_Name = c.String(maxLength: 255),
                        Clinic_Password = c.String(maxLength: 255),
                        Contact_Person = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        Phone = c.String(maxLength: 20),
                        Address = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Client_ID);
            
            CreateTable(
                "dbo.feedback",
                c => new
                    {
                        Feedback_ID = c.Int(nullable: false, identity: true),
                        Client_ID = c.Int(),
                        Feedback_Text = c.String(unicode: false, storeType: "text"),
                        Date = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Feedback_ID)
                .ForeignKey("dbo.client", t => t.Client_ID)
                .Index(t => t.Client_ID);
            
            CreateTable(
                "dbo.online_purchase",
                c => new
                    {
                        Purchase_ID = c.Int(nullable: false, identity: true),
                        Client_ID = c.Int(nullable: false),
                        Total_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Purchase_Date = c.DateTime(storeType: "date"),
                        medical_application_Medicine_ID = c.Int(),
                        scientific_application_Apparatus_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Purchase_ID)
                .ForeignKey("dbo.client", t => t.Client_ID, cascadeDelete: true)
                .ForeignKey("dbo.medical_application", t => t.medical_application_Medicine_ID)
                .ForeignKey("dbo.scientific_application", t => t.scientific_application_Apparatus_ID)
                .Index(t => t.Client_ID)
                .Index(t => t.medical_application_Medicine_ID)
                .Index(t => t.scientific_application_Apparatus_ID);
            
            CreateTable(
                "dbo.online_purchase_detail",
                c => new
                    {
                        Purchase_Detail_ID = c.Int(nullable: false, identity: true),
                        Purchase_ID = c.Int(nullable: false),
                        Item_ID = c.Int(),
                        Item_Type = c.String(maxLength: 50),
                        Quantity = c.Int(),
                    })
                .PrimaryKey(t => t.Purchase_Detail_ID)
                .ForeignKey("dbo.medical_application", t => t.Item_ID)
                .ForeignKey("dbo.online_purchase", t => t.Purchase_ID, cascadeDelete: true)
                .ForeignKey("dbo.scientific_application", t => t.Item_ID)
                .Index(t => t.Purchase_ID)
                .Index(t => t.Item_ID);
            
            CreateTable(
                "dbo.medical_application",
                c => new
                    {
                        Medicine_ID = c.Int(nullable: false, identity: true),
                        Medicine_Type_ID = c.Int(),
                        Img = c.String(maxLength: 50),
                        Cost = c.Decimal(precision: 18, scale: 2),
                        Code = c.String(maxLength: 50),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Medicine_ID)
                .ForeignKey("dbo.medical_type", t => t.Medicine_Type_ID)
                .Index(t => t.Medicine_Type_ID);
            
            CreateTable(
                "dbo.medical_type",
                c => new
                    {
                        Medicine_Type_ID = c.Int(nullable: false, identity: true),
                        Medicine_Type_Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Medicine_Type_ID);
            
            CreateTable(
                "dbo.scientific_application",
                c => new
                    {
                        Apparatus_ID = c.Int(nullable: false, identity: true),
                        Img = c.String(maxLength: 50),
                        Cost = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(unicode: false, storeType: "text"),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Apparatus_ID);
            
            CreateTable(
                "dbo.staff",
                c => new
                    {
                        Staff_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Position = c.String(maxLength: 255),
                        Contact_Number = c.String(maxLength: 20),
                        Email = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Staff_ID);
            
            CreateTable(
                "dbo.education_application",
                c => new
                    {
                        Education_ID = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 255),
                        Type = c.String(maxLength: 50),
                        Staff_ID = c.Int(),
                        Date = c.DateTime(storeType: "date"),
                        Time = c.Time(precision: 7),
                    })
                .PrimaryKey(t => t.Education_ID)
                .ForeignKey("dbo.staff", t => t.Staff_ID)
                .Index(t => t.Staff_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.education_application", "Staff_ID", "dbo.staff");
            DropForeignKey("dbo.online_purchase_detail", "Item_ID", "dbo.scientific_application");
            DropForeignKey("dbo.online_purchase", "scientific_application_Apparatus_ID", "dbo.scientific_application");
            DropForeignKey("dbo.online_purchase_detail", "Purchase_ID", "dbo.online_purchase");
            DropForeignKey("dbo.online_purchase_detail", "Item_ID", "dbo.medical_application");
            DropForeignKey("dbo.online_purchase", "medical_application_Medicine_ID", "dbo.medical_application");
            DropForeignKey("dbo.medical_application", "Medicine_Type_ID", "dbo.medical_type");
            DropForeignKey("dbo.online_purchase", "Client_ID", "dbo.client");
            DropForeignKey("dbo.feedback", "Client_ID", "dbo.client");
            DropIndex("dbo.education_application", new[] { "Staff_ID" });
            DropIndex("dbo.medical_application", new[] { "Medicine_Type_ID" });
            DropIndex("dbo.online_purchase_detail", new[] { "Item_ID" });
            DropIndex("dbo.online_purchase_detail", new[] { "Purchase_ID" });
            DropIndex("dbo.online_purchase", new[] { "scientific_application_Apparatus_ID" });
            DropIndex("dbo.online_purchase", new[] { "medical_application_Medicine_ID" });
            DropIndex("dbo.online_purchase", new[] { "Client_ID" });
            DropIndex("dbo.feedback", new[] { "Client_ID" });
            DropTable("dbo.education_application");
            DropTable("dbo.staff");
            DropTable("dbo.scientific_application");
            DropTable("dbo.medical_type");
            DropTable("dbo.medical_application");
            DropTable("dbo.online_purchase_detail");
            DropTable("dbo.online_purchase");
            DropTable("dbo.feedback");
            DropTable("dbo.client");
        }
    }
}
