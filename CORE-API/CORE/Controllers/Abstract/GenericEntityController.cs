using AutoMapper;
using CORE_API.CORE.Helpers;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Controllers
{
    public abstract class GenericEntityController<TEntity, TEntityInputResource, TEntityOutputResource> : ControllerBase 
        where TEntity : CoreEntity
        where TEntityInputResource : CoreInputResource
        where TEntityOutputResource : CoreOutputResource
    {

        public IControllerHelper _controllerHelper;
        public readonly IGenericEntityService<TEntity> _entityService;
        protected readonly CoreConfigurationOptions _coreConfigurationOptions;
        public IMapper _mapper;

        protected GenericEntityController(IControllerHelper controllerHelper, IGenericEntityService<TEntity> entityService, IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
        {
            _controllerHelper = controllerHelper;
            _entityService = entityService;
            _coreConfigurationOptions = coreConfigurationOptions.Value;
            _mapper = mapper;
        }


        [NonActionMethod(IgnoreApi = true)]
        public virtual async Task<CoreListOutputResource<TEntityOutputResource>> List(int skip = 0, int count = 20)
        {
            var result = _entityService.FindQueryableList(skip, count);

            var total = await _entityService.Count();

            var output = new CoreListOutputResource<TEntityOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<TEntity>, IList<TEntityOutputResource>>(result),
                TotalEntities = total
            };

            return output;
        }

        [NonActionMethod(IgnoreApi = true)]
        public virtual TEntityOutputResource Read(Guid id)
        {
            var result = _entityService.FindById(id);

            var output = _mapper.Map<TEntity, TEntityOutputResource>(result);

            return output;
        }


        [NonActionMethod(IgnoreApi = true)]
        public virtual async Task<TEntityOutputResource> Create(TEntityInputResource resource)
        {
            var entity = _mapper.Map<CoreInputResource, TEntity>(resource);

            var result = await _entityService.AddAsync(entity);

            if(!result.Success)
            {
                //TODO Throw Error
            }

            var output = _mapper.Map<TEntity, TEntityOutputResource>(result.Entity);
            
            return output;
        }

        [NonActionMethod(IgnoreApi = true)]
        public virtual async Task<CoreListOutputResource<TEntityOutputResource>> CreateMany(List<TEntityInputResource> resources)
        {
            var entities = _mapper.Map<List<TEntityInputResource>, List<TEntity>>(resources);

            var result = await _entityService.AddManyAsync(entities);

            if(!result.Success)
            {
                //TODO Throw Error
            }

            var output = new CoreListOutputResource<TEntityOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<TEntity>, IList<TEntityOutputResource>>(result.Entities),
                TotalEntities = result.Entities.Count
            };

            return output;
        }


        [NonActionMethod(IgnoreApi = true)]
        public virtual async Task<TEntityOutputResource> Update(Guid id, TEntityInputResource resource)
        {
            var entity = _entityService.FindById(id);

            entity = _mapper.Map<CoreInputResource, TEntity>(resource, entity);

            var result = await _entityService.UpdateAsync(entity);

            if(!result.Success)
            {
                //TODO Throw Error
            }

            var output = _mapper.Map<TEntity, TEntityOutputResource>(result.Entity);

            return output;
        }


        [NonActionMethod(IgnoreApi = true)]
        public virtual async Task<CoreListOutputResource<TEntityOutputResource>> UpdateMany(List<CoreUpdateResource<TEntityInputResource>> resources)
        {
            var updateList = new List<TEntity>();

            foreach(var resource in resources)
            {
                var entity = _entityService.FindById(resource.Id);
                entity = _mapper.Map<TEntityInputResource, TEntity>(resource.Resource, entity);
                updateList.Add(entity);
            }

            var result = await _entityService.UpdateManyAsync(updateList);

            if(!result.Success)
            {
                //TODO Throw Error
            }

            var output = new CoreListOutputResource<TEntityOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<TEntity>, IList<TEntityOutputResource>>(result.Entities),
                TotalEntities = result.Entities.Count
            };

            return output;
        }


        [NonActionMethod(IgnoreApi = true)]
        public virtual async Task<TEntityOutputResource> Delete(Guid id)
        {
            var result = await _entityService.DeleteById(id);

            var output = _mapper.Map<TEntity, TEntityOutputResource>(result);

            return output;

        }


        [NonActionMethod(IgnoreApi = true)]
        public virtual async Task<CoreListOutputResource<TEntityOutputResource>> DeleteMany([FromBody] List<Guid> ids)
        {
            var deleteList = new List<TEntity>();
            foreach(var id in ids){
                var entity = _entityService.FindById(id);
                if(entity==null)
                {
                    //TODO Throw Error
                }
                deleteList.Add(entity);
            }

            var result = await _entityService.DeleteMany(deleteList);

            var output = new CoreListOutputResource<TEntityOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<TEntity>, IList<TEntityOutputResource>>(result),
                TotalEntities = result.Count
            };

            return output;
        }

        protected User GetCurrentUser()
        {
            return _controllerHelper.GetCurrentUser(User);
        }
    }
}
