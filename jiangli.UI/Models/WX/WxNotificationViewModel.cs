﻿using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.UI.Models.WX
{
    public class WxNotificationViewModel : Notification
    {
        /// <summary>
        /// 申诉的A方
        /// </summary>
        public string speaker { get; set; }
        /// <summary>
        /// 申诉的B方
        /// </summary>
        public string receiver { get; set; }

    }
}