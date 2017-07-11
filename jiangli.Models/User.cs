using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Models
{
    /// <summary>
    /// 用户简单表
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户的账户---对外唯一
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 角色---int代表角色表的ID
        /// </summary>
        public int Role { get; set; }
        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 参与案件的数量
        /// </summary>
        public int NumberOfCase { get; set; }
        /// <summary>
        /// 权重的变化，int代表权重变化表的id
        /// </summary>
        public int PointOfWeigth { get; set; }
        /// <summary>
        /// 回收站删除
        ///     标记？
        /// </summary>
        public int DeleteRecycle { get; set; }
        /// <summary>
        /// 微信登陆的openid
        /// </summary>
        public string openid { get; set; }
    }
}