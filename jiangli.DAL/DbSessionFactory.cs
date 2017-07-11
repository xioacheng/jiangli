using jiangli.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.DAL
{
    public static class DbSessionFactory
    {
        public static IDbSession GetCurrentDbsession()
        {
            IDbSession _dbSession = CallContext.GetData("DbSession") as IDbSession;
            if (_dbSession == null)
            {
                _dbSession = new DbSession();
                CallContext.SetData("DbSession",_dbSession);
            }
            return _dbSession;
        }

    }
}
