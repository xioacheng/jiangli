using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Manager.Constants
{
    public class NotificationState
    {
        /// <summary>
        /// 被投诉 - 针对应诉人
        /// </summary>
        public static readonly int COMPLAINTED = 1;
        
        /// <summary>
        /// 案件状态变化--由管理员发布给案件
        /// </summary>
        public static readonly int CASESTATEMODIFIED= 2;
        /// <summary>
        /// 申诉类型的消息---当申诉开始时主动提醒应该进行申诉的一方
        /// </summary>
        public static readonly int APPEAL = 3;
    }
}