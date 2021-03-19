using SchoolsSys.BL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SchoolsSys.BL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentsRepository StudentsRepo { get;}
        IAttachmentsRepository AttachmentsRepo { get; }
        Task<int> SaveChanges();
        void BeginTransaction();
        void RollBack();
        void Commit();
        DbContext Context { get; }
    }
}
