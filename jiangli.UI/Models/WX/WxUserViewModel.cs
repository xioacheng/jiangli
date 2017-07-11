using System.Collections.Generic;

namespace jiangli.UI.Models.WX
{
    /// <summary>
    /// 通过UserId 获取用户数据的返回类型
    ///     用户界面的基本显示信息
    ///     该用户所发布的所有案件列表
    /// </summary>
    public class WxUserViewModel
    {
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string name { get; set; }

        /// <summary>
        /// 简单介绍
        /// </summary>
        public string headline { get; set; }
        public string description { get; set; }
        public int gender { get; set; }
        /// <summary>
        /// 发起的案件数目
        /// </summary>
        public int case_number { get; set; }
        public int weight { get; set; }
        /// <summary>
        /// 该用户所发起的案件列表
        /// </summary>
        public IEnumerable<BasicCaseViewModel> caselist { get; set; }

     
        
    }
}