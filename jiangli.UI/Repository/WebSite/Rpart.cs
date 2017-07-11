using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jiangli.Manager.BaseRespository;
using jiangli.UI.Models.WebSite;
using jiangli.Models;

namespace jiangli.UI.Repository.WebSite
{
    public class Rpart:BaseRpart
    {
        protected static Rpart parts = new Rpart();
        public static Rpart Current
        {
            get
            {
                return parts;
            }
        }
        protected Rpart()
        {
        }

        /// <summary>
        /// 一个案件下的裁决个数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int Count(int cid)
        {
            return m_count(cid);
        }
        /// <summary>
        /// 通过案件Id获得裁决列表
        /// </summary>
        /// <param name="id">案件id</param>
        /// <returns></returns>
        public IEnumerable<InvolveViewModel> GetPartsByCaseId(int id)
        {
            var parts = m_getPartByCid(id);
            List<InvolveViewModel> result = new List<InvolveViewModel>();
            foreach (var item in parts)
            {
                result.Add(_transModel(item));
            }
            return result;
        }
        #region 内部使用方法

        protected InvolveViewModel _transModel(Involve item)
        {
            return new InvolveViewModel()
            {
                id = item.ID,
                assignmentExplain = item.AssignmentExplain ?? "无赋值描述",
                doublePay = item.DoublePay,
                ParticipateDate = item.ParticipateDate.ToLongDateString(),
                modepay = item.ModeOfPay,
                mostSatisfied = item.MostSatisfied,
                doubleMostSatisfied = item.MostSatisfied * 2,
                nonSatisfied = item.NonSatisfied,
                user = Ruser.Current.getUserById(item.UserID),
                modedot = item.ModeOfDot,
            };
        }
        #endregion
    }
}