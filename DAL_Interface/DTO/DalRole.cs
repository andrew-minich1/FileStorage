using System.Collections.Generic;
namespace DAL_Interface.DTO
{
    public class DalRole : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<DalUser> Users { get; set; }
    }
}
