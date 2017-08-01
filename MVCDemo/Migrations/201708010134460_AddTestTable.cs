namespace MVCDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TestName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Test");
        }
    }
}
