using SchoolsSys.BL.Repository;
using System;
using System.Collections.Generic;

namespace SchoolsSys.BL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAttachmentsRepository Attachments { get; }
        IStudentsRepository Students { get; }
        int Complete();
    }
}
