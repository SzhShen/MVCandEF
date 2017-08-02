using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDemo.Models
{
    public class SysUser
    {
        public int ID { get; set; }
        [StringLength(10,ErrorMessage ="名字不能超过10个字。")]
        [Column("LoginName")]
        [Display(Name ="用户名")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime CreateDate { get; set; }
        public int? SysDepartmentID { get; set; }
        public virtual SysDepartment SysDepartment { get; set; }
    }
}