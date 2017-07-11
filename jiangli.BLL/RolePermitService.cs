using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jiangli.IBLL;

namespace jiangli.BLL
{
    public class RolePermitService:BaseService<RolePermit>,IRolePermitService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepostory = DAL.RepositoryFactory.RolePermitRepository;
        }
        public bool deleteByRoleid(int id)
        {
            var temp = CurrentRepostory.LoadEntities(t=>t.RoleID==id);
            foreach(var item in temp)
            {
                CurrentRepostory.DeleteEntity(item);
            }
            _DbSession.SaveChanges();
            return true;
        }
    }
}
