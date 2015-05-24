using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Interface.Services
{
    public interface IService<TEntity> 
    {
        IEnumerable<TEntity> GetAllEntities();

        TEntity GetById(int id);

        TEntity GetOneByPredicate(Expression<Func<TEntity, bool>> predicates);

        IEnumerable<TEntity> GetAllByPredicate(Expression<Func<TEntity, bool>> predicates);

        void Create(TEntity entity);

        void Edit(TEntity entity);

        void Delete(TEntity entity);

    }
}
