using jiangli.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace jiangli.DAL
{
    public class DbSession:IDbSession
    {
        public IAppealCaseRepository AppealCaseRepository { get { return new AppealCaseRepository(); } }
        public ICaseRepository CaseRepository { get { return new CaseRepository(); } }
        public ICommentOfCaseRepository CommentOfCaseRepository { get { return new CommentOfCaseRepository(); } }
        public IExaminerConteRepository ExaminerConteRepository { get { return new ExaminerConteRepository(); } }
        public IPartOfCaseRepository PartOfCaseRepository { get { return new PartOfCaseRepository(); } }
        public IRoleRepository RoleRepository { get { return new RoleRepository(); } }
        public IStationFeedBackRepository StationFeedBackRepository { get { return new StationFeedBackRepository(); } }
        public IUserInfoRepository UserInfoRepository { get { return new UserInfoRepository(); } }
        public IUserRepository UserRepository { get { return new UserRepository(); } }
        public IUserRoleRepository UserRoleRepository { get { return new UserRoleRepository(); } }
        public IPermitRepository PermitRepository { get { return new PermitRepository(); } }
        public IRolePermitRepository RolePermitRepository { get { return new RolePermitRepository(); } }
        public int SaveChanges()
        {
            return DAL.EFContextFactory.GetCurrentDbContext().SaveChanges();
        }
        public int ExecuteSql(string strSql, DbParameter[] parameters)
        {
            return DAL.EFContextFactory.GetCurrentDbContext().Database.ExecuteSqlCommand(strSql,parameters);
            //throw new NotImplementedException();
        }

        public IQueryable<T> ExecuteSql<T>(string strSql, DbParameter[] parameters)
        {
            return DAL.EFContextFactory.GetCurrentDbContext().Database.SqlQuery<T>(strSql, parameters).AsQueryable();
        }

        public IQueryable<T> ExecuteSql<T>(string strSql)
        {
            return DAL.EFContextFactory.GetCurrentDbContext().Database.SqlQuery<T>(strSql).AsQueryable();
        }
    }
}
