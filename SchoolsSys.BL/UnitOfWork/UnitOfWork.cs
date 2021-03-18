using SchoolsSys.BL.Models;
using SchoolsSys.BL.Repository;
using System.Collections.Generic;

namespace SchoolsSys.BL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolsSysDBContext _ctx;

        public UnitOfWork(SchoolsSysDBContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.AutoDetectChangesEnabled = true;
            Attachments = new AttachmentsRepository(_ctx);
            Students = new StudentsRepository(_ctx);
        }

        public IAttachmentsRepository Attachments { get; private set; }
        public IStudentsRepository Students { get; private set; }


        public int Complete()
        {
            return _ctx.SaveChanges();
        }

        public T GetAdded<T>()
        {
            T s = (T)new object();
            var entries = _ctx.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == System.Data.Entity.EntityState.Added)
                {
                    s = (T)entry.Entity;
                }
            }
            return s;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
