using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace ProductProject.Interfaces
{
    public interface IDbFactory 
    {
        DbContext Init();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void Rollback();
        void SaveChanges();
    }
}
