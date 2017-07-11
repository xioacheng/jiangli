using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jiangli.Manager.BaseRespository;
using jiangli.UI.Models;
using jiangli.Models;

namespace jiangli.UI.Repository.WebSite
{
    public class Ruser:BaseRuser
    {
        private static Ruser users = new Ruser();
        public static Ruser Current
        {
            get { return users; }
        }

        public Ruser()
        {
        }

        /// <summary>
        /// 通过userid获取用户基础类
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public BasicUserViewModel getUserById(int uid)
        {
            User user = m_getUserById(uid);
            UserInfo userinfo = m_getUserinfoByid(uid);
            try
            {
                return new BasicUserViewModel()
                {
                    id = user.ID,
                    nickname = user.nickName,
                    weight = user.Weight,
                    numbercase = Rcase.Current.GetLength(uid),
                    avatar_url = userinfo.Avatar_url,

                };
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            
        }

    }
}