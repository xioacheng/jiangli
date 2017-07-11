using jiangli.Models.Constants;
using jiangli.UI.Repository.WebSite;
using jiangli.UI.Models.WebSite; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jiangli.Controllers
{
    /// <summary>
    /// 审核员控制器
    /// </summary>
    public class AuditorController : Controller
    { 
        // GET: Auditor
        public ActionResult Index()
        {
            return View(Rcase.Current.getCaselistBystate(CaseState.PENDING,1));
        }
        /// <summary>
        /// 审核申诉完毕的案件
        /// </summary>
        /// <returns></returns>
        public ActionResult Appeal()
        {
            return View(Rcase.Current.getCaselistByCondition(AppealState.Finish, 1));
        }
        /// <summary>
        /// 审核刚提交案件--申诉
        /// </summary>
        /// <returns></returns>
        public ActionResult examine(CaseReviewResultViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            if(model.right)
            {
                //审核通过
                Rcase.Current.ModifiedState(model.id, CaseState.COMPLAIN);
            }
            else
            {
            }
            return RedirectToAction("Index");
        } 
        /// <summary>
        /// 审核申诉完毕案件---发布
        /// </summary>
        /// <returns></returns>
        public ActionResult examineComplain(CaseReviewResultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            if (model.right)
            {
                //审核通过
                Rcase.Current.ModifiedState(model.id, CaseState.PUBLISH);
            }
            else
            {
            }
            return RedirectToAction("Index");
        }
    }
}