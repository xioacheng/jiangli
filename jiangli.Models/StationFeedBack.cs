using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Models
{
    /// <summary>
    /// 网站意见反馈
    /// </summary>
    public class StationFeedBack
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 意见内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime Date { get; set; }
    }
}