using DAL_Interface.Repository;
using DAL_Interface.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using Helpers;

namespace DAL.Concrete
{
    public class FileRepository : IFileRepository
    {
        private readonly DbContext context;

        public FileRepository(DbContext uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            this.context = uow;
        }

        public IEnumerable<DalFile> GetAll()
        {
            return context.Set<File>().Select(file => new DalFile()
            {
                Id = file.Id,
                Name = file.Name,
                IsDelete = file.IsDelete,
                IsOpen = file.IsOpen,
                UserId = file.UserId,
                DateCreated = file.DateCreated,
                Path = file.Path,
                Type = file.Type,
                Size = file.Size
            });
        }

        public DalFile GetById(int key)
        {
            var file = context.Set<File>().FirstOrDefault(f => f.Id == key);
            return new DalFile
            {
                Id = file.Id,
                Name = file.Name,
                IsDelete = file.IsDelete,
                IsOpen = file.IsOpen,
                UserId = file.UserId,
                DateCreated = file.DateCreated,
                Path = file.Path,
                Type = file.Type,
                Size = file.Size
            };
        }

        public DalFile GetOneByPredicate(Expression<Func<DalFile, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();          
        }

        public IEnumerable<DalFile> GetAllByPredicate(Expression<Func<DalFile, bool>> f)
        {
            var visitor = new MyExpressionVisitor<DalFile, File>(Expression.Parameter(typeof(File), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<File, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return context.Set<File>().Where(exp2).Select(file => new DalFile()
            {
                Id = file.Id,
                Name = file.Name,
                IsOpen = file.IsOpen,
                IsDelete = file.IsDelete,
                UserId = file.UserId,
                DateCreated = file.DateCreated,
                Path = file.Path,
                Type = file.Type,
                Size = file.Size
            });
        }

        public void Create(DalFile e)
        {
            var file = new File()
            {
                Id = e.Id,
                Name = e.Name,
                IsOpen = e.IsOpen,
                IsDelete = e.IsDelete,
                UserId = e.UserId,
                DateCreated = e.DateCreated,
                Path = e.Path,
                Type = e.Type,
                Size = e.Size
            };
            context.Set<File>().Add(file);
        }

        public void Delete(DalFile e)
        {
            var file = new File()
            {
                Id = e.Id,
                Name = e.Name,
                IsOpen = e.IsOpen,
                IsDelete = e.IsDelete,
                UserId = e.UserId,
                DateCreated = e.DateCreated,
                Path = e.Path,
                Type = e.Type,
                Size = e.Size
            };
            context.Set<File>().Attach(file);
            context.Set<File>().Remove(file);
        }

        public void Update(DalFile e)
        {
            var file = new File()
            {
                Id = e.Id,
                Name = e.Name,
                IsOpen = e.IsOpen,
                IsDelete = e.IsDelete,
                UserId = e.UserId,
                DateCreated = e.DateCreated,
                Path = e.Path,
                Type = e.Type,
                Size = e.Size
            };
            context.Entry(file).State = EntityState.Modified;
        }
    }
}
