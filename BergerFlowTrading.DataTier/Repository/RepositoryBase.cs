using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model;
using BergerFlowTrading.Shared.DTO.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.DataTier.Repository
{
    public abstract class RepositoryBase<DTO, TEntity>
        where DTO : DataDTOBase
        where TEntity : BaseModel
    {
        protected readonly ApplicationDbContext ctxt;
        protected readonly IMapper mapper;
        protected List<string> usualIncludes { get; set; }

        public RepositoryBase(ApplicationDbContext ctxt, IMapper mapper)
        {
            this.ctxt = ctxt;
            this.mapper = mapper;
            usualIncludes = new List<string>();
        }

        public virtual async Task<DTO> GetById(int id)
        {
            TEntity entity = await this.GetById(id, false);
            return mapper.Map<TEntity, DTO>(entity);
        }

        protected virtual async Task<TEntity> GetById(int id, bool tracking = false)
        {
            var local = this.Query(x => x.ID.Value == id, this.usualIncludes, tracking).FirstOrDefault();

            if (local != null && !tracking)
            {
                // detach
                this.ctxt.Entry(local).State = EntityState.Detached;
            }

            return local;
        }

        public virtual async Task<List<DTO>> GetAll()
        {
            return await this.GetAll(false);
        }

        protected virtual async Task<List<DTO>> GetAll(bool tracking = false)
        {
            List<TEntity> entities = this.Query(x => x != null, this.usualIncludes, tracking).ToList();
            return mapper.Map<List<TEntity>, List<DTO>>(entities);
        }

        protected virtual IQueryable<TEntity> Query(Func<TEntity, bool> predicate, List<string> includes = null, bool tracking = false)
        {
            var query = this.ctxt.Set<TEntity>().Where(x => true);

            if(includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.Where(predicate).AsQueryable();

            query = tracking ? query : query.AsNoTracking();
            return query;
        }
      

        public virtual async Task<bool> Insert(DTO dto)
        {
            return await this.Insert(dto, true);
        }

        protected virtual async Task<bool> Insert(DTO dto, bool andSave = true)
        {
            TEntity entity = mapper.Map<DTO, TEntity>(dto);
            return await this.Insert(entity, andSave);
        }

        protected virtual async Task<bool> Insert(TEntity entity, bool andSave = true)
        {
            await this.ctxt.Set<TEntity>().AddAsync(entity);

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }

        public virtual async Task<bool> Insert(IEnumerable<DTO> dtos)
        {
            return await this.Insert(dtos, true);
        }

        protected virtual async Task<bool> Insert(IEnumerable<DTO> dtos, bool andSave = true)
        {
            IEnumerable<TEntity> entities = mapper.Map<IEnumerable<DTO>, IEnumerable<TEntity>>(dtos);
            return await this.Insert(entities, andSave);
        }

        protected virtual async Task<bool> Insert(IEnumerable<TEntity> entities, bool andSave = true)
        {
            await this.ctxt.Set<TEntity>().AddRangeAsync(entities);

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }



        public virtual async Task<bool> Update(DTO dto)
        {
            return await this.Update(dto, true);
        }

        protected virtual async Task<bool> Update(DTO dto, bool andSave = true)
        {
            TEntity entity = mapper.Map<DTO, TEntity>(dto);
            return await this.Update(entity, andSave);
        }

        protected virtual async Task<bool> Update(TEntity entity, bool andSave = true)
        {
            this.ctxt.Entry<TEntity>(entity).State = EntityState.Modified;

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }


        public virtual async Task<bool> Update(IEnumerable<DTO> dtos)
        {
            return await this.Update(dtos, true);
        }

        protected virtual async Task<bool> Update(IEnumerable<DTO> dtos, bool andSave = true)
        {
            IEnumerable<TEntity> entities = mapper.Map< IEnumerable<DTO>, IEnumerable<TEntity>>(dtos);
            return await this.Update(entities, andSave);
        }

        protected virtual async Task<bool> Update(IEnumerable<TEntity> entities, bool andSave = true)
        {
            foreach(TEntity entity in entities)
            {
                this.ctxt.Entry<TEntity>(entity).State = EntityState.Modified;
            }

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }





        public virtual async Task<bool> Delete(int id)
        {
            TEntity entity = await this.GetById(id, true);
            return await this.Delete(entity, true);
        }

        public virtual async Task<bool> Delete(DTO dto)
        {
            return await this.Delete(dto, true);
        }

        public virtual async Task<bool> Delete(IEnumerable<DTO> dtos)
        {
            IEnumerable<TEntity> entities = mapper.Map<IEnumerable<DTO>, IEnumerable<TEntity>>(dtos);
            return await this.Delete(entities, true);
        }

        protected virtual async Task<bool> Delete(DTO dto, bool andSave = true)
        {
            TEntity entity = mapper.Map<DTO, TEntity>(dto);
            return await this.Delete(entity, andSave);
        }

        protected virtual async Task<bool> Delete(Func<TEntity, bool> predicate, bool andSave = true)
        {
            IEnumerable<TEntity> entities = this.Query(predicate, null, true);
            return await this.Delete(entities, andSave);
        }

        protected virtual async Task<bool> Delete(TEntity entity, bool andSave = true)
        {
            this.ctxt.Set<TEntity>().Remove(entity);

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }

        protected virtual async Task<bool> Delete(IEnumerable<TEntity> entities, bool andSave = true)
        {
            this.ctxt.Set<TEntity>().RemoveRange(entities);

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }







        protected virtual async Task Save()
        {
            await this.ctxt.SaveChangesAsync();
        }
    }
}
