using BLL_Interface.Services;
using DAL_Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Mappers;
using BLL_Interface.Entities;
using System.Web.Helpers;
using System.IO;
using System.Linq.Expressions;
using DAL_Interface.DTO;
using Helpers;

namespace BLL.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly string path;

        public UserService(IUnitOfWork uow, IUserRepository userRepository, IRoleRepository roleRepository, string path)
        {
            this.uow = uow;
            this.userRepository = userRepository;
            this.path = path;
            this.roleRepository = roleRepository;
        }

        public IEnumerable<UserEntity> GetAllEntities()
        {
            return userRepository.GetAll().Select(u => u.GetBllEntity());
        }

        public UserEntity GetById(int id)
        {
            return userRepository.GetById(id).GetBllEntity();
        }

        public IEnumerable<UserEntity> GetAllByPredicate( Expression<Func<UserEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return userRepository.GetAllByPredicate(exp2).Select(user => user.GetBllEntity());
        }

        public UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return userRepository.GetOneByPredicate(exp2).GetBllEntity();
        }

        public void Create(UserEntity user)
        {
            user.Password = Crypto.HashPassword(user.Password);
            user.CreationDate = DateTime.Now;
            user.Roles = new List<RoleEntity>{roleRepository.GetById(2).GetBllEntity()};
            userRepository.Create(user.GetDalEntity());
            if (!(Directory.Exists(path + user.Login)))
            {
                try
                {
                    Directory.CreateDirectory(path + user.Login);
                }
                catch (Exception error)
                {

                }
            }
            uow.Commit();
        }

        public void Edit(UserEntity entity)
        {
            userRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void Delete(UserEntity entity)
        {
            userRepository.Delete(entity.GetDalEntity());
            uow.Commit();
        }

    }
}
