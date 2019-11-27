using System;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.Infrastructure;
using FreelancerCorp.Infrastructure.Query;

namespace FreelancerCorp.BusinessLayer.Services.Common
{
    public abstract class CrudQueryServiceBase<TEntity, TDto, TFilterDto> : ServiceBase
    where TFilterDto : FilterDTOBase, new()
        where TEntity : class, IEntity, new()
        where TDto : DTOBase
    {
        protected readonly IRepository<TEntity> Repository;

        protected readonly QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> Query;

        protected CrudQueryServiceBase(IMapper mapper, IRepository<TEntity> repository, QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> query) : base(mapper)
        {
            this.Query = query;
            this.Repository = repository;
        }
        
        public virtual async Task<TDto> GetAsync(int entityId, bool withIncludes = true)
        {
            TEntity entity;
            if (withIncludes)
            {
                entity = await GetWithIncludesAsync(entityId);
            } 
            else
            {
                entity = await Repository.GetAsync(entityId);
            }

            return entity == null ? null : Mapper.Map<TDto>(entity);
        }
        
        protected abstract Task<TEntity> GetWithIncludesAsync(int entityId);
        
        public virtual int Create(TDto entityDto)
        {
            var entity = Mapper.Map<TEntity>(entityDto);
            Repository.Create(entity);
            return entity.Id;
        }
        
        public virtual async Task Update(TDto entityDto)
        {
            var entity = await GetWithIncludesAsync(entityDto.Id);
            Mapper.Map(entityDto, entity);
            Repository.Update(entity);
        }
        
        public virtual void Delete(int entityId)
        {
            Repository.Delete(entityId);
        }
        
        public virtual async Task<QueryResultDTO<TDto, TFilterDto>> ListAllAsync()
        {
            return await Query.ExecuteEmptyQuery();
        }
    }
}
