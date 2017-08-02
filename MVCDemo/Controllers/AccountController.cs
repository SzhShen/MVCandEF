using MVCDemo.DAL;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MVCDemo.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db = new AccountContext();
        //
        // GET: /Account/
        public ActionResult Index(string sortOrder,string searchString,string currentFilter,int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString!=null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //var users = from u in db.SysUsers
            //            select u;
            var users = db.SysUsers.Include(u => u.SysDepartment);
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                default:
                    users = users.OrderBy(u => u.UserName);
                    break;
            }

            int pageSize = 3;
            int pageNumber=(page ?? 1);
            return View(users.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Login()
        {
            ViewBag.LoginState = "登录前。。。";
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            //获取表单数据
            string email = fc["inputEmail3"];
            string password = fc["inputPassword3"];
            //进行下一步处理，这里先改下文字
            var user = db.SysUsers.Where(b => b.Email == email & b.Password == password);
            if (user.Count() > 0)
            {

                ViewBag.LoginState = email + "登录后。。。";
            }
            else
            {
                ViewBag.LoginState = email + "用户不存在。。。";
            }
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Register = "注册前。。。";
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            //获取表单数据
            string email = fc["inputEmail3"];
            string password = fc["inputPassword3"];

            //进行下一步处理，这里先改下文字
            ViewBag.LoginState = "注册账号 " + email;
            return View();
        }

        //查询用户及角色
        public ActionResult Details(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }

        //新建用户
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SysUser sysUser)
        {
            if (ModelState.IsValid)
            {
                db.SysUsers.Add(sysUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
   
        //修改用户
        public ActionResult Edit(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }
        [HttpPost]
        public ActionResult Edit(SysUser sysUser)
        {
            db.Entry(sysUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //删除用户
        public ActionResult Delete(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            db.SysUsers.Remove(sysUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region 示例代码，不实际执行
        public ActionResult EFQueryDemo()
        {
            //1.	[一般查询] 查询所有的SysUser 
            var users = from u in db.SysUsers
                        select u; //表达式方式
            users = db.SysUsers; //函数式方式
            //2.	[条件查询] 加入条件
            users = from u in db.SysUsers
                    where u.UserName == "Tom"
                    select u;
            users = db.SysUsers.Where(u => u.UserName == "Tom");
            //3.	[排序和分页查询], 只有对查询结果排序了才能分页!
            users = (from u in db.SysUsers
                     orderby u.UserName
                     select u).Skip(0).Take(5);
            users = db.SysUsers.OrderBy(u => u.UserName).Skip(0).Take(5);
            //4.	[聚合查询], 只能通过函数式查询！
            //查user总数
            var num = db.SysUsers.Count();
            //查最小ID
            var minId = db.SysUsers.Min(u => u.ID);
            //5.	[连接查询]  [todo]?多表复杂查询如何做？
            var admin = from u in db.SysUsers
                        join ur in db.SysUserRoles
                        on u.ID equals ur.SysUserID
                        select u;
            return View();
        }

        //数据更新,分三步：找到对象--> 更新对象数据--> 保存更改
        public ActionResult EFUpdateDemo()
        {
            //1.找到对象
            var sysUser = db.SysUsers.FirstOrDefault(u => u.UserName == "Tom");
            //2.更新对象数据
            if (sysUser != null)
            {
                sysUser.UserName = "Tom2";
            }
            //3.保存修改
            db.SaveChanges();
            return View();
        }

        //数据添加和删除
        public ActionResult EFAddOrDeleteDemo()
        {
            //添加
            //1.创建新的实体
            var newSysUser = new SysUser()
            {
                UserName = "Scott",
                Password = "tiger",
                Email = "Scott@sohu.com"
            };
            //2.增加 ???
            db.SysUsers.Add(newSysUser);
            //3.保存修改
            db.SaveChanges();

            //删除
            //1.找到需要删除的对象
            var delSysUser = db.SysUsers.FirstOrDefault(u => u.UserName == "Scott");
            //2.删除
            if (delSysUser != null)
            {
                db.SysUsers.Remove(delSysUser);
            }
            //3.保存修改
            db.SaveChanges();
            return View("EFQueryDemo");
        }
        #endregion
        //各种查询



    }
}