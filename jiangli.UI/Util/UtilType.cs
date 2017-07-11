using jiangli.Models.Constants;

namespace jiangli.Util
{
    /// <summary>
    /// 文件类型帮助类
    /// </summary>
    public class UtilFileType
    {
        public static string switchType(string memitype)
        {
            if (memitype.Contains("bmp"))
            {
                return ".bmp";
            }
            else if (memitype.Contains("jpeg"))
            {
                return ".jpeg";
            }
            else if (memitype.Contains("png"))
            {
                return ".png";
            }
            else
            {
                return "error";
            }
        }
        /// <summary>
        /// 状态值和意思的转化
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string swithState(CaseState state)
        {
            switch(state)
            {
                case CaseState.COMPLAIN :
                    return "正在申诉";
                case CaseState.DRAFT :
                    return "草稿";
                case CaseState.FINISH :
                    return "已完成";
                case CaseState.PENDING :
                    return "待审核";
                case CaseState.PUBLISH :
                    return "已经发布";
                default :
                    return "error";
            }
        }
    }


}