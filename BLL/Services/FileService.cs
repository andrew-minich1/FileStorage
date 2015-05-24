using BLL_Interface.Entities;
using BLL_Interface.Services;
using DAL_Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Mappers;
using System.Linq.Expressions;
using DAL_Interface.DTO;
using Helpers;
using System.IO;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork uow;

        private readonly IFileRepository fileRepository;

        private string path;

        public FileService(IUnitOfWork uow, IFileRepository fileRepository, string path)
        {
            this.uow = uow;
            this.fileRepository = fileRepository;
            this.path = path;
        }

        public IEnumerable<FileEntity> GetAllEntities()
        {
            return fileRepository.GetAll().Select(file => file.GetBllEntity());
        }

        public FileEntity GetById(int id)
        {
            return fileRepository.GetById(id).GetBllEntity();
        }

        public FileEntity GetOneByPredicate(Expression<Func<FileEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<FileEntity, DalFile>(Expression.Parameter(typeof(DalFile), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalFile, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return fileRepository.GetOneByPredicate(exp2).GetBllEntity();
        }

        public IEnumerable<FileEntity> GetAllByPredicate(Expression<Func<FileEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<FileEntity, DalFile>(Expression.Parameter(typeof(DalFile), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalFile, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return fileRepository.GetAllByPredicate(exp2).Select(file => file.GetBllEntity());
        }

        public void Create(FileEntity entity)
        {
            if (File.Exists(entity.Path)) return;
            entity.IsDelete = false;
            entity.IsOpen = false;
            entity.DateCreated = DateTime.Now;
            entity.Size = Math.Round(entity.Size / 1024, 2);
            fileRepository.Create(entity.GetDalEntity());
            uow.Commit();
        }

        public void Restore(FileEntity entity)
        {
            entity.IsDelete = false;
            fileRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void MakeOpen(int id)
        {
            var file = fileRepository.GetOneByPredicate(f => f.Id == id);
            file.IsOpen = !(file.IsOpen);
            fileRepository.Update(file);
            uow.Commit();
        }

        public void Edit(FileEntity entity)
        {
            fileRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void Delete(FileEntity entity)
        {
            if (File.Exists(entity.Path))
                File.Delete(entity.Path);
            fileRepository.Delete(entity.GetDalEntity());
            uow.Commit();
        }

        public IEnumerable<FileEntity> SortFiles(IEnumerable<FileEntity> files, string sortOrder)
        {
            switch (sortOrder)
            {
                case "Name desc":
                    files = files.OrderByDescending(f => f.Name);
                    break;
                case "Date":
                    files = files.OrderBy(f => f.DateCreated);
                    break;
                case "Date desc":
                    files = files.OrderByDescending(f => f.DateCreated);
                    break;
                default:
                    files = files.OrderBy(f => f.Name);
                    break;
            }
            return files;
        }

        public void AddToCart(FileEntity entity)
        {

            entity.IsDelete = true;
            fileRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public string RenameFile(int id, string newName)
        {
            var file = fileRepository.GetById(id);
            string oldName = file.Name;
            var directory = Path.GetDirectoryName(file.Path);
            if (directory != null)
            {
                var newPath = Path.Combine(directory, newName + file.Type);
                try
                {
                    if (File.Exists(file.Path))
                        File.Move(file.Path, newPath);
                }
                catch
                {
                    return oldName;
                }
                file.Name = newName;
                file.Path = newPath;
                fileRepository.Update(file);
                uow.Commit();
                return newName;
            }
            return oldName;
        }
    }
}
