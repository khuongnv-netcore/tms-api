using CORE_API.CORE.Models.Entities;
using System;
using System.Threading.Tasks;

namespace CORE_API.CORE.Services
{
    public interface IDistributedLockService
    {

        Task<DistributedLock> AcquireLockAsync(string Name, DateTime Expires);

    }
}