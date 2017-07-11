using jiangli.IDAL;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.DAL
{
    public class CaseRepository:BaseRepository<Case>,ICaseRepository
    {
        public IQueryable<Case> getCaseByWord()
        {
            return null;
        }
    }
}
