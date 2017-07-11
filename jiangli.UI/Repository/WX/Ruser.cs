using jiangli.Models.Constants;
using jiangli.Manager.BaseRespository;
using jiangli.Models;
using jiangli.UI.Models;
using jiangli.UI.Models.WX;
using System;
using System.Collections.Generic;
namespace jiangli.Respository.Wx
{
    public class Ruser : BaseRuser
    {
        private static Ruser users = new Ruser();
        public static Ruser Current
        {
            get { return users; }
        } 

        public Ruser()
        {
            
        }
        


        #region 对WebApi提供的接口
        /// <summary>
        /// 微信登陆接口，先判断是否存在，不存在就将用户信息放置在数据库中。
        /// </summary>
        /// <param name="model"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public WxResultLoginViewModel Login(WxLoginViewModel model, string openid)
        {
            int userid = 0;
            /*
             * 该openid用户是否已经注册 
             */
            if (m_isHasUser(openid))
            {
                userid = m_getUserByOpenid(openid).ID;
                m_setLastLoginDate(userid);
            }
            /*
             * 该openid未注册-则自动注册 
             */
            else
            {
                User user = new User()
                {
                    openid = openid,
                    nickName = model.nickName,
                    Weight = GlobalConfig.WEIGHT,
                };
                UserInfo userinfo = new UserInfo()
                {
                    Avatar_url = model.avatarUrl,
                    Gender = model.gender,
                    RegisterDate = DateTime.Now,
                    DateOfLast = DateTime.Now,
                };
                userid = m_registerbreif(user, userinfo);
            }
            return new WxResultLoginViewModel()
            {
                userid = userid,
                openid = openid,
            };

        }
        #endregion
        /// <summary>
        /// 通过用户获取用户的卡片信息-
        ///     卡片信息--简短信息。
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BasicUserViewModel GetBasicUserById(int userId)
        {
            User user = m_getUserById(userId);
            UserInfo userinfo = m_getUserinfoById(userId);
            return new BasicUserViewModel()
            {
                id = user.ID,
                nickname = user.nickName,
                avatar_url = userinfo.Avatar_url,
                numbercase = Rcase.Current.GetLength(userId),
                weight = user.Weight,
            };
        }
        /// <summary>
        /// 通过用户id获取用户的详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public WxUserViewModel GetUserById(int userId)
        {
            User user = m_getUserById(userId);
            UserInfo userinfo = m_getUserinfoById(userId);
            return new WxUserViewModel()
            {
                avatar_url = userinfo.Avatar_url,
                case_number = Rcase.Current.GetLength(userId),
                gender = userinfo.Gender,
                description = userinfo.SelfIntroduction,
                headline = userinfo.Hobby,
                id = userinfo.UserID,
                name = user.nickName,
                weight = user.Weight,
                caselist = Rcase.Current.getCaseByUserIdBreif(userId),
            };
        }
        /// <summary>f
        /// 通过昵称获取相关用户列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public IEnumerable<BasicUserViewModel> GetUsersByNickname(string q)
        {
            var currentusers = m_getUserByNickname(q);
            List<BasicUserViewModel> result = new List<BasicUserViewModel>();
            foreach (var item in currentusers)
            {
                var userinfo = m_getUserinfoById(item.ID);
                result.Add(new BasicUserViewModel()
                {
                    avatar_url = userinfo.Avatar_url,
                    id = userinfo.UserID,
                    nickname = item.nickName,
                    numbercase = Rcase.Current.GetLength(item.ID),
                    weight = item.Weight,
                });
            }
            return result;
        }
    }
}