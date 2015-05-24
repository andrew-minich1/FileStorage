using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using DAL_Interface.Repository;
using DAL_Interface.DTO;
using ORM;
using System.Linq.Expressions;
using Helpers;


namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {

        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            this.context = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Include(u => u.Roles).Select(user => new DalUser()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Email = user.Email,
                    Password = user.Password,
                    CreationDate = user.CreationDate,
                    Roles = user.Roles.Select(r => new DalRole() { Id = r.Id, Name = r.Name }).ToList(),                    
                    Files = user.Files.Select(file => new DalFile()
                        {
                            Id = file.Id,
                            Name = file.Name,
                            IsDelete = file.IsDelete,
                            IsOpen = file.IsOpen,
                            DateCreated = file.DateCreated,
                            Path = file.Path,
                            UserId = file.UserId,
                            Size = file.Size,
                            Type = file.Type
                        }).ToList()
                });
        }

        public DalUser GetById(int key)
        {
            var ormuser = context.Set<User>().Include(u => u.Roles).FirstOrDefault(u => u.Id == key);
            return new DalUser()
            {
                Id = ormuser.Id,
                Login = ormuser.Login,
                Email = ormuser.Email,
                Password = ormuser.Password,
                CreationDate = ormuser.CreationDate,
                Roles = new List<DalRole>(ormuser.Roles.Select(role => new DalRole()
                {
                    Id = role.Id,
                    Name = role.Name
                })),
                Files = new List<DalFile>(ormuser.Files.Select(file => new DalFile()
                {
                    Id = file.Id,
                    Name = file.Name,
                    IsDelete = file.IsDelete,
                    IsOpen = file.IsOpen,
                    DateCreated = file.DateCreated,
                    Path = file.Path,
                    UserId = file.UserId,
                    Size = file.Size,
                    Type = file.Type
                }))
            };
        }

        public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var visitor = new MyExpressionVisitor<DalUser, User>(Expression.Parameter(typeof(User), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<User, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return context.Set<User>().Include(u => u.Roles).Where(exp2).Select(u => new DalUser()
            {
                Id = u.Id,
                Login = u.Login,
                Email = u.Email,
                Password = u.Password,
                CreationDate = u.CreationDate,
                Roles = u.Roles.Select(r => new DalRole() { Id = r.Id, Name = r.Name }).ToList(),
                Files = u.Files.Select(file => new DalFile()
                {
                    Id = file.Id,
                    Name = file.Name,
                    IsDelete = file.IsDelete,
                    IsOpen = file.IsOpen,
                    DateCreated = file.DateCreated,
                    Path = file.Path,
                    UserId = file.UserId,
                    Size = file.Size,
                    Type = file.Type
                }).ToList()
            });
        }

        public void Create(DalUser e)
        {
            var user = new User()
            {
                Id = e.Id,
                Login = e.Login,
                Email = e.Email,
                Password = e.Password,
                CreationDate = e.CreationDate,
            };
            user.Roles.Add(context.Set<Role>().Where(r => r.Name == "user").FirstOrDefault());
            context.Set<User>().Add(user);
        }

        public void Delete(DalUser e)
        {
            var user = new User()
            {
                Id = e.Id,
                Login = e.Login,
                Email = e.Email,
                Password = e.Password,
                CreationDate = e.CreationDate
            };
            context.Set<User>().Attach(user);
            context.Set<User>().Remove(user);
        }

        public void Update(DalUser e)
        {
            var user = new User()
            {
                Id = e.Id,
                Login = e.Login,
                Email = e.Email,
                Password = e.Password,
                CreationDate = e.CreationDate
            };
            context.Entry(user).State = EntityState.Modified;
        }

    }
}
