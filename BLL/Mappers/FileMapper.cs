using BLL_Interface.Entities;
using DAL_Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class FileMapper
    {
        public static FileEntity GetBllEntity(this DalFile dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new FileEntity()
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name,
                DateCreated = dalEntity.DateCreated,
                IsDelete = dalEntity.IsDelete,
                IsOpen = dalEntity.IsOpen,
                Path = dalEntity.Path,
                UserId = dalEntity.UserId,
                Size = dalEntity.Size,
                Type = dalEntity.Type
            };
        }

        public static DalFile GetDalEntity(this FileEntity bllEntity)
        {
            return new DalFile()
            {
                Id = bllEntity.Id,
                Name = bllEntity.Name,
                DateCreated = bllEntity.DateCreated,
                IsDelete = bllEntity.IsDelete,
                IsOpen = bllEntity.IsOpen,
                Path = bllEntity.Path,
                UserId = bllEntity.UserId,
                Size = bllEntity.Size,
                Type = bllEntity.Type
            };
        }

    }
}
