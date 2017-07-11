using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jiangli.Manager.BaseRespository;
using jiangli.Models;
using jiangli.Models.Constants;

namespace jiangli.UI.Controllers
{
    public class CaseController : Controller
    {
        private int pageIndex = 1;
        private int pageSize = 10;
        private int total = 10;
        private BaseRcase db = new BaseRcase();
        public ActionResult AddCase()
        {
            List<Case> add = new List<Case>
            {
                //new Case {Title="买的书和图片不一致",originalpay=1000,Publisher="安成",Complainant="海彬",Respondent="京东自营",ModeOfPay=1,StatementOfCase="课本的名称一样，内容只有一半fjdklsa;jfdksaf 发动机考试辅导教师看范德萨拉飞机的卡萨四；  ",DateOfBegin=(new DateTime()), DateOfEnd=new DateTime(),CaseOfSubmit=new DateTime(),StateOfCase=CaseState.PUBLISH },
                new Case { Title="第2个案件",originalpay=200,Publisher="xiacheng",PublisherId = 1,RespondentId = 2,Complainant="haiibn",Respondent="商店",ModeOfPay=2,StatementOfCase="商店的东西过期",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH, }
                //new Case { Title="隔壁的们管理",originalpay=9342,Publisher="范德萨",Complainant="金坷垃",Respondent="剖解",ModeOfPay=3,StatementOfCase="发的扫平稳健康不能看",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH },
                // new Case {Title="买的书和图片不一致",originalpay=1000,Publisher="安成",Complainant="海彬",Respondent="京东自营",ModeOfPay=1,StatementOfCase="课本的名称一样，内容只有一半",DateOfBegin=(new DateTime()), DateOfEnd=new DateTime(),CaseOfSubmit=new DateTime(),StateOfCase=CaseState.FINISH },
                //new Case { Title="第2个案件",originalpay=200,Publisher="xiacheng",Complainant="haiibn",Respondent="商店",ModeOfPay=2,StatementOfCase="商店的东西过期",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PENDING, },
                //new Case { Title="隔壁的们管理",originalpay=9342,Publisher="范德萨",Complainant="金坷垃",Respondent="剖解",ModeOfPay=3,StatementOfCase="发的扫平稳健康不能看",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH },
                // new Case {Title="买的书和图片不一致",originalpay=1000,Publisher="安成",Complainant="海彬",Respondent="京东自营",ModeOfPay=1,StatementOfCase="课本的名称一样，内容只有一半",DateOfBegin=(new DateTime()), DateOfEnd=new DateTime(),CaseOfSubmit=new DateTime(),StateOfCase=CaseState.DRAFT },
                //new Case { Title="第2个案件",originalpay=200,Publisher="xiacheng",Complainant="haiibn",Respondent="商店",ModeOfPay=2,StatementOfCase="商店的东西过期",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.COMPLAIN, },
                //new Case { Title="隔壁的们管理",originalpay=9342,Publisher="范德萨",Complainant="金坷垃",Respondent="剖解",ModeOfPay=3,StatementOfCase="发的扫平稳健康不能看",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.DRAFT },
                // new Case {Title="买的书和图片不一致",originalpay=1000,Publisher="安成",Complainant="海彬",Respondent="京东自营",ModeOfPay=1,StatementOfCase="课本的名称一样，内容只有一半",DateOfBegin=(new DateTime()), DateOfEnd=new DateTime(),CaseOfSubmit=new DateTime(),StateOfCase=CaseState.PENDING },
                //new Case { Title="第2个案件",originalpay=200,Publisher="xiacheng",Complainant="haiibn",Respondent="商店",ModeOfPay=2,StatementOfCase="商店的东西过期",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH, },
                //new Case { Title="隔壁的们管理",originalpay=9342,Publisher="范德萨",Complainant="金坷垃",Respondent="剖解",ModeOfPay=3,StatementOfCase="发的扫平稳健康不能看",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH },
                // new Case {Title="买的书和图片不一致",originalpay=1000,Publisher="安成",Complainant="海彬",Respondent="京东自营",ModeOfPay=1,StatementOfCase="课本的名称一样，内容只有一半",DateOfBegin=(new DateTime()), DateOfEnd=new DateTime(),CaseOfSubmit=new DateTime(),StateOfCase=CaseState.PUBLISH },
                //new Case { Title="第2个案件",originalpay=200,Publisher="xiacheng",Complainant="haiibn",Respondent="商店",ModeOfPay=2,StatementOfCase="商店的东西过期",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH, },
                //new Case { Title="隔壁的们管理",originalpay=9342,Publisher="范德萨",Complainant="金坷垃",Respondent="剖解",ModeOfPay=3,StatementOfCase="发的扫平稳健康不能看",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH },
                // new Case {Title="买的书和图片不一致",originalpay=1000,Publisher="安成",Complainant="海彬",Respondent="京东自营",ModeOfPay=1,StatementOfCase="课本的名称一样，内容只有一半",DateOfBegin=(new DateTime()), DateOfEnd=new DateTime(),CaseOfSubmit=new DateTime(),StateOfCase=CaseState.PUBLISH },
                //new Case { Title="第2个案件",originalpay=200,Publisher="xiacheng",Complainant="haiibn",Respondent="商店",ModeOfPay=2,StatementOfCase="商店的东西过期",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH, },
                //new Case { Title="隔壁的们管理",originalpay=9342,Publisher="范德萨",Complainant="金坷垃",Respondent="剖解",ModeOfPay=3,StatementOfCase="发的扫平稳健康不能看",DateOfBegin=DateTime.Now,DateOfEnd=DateTime.Now,CaseOfSubmit=DateTime.Now,StateOfCase=CaseState.PUBLISH },
            };
            for (int i = 0; i < add.Count(); i++)
            {
                db.m_addItem(add[i]);
            }
            return View();
        }
        public ActionResult Search()
        {
            if (Request.QueryString["page"] != null) pageIndex = int.Parse(Request.QueryString["page"]);
            string keyWord = Request.Form["word"];
            var cases = db.m_getStateCaseByWord(pageIndex, pageSize, out total, 0, keyWord);
            ViewData["cases"] = cases.ToList();
            ViewData["page"] = pageSize;
            ViewData["total"] = total;
            return View("~/views/Case/Index.cshtml");
        }
        // GET: Case
        public ActionResult Index()
        {
            //Case item = new Case{Title="买的书和图片不一致",originalpay=1000,Publisher="安成",Complainant="海彬",Respondent="京东自营",ModeOfPay=1,StatementOfCase="课本的名称一样，内容只有一半",DateOfBegin=(new DateTime()), DateOfEnd=new DateTime(),CaseOfSubmit=new DateTime(),StateOfCase=3};
            //db.m_addItem(item);
            if (Request.QueryString["page"] != null) pageIndex = int.Parse(Request.QueryString["page"]);
            string word = Request.Form["word"];
            var cases = db.m_getAllCase(pageIndex, pageSize, out total);
            ViewData["cases"] = cases.ToList();
            ViewData["page"] = pageIndex;
            ViewData["total"] = total;
            return View();
        }
        public ActionResult Modifystate()
        {
            string id = Request.Form["id"];
            string value = Request.Form["value"];
            bool temp = false;
            switch (int.Parse(value))
            {
                case 0: db.m_modifiedState(int.Parse(id), CaseState.DRAFT); break;
                case 1: db.m_modifiedState(int.Parse(id), CaseState.PENDING); break;
                case 2: db.m_modifiedState(int.Parse(id), CaseState.PUBLISH); break;
                case 3: db.m_modifiedState(int.Parse(id), CaseState.FINISH); break;
                case 4: db.m_modifiedState(int.Parse(id), CaseState.COMPLAIN); break;
            }
            
            if (temp == true) return Json("修改成功");
            else return Json("修改失败");
        }
        public ActionResult delete()
        {
            string id = Request.Form["deleteid"];
            bool result = db.m_deleteCase(int.Parse(id), true);
            return Json(result);
        }
        public ActionResult CaseDetail()
        {
            string id = Request.Form["id"];
            Case result = db.m_getCaseById(int.Parse(id));
            if (result == null) return Json("获取案件失败，案件详细信息可能为空");
            else return Json(result.StatementOfCase);
        }
    }
}