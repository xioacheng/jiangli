using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jiangli.BLL;
using jiangli.Manager.Constants;
using jiangli.Models.Constants;

namespace jiangli.Manager.BaseRespository
{
    public class BaseRcase
    {
        protected CaseService caseinfo = new CaseService();

        private DataContainerContext db = new DataContainerContext();

        /// <summary>
        /// 通过案件id获取案件的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Case m_getCaseById(int id)
        {
            return caseinfo.LoadEntities(t => t.CaseID == id).FirstOrDefault();
        }
        /// <summary>
        /// 通过用户id查看自己发布的所有的案件
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IQueryable<Case> m_getCasesByUserid(int userid)
        {
            return caseinfo.LoadEntities(t => t.PublisherId == userid || t.RespondentId == userid);
        }
        /// <summary>
        /// 通过用户id获取自己发布的全部案件列表总数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int m_getLengthByUserId(int uid)
        {
            try {
                return caseinfo.LoadEntities(t => t.PublisherId == uid).Count();
            }catch(Exception e)
            {
                return 0;
            }
        }
        /// <summary>
        /// 创建一个案件
        /// </summary>
        /// <param name="c"></param>
        public void m_addItem(Case c)
        {
            caseinfo.AddEntity(c);
        }
        /// <summary>
        /// 修改一个案件
        /// </summary>
        /// <param name="c"></param>
        public void m_modifiedItem(Case c)
        {
            Case item = m_getCaseById(c.CaseID);
            if (item == null)
            {
                return;
            }
            item.Title = c.Title;
            item.originalpay = c.originalpay;
            item.StatementOfCase = c.StatementOfCase;
            item.Photo = c.Photo;
            item.StateOfCase = c.StateOfCase;

            caseinfo.UpdateEntity(item);
        }
        /// <summary>
        /// 修改案件的状态
        /// </summary>
        /// <param name="cid">案件id</param>
        /// <param name="state">案件状态</param>
        public bool m_modifiedState(int cid, CaseState state)
        {
            try
            {
                var c = m_getCaseById(cid);
                c.StateOfCase = state;
                if (state == CaseState.COMPLAIN)
                {
                    c.Condition = AppealState.RespondentToOne;
                }
                return caseinfo.UpdateEntity(c);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }

        /// <summary>
        /// 修改应诉人第一轮申诉 - 数据库
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="msg"></param>
        protected void m_modifiedAppealTurn1(int cid, string msg)
        {
            var item = m_getCaseById(cid);

            item.TrunOneComplainByRespondent = msg;
            item.Condition = AppealState.AccuserOne;
            caseinfo.UpdateEntity(item);
        }
        /// <summary>
        /// 修改投诉人第一轮申诉 - 数据库
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="msg"></param>
        protected void m_modifiedAppealTurn2(int cid, string msg)
        {
            var item = m_getCaseById(cid);
            item.TrunOneComplainByComplainant = msg;
            item.Condition = AppealState.RespondentToTwo;
            caseinfo.UpdateEntity(item);

        }
        /// <summary>
        /// 修改应诉人第二轮申诉 - 数据库
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="msg"></param>
        protected void m_modifiedAppealTurn3(int cid, string msg)
        {
            var item = m_getCaseById(cid);

            item.TrunTwoComplainByRespondent = msg;
            item.Condition = AppealState.AccuserTwo;
            caseinfo.UpdateEntity(item);

        }

        /// <summary>
        /// 修改投诉人第二轮申诉 - 数据库
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="msg"></param>
        protected void m_modifiedAppealTurn4(int cid, string msg)
        {
            var item = m_getCaseById(cid);
            item.TrunTwoComplainByComplainant = msg;
            item.Condition = AppealState.Finish;
            caseinfo.UpdateEntity(item);
        }
        /// <summary>
        /// 通过案件状态和页数获取某一页案件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected IQueryable<Case> m_getCasesByStateAndPage(int page, CaseState state)
        {
            int getcount = 0;
            int skipcount = (page - 1) * CaseConfig.PageLimit;
            return caseinfo.LoadPageEntities(page, 20, out getcount, r => r.StateOfCase == state, false,r=>r.CaseID);
        }
        /// <summary>
        /// 通过
        /// </summary>
        /// <returns></returns>
        protected IQueryable<Case> m_getCasesByKeyWord(string word,int page,CaseState state)
        {
            //return caseinfo.b_getCaseByKey(word);
            int skipcount = (page - 1) * 20;
            return state == CaseState.ALL ?
                caseinfo.LoadEntities(r => r.Title.Contains(word)).Skip(skipcount).Take(20).AsQueryable() :
                caseinfo.LoadEntities(r => r.StateOfCase == state && r.Title.Contains(word)).Skip(skipcount).Take(20).AsQueryable();
        }
        /// <summary>
        /// 通过用户id和案件状态值查看用户的案件
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IQueryable<Case> stateCaseInfo(int userid, CaseState state)
        {
            return caseinfo.LoadEntities(t => t.PublisherId == userid && t.StateOfCase == state);
        }
        /// <summary>
        /// 获取特定状态的一页案件
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数据的条数</param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IQueryable<Case> m_getCaseByPageState(int pageIndex,int pageSize,CaseState state)
        {
            try
            {
                int total = 10;
                var ICase = caseinfo.LoadPageEntities(pageIndex, pageSize, out total, t => t.StateOfCase == state, false, t => t.CaseID);
                return ICase.AsQueryable();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            
        }
        /// <summary>
        /// 通过关键词查找特定状态的一页案件
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="state"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IQueryable<Case> m_getStateCaseByWord(int pageIndex,int pageSize,out int total,CaseState state,string keyWord)
        {
            if (state == 0)
            {
                var result = caseinfo.LoadPageEntities(pageIndex, pageSize, out total,
                t => t.Title.Contains(keyWord) || t.StatementOfCase.Contains(keyWord) || t.Publisher.Contains(keyWord) || t.Respondent.Contains(keyWord) || t.Complainant.Contains(keyWord),
                false, t => t.CaseID);
                total = result.Count();
                return result.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                var result = caseinfo.LoadPageEntities(pageIndex, pageSize, out total, t => t.StateOfCase == state && (t.Title.Contains(keyWord) || t.StatementOfCase.Contains(keyWord) || t.Publisher.Contains(keyWord) || t.Respondent.Contains(keyWord) || t.Complainant.Contains(keyWord)),
                false, t => t.CaseID);
                total = result.Count();
                return result.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }
        /// <summary>
        /// 通过condition获取一页案件
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQueryable<Case> m_getCaseByCondition(int pageIndex,int pageSize, AppealState condition)
        {
            int total = 10;
            return caseinfo.LoadPageEntities(pageIndex,pageSize,out total,t=>t.Condition==condition,false,t=>t.CaseID);
        }
        /// <summary>
        /// 获取数据库总全部的案件信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<Case> m_getAllCase(int pageIndex,int pageSize,out int total)
        {
            return caseinfo.LoadPageEntities(pageIndex, pageSize, out total, t => t.CaseID != 0, true, t => t.CaseID);
        }
        /// <summary>
        /// 通过案件id删除案件，包括删除和伪删除
        /// </summary>
        /// <param name="id">删除案件的id</param>
        /// <param name="isTrue">是否是伪删除</param>
        /// <returns></returns>
        public bool m_deleteCase(int id,bool isTrue)
        {
            Case temp = caseinfo.LoadEntities(t => t.CaseID == id).FirstOrDefault();
            if (isTrue == true)
            {
                temp.CaseID = id;
                return caseinfo.DeleteEntity(temp);
            }
            else
            {
                temp.CaseID = id;
                temp.DeleteRecycle = 1;
                return caseinfo.UpdateEntity(temp);
            }
        }
        public void ModifiedState(int cid, CaseState state)
        {
            m_modifiedState(cid, state);
        }
    }
}
