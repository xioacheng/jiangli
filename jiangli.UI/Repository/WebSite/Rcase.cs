using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jiangli.Manager.BaseRespository;
using jiangli.UI.Models.WebSite;
using jiangli.Models;
using jiangli.Models.Constants;
using jiangli.Manager.Constants;
using jiangli.UI.Models;

namespace jiangli.UI.Repository.WebSite
{
    public class Rcase:BaseRcase
    {
        private static Rcase cases = new Rcase();
        public static Rcase Current
        {
            get { return cases; }
        }
        #region 对外接口
        /// <summary>f
        /// 针对网站的接口，获取某状态下的案件
        /// </summary>
        /// <param name="state"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<CasebriefViewModel> getCaselistBystate(CaseState state, int page)
        {
            int skipcount = (page - 1) * 20;
            //var currentlist = m_getPieceCaseByPageAndState(page, state);
            var currentlist = m_getCaseByPageState(page,10,state);
            return _getReviewList(currentlist);
        }
        /// <summary>
        /// 获取某用户所参与的案件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int GetLength(int uid)
        {
            return m_getLengthByUserId(uid);
        }
        /// <summary
        /// 创建一个案件
        /// </summary>
        /// <param name="model"></param>
        public void Create(CreateCaseViewModel model)
        {
            m_addItem(new Case
            {
                CaseOfSubmit = DateTime.Now,
                DateOfBegin = DateTime.Now,
                Respondent = model.respondent,
                ModeOfPay = ModePayState.PAY,
                originalpay = model.originalpay,
                Complainant = model.accuser,
                Publisher = model.issuer,
                StatementOfCase = model.description,
                Title = model.title,
                ComplainantId = model.accuserid,
                RespondentId = model.respondentid,
                PublisherId = model.accuserid,
                StateOfCase = model.state,
                Photo = model.imageurl,
            });
        }
        /// <summary>
        /// 获取申诉阶段下的案件列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<CasebriefViewModel> getCaselistByCondition(AppealState condition, int page)
        {
            int skipcount = (page - 1) * 20;
            //var currentlist = m_getPieceCaseByConditon(page, condition);
            var currentlist = m_getCaseByCondition(page, GlobalConfig.PageSize,condition);
            return _getReviewList(currentlist);
        }
        public DetailCaseViewModel GetDetailCaseById(int cid)
        {
            return _getDetailCaseById(cid);
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 内部方法 -- 将Case -- 转化为DetailCase
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        private DetailCaseViewModel _getDetailCaseById(int cid)
        {
            Case item = m_getCaseById(cid);
            if (item == null)
            {
                // 日志处理
                return null;
            }
            var accusor = Ruser.Current.getUserById(item.PublisherId);
            var respondent = Ruser.Current.getUserById(item.RespondentId);
            int numberComment = Rcomment.Current.Count(item.CaseID);

            var result = new DetailCaseViewModel
            {
                id = item.CaseID,
                description = item.StatementOfCase,
                involves = Rpart.Current.GetPartsByCaseId(item.CaseID),
                numberOfComment = Rcomment.Current.Count(item.CaseID),
                numberOfJoin = Rpart.Current.Count(item.CaseID),
                start_at = item.DateOfBegin.ToString(),
                state = Util.UtilFileType.swithState(item.StateOfCase),
                title = item.Title ?? "",
                comments = Rcomment.Current.GetCommentsByCaseId(item.CaseID, 1),
                originalpay = item.originalpay,
                accusor = accusor,
                condition = item.Condition,
                imageSrc = item.Photo ?? "",
                respondent = respondent,
                respondentturnone = item.TrunOneComplainByRespondent,
                respondentturntwo = item.TrunTwoComplainByRespondent,
                userturnone = item.TrunOneComplainByComplainant,
                userturntwo = item.TrunTwoComplainByComplainant,
                commentsStr = numberComment == 0 ? "评论" : numberComment + "条评论",
            };
            if(result.accusor == null)
            {
                return null;
            }
            if(result.respondent == null)
            {
                return null; 
            }
            return result;

        }
        /// <summary>
        /// 将cases接口转化为
        /// </summary>
        /// <param name="cases"></param>
        /// <returns></returns>
        private IEnumerable<CasebriefViewModel> _getReviewList(IEnumerable<Case> cases)
        {
            List<CasebriefViewModel> lists = new List<CasebriefViewModel>();
            try
            {
                foreach (var item in cases)
                {
                    MainCaseViewModel c = new MainCaseViewModel
                    {
                        description = item.StatementOfCase,
                        id = item.CaseID,
                        start_at = item.DateOfBegin.ToLongDateString(),
                        title = item.Title,
                        state = Util.UtilFileType.swithState(item.StateOfCase),
                        originalpay = item.originalpay,
                        numberOfComment = Rcomment.Current.Count(item.CaseID),
                        numberOfJoin = Rpart.Current.Count(item.CaseID),
                    };
                    lists.Add(new CasebriefViewModel()
                    {
                        basicAccusor = Ruser.Current.getUserById(item.PublisherId),
                        basicCase = c,
                        basicRespondent = Ruser.Current.getUserById(item.RespondentId),
                    });
                }
                return lists;
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
            
        }
        /// <summary>
        /// 内部方法--将Case -- BasicCase
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private BasicCaseViewModel _getBasicCaseViewModel(Case item)
        {
            return new BasicCaseViewModel
            {
                id = item.CaseID,
                description = item.StatementOfCase,
                numberOfComment = Rcomment.Current.Count(item.CaseID),
                numberOfJoin = Rpart.Current.Count(item.CaseID),
                start_at = item.DateOfBegin.ToString(),
                state = item.StateOfCase,
                title = item.Title,
            };
        }
        /// <summary>
        /// 内部方法-- 将Case -- MainCase
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private MainCaseViewModel _getMainCaseViewModel(Case item)
        {
            return new MainCaseViewModel
            {
                id = item.CaseID,
                description = item.StatementOfCase,
                numberOfComment = Rcomment.Current.Count(item.CaseID),
                numberOfJoin = Rpart.Current.Count(item.CaseID),
                start_at = item.DateOfBegin.ToString(),
                state = Util.UtilFileType.swithState(item.StateOfCase),
                title = item.Title,
                originalpay = item.originalpay,
            };
        }
        #endregion
    }
}