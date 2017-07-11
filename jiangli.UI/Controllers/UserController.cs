using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jiangli.BLL;
using jiangli.Manager.Models;
using jiangli.Manager.BaseRespository;
using jiangli.UI.Attribute;

namespace jiangli.UI.Controllers
{
    public class UserController : Controller
    {
        private int pageSize = 10;
        private BaseRuser db = new BaseRuser();
        private int page = 1;
        private int total = 1;
        private int role = 0;
        public ActionResult AddUser()
        {
            List<T_UserInfo> temp = new List<T_UserInfo>
            {
                //new T_UserInfo { Account="123",nickName="haibin",Role=1,Weight=100,NumberOfCase=1 ,cellPhone="7568954",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now,Email="123456@qq.com"},
                //new T_UserInfo { Account="156",nickName="海彬",Role=2,Weight=20,NumberOfCase=10 ,cellPhone="18333603795",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now,Email="3336363@163.com"},
                //new T_UserInfo { Account="456",nickName="小城",Role=1,Weight=50,NumberOfCase=11, cellPhone = "132456798", RegisterDate = DateTime.Now, DateOfLast = DateTime.Now,Email="222222@qq.com" },
                //new T_UserInfo { Account="123",nickName="haibin",Role=1,Weight=100,NumberOfCase=1 ,cellPhone="7568954",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now},
                //new T_UserInfo { Account="156",nickName="海彬",Role=2,Weight=20,NumberOfCase=10 ,cellPhone="18333603795",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now},
                //new T_UserInfo { Account="456",nickName="小城",Role=1,Weight=50,NumberOfCase=11, cellPhone = "132456798", RegisterDate = DateTime.Now, DateOfLast = DateTime.Now },
                //new T_UserInfo { Account="123",nickName="haibin",Role=1,Weight=100,NumberOfCase=1 ,cellPhone="7568954",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now},
                //new T_UserInfo { Account="156",nickName="海彬",Role=2,Weight=20,NumberOfCase=10 ,cellPhone="18333603795",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now},
                //new T_UserInfo { Account="456",nickName="小城",Role=1,Weight=50,NumberOfCase=11, cellPhone = "132456798", RegisterDate = DateTime.Now, DateOfLast = DateTime.Now },
                //new T_UserInfo { Account="123",nickName="haibin",Role=1,Weight=100,NumberOfCase=1 ,cellPhone="7568954",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now},
                //new T_UserInfo { Account="156",nickName="海彬",Role=2,Weight=20,NumberOfCase=10 ,cellPhone="18333603795",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now},
                new T_UserInfo { Account="123456",nickName="小城",Role=1,Weight=50,NumberOfCase=11, cellPhone = "132456798", RegisterDate = DateTime.Now, DateOfLast = DateTime.Now,password="123456" },
                new T_UserInfo { Account="123",nickName="haibin",Role=1,Weight=100,NumberOfCase=1 ,cellPhone="7568954",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now,password="123456"},
                new T_UserInfo { Account="156",nickName="海彬",Role=2,Weight=20,NumberOfCase=10 ,cellPhone="18333603795",RegisterDate=DateTime.Now,DateOfLast=DateTime.Now,password="123456"},
                new T_UserInfo { Account="456",nickName="小城",Role=1,Weight=50,NumberOfCase=11, cellPhone = "132456798", RegisterDate = DateTime.Now, DateOfLast = DateTime.Now ,password="123456"},
            };
            for (int i = 0; i < temp.Count(); i++)
            {
                db.Register(temp[i]);
            }
            return View();
        }
        // GET: User
        /// <summary>
        /// 初始页面
        /// </summary>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult Index()
        {
            if (Request.QueryString["page"] != null) page = int.Parse(Request.QueryString["page"]);
            string word = Request.Form["word"];
            if (word == null)
            {
                var users = db.m_getAllUser(page, pageSize, out total, role);
                ViewData["total"] = total;
                ViewData["page"] = page;
                ViewData["users"] = users.ToList();
                return View();
            }
            else
            {
                if (Request.QueryString["page"] != null) page = int.Parse(Request.QueryString["page"]);
                var users = db.m_searchByKeyWord(page, pageSize, out total, word);
                ViewData["total"] = total;
                ViewData["page"] = page;
                ViewData["users"] = users.ToList();
                return View();
            }
        }
        public ActionResult Delete()
        {
            string id = Request.QueryString["deleteID"];
            bool result = db.m_deleteById(int.Parse(id));
            if (result == true) ViewData["delete"] = "true";
            else ViewData["delete"] = "false";
            var users = db.m_getAllUser(page, pageSize, out total, role);
            ViewData["total"] = total;
            ViewData["page"] = page;
            ViewData["users"] = users.ToList();
            return View("~/views/User/Index.cshtml");
        }
        /// <summary>
        /// 通过id获取用户的所有信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllInfo()
        {
            string id = Request.Form["id"];
            T_UserInfo result = db.m_getAllinfoById(int.Parse(id));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ModifiedRole()
        {
            string id = Request.Form["id"];
            string value = Request.Form["value"];
            bool temp = db.m_modifyUserRole(int.Parse(id), int.Parse(value));
            if (temp == true) return Json("修改成功");
            else return Json("修改失败");
        }
    }
}