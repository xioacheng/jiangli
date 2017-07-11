using jiangli.Manager.BaseRespository;
using jiangli.Manager.Constants;
using jiangli.Models;
using jiangli.Util;
using jiangli.UI.Models.WX;
using System;
using System.Collections.Generic;
using System.Linq;
namespace jiangli.Respository.Wx
{
    /// <summary>
    /// 参与案件裁决的仓库
    /// </summary>
    public class Rpart : BaseRpart
    {
        protected static Rpart parts = new Rpart();
        public static Rpart Current
        {
            get
            {
                return parts;
            }
        } 
        #region 对外借口

        //public 
        /// <summary>
        /// 通过裁决id获取相应的裁决
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WxInvolveViewModel GetPartById(int id)
        {
            return _transModel(m_getPartById(id));
        }

        /// <summary>
        /// 通过案件Id获得裁决列表
        /// </summary>
        /// <param name="id">案件id</param>
        /// <returns></returns>
        public IEnumerable<WxInvolveViewModel> GetPartsByCaseId(int id)
        {
            var parts = m_getPartByCid(id);
            List<WxInvolveViewModel> result = new List<WxInvolveViewModel>();
            foreach (var item in parts)
            {
                result.Add(_transModel(item));
            }
            return result;
        }
        public int GetPartIdByCaseAndUser(int cid, int uid)
        {
            var parts = m_getPartByCid(cid);
            int id = parts.Where(r => r.UserID == uid).FirstOrDefault().ID;
            return id;

        }
        /// <summary>
        /// 创建一个参与裁决
        /// </summary>
        /// <param name="model"></param>
        public void CreateInvolve(WxPostInvolveViewModel model)
        {
            m_addInvolve(_transReserveModel(model));
        }
        /// <summary>
        /// 修改一个参与裁决
        /// </summary>
        /// <param name="model"></param>
        public void ModifiedInvolve(WxPostInvolveViewModel model)
        {
            m_modifiedInvolve(_transReserveModel(model));
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
        /// 为获取裁决曲线生成标准点
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Point> GetStandardPoints(int pid)
        {
            Involve item = m_getPartById(pid);
            if(item == null)
            {
                /* 异常：未找到该裁决 */
                return null; 
            }
            /* 异常：该裁决方式为单点赋值，则不应有裁决曲线 */
            List<Point> list = new List<Point>(){
                new Point{
                    x = item.MostSatisfied,
                    y = 10,
                },
                new Point{
                    x = item.MostSatisfied * 2,
                    y = item.DoublePay,
                },
                new Point{
                    x = 0,
                    y = item.NonSatisfied,
                },
            };
            return list;
        }
        #endregion
        #region 内部使用方法
        protected Involve _transReserveModel(WxPostInvolveViewModel model)
        {
            return new Involve()
            {
                ID = model.id,
                UserID = model.userid,
                CaseId = model.caseid,
                DoublePay = model.doublePay,
                AssignmentExplain = model.assignmentExplain,
                ModeOfDot = model.modedot,
                ModeOfPay = model.modepay,
                ModifiedDate = DateTime.Now,
                MostSatisfied = model.mostSatisfied,
                NonSatisfied = model.nonSatisfied,
                ParticipateDate = DateTime.Now,
            };
        }
        protected WxInvolveViewModel _transModel(Involve item)
        {
            return new WxInvolveViewModel()
            {
                id = item.ID,
                assignmentExplain = item.AssignmentExplain,
                doublePay = item.DoublePay,
                ParticipateDate = item.ParticipateDate.ToShortDateString(),
                modepay = item.ModeOfPay,
                mostSatisfied = item.MostSatisfied,
                nonSatisfied = item.NonSatisfied,
                user = Ruser.Current.GetBasicUserById(item.UserID),
                modedot = item.ModeOfDot,
            };
        }
        #endregion

    }
}