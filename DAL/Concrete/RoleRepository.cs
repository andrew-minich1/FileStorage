using System;
using System.Collections.Generic;
using System.Linq;
using DAL_Interface.Repository;
using DAL_Interface.DTO;
using ORM;
using System.Data.Entity;
using System.Linq.Expressions;
using Helpers;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {

        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            this.context = uow;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().Select(role => new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        public DalRole GetById(int key)
        {
            var ormrole = context.Set<Role>().FirstOrDefault(role => role.Id == key);
            return new DalRole()
            {
                Id = ormrole.Id,
                Name = ormrole.Name
            };
        }

        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> f)
        {
            var visitor = new MyExpressionVisitor<DalRole, Role>(Expression.Parameter(typeof(Role), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<Role, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return context.Set<Role>()
                .Where(exp2)
                .Select(r => new DalRole()
                {
                    Id = r.Id,
                    Name = r.Name
                });
        }

        public void Create(DalRole e)
        {
            var role = new Role()
            {
                Name = e.Name
            };
            context.Set<Role>().Add(role);
        }

        public void Delete(DalRole e)
        {
            var role = new Role()
            {
                Id = e.Id,
                Name = e.Name
            };
            context.Set<Role>().Attach(role);
            context.Set<Role>().Remove(role);
        }

        public void Update(DalRole e)
        {
            var role = new Role()
            {
                Id = e.Id,
                Name = e.Name

            };
            context.Entry(role).State = EntityState.Modified;
        }
    }
}
