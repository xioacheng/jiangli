using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.Common
{
    public enum LoginResult
    {
        PwdError,//密码错误
        UserNotExit,//用户不存在
        UserIsNull,//用户为空
        PwdIsNull,//密码为空
        OK,//登陆成功
        UserExit,//用户退出
    }
}
