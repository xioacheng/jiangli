using jiangli.BLL;
using jiangli.Models;
using System.Linq;

namespace jiangli.Manager.BaseRespository
{
    public class BaseRcomment
    {
        protected CommentOfCaseService comments = new CommentOfCaseService();
        /// <summary>
        /// 通过案件id获取所有评论
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        protected IQueryable<Comment> m_getCommentsByCaseid(int cid, int page,out int sum)
        {
            return comments.LoadPageEntities(page,20,out sum,t=>t.CaseId==cid,false,t=>t.CaseId);
        }
        /// <summary>
        /// 创建一个评论
        /// </summary>
        /// <param name="comment"></param>
        protected Comment m_create(Comment comment)
        {
            return comments.AddEntity(comment);
        }
        /// <summary>
        /// 获取某案件下的评论数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        protected int m_getCount(int cid)
        {
            try
            {
                return comments.LoadEntities(r => r.CaseId == cid).Count();
            }
            catch (System.Exception)
            {
                return 0;
                throw;
            }
        }

    }
}
