using jiangli.UI.Repository.WebSite;
using jiangli.Util;
using System.Web.Mvc;
using jiangli.Models.Constants;
using jiangli.UI.Models.WebSite;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace jiangli.Controllers
{

    public class HomeController : AsyncController
    {
        private Rcase caselist = Rcase.Current; 
        public async Task<ActionResult> Index()
        {
            List<CasebriefViewModel> list = await Task.Factory.StartNew<List<CasebriefViewModel>>(() => (List<CasebriefViewModel>) caselist.getCaselistBystate(CaseState.PUBLISH, 1));
            return View(list);
        }
        /// <summary>
        /// 索引详细案件
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public async Task<ActionResult> Article(int cid)
        {
            //var result = caselist.GetDetailCaseById(cid);
            DetailCaseViewModel result = await Task.Factory.StartNew<DetailCaseViewModel>(() => caselist.GetDetailCaseById(cid));
            if(result == null)
            {
                return View("Error");
            } 
            ViewData["achang"] = "123";
            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        } 
    }
}