using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Logs;
using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CORE_API.CORE.Services
{
    public class DistributedLockService : IDistributedLockService
    {
        private readonly IGenericEntityService<DistributedLock> _entityService;

        public DistributedLockService(IGenericEntityService<DistributedLock> entityService, IHttpContextAccessor httpContextAccessor)
        {
            _entityService = entityService;
        }

        public async Task<DistributedLock> AcquireLockAsync(string Name, DateTime Expires)
        {
            if (await IsLockAvailable(Name))
            {
                return await CreateLock(Name, Expires);
            }

            return null;

        }

        private async Task<bool> IsLockAvailable(string Name)
        {
            var dlock = _entityService.FindOne(m => m.LockName.Equals(Name));

            if(dlock == null)
            {
                return true;
            }

            if (dlock.IsExpired)
            {
                await _entityService.DeleteById(dlock.Id);
                return true;
            }

            return false;
        }

        private async Task<DistributedLock> CreateLock(string Name, DateTime Expires)
        {
            var dlock = new DistributedLock
            {
                LockName = Name,
                Expiration = Expires
            };

            var saveResult = await _entityService.AddAsync(dlock);

            if(saveResult.Success == false)
            {
                //TODO Log this
            }

            return saveResult.Entity;
        }
        
    }
}
