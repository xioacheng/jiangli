using jiangli.BLL;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.Manager.BaseRespository
{
    /// <summary>
    /// 参与裁决的数据库类
    /// </summary>
    public class BaseRpart
    {
        protected PartOfCaseService Dbinvolves = new PartOfCaseService();

        /// <summary>
        /// 通过裁决Id获取裁决的基本信息 -- 数据库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Involve m_getPartById(int id)
        {
            return Dbinvolves.LoadEntities(r => r.ID == id).FirstOrDefault();
        }
        /// <summary>
        /// 通过案件id获取裁决的基本信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public IQueryable<Involve> m_getPartByCid(int cid)
        {
            return Dbinvolves.LoadEntities(r => r.CaseId == cid);
        }
        /// <summary>
        /// 创建一个参与裁决--数据库
        /// </summary>
        /// <param name="item"></param>
        public void m_addInvolve(Involve item)
        { 
            Dbinvolves.AddEntity(item);
        }
        /// <summary>
        /// 修改一个参与裁决-- 数据库
        /// </summary>
        /// <param name="item"></param>
        public void m_modifiedInvolve(Involve item)
        {
            var temp = m_getPartById(item.ID);
            temp.ModifiedDate = item.ModifiedDate;
            temp.MostSatisfied = item.MostSatisfied;
            temp.ModeOfPay = item.ModeOfPay;
            temp.ModeOfDot = item.ModeOfDot;
            temp.AssignmentExplain = item.AssignmentExplain;
            temp.DoublePay = item.DoublePay;
            temp.NonSatisfied = item.NonSatisfied;
            Dbinvolves.UpdateEntity(temp);
        }
        /// <summary>
        /// 某案件下的裁决个数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int m_count(int cid)
        {
            try
            {
                int count = Dbinvolves.LoadEntities(r => r.CaseId == cid).Count();
                return count;
            }catch(Exception e)
            {
                return 0;
            }
        }
    }
}
