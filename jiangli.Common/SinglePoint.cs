using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.Common
{
    public class SinglePoint
    {
        /// <summary>
        /// 通过单个用户评论获取该用户的满意度曲线
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public string GetSiaglePoint(string point)
        {
            WebClient wb = new WebClient();
            NameValueCollection PostVars = new NameValueCollection();
            PostVars.Add("points", point);
            PostVars.Add("curve_cnt", "21");
            PostVars.Add("apikey", "c014c68db505c3374e3dd3b0dc602f58");
            byte[] byRemoteInfo = wb.UploadValues("http://api.gping.cn/assignment/", "POST", PostVars);
            string sRemoteInfo = Encoding.Default.GetString(byRemoteInfo);
            return sRemoteInfo;
        }
    }
}
