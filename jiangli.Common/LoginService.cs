using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace jiangli.Common
{
    public class LoginService
    {
        private HttpRequestBase Request;
        private HttpResponseBase Response;
        public LoginService(HttpRequestBase request,HttpResponseBase response)
        {
            if(request==null)
                throw new ArgumentNullException("request","初始化登录request不能为空");
            if (response == null)
            {
                throw new ArgumentNullException("response", "初始化登录response不能为空");
            }
            Request = request;
            Response = response;
        }
        public LoginService(HttpRequest request, HttpResponse response) : this(new HttpRequestWrapper(request),new HttpResponseWrapper(response)) { }
        public string GetCookia(string cookiaName)
        {
            var cookia = Request.Cookies[cookiaName].Value;
            if (cookia == null)
            {
                return null;
            }
            else
            {
                return cookia;
            }

        }
        private string GetHeader(string headerName)
        {
            var headerValue = Request.Headers[headerName];
            if (string.IsNullOrEmpty(headerValue)) return null;
            else return headerValue;
        }
    }
}
