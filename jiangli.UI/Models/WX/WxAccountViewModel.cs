using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.UI.Models.WX
{
    /// <summary>
    /// 微信登陆的用户基本信息
    /// </summary>
    public class WxLoginViewModel
    {
        public string nickName { get; set; }
        public int gender { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }

    }
    /// <summary>
    /// 微信登陆用户的返回信息
    /// </summary>
    public class WxResultLoginViewModel
    {
        public string openid { get; set; }
        public int userid { get; set; }
        
        /// <summary>
        /// 角色--
        /// </summary>
        public int role { get; set; }

    }
}