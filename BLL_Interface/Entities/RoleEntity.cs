using System.Collections.Generic;
namespace BLL_Interface.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserEntity> Users { get; set; }
    }
}
