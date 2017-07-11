using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Models
{
    /// <summary>
    /// 参与案件裁决表
    /// </summary>
    public class Involve
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 参与者Id
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 所属案件的ID -- 新增
        /// </summary>
        public int CaseId { get; set; }
        /// <summary>
        /// 赋值方式
        /// </summary>
        public int ModeOfDot { get; set; }
        /// <summary>
        /// 赔付方式
        /// </summary>
        public int ModeOfPay { get; set; }
        /// <summary>
        /// 最满意的数额
        /// </summary>
        public double MostSatisfied { get; set; }
        /// <summary>
        /// 不赔偿的满意度
        /// </summary>
        public double NonSatisfied { get; set; }
        /// <summary>
        /// 两倍赔偿的满意度
        /// </summary>
        public double DoublePay { get; set; }
        /// <summary>
        /// 赋值说明
        /// </summary>
        public string AssignmentExplain { get; set; }
        /// <summary>
        /// 参与评论日期
        /// </summary>
        public DateTime ParticipateDate { get; set; }
        /// <summary>
        /// 修改裁决日期
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}