namespace MVCDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        RoleDesc = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SysUserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SysUserID = c.Int(nullable: false),
                        SysRoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SysRole", t => t.SysRoleID, cascadeDelete: true)
                .ForeignKey("dbo.SysUser", t => t.SysUserID, cascadeDelete: true)
                .Index(t => t.SysUserID)
                .Index(t => t.SysRoleID);
            
            CreateTable(
                "dbo.SysUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SysUserRole", "SysUserID", "dbo.SysUser");
            DropForeignKey("dbo.SysUserRole", "SysRoleID", "dbo.SysRole");
            DropIndex("dbo.SysUserRole", new[] { "SysRoleID" });
            DropIndex("dbo.SysUserRole", new[] { "SysUserID" });
            DropTable("dbo.SysUser");
            DropTable("dbo.SysUserRole");
            DropTable("dbo.SysRole");
        }
    }
}
