using System;
using DAL_Interface.Repository;
using System.Data.Entity;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            Context = context;
        }

        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }
    }
}
