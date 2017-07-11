using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jiangli.Manager.BaseRespository;
using jiangli.Models;

namespace jiangli.UI.Controllers
{
    public class PermitController : Controller
    {
        private BaseRpermit dbPermit = new BaseRpermit();
        public ActionResult addPermit()
        {
            List<Permit> permit = new List<Permit>
            {
                new Permit { Controller="user",Action="Index",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="查看所有用户"},
                new Permit { Controller="user",Action="search",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="查找用户"},
                new Permit { Controller="user",Action="delete",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="删除用户"},
                new Permit { Controller="user",Action="getallinfo",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="用户全部信息"},
                new Permit { Controller="user",Action="ModifiedRole",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="修改用户"},
                new Permit { Controller="case",Action="Index",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="查看所有案件"},
                new Permit { Controller="case",Action="search",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="查找案件"},
                new Permit { Controller="case",Action="delete",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="删除案件"},
                new Permit { Controller="case",Action="casedetail",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="案件详细信息"},
                new Permit { Controller="case",Action="Modifystate",URL=null,CreateBy=1,CreateByName="haibin",CreateOn=DateTime.Now,PermitName="修改案件信息"},
            };
            foreach (var item in permit)
            {
                dbPermit.m_addPermit(item);
            }
            return View();
        }
        // GET: Permit
        public ActionResult Index()
        {
            var temp = dbPermit.m_getAllPermit();
            ViewData["permit"] = temp.ToList();
            return View();
        }
    }
}