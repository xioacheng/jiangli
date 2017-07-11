using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jiangli.BLL;
using jiangli.Manager.Models;
using jiangli.Models;
using System.Linq.Expressions;
using jiangli.Common;
namespace jiangli.Manager.BaseRespository
{
    public class BaseRuser
    {
        private UserInfoService Dbuserinfo = new UserInfoService();
        private UserService Dbuser = new UserService();
        BaseRrole DbRole = new BaseRrole();
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public LoginResult LoginByAccount(string account,string password)
        {
            //var temp = Dbuser.LoadEntities(t=>t.Account == account).FirstOrDefault();
            //if (temp == null)
            //{
            //    return LoginResult.UserNotExit;
            //}
            //else
            //{
            //    var pwd = Dbuser.LoadEntities(t => t.Account == account).FirstOrDefault();
            //    if (pwd.password == null)
            //    {
            //        return LoginResult.PwdIsNull;
            //    }
            //    else if (pwd.password == password)
            //    {
            //        return LoginResult.OK;
            //    }
            //    else return LoginResult.PwdError;
            //}
            return Dbuser.CheckUserInfo(new User { Account=account,password=password});
        }
        /// <summary>
        /// 查询所有的用户信息
        /// </summary>
        /// <param name="pageIndx">页码</param>
        /// <param name="pageSize">每页的数据个数</param>
        /// <param name="total">总共的数据个数</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public IQueryable<T_UserInfo> m_getAllUser(int pageIndex,int pageSize,out int total,int role)
        {
            T_UserInfo tUser = new T_UserInfo();
            DataContainerContext db = new DataContainerContext();
            if (role == 0)
            {
                var linq = (from user in db.User
                            join userinfo in db.UserInfo
                            on user.ID equals userinfo.UserID
                            orderby user.ID
                            where user.Role != 0
                            select new T_UserInfo
                            {
                                ID = user.ID,
                                Account = user.Account,
                                password = user.password,
                                nickName = user.nickName,
                                Role = user.Role,
                                Weight = user.Weight,
                                NumberOfCase = user.NumberOfCase,
                                PointOfWeigth = user.PointOfWeigth,
                                DeleteRecycle = user.DeleteRecycle,
                                openid = user.openid,
                                IDcard = userinfo.IDcard,
                                Name = userinfo.Name,
                                cellPhone = userinfo.cellPhone,
                                Email = userinfo.Email,
                                Gender = userinfo.Gender,
                                Hobby = userinfo.Hobby,
                                BloodType = userinfo.BloodType,
                                RegisterDate = userinfo.RegisterDate,
                                DateOfLast = userinfo.DateOfLast,
                                SelfIntroduction = userinfo.SelfIntroduction,
                                Avatar_url = userinfo.Avatar_url
                            });
                total = linq.Count();
                var temp = new List<T_UserInfo>();
                var result = linq.Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                foreach(T_UserInfo item in result)
                {
                    item.RoleName = DbRole.m_getRoleNameById(item.Role);
                    temp.Add(item);
                }
                return temp.AsQueryable();
            }
            else
            {
                var linq = (from user in db.User
                            join userinfo in db.UserInfo
                            on user.ID equals userinfo.UserID
                            orderby user.ID
                            where user.Role == role
                            select new T_UserInfo
                            {
                                ID = user.ID,
                                Account = user.Account,
                                password = user.password,
                                nickName = user.nickName,
                                Role = user.Role,
                                Weight = user.Weight,
                                NumberOfCase = user.NumberOfCase,
                                PointOfWeigth = user.PointOfWeigth,
                                DeleteRecycle = user.DeleteRecycle,
                                openid = user.openid,
                                IDcard = userinfo.IDcard,
                                Name = userinfo.Name,
                                cellPhone = userinfo.cellPhone,
                                Email = userinfo.Email,
                                Gender = userinfo.Gender,
                                Hobby = userinfo.Hobby,
                                BloodType = userinfo.BloodType,
                                RegisterDate = userinfo.RegisterDate,
                                DateOfLast = userinfo.DateOfLast,
                                SelfIntroduction = userinfo.SelfIntroduction,
                                Avatar_url = userinfo.Avatar_url
                            });

                total = linq.Count();
                var result = linq.Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                var temp = new List<T_UserInfo>();
                foreach (T_UserInfo item in result)
                {
                    item.RoleName = DbRole.m_getRoleNameById(item.Role);
                    temp.Add(item);
                }
                return temp.AsQueryable();
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="TUser"></param>
        /// <returns></returns>
        public int Register(T_UserInfo user)
        {
            User Buser = new User
            {
                ID = user.ID,
                Account = user.Account,
                password = user.password,
                nickName = user.nickName,
                Role = user.Role,
                Weight = user.Weight,
                NumberOfCase = user.NumberOfCase,
                PointOfWeigth = user.PointOfWeigth,
                DeleteRecycle = user.DeleteRecycle,
                openid = user.openid,
            };
            try
            {
                Buser = Dbuser.AddEntity(Buser);
            }
            catch (Exception e)
            {
                return -1;
            }
            UserInfo BUserInfo = new UserInfo
            {
                UserID = Buser.ID,
                IDcard = user.IDcard,
                Name = user.Name,
                cellPhone = user.cellPhone,
                Email = user.Email,
                Gender = user.Gender,
                Hobby = user.Hobby,
                BloodType = user.BloodType,
                RegisterDate = user.RegisterDate,
                DateOfLast = user.DateOfLast,
                SelfIntroduction = user.SelfIntroduction,
                Avatar_url = user.Avatar_url
            };
            try
            {
                Dbuserinfo.AddEntity(BUserInfo);
                return 1;
            }
            catch (Exception)
            {
                return -2;
                throw;
            }
        }
        /// <summary>
        /// 通过关键词查找用户
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="word">包含昵称邮箱电话账户</param>
        /// <returns></returns>
        public IQueryable<T_UserInfo> m_searchByKeyWord(int pageIndex, int pageSize, out int total, string word)
        {
            T_UserInfo tUser = new T_UserInfo();
            DataContainerContext db = new DataContainerContext();
            
            var linq = (from user in db.User
                        join userinfo in db.UserInfo
                        on user.ID equals userinfo.UserID
                        orderby user.ID
                        where user.nickName.Contains(word)||user.Account.Contains(word)||userinfo.Email.Contains(word)||userinfo.cellPhone.Contains(word)
                        select new T_UserInfo
                        {
                            ID = user.ID,
                            Account = user.Account,
                            password = user.password,
                            nickName = user.nickName,
                            Role = user.Role,
                            Weight = user.Weight,
                            NumberOfCase = user.NumberOfCase,
                            PointOfWeigth = user.PointOfWeigth,
                            DeleteRecycle = user.DeleteRecycle,
                            openid = user.openid,
                            IDcard = userinfo.IDcard,
                            Name = userinfo.Name,
                            cellPhone = userinfo.cellPhone,
                            Email = userinfo.Email,
                            Gender = userinfo.Gender,
                            Hobby = userinfo.Hobby,
                            BloodType = userinfo.BloodType,
                            RegisterDate = userinfo.RegisterDate,
                            DateOfLast = userinfo.DateOfLast,
                            SelfIntroduction = userinfo.SelfIntroduction,
                            Avatar_url = userinfo.Avatar_url
                        });
            total = linq.Count();
            var result = linq.Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            var temp = new List<T_UserInfo>();
            foreach (T_UserInfo item in result)
            {
                item.RoleName = DbRole.m_getRoleNameById(item.Role);
                temp.Add(item);
            }
            return temp.AsQueryable();
        }
        public IQueryable<User> m_getUserByNickname(string word)
        {
            return Dbuser.LoadEntities(t=>t.nickName.Contains(word));
        }
        /// <summary>
        /// 通过id删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool m_deleteById(int id)
        {
            User tUser = new User { ID=id};
            UserInfo tUserinfo = new UserInfo { UserID=id};
            if (Dbuser.DeleteEntity(tUser) && Dbuserinfo.DeleteEntity(tUserinfo))
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// 通过id获取用户的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_UserInfo m_getAllinfoById(int id)
        {
            T_UserInfo tUser = new T_UserInfo();
            DataContainerContext db = new DataContainerContext();
            var linq = (from user in db.User
                        join userinfo in db.UserInfo
                        on user.ID equals userinfo.UserID
                        orderby user.ID
                        where user.ID==id
                        select new T_UserInfo
                        {
                            ID = user.ID,
                            Account = user.Account,
                            password = user.password,
                            nickName = user.nickName,
                            Role = user.Role,
                            Weight = user.Weight,
                            NumberOfCase = user.NumberOfCase,
                            PointOfWeigth = user.PointOfWeigth,
                            DeleteRecycle = user.DeleteRecycle,
                            openid = user.openid,
                            IDcard = userinfo.IDcard,
                            Name = userinfo.Name,
                            cellPhone = userinfo.cellPhone,
                            Email = userinfo.Email,
                            Gender = userinfo.Gender,
                            Hobby = userinfo.Hobby,
                            BloodType = userinfo.BloodType,
                            RegisterDate = userinfo.RegisterDate,
                            DateOfLast = userinfo.DateOfLast,
                            SelfIntroduction = userinfo.SelfIntroduction,
                            Avatar_url = userinfo.Avatar_url
                        });
            var result = linq.FirstOrDefault();
            result.RoleName = DbRole.m_getRoleNameById(result.Role);
            return result;
        }
        /// <summary>
        /// 通过用户id获取用户详信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo m_getUserinfoByid(int id)
        {
            return Dbuserinfo.LoadEntities(t => t.UserID == id).FirstOrDefault();
        }
        /// <summary>
        /// 通过账户查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User m_getUserByAccount(string account)
        {
            return Dbuser.LoadEntities(t => t.Account == account).FirstOrDefault();
        }
        public User m_getUserById(int id)
        {
            return Dbuser.LoadEntities(t => t.ID == id).FirstOrDefault();
        }
        public UserInfo m_getUserinfoById(int id)
        {
            return Dbuserinfo.LoadEntities(t => t.UserID == id).FirstOrDefault();
        }
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool m_modifyUserRole(int userid,int role)
        {
            User temp = Dbuser.LoadEntities(t=>t.ID==userid).FirstOrDefault();
            temp.Role = role;
            return Dbuser.UpdateEntity(temp);
        }
        /// <summary>
        /// 查看邮箱是否存在
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public bool m_checkEmail(string Email)
        {
            var temp = Dbuserinfo.LoadEntities(t => t.Email == Email).FirstOrDefault() == null ? true : false;
            return temp;
        }
        protected bool m_isHasUser(string openid)
        {
            return Dbuser.LoadEntities(t => t.openid == openid).FirstOrDefault()==null?false:true;
        }
        public User m_getUserByOpenid(string openid)
        {
            return Dbuser.LoadEntities(t=>t.openid==openid).FirstOrDefault();
        }
        public bool m_setLastLoginDate(int id)
        {
            return Dbuser.UpdateEntity(new User { ID=id});
        }
        public int m_registerbreif(User user,UserInfo userinfo)
        {
            try
            {
                int id = Dbuser.AddEntity(user).ID;
                userinfo.UserID = id;
                Dbuserinfo.AddEntity(userinfo);
                return id;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
            
        }
     }
    public class BaseRrole
    {
        private RoleService DbRole = new RoleService();
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool m_addRole(Role role)
        {
            try
            {
                DbRole.AddEntity(role);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool m_deleteRole(int id)
        {
            return DbRole.DeleteEntity(new Role { RoleID=id});
        }
        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <returns></returns>
        public IQueryable<Role> m_getAllRole()
        {
            return DbRole.LoadEntities(t=>t.RoleID!=-1);
        }
        /// <summary>
        /// 通过角色id获取角色名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string m_getRoleNameById(int id)
        {
            try
            {
                return DbRole.LoadEntities(t => t.RoleID == id).FirstOrDefault().RoleName;
            }
            catch (Exception)
            {
                return DbRole.LoadEntities(t=>t.RoleID == 1).FirstOrDefault().RoleName;
                throw;
            }
        }
    }
    public class BaseRpermit
    {
        private PermitService DbPermit = new PermitService();
        /// <summary>
        /// 添加controller和action
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool m_addPermit(Permit item)
        {
            try
            {
                DbPermit.AddEntity(item);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// 删除url
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool m_deletePermit(int id)
        {
            return DbPermit.DeleteEntity(new Permit { ID=id});
        }
        /// <summary>
        /// 获取所有的权限列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<Permit> m_getAllPermit()
        {
            return DbPermit.LoadEntities(t=>t.ID!=-1);
        }
        public Permit m_getPermitByConAction(string controller,string action)
        {
            return DbPermit.LoadEntities(t=>(t.Controller==controller)&&(t.Action==action)).FirstOrDefault();
        }

    }
    public class BaseRrole_permit
    {
        private RolePermitService DbRP = new RolePermitService();
        /// <summary>
        /// 为用户增加权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="permitID"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool m_addRole_Permit(int roleID,int permitID,int userid)
        {
            try
            {
                var temp = DbRP.LoadEntities(t => (t.RoleID == roleID) && (t.PermitID == permitID));
                int a = temp.Count();
                if (a == 0)
                {
                    DbRP.AddEntity(new RolePermit { RoleID = roleID, PermitID = permitID, CreateBy = userid, CreateOn = DateTime.Now });
                    
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool m_deleteRole_Permit(int id)
        {
            return DbRP.DeleteEntity(new RolePermit { id=id});
        }
        /// <summary>
        /// 通过角色id将用户的所有权限删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool m_deletePermitByRoleid(int id)
        {
            return DbRP.deleteByRoleid(id);
        }
        /// <summary>
        /// 查询角色是否拥有权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public bool m_searchRP(int roleid,int permitid)
        {
            return DbRP.LoadEntities(t => (t.RoleID == roleid) && t.PermitID == permitid).FirstOrDefault() == null ? false : true;
        }
        /// <summary>
        /// 通过角色id获取角色的所有权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<string> m_getPermitByRoleid(int id)
        {
            DataContainerContext db = new DataContainerContext();
            var temp = from rolepermit in db.RolePermit
                       join
permit in db.Permit
on rolepermit.PermitID equals permit.ID
                       where rolepermit.RoleID == id
                       select permit.PermitName;
            return temp;
        }
    }
}
