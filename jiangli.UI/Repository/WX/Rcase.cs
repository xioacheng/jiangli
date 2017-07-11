using jiangli.Models.Constants;
using jiangli.Manager.BaseRespository; 
using jiangli.Models;
using jiangli.UI.Models;
using jiangli.UI.Models.WX;
using System;
using System.Collections.Generic;
using System.Linq;
using jiangli.Manager.Constants;
namespace jiangli.Respository.Wx
{
    /// <summary>
    /// 案件仓库类
    /// </summary>
    public class Rcase : BaseRcase
    {
        private static Rcase cases = new Rcase();
        public static Rcase Current
        {
            get { return cases; }
        } 

        public Rcase()
        { 
        } 
      

        #region 对微信的接口
        /// <summary>
        /// 获取某用户所参与的案件
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int GetLength(int cid)
        {
            return m_getLengthByUserId(cid);
        }
        /// <summary
        /// 创建一个案件
        /// </summary>
        /// <param name="model"></param>
        public void Create(WxPostCaseViewModel model)
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
        /// 通过页数和状态获取20条案件
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public IQueryable<WxCaseListItemViewModel> GetPieceByPagesAndState(int page, CaseState state)
        {
            int skipcount = (page - 1) * 20;
            var currentlist = m_getCaseByPageState(page,20, state);
            //var currentlist = list.Where(r => r.StateOfCase == state).Skip(skipcount).Take(20);
            return _getCardCaseByCases(currentlist).AsQueryable();

        }
        /// <summary>
        /// 获取建议的案件
        /// </summary>
        /// <param name="q">查询关键词</param>
        /// <param name="state">案件的状态</param>
        /// <returns></returns>
        public IQueryable<BasicCaseViewModel> GetPieceBySuggest(string q, CaseState state)
        {

            var currentlist = m_getCasesByKeyWord(q, 1, state);
            return _getBasicCaseList(currentlist.Take(5));
        }

        /// <summary>
        /// 搜索案件
        /// </summary>
        /// <param name="q"></param>
        /// <param name="state"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IQueryable<WxCaseListItemViewModel> GetPieceByPageAndWordAndState(string q, CaseState state, int page)
        {

            if (q == null)
            {
                return GetPieceByPagesAndState(page, state);
            }
            var currentlist = m_getCasesByKeyWord(q, page, state);
            return _getCardCaseByCases(currentlist).AsQueryable();
        }
        
        /// <summary>
        /// 修改案件
        /// </summary>
        /// <param name="model"></param>
        public void Modified(WxPostCaseViewModel model)
        {
            m_modifiedItem(new Case()
            {
                CaseID = model.id,
                Title = model.title,
                originalpay = model.originalpay,
                StatementOfCase = model.description,
                Photo = model.imageurl,
                StateOfCase = model.state,
            });
        }
        /// <summary>
        /// 通过案件id获取详细案件模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WxCaseDetailViewModel GetCaseDetailById(int id)
        {
            return _getCaseDetailById(id);
        }

        /// <summary>
        /// 获取用户少量的两个案件
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IQueryable<BasicCaseViewModel> getCaseByUserIdBreif(int userid)
        {
            return _getBasicCaseList(m_getCasesByUserid(userid).Take(2));
        }
        /// <summary>
        /// 获取用户所有的案件
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IQueryable<WxCaseListItemViewModel> getCaseListOfOwner(int userid)
        {
            return _getCardCaseByCases(m_getCasesByUserid(userid)).AsQueryable();
        }
        /// <summary>
        /// 修改申诉过程的信息
        /// </summary>
        /// <param name="cid">案件id</param>
        /// <param name="condition">阶段</param>
        /// <param name="msg">所修改的信息</param>
        public void setAppealMsg(int cid, string msg)
        {
            /* 优化：可以在一次打开数据库中将字段进行修改，减少一次打开数据库的开销 */
            /* 先通过cid获取condition阶段表征 */
            AppealState condition = m_getCaseById(cid).Condition;
            /* 根据阶段更换 */
            switch (condition)
            {
                case AppealState.RespondentToOne:
                    m_modifiedAppealTurn1(cid, msg);
                    break;
                case AppealState.AccuserOne:
                    m_modifiedAppealTurn2(cid, msg);
                    break;
                case AppealState.RespondentToTwo:
                    m_modifiedAppealTurn3(cid, msg);
                    break;
                case AppealState.AccuserTwo:
                    m_modifiedAppealTurn4(cid, msg);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 内部使用方法

        /// <summary>
        /// 通过caselist 获取传送给微信的数据格式
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<WxCaseListItemViewModel> _getCardCaseByCases(IQueryable<Case> list)
        {
            List<WxCaseListItemViewModel> cardCaseList = new List<WxCaseListItemViewModel>();
            foreach (var item in list)
            {

                cardCaseList.Add(new WxCaseListItemViewModel()
                {

                    basic = _getBasicCase(item),
                    author = Ruser.Current.GetBasicUserById(item.PublisherId),

                });
            }
            return cardCaseList;
        }
        /// <summary>
        /// 将原始模型case列表转化成微信的基本case列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private IQueryable<BasicCaseViewModel> _getBasicCaseList(IQueryable<Case> list)
        {
            List<BasicCaseViewModel> result = new List<BasicCaseViewModel>();
            foreach (var item in list)
            {
                result.Add(_getBasicCase(item));
            }
            return result.AsQueryable();
        }
        /// <summary>
        /// 将Case原始模型转换成微信所需的格式
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private BasicCaseViewModel _getBasicCase(Case c)
        {
            return new BasicCaseViewModel()
            {
                id = c.CaseID,
                cover = "",
                description = c.StatementOfCase,
                numberOfComment = Rcomment.Current.Count(c.CaseID),
                numberOfJoin = Rpart.Current.Count(c.CaseID),
                start_at = c.DateOfBegin.ToShortDateString(),
                state = c.StateOfCase,
                title = c.Title,
            };
        }
        /// <summary>
        /// 通过案件id获得返回微信格式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private WxCaseDetailViewModel _getCaseDetailById(int id)
        {
            Case caseinfo = m_getCaseById(id);
            var parts = Rpart.Current.GetPartsByCaseId(id);
            var respondent = Ruser.Current.GetBasicUserById(caseinfo.RespondentId);
            var owner = Ruser.Current.GetBasicUserById(caseinfo.PublisherId);
            return new WxCaseDetailViewModel()
            {
                user = owner,
                basic = new BasicCaseViewModel()
                {
                    description = caseinfo.StatementOfCase,
                    id = caseinfo.CaseID,
                    numberOfComment = Rcomment.Current.Count(id),
                    title = caseinfo.Title,
                    start_at = caseinfo.DateOfBegin.ToShortDateString(),
                    state = caseinfo.StateOfCase,
                    numberOfJoin = Rpart.Current.Count(id),

                },
                imageSrc = caseinfo.Photo,
                involves = parts,
                respondent = respondent,
                respondentturnone = caseinfo.TrunOneComplainByRespondent ?? "",
                respondentturntwo = caseinfo.TrunTwoComplainByRespondent ?? "",
                userturnone = caseinfo.TrunOneComplainByComplainant ?? "",
                userturntwo = caseinfo.TrunTwoComplainByComplainant ?? "",
                condition = caseinfo.Condition,
                orginalpay = caseinfo.originalpay,
            };
        }
        #endregion 
    }
}