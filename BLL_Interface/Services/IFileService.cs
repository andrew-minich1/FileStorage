using BLL_Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Interface.Services
{
    public interface IFileService : IService<FileEntity>
    {
        IEnumerable<FileEntity> SortFiles(IEnumerable<FileEntity> files, string sortOrder);
        void Restore(FileEntity entity);
        void MakeOpen(int id);
        void AddToCart(FileEntity entity);
        string RenameFile(int id, string newName);
    }
}
