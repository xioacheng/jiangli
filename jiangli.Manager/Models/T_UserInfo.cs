using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.Manager.Models
{
    public class T_UserInfo
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
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
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
