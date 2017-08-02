namespace MVCDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnLoginName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SysUser", name: "UserName", newName: "LoginName");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.SysUser", name: "LoginName", newName: "UserName");
        }
    }
}
