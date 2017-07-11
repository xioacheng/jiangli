using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Manager.Constants
{
    public class ModePayState
    {
        /// <summary>
        /// 互不赔偿
        /// </summary>
        public const int NONPAY = 0;
        /// <summary>
        /// 赔偿
        /// </summary>
        public const int PAY = 1;
    }
    public class ModeDotState
    {
        public const int SINGLE = 1;
        public const int THREE = 2;
    }
}