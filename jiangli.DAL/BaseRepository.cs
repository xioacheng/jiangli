using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.DAL
{
    public class BaseRepository<T> where T :class
    {
        //private jiangli.Models.DataContainerContext db = new jiangli.Models.DataContainerContext();
        protected DbContext db = EFContextFactory.GetCurrentDbContext();
        public T AddEntity(T entity)
        {
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            return entity;
        }
        public bool UpdateEntity(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return true;
        }
        public bool DeleteEntity(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return true;
        }
        public IQueryable<T> LoadEntities(Expression<Func<T,bool>> wherelamba)
        {
            return db.Set<T>().Where<T>(wherelamba).AsQueryable();
        }
        public IQueryable<T> LoagPageEntities<S>(int pageIndex,int pageSize,out int total,Expression<Func<T,bool>> wherelamba,bool isAsc,Expression<Func<T,S>> orderBylambda)
        {
            try
            {
                var temp = db.Set<T>().Where<T>(wherelamba);
                total = temp.Count();
                if (isAsc)
                {
                    temp = temp.OrderBy<T, S>(orderBylambda)
                        .Skip<T>(pageSize * (pageIndex - 1))
                        .Take<T>(pageSize).AsQueryable();
                }
                else
                {
                    temp = temp.OrderByDescending<T, S>(orderBylambda)
                        .Skip<T>(pageSize * (pageIndex - 1))
                        .Take<T>(pageSize).AsQueryable();
                }
                return temp.AsQueryable();
            }
            catch (Exception)
            {
                total = 0;
                return null;
                throw;
            }
            
        }
    }
}
