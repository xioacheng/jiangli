using jiangli.Common;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.IBLL
{
    public interface IUserService:IBaseService<User>
    {
        LoginResult CheckUserInfo(User user);
        LoginResult ChectUserExit(string userName);
    }
}
