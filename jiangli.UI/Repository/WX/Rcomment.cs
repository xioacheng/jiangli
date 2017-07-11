using jiangli.Manager.BaseRespository;
using jiangli.Models;
using jiangli.UI.Models.WX;
using System;
using System.Collections.Generic;

namespace jiangli.Respository.Wx
{
    public class Rcomment : BaseRcomment
    {
        private Rcomment()
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


        public int Count(int cid)
        {
            return m_getCount(cid);
        }
        public WxCommentViewModel Create(WxCommentViewModel model)
        {
            var result = m_create(new Comment()
            {
                CaseId = model.caseid,
                UserID = model.uid,
                UserName = model.username,
                Content = model.content,
                Date = DateTime.Now,
            });
            return _transformviewmodel(result);
        }
        /// <summary>
        /// 将基础模型类转化成微信所需的模型格式
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        private WxCommentViewModel _transformviewmodel(Comment comment)
        {
            return new WxCommentViewModel()
            {
                caseid = comment.CaseId,
                content = comment.Content,
                created_at = comment.Date.ToString(),
                id = comment.ID,
                pid = 0,
                pusername = "achang",
                uid = comment.UserID,
                username = comment.UserName,
                avatar_url = Ruser.Current.GetUserById(comment.UserID).avatar_url,
            };
        }
        public IEnumerable<WxCommentViewModel> GetCommentsByCaseId(int cid, int page)
        {
            int total = 0;
            var list = m_getCommentsByCaseid(cid, page,out total);
            List<WxCommentViewModel> result = new List<WxCommentViewModel>();
            foreach (var item in list)
            {
                result.Add(_transformviewmodel(item));
            }
            return result;
        }



    }
}