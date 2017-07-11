using jiangli.IBLL;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.BLL
{
    public class RoleService:BaseService<Role>,IRoleService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepostory = DAL.RepositoryFactory.RoleRepository;
        }
    }
}
