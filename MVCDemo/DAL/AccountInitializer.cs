using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace MVCDemo.DAL
{
    public class AccountInitializer : DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            //var sysUsers = new List<SysUser>
            //{
            //    new SysUser {UserName="Tom",Email="Tom@sohu.com",Password="1" },
            //    new SysUser {UserName="Jerry",Email="Jerry@sohu.com",Password="2" }
            //};
            //sysUsers.ForEach(s => context.SysUsers.Add(s));
            //context.SaveChanges();

            //var sysRoles = new List<SysRole>
            //{
            //    new SysRole {RoleName="Administrators",RoleDesc="Administrators have full authorization to perform system administration." },
            //    new SysRole {RoleName="General Users",RoleDesc="General Users can access the shared data they are authorized for" }
            //};
            //sysRoles.ForEach(s => context.SysRoles.Add(s));
            //context.SaveChanges();

            //var sysUserRoles = new List<SysUserRole>
            //{
            //    new SysUserRole {SysUserID=1,SysRoleID=1 },
            //    new SysUserRole {SysUserID=1,SysRoleID=2 },
            //    new SysUserRole {SysUserID=2,SysRoleID=2 }
            //};
            //sysUserRoles.ForEach(s => context.SysUserRoles.Add(s));
            //context.SaveChanges();
            var sysUsers = new List<SysUser>
            {
                 new SysUser {UserName="Tom",Email="Tom@sohu.com",Password="1" },
                 new SysUser {UserName="Jerry",Email="Jerry@sohu.com",Password="2" }
            };
            sysUsers.ForEach(s => context.SysUsers.AddOrUpdate(p => p.UserName, s));
                context.SaveChanges();
            var sysRoles = new List<SysRole>
            {
               new SysRole {RoleName="Administrators",RoleDesc="Administrators have full authorization to perform system administration." },
               new SysRole {RoleName="General Users",RoleDesc="General Users can access the shared data they are authorized for" }
            };
            sysRoles.ForEach(s => context.SysRoles.AddOrUpdate(r => r.RoleName, s));
            context.SaveChanges();

            var sysUserRoles = new List<SysUserRole>
            {
                new SysUserRole
                {
                    SysUserID=sysUsers.Single(s=>s.UserName=="Tom").ID,
                    SysRoleID=sysRoles.Single(r=>r.RoleName=="Administrators").ID
                },
                new SysUserRole
                {
                    SysUserID=sysUsers.Single(s=>s.UserName=="Tom").ID,
                    SysRoleID=sysRoles.Single(r=>r.RoleName=="General Users").ID
                },
                new SysUserRole
                {
                    SysUserID=sysUsers.Single(s=>s.UserName=="Jerry").ID,
                    SysRoleID=sysRoles.Single(r=>r.RoleName=="General Users").ID
                }
            };
            foreach (SysUserRole item in sysUserRoles)
            {
                var sysUserRoleInDataBase = context.SysUserRoles.Where(
                     s =>
                        s.SysUser.ID == item.SysUserID &&
                        s.SysRole.ID == item.SysRoleID).SingleOrDefault();
                if (sysUserRoleInDataBase == null)
                {
                    context.SysUserRoles.Add(item);
                }
                    
            }
            context.SaveChanges();

        }

    }
}