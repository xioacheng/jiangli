
namespace jiangli.Models.Constants
{
    public class GlobalConfig
    {
        /// <summary>
        /// 权重
        /// </summary>
        public static readonly int WEIGHT = 100;
        /// <summary>
        /// appid
        /// </summary>
        public static readonly string appid = "wxc677329c2c8d9f54";
        //public static readonly string appid = "wx38aac816d132c99a";
        /// <summary>
        /// secret
        /// </summary>
        public static readonly string secret = "acf46e99bff243025564e34dabb01bef";
        //public static readonly string secret = "466e65a279b04e06fec42f3707fac91b";
        public static readonly string imgPath = "/LocalImgs";

        public static readonly string RequireApiKEY = "c014c68db505c3374e3dd3b0dc602f58";
        public static readonly int PageSize = 10;

    }
    public static class CaseConfig
    {
        /// <summary>
        /// 一页的案件数
        /// </summary>
        public static readonly int PageLimit = 20;
    }
    public static class ConstantLogin
    {
        public const string WX_SESSION_MAGIC_ID = "F2C224D4-2BCE-4C64-AF9F-A6D872000D1A";
        public const string WX_HEADER_CODE = "X-WX-Code";
        public const string WX_HEADER_ID = "X-WX-Id";
        public const string WX_HEADER_SKEY = "X-WX-Skey";
        public const string WX_HEADER_ENCRYPTED_DATA = "X-WX-Encrypted-Data";
        public const string WX_HEADER_IV = "X-WX-IV";
    }
}