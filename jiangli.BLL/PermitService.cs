using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jiangli.IBLL;

namespace jiangli.BLL
{
    public class PermitService:BaseService<Permit>,IPermitService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepostory = DAL.RepositoryFactory.PermitRepository;
        }
    }
}
