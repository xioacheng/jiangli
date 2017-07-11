using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.IDAL
{
    public interface IDbSession
    {
        IAppealCaseRepository AppealCaseRepository { get; }
        ICaseRepository CaseRepository { get; }
        ICommentOfCaseRepository CommentOfCaseRepository { get ;}
        IExaminerConteRepository ExaminerConteRepository { get ; }
        IPartOfCaseRepository PartOfCaseRepository { get ; }
        IRoleRepository RoleRepository { get ; }
        IStationFeedBackRepository StationFeedBackRepository { get ; }
        IUserInfoRepository UserInfoRepository { get ; }
        IUserRepository UserRepository { get ; }
        IUserRoleRepository UserRoleRepository { get ; }
        IPermitRepository PermitRepository { get; }
        IRolePermitRepository RolePermitRepository { get; }
        int SaveChanges();
        int ExecuteSql(string strSql, DbParameter[] parameters);
        IQueryable<T> ExecuteSql<T>(string strSql, DbParameter[] parameters);
        IQueryable<T> ExecuteSql<T>(string strSql);
    }
}
