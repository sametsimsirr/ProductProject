using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProductProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Repositories
{
    public class DbFactory: IDbFactory
    {
        DbContext dbContext;
        IDbContextTransaction transaction;
        protected DbContextOptions<BaseDbContext> _options;

        public DbFactory(DbContextOptions<BaseDbContext> options)
        {
            _options = options;
        }

        public IDbContextTransaction BeginTransaction()
        {
            transaction = Init().Database.BeginTransaction();
            return transaction;

        }

        public void Commit()
        {
            if (transaction != null)
                transaction.Commit();
        }

        public void Rollback()
        {
            if (transaction != null)
                transaction.Rollback();
        }

        public DbContext Init()
        {
            var context = dbContext ?? (dbContext = new BaseDbContext(_options));
            return context;
        }

        public void SaveChanges()
        {
            if (dbContext != null)
                dbContext.SaveChanges();
        }

    }
}
