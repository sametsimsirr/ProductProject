using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
        void Commit();
        void Rollback();
    }
}
