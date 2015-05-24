using BLL_Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileStorage.Models.Mapper
{
    public static class ViewModelMapper
    {
        public static FileViewModel GetFileViewModel(this FileEntity bllEntity)
        {
            if (bllEntity == null)
                return null;
            return new FileViewModel()
            {
                Id = bllEntity.Id,
                Name = bllEntity.Name,
                DateCreated = bllEntity.DateCreated,
                IsDelete = bllEntity.IsDelete,
                IsOpen = bllEntity.IsOpen,
                URL = bllEntity.Path,
                UserId = bllEntity.UserId,
                Type = bllEntity.Type,
                Size = bllEntity.Size
            };
        }

        public static FilesViewModel GetFilesViewModel(this List<FileEntity> listBllEntity)
        {
            FilesViewModel model = new FilesViewModel()
            {
                Files = listBllEntity.Where(f => f.IsDelete != true).Select(f => f.GetFileViewModel()).ToList(),
                DeleteFiles = listBllEntity.Where(f => f.IsDelete == true).Select(f => f.GetFileViewModel()).ToList(),
                TotalSize = listBllEntity.Sum(f=>f.Size)
            };
            model.CountFiles = model.Files.Count;
            model.CountDeleteFiles = model.DeleteFiles.Count;
            return model;
        }
    }
}