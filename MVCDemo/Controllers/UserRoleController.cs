using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.DAL;
using MVCDemo.Models;
using System.Data.Entity;
using MVCDemo.ViewModels;


namespace MVCDemo.Controllers
{
    public class UserRoleController : Controller
    {
        private AccountContext db = new AccountContext();
        //
        // GET: /UserRole/
        public ActionResult Index(int? id)
        {
            var viewModel = new UserRoleIndexData();
            viewModel.SysUsers = db.SysUsers
            .Include(u => u.SysDepartment)
            .Include(u => u.SysUserRoles.Select(ur => ur.SysRole))
            .OrderBy(u => u.UserName);
            if (id != null)

            {
                ViewBag.UserID = id.Value;
                viewModel.SysUserRoles = viewModel.SysUsers.Where(u => u.ID == id.Value).Single().SysUserRoles;
                viewModel.SysRoles = (viewModel.SysUserRoles.Where(
                ur => ur.SysUserID == id.Value)).Select(ur => ur.SysRole);
            }
            return View(viewModel);
        }
    }
}

