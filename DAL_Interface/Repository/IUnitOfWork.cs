using System;

namespace DAL_Interface.Repository
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
