using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jiangli.Models
{
    /// <summary>
    /// 用户详细表
    /// </summary>
    public class UserInfo
    {
        [Key]
        public int UserID { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDcard { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string cellPhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 性别
        ///     ？
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        public string Hobby { get; set; }
        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// 最后登陆日期
        /// </summary>
        public DateTime DateOfLast { get; set; }
        /// <summary>
        /// 自我介绍
        /// </summary>
        public string SelfIntroduction { get; set; }
        /// <summary>
        /// 微信头像
        /// </summary>
        public string Avatar_url { get; set; }
    }
}