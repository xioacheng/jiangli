namespace jiangli.UI.Models.WX
{
    /// <summary>
    /// 微信创建新的案件所需格式
    /// </summary>
    public class WxPostCaseViewModel : BasicCaseViewModel
    { 
        public string accuser { get; set; }
        public double originalpay { get; set; }
        public string respondent { get; set; }
        public string issuer { get; set; }
        public int accuserid { get; set; }
        public int respondentid { get; set; } 
        public string imageurl { get; set; }
    }
}