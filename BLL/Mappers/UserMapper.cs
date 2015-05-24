using BLL_Interface.Entities;
using DAL_Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class UserMapper
    {
        public static UserEntity GetBllEntity(this DalUser dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new UserEntity()
            {
                Id = dalEntity.Id,
                Login = dalEntity.Login,
                Password = dalEntity.Password,
                Email = dalEntity.Email,
                CreationDate = dalEntity.CreationDate,
                Roles =
                    dalEntity.Roles != null
                        ? dalEntity.Roles.Select(r => r.GetBllEntity()).ToList()
                        : null,
                Files = dalEntity.Files != null
                        ? dalEntity.Files.Select(f => f.GetBllEntity()).ToList()
                        : null
                        
            };
        }

        public static DalUser GetDalEntity(this UserEntity bllEntity)
        {
            return new DalUser()
            {
                Id = bllEntity.Id,
                Login = bllEntity.Login,
                Email = bllEntity.Email,
                Password = bllEntity.Password,
                CreationDate = bllEntity.CreationDate,
                Roles = bllEntity.Roles.Select(r=>r.GetDalEntity()).ToList()
            };
        }
    }
}
