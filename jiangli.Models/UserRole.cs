using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Models
{
    /// <summary>
    /// 用户-角色对应表
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 数据表唯一ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 用户角色关闭和打开
        /// </summary>
        public int Enable { get; set; }
        /// <summary>
        /// 创建角色的用户ID
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 角色创建的日期
        /// </summary>
        public DateTime CreateOn { get; set; }
        /// <summary>
        /// 修改角色的用户ID
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// 最后修改日期
        /// </summary>
        public int ModifiedOn { get; set; }
    }
}