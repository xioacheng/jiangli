using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 角色的iD
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 创建角色用户的id
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 创建者用户名
        /// </summary>
        public string CreateByName { get; set; }
        /// <summary>
        /// 创建角色的日期
        /// </summary>
        public DateTime CreateOn { get; set; }
       /// <summary>
       /// 删除角色的用户id
       /// </summary>
        public int DeleteBy { get; set; }
        /// <summary>
        /// 删除者用户名
        /// </summary>
        public string DeleteByName { get; set; }
        /// <summary>
        /// 删除角色的日期
        /// </summary>
        public DateTime DeleteOn { get; set; }   
        /// <summary>
        /// 回收站删除
        /// </summary>
        public int DeleRecycle { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public int Option { get; set; }
    }
}