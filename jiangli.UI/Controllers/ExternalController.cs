using System.Collections.Specialized;
using System.Net;
using System.Web.Http;
using jiangli.Models.Constants;
using System.Collections.Generic;
using System.Web.Script.Serialization;
namespace jiangli.Controllers
{
    using jiangli.Util;
    using jiangli.Respository.Wx;
    using System;
    /// <summary>
    /// 外部函数控制器
    /// </summary>
    public class ExternalController : ApiController
    {
        /// <summary>
        /// 通过参与裁决的id获取该曲线点
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet]
        public string getSinglePoints(int pid)
        {
            WebClient wb = new WebClient();
            NameValueCollection postValues = new NameValueCollection();

            /* 获取点集合 */
            var list = Rpart.Current.GetStandardPoints(pid);

            postValues.Add("points", list.ToListString());
            //postValues.Add("points", "[[0,5],[100,10],[200,8]]");
            postValues.Add("curve_cnt", "21");
            postValues.Add("apikey", GlobalConfig.RequireApiKEY);
            byte[] byRemoteInfo = wb.UploadValues("http://api.gping.cn/assignment/", "POST", postValues);
            string sRemouteInfo = System.Text.Encoding.Default.GetString(byRemoteInfo);

            return sRemouteInfo;
        }
        

    }
}
