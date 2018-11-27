namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtabletblUserSignup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblSignupUser",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        Timezone = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        PasswordSalt = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblSignupUser");
        }
    }
}
