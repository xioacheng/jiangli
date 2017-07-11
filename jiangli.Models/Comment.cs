using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Models
{
    /// <summary>
    /// 案件评论信息
    /// </summary>
    public class Comment
    {
        public int ID { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int CaseId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 评论者姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 评论日期
        /// </summary>
        public DateTime Date { get; set; }
    }
}