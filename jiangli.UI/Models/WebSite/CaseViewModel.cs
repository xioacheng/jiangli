using jiangli.Models.Constants;
using jiangli.UI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace jiangli.UI.Models.WebSite
{
    /// <summary>
    /// 创建案件
    /// </summary>
    public class CreateCaseViewModel : BasicCaseViewModel
    {
        /// <summary>
        /// 投诉人
        /// </summary>
        [Required]
        public string accuser { get; set; }
        /// <summary>
        /// 投诉金额
        /// </summary>
        [Required]        
        public double originalpay { get; set; }
        /// <summary>
        /// 应诉人
        /// </summary>
        [Required]        
        public string respondent { get; set; }
        /// <summary>
        /// 发布者--应该与投诉人一样
        /// </summary>
        [Required]        
        public string issuer { get; set; }
        /// <summary>
        /// 投诉人id
        /// </summary>
        [Required]
        public int accuserid { get; set; }
        /// <summary>
        /// 应诉人id
        /// </summary>
        [Required]
        public int respondentid { get; set; }
        /// <summary>
        /// 取证图片地址
        /// </summary>
        public string imageurl { get; set; }
        /// <summary>
        /// 取证视频地址
        /// </summary>
        public string vidiourl { get; set; }
    }
    /// <summary>
    /// 首页显示的卡片
    /// </summary>
    public class CasebriefViewModel
    {
        public MainCaseViewModel basicCase { get; set; }
        public BasicUserViewModel basicAccusor { get; set; }
        public BasicUserViewModel basicRespondent { get; set; }
    }
    /// <summary>
    /// 首页显示案件基类
    /// </summary>
    public class MainCaseViewModel : BasicCaseViewModel
    {
        public new string state { get; set; }
        public double originalpay { get; set; }
    }
    /// <summary>
    /// 案件详细信息
    /// </summary>
    public class DetailCaseViewModel:MainCaseViewModel
    { 
        /* 案件发起人 */
        public BasicUserViewModel accusor { get; set; }
        /* 案件应诉人 */
        public BasicUserViewModel respondent { get; set; }
        /* 案件下的裁决 */
        public IEnumerable<InvolveViewModel> involves { get; set; }
        /* 案件下的评论 */
        public IEnumerable<CommentViewModel> comments { get; set; }
        /* 案件新增信息 */
        /// <summary>
        /// 投诉人第一轮申诉
        /// </summary>
        public string userturnone { get; set; }
        /// <summary>
        /// 应诉人第一轮申诉
        /// </summary>
        public string respondentturnone { get; set; }
        /// <summary>
        /// 投诉人第二轮申诉
        /// </summary>
        public string userturntwo { get; set; }
        /// <summary>
        /// 应诉人第二轮申诉
        /// </summary>
        public string respondentturntwo { get; set; }
        /// <summary>
        /// 申诉阶段的标记
        /// </summary>
        public AppealState condition { get; set; }
        /// <summary>
        /// 举证照片的url
        /// </summary>
        public string imageSrc { get; set; }
        /// <summary>
        /// 评论部分内容
        /// </summary>
        public string commentsStr { get; set; }
    }
}