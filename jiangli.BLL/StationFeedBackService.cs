using jiangli.IBLL;
using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.BLL
{
    public class StationFeedBackService:BaseService<StationFeedBack>,IStationFeedBackService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepostory = DAL.RepositoryFactory.StationFeedBackRepository;
        }
    }
}
