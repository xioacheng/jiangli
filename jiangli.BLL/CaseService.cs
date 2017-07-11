using jiangli.IBLL;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace jiangli.BLL
{
    public class CaseService:BaseService<Case>,ICaseService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepostory = DAL.RepositoryFactory.CaseRepository;
        }
        public IQueryable<Case> b_getCaseByKey(string word)
        { 
            return CurrentRepostory.LoadEntities(t=>(t.Title.Contains(word))||t.StatementOfCase.Contains(word)).OrderBy(t=>t.CaseID);
        }
    }
}
