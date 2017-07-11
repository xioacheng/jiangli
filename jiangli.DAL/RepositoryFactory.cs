using jiangli.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.DAL
{
    public static class RepositoryFactory
    {
        public static IAppealCaseRepository AppealCaseRepository { get { return new AppealCaseRepository(); } }
        public static ICaseRepository CaseRepository { get { return new  CaseRepository(); } }
        public static ICommentOfCaseRepository CommentOfCaseRepository { get { return new CommentOfCaseRepository(); } }
        public static IExaminerConteRepository ExaminerConteRepository { get { return new ExaminerConteRepository(); } }
        public static IPartOfCaseRepository PartOfCaseRepository { get { return new PartOfCaseRepository(); } }
        public static IRoleRepository RoleRepository { get { return new RoleRepository(); } }
        public static IStationFeedBackRepository StationFeedBackRepository { get { return new StationFeedBackRepository(); } }
        public static IUserInfoRepository UserInfoRepository { get { return new UserInfoRepository(); } }
        public static IUserRepository UserRepository { get { return new UserRepository(); } }
        public static IUserRoleRepository UserRoleRepository { get { return new UserRoleRepository(); } }
        public static IPermitRepository PermitRepository { get { return new PermitRepository(); } }
        public static IRolePermitRepository RolePermitRepository { get { return new RolePermitRepository(); } }
    }
}
