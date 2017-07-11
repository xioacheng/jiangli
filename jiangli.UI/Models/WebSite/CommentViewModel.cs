using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.UI.Models.WebSite
{
    public class CommentViewModel
    {
        public int id { get; set; }
        public int caseid { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int uid { get; set; }
        public string username { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 被回复的用户id
        /// </summary>
        public int pid { get; set; }
        public string pusername { get; set; }
        /// <summary>
        /// 日期--时间
        /// </summary>
        public string created_at { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar_url { get; set; }
    }
}