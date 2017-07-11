using jiangli.Manager.BaseRespository;
using jiangli.Models;
using jiangli.Models.Constants;
using jiangli.UI.Models.WebSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.UI.Repository.WebSite
{
    public class Rcomment:BaseRcomment
    {
        private Rcomment()
        {
            //Initdata();
        }
        private void Initdata()
        {

        }


        private static Rcomment Rc = new Rcomment();
        public static Rcomment Current
        {
            get
            {
                return Rc;
            }
        }


        #region 对外接口
        /// <summary>
        /// 获取某案件下的评论数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int Count(int cid)
        {
            return m_getCount(cid);
        }

        public IEnumerable<CommentViewModel> GetCommentsByCaseId(int cid, int page)
        {
            int OutNum = GlobalConfig.PageSize;
            var list = m_getCommentsByCaseid(cid, page,out OutNum);
            List<CommentViewModel> result = new List<CommentViewModel>();
            foreach (var item in list)
            {
                result.Add(_transformviewmodel(item));
            }
            return result;
        }

        #endregion
        /// <summary>
        /// 将基础模型类转化成微信所需的模型格式
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        private CommentViewModel _transformviewmodel(Comment comment)
        {
            return new CommentViewModel()
            {
                caseid = comment.CaseId,
                content = comment.Content,
                created_at = comment.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                id = comment.ID,
                pid = 0,
                pusername = "achang",
                uid = comment.UserID,
                username = comment.UserName,
                avatar_url = Ruser.Current.getUserById(comment.UserID).avatar_url,
            };
        }
    }
}