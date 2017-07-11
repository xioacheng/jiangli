using jiangli.UI.Models.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jiangli.UI.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LogInModal model)
        {
            var result = Repository.WebSite.Repository.DbUser.LoginByAccount(model.Account,model.PassWord);
            if(result==Common.LoginResult.OK)
            {
                Session["User"] = Repository.WebSite.Repository.DbUser.m_getUserByAccount(model.Account);
                return RedirectToAction("index","user");
            }
            ModelState.AddModelError("",result.ToString());
            return View("Index",model);
        }
        public ActionResult RegisterCheck(Register model)
        {

            //ModelState.AddModelError("","邮箱格式不规范");
            return View("Register",model);
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult GetEmail(string Email)
        {
            return Json(Repository.WebSite.Repository.DbUser.m_checkEmail(Email),JsonRequestBehavior.AllowGet);
        }
    }
}