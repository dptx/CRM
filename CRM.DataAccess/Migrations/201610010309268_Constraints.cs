namespace CRM.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Constraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Account", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Account", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Account", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customer", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Note", "Body", c => c.String(nullable: false));
            CreateIndex("dbo.Account", "Email", unique: true);
            CreateIndex("dbo.Note", "AccountID");
            CreateIndex("dbo.Customer", "Email", unique: true);
            AddForeignKey("dbo.Note", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "AccountID", "dbo.Account");
            DropIndex("dbo.Customer", new[] { "Email" });
            DropIndex("dbo.Note", new[] { "AccountID" });
            DropIndex("dbo.Account", new[] { "Email" });
            AlterColumn("dbo.Note", "Body", c => c.String());
            AlterColumn("dbo.Customer", "Email", c => c.String());
            AlterColumn("dbo.Customer", "LastName", c => c.String());
            AlterColumn("dbo.Customer", "FirstName", c => c.String());
            AlterColumn("dbo.Account", "Password", c => c.String());
            AlterColumn("dbo.Account", "Email", c => c.String());
            AlterColumn("dbo.Account", "LastName", c => c.String());
            AlterColumn("dbo.Account", "FirstName", c => c.String());
        }
    }
}
