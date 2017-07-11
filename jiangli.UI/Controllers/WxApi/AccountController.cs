using jiangli.Models.Constants;
using jiangli.Respository.Wx;
using jiangli.Util;
using jiangli.UI.Models;
using jiangli.UI.Models.WX;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace jiangli.Controllers
{
    /// <summary>
    /// 微信登录服务，提供登录和检查登录的接口
    /// </summary>
    public class AccountController : ApiController
    {
        private ILog Ilog = LogHelper.GetInstance();
        /// <summary>
        /// 微信小程序的登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public WxResultLoginViewModel Login(WxLoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                Ilog.Error("登陆信息不全");
                return null;
            }
            if (model == null)
            {
                Ilog.Debug("登录信息不全");
                return null;
            }
            LoginResult result = null;
            try
            {
                string code = GetHeader(ConstantLogin.WX_HEADER_CODE);
                string iv = GetHeader(ConstantLogin.WX_HEADER_IV);
                string encrytedData = GetHeader(ConstantLogin.WX_HEADER_ENCRYPTED_DATA);
                result = GetUsersHelper.GetOpenId(code);
                if (result.Openid == null)
                {
                    Ilog.Error("Openid请求失败");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Ilog.Error(ex.Message);
                return null;
            }
            var json = PrepareResponseJsonDictionary();
            json["session"] = new { id = result.Openid, skey = result.Skey}; 
            // 暂时以openid为唯一标识
            return Ruser.Current.Login(model, result.Openid);
        }
        /// <summary>
        /// 通过用户id获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public WxUserViewModel GetUserById(int userId)
        {
            return Ruser.Current.GetUserById(userId);
        }
        /// <summary>
        /// 通过昵称获取用户列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<BasicUserViewModel> GetUsersByNickname(string q)
        {
            return Ruser.Current.GetUsersByNickname(q);
        }

        #region 内部方法
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="headerName"></param>
        /// <returns></returns>
        [NonAction]
        private string GetHeader(string headerName)
        {
            var headerValue = Request.Headers.GetValues(headerName).FirstOrDefault();

            if (String.IsNullOrEmpty(headerValue))
            {
                Ilog.Debug("获取[" + headerName + "的请求头失败");
            }

            return headerValue;
        }
        /// <summary>
        /// 设置字典
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private IDictionary<string, object> PrepareResponseJsonDictionary()
        {
            var dictionary = new Dictionary<string, object>();
            dictionary[ConstantLogin.WX_SESSION_MAGIC_ID] = 1;
            return dictionary;
        }
        #endregion

    }
}