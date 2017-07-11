using jiangli.Common;
using jiangli.IBLL;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.BLL
{
    public class UserService:BaseService<User>,IUserService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepostory = DAL.RepositoryFactory.UserRepository;
        }
        /// <summary>
        /// 添加用户时候检查用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public LoginResult CheckUserInfo(User user)
        {
            if(string.IsNullOrEmpty(user.Account))
            {
                return LoginResult.UserIsNull;
            }
            if (string.IsNullOrEmpty(user.password))
            {
                return LoginResult.PwdError;
            }

            var temp = _DbSession.UserRepository.LoadEntities(u => u.Account == user.Account).FirstOrDefault();
            if (temp == null)
            {
                return LoginResult.UserNotExit;
            }
            if (user.password != temp.password)
            {
                return LoginResult.PwdError;
            }
            else
            {
                /*修改最后登录日期，返回登录成功标记*/
                var userinfo = _DbSession.UserInfoRepository.LoadEntities(t => t.UserID == temp.ID).FirstOrDefault();
                userinfo.DateOfLast = DateTime.Now;
                _DbSession.UserInfoRepository.UpdateEntity(userinfo);
                return LoginResult.OK;
            } 

        }
        /// <summary>
        /// 检查账户是否唯一
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public LoginResult ChectUserExit(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                return LoginResult.UserIsNull;
            }
            var checkUserName = _DbSession.UserRepository.LoadEntities(T=>T.Account==account).FirstOrDefault();
            if (checkUserName != null)
            {
                return LoginResult.UserNotExit;
            }
            else return LoginResult.OK;
        }
        /// <summary>
        /// 通过昵称关键字获取所有的用户信息
        /// </summary>
        /// <param name="NickName"></param>
        /// <returns></returns>
        public IQueryable<User> getUserByNickname(string NickName)
        {
            return CurrentRepostory.LoadEntities(t => (t.nickName.Contains(NickName))).AsQueryable();
        }
    }
}
