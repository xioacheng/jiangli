
using jiangli.Models.Constants;
namespace jiangli.UI.Models
{
    /// <summary>
    /// 案件的基本信息
    /// </summary>
    public class BasicCaseViewModel
    {
        public int id { get; set; }
        public string cover { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int numberOfComment { get; set; }
        public int numberOfJoin { get; set; }
        /// <summary>
        /// 案件开始的时间
        /// </summary>
        public string start_at { get; set; }
        /// <summary>
        /// 案件的状态
        /// </summary>
        public CaseState state { get; set; }
    }
    /// <summary>
    /// 用户的基本信息
    /// </summary>
    public class BasicUserViewModel
    {
        public int id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 头像的url
        /// </summary>
        public string avatar_url { get; set; }
        public int numbercase { get; set; }
        public int weight { get; set; }

    }
}