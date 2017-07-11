using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jiangli.IDAL;
using jiangli.DAL;
using System.Linq.Expressions;

namespace jiangli.BLL
{
    public abstract class BaseService<T> where T :class,new ()
    {
        public IBaseRepository<T> CurrentRepostory { get; set; }
        //public DbSession _DbSession = new DbSession();
        public IDbSession _DbSession = DbSessionFactory.GetCurrentDbsession();
        public BaseService()
        {
            SetCurrentRepository();
        }
        public abstract void SetCurrentRepository();
        public T AddEntity(T entity)
        {
            var AddEntity =  CurrentRepostory.AddEntity(entity);
            _DbSession.SaveChanges();
            return AddEntity;
        }
        public bool UpdateEntity(T entity)
        {
            CurrentRepostory.UpdateEntity(entity);
            return _DbSession.SaveChanges() > 0;
        }
        public bool DeleteEntity(T entity)
        {
            CurrentRepostory.DeleteEntity(entity);
            return _DbSession.SaveChanges()>0; 
        }
        public IQueryable<T> LoadEntities(Expression<Func<T,bool>> wherelambda)
        {
            return CurrentRepostory.LoadEntities(wherelambda);
        }
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total,Expression<Func<T,bool>> wherelambda,bool isAsc,Expression<Func<T,S>> orderBylambda)
        {
            return CurrentRepostory.LoagPageEntities<S>(pageIndex,pageSize,out total,wherelambda,isAsc,orderBylambda); 
        }
    }
}
