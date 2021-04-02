using SchoolsSys.BL.Models;
using SchoolsSys.BL.Repository;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SchoolsSys.BL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private SchoolsSysDBContext _dbContext;
        DbContextTransaction dbContextTransaction = null;
        private bool disposed = false;

        public UnitOfWork()
        {
            _dbContext = new SchoolsSysDBContext();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                if (_dbContext.ChangeTracker.HasChanges())
                {
                    return await _dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex) { }
            return -1;
        }

        public DbContext Context
        {
            get { return _dbContext; }
        }

        public void BeginTransaction()
        {
            dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Commit();
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                if (dbContextTransaction != null)
                    RollBack();
            }
        }

        public void RollBack()
        {
            dbContextTransaction.Rollback();
        }

        private IStudentsRepository studentsRepo;
        public IStudentsRepository StudentsRepo
        {
            get
            {
                if (studentsRepo == null)
                {
                    studentsRepo = new StudentsRepository(_dbContext);
                }

                return studentsRepo;
            }
        }

        private IAttachmentsRepository attachmentsRepo;
        public IAttachmentsRepository AttachmentsRepo
        {
            get
            {
                if (attachmentsRepo == null)
                {
                    attachmentsRepo = new AttachmentsRepository(_dbContext);
                }

                return attachmentsRepo;
            }
        }
    }
}
