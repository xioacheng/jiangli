using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.IBLL
{
    public interface IBaseService<T> where T :class,new ()
    {
        T AddEntity(T entity);
        bool UpdateEntity(T entity);
        bool DeleteEntity(T entity);
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> wherelambda);
        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> wherelambda, bool isAsc, Expression<Func<T, S>> orderBylambda);
    }
}
