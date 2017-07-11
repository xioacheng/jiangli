using jiangli.Manager.BaseRespository;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jiangli.UI.Controllers
{
    public class RoleController : Controller
    {
        private BaseRrole dbRole = new BaseRrole();
        private BaseRpermit dbPermit = new BaseRpermit();
        private BaseRrole_permit dbRolePermit = new BaseRrole_permit();
        public ActionResult addRole()
        {
            List<Role> role = new List<Role>
            {
                new Role { RoleName="普通用户",CreateBy=1,CreateOn=DateTime.Now,CreateByName="haibin"},
                new Role { RoleName="管理员",CreateBy=1,CreateOn=DateTime.Now,CreateByName="haibin"},
                new Role { RoleName="审核员",CreateBy=1,CreateOn=DateTime.Now,CreateByName="haibin"},
                new Role { RoleName="超级管理员",CreateBy=1,CreateOn=DateTime.Now,CreateByName="haibin"},
            };
            foreach (Role item in role)
            {
                dbRole.m_addRole(item);
            }
            return View();
        }
        //public ActionResult addrole()
        //{
        //    var RoleName = Request.Form["Role"];
        //    if (RoleName == "")
        //    {
        //        return Json("添加角色名不能为空");
        //    }
        //    return Json(dbRole.m_addRole(new Role { RoleName = RoleName, CreateBy = 1, CreateOn = DateTime.Now, CreateByName = "haibin" }) == true ? "添加成功" : "添加失败");
        //}
        public ActionResult Delete()
        {
            var id = Request.Form["id"];
            return Json(dbRole.m_deleteRole(int.Parse(id))==true?"删除成功":"删除失败"); 
        }
        // GET: Role
        public ActionResult Index()
        {
            var permit = dbPermit.m_getAllPermit();
            var roles = dbRole.m_getAllRole();
            ViewData["roles"] = roles.ToList();
            ViewData["Permit"] = permit.ToList();
            return View();
        }
        public ActionResult GetAllRole()
        {
            var permit = dbPermit.m_getAllPermit();
            var roles = dbRole.m_getAllRole();
            return Json(roles);
        }
        public ActionResult addPermit()
        {
            int userid = 2;
            var value = Request.Form["value"];
            var id = Request.Form["id"];
            if (value == null)
            {
                dbRolePermit.m_deletePermitByRoleid(int.Parse(id));
                return Json("修改成功");
            }
            var temp = value.Split(',');
            dbRolePermit.m_deletePermitByRoleid(int.Parse(id));
            foreach (string item in temp)
            {
                if (dbRolePermit.m_addRole_Permit(int.Parse(id), int.Parse(item), userid) == false)
                    return Json(item + "修改失败");
            }
            return Json("修改成功");
        }
        public ActionResult PermitDetail()
        {
            var id = Request.Form["id"];
            var temp = dbRolePermit.m_getPermitByRoleid(int.Parse(id));
            if (temp.Count() != 0)
                return Json(temp);
            else return Json("false");
        }
    }
}