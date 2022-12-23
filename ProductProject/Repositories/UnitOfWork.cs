using ProductProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProductProject.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private IDbContextTransaction transaction;
        private readonly IDbFactory dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
            transaction = dbFactory.BeginTransaction();

        }

        public void Commit()
        {
            dbFactory.Commit();
        }

        public void Rollback()
        {
            dbFactory.Rollback();
        }


        public void Save()
        {
            dbFactory.SaveChanges();
        }

    }
}
