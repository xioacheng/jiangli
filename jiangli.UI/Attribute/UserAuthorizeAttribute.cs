using jiangli.Manager.BaseRespository;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jiangli.UI.Attribute
{
    public class UserAuthorizeAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.Session["User"] as User;
            //var user = new User { ID=1,Role=2};
            var controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();
            if (user != null)
            {
                if (isAllow(user.Role, controller, action) == false)
                    filterContext.RequestContext.HttpContext.Response.Write("<script>alert('权限不足');history.go(-1);</script>");
            }
            else
            {
                filterContext.RequestContext.HttpContext.Response.Write("<script>alert('请登录');history.go(-1);</script>");
                filterContext.RequestContext.HttpContext.Response.Redirect("/login");
            }
        }
        /// <summary>
        /// 判断用户访问控制器的权限是否存在
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool isAllow(int RoleID,string controller,string action)
        {
            BaseRpermit dbPermit = new BaseRpermit();
            BaseRrole_permit dbRP = new BaseRrole_permit();
            var temp = dbPermit.m_getPermitByConAction(controller,action);//判断是否对该操作设置了权限，如果没有设置权限，则直接让用户通过验证
            if (temp == null) return true;//不存在对该操作的权限认证
            else
            {
                if (dbRP.m_searchRP(RoleID, temp.ID)) return true;
                else return false;
            }
        }
    }
}