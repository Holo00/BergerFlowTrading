using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model;
using BergerFlowTrading.Shared.DTO.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.DataTier.Repository
{
    public abstract class RepositoryByUser<DTO, TEntity>
        where DTO : UserDataDTO
        where TEntity : AsUserModel

    {
        protected readonly ApplicationDbContext ctxt;
        protected readonly IMapper mapper;
        protected List<string> usualIncludes { get; set; }

        public RepositoryByUser(ApplicationDbContext ctxt, IMapper mapper)
        {
            this.ctxt = ctxt;
            this.mapper = mapper;
            usualIncludes = new List<string>();
            usualIncludes.Add("User");
        }

        public virtual async Task<DTO> GetById(int id, string userId)
        {
            TEntity entity = await this.GetById(id, userId, false);
            return mapper.Map<TEntity, DTO>(entity);
        }

        protected virtual async Task<TEntity> GetById(int id, string userId, bool tracking = false)
        {
            var local = this.Query(x => x.ID.Value == id, userId, this.usualIncludes, tracking).FirstOrDefault();

            if (local != null && !tracking)
            {
                // detach
                this.ctxt.Entry(local).State = EntityState.Detached;
            }

            return local;
        }



        public virtual async Task<List<DTO>> GetAll(string userId)
        {
            return await this.GetAll(userId, false);
        }

        protected virtual async Task<List<DTO>> GetAll(string userId, bool tracking = false)
        {
            List<TEntity> entities = this.Query(x => x != null, userId, this.usualIncludes, tracking).ToList();
            return entities.Select(x => mapper.Map<TEntity, DTO>(x)).ToList();
        }

        protected virtual IQueryable<TEntity> Query(Func<TEntity, bool> predicate, string userId, List<string> includes = null, bool tracking = false)
        {
            var query = this.ctxt.Set<TEntity>().Where(x => x.User_ID == userId);

            if (includes != null)
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

        protected virtual IQueryable<TEntity> Query(Func<TEntity, bool> predicate, List<string> includes = null, bool tracking = false)
        {
            var query = this.ctxt.Set<TEntity>().Where(x => true);

            if (includes != null)
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




        public virtual async Task<DTO> Insert(DTO dto, string userId)
        {
            return await this.Insert(dto, userId, true);
        }

        protected virtual async Task<DTO> Insert(DTO dto, string userId, bool andSave = true)
        {
            TEntity entity = mapper.Map<DTO, TEntity>(dto);
            await this.Insert(entity, userId, andSave);
            return mapper.Map<TEntity, DTO>(entity);
        }

        protected virtual async Task<bool> Insert(TEntity entity, string userId, bool andSave = true)
        {
            entity.CreatedBy = userId;
            entity.CreatedTimeStamp = DateTime.UtcNow;
            entity.UpdatedBy = userId;
            entity.UpdatedTimeStamp = DateTime.UtcNow;
            entity.User_ID = userId;
            this.ctxt.Set<TEntity>().Add(entity);

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }

        public virtual async Task<List<DTO>> Insert(IEnumerable<DTO> dtos, string userId)
        {
            return await this.Insert(dtos, userId, true);
        }

        protected virtual async Task<List<DTO>> Insert(IEnumerable<DTO> dtos, string userId, bool andSave = true)
        {
            IEnumerable<TEntity> entities = mapper.Map<IEnumerable<DTO>, IEnumerable<TEntity>>(dtos);
            await this.Insert(entities, userId, andSave);
            return mapper.Map<IEnumerable<TEntity>, IEnumerable<DTO>>(entities).ToList();
        }

        protected virtual async Task<bool> Insert(IEnumerable<TEntity> entities, string userId, bool andSave = true)
        {
            foreach(TEntity entity in entities)
            {
                entity.CreatedBy = userId;
                entity.CreatedTimeStamp = DateTime.UtcNow;
                entity.UpdatedBy = userId;
                entity.UpdatedTimeStamp = DateTime.UtcNow;
                entity.User_ID = userId;
            }

            this.ctxt.Set<TEntity>().AddRange(entities);

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }






        public virtual async Task<bool> Update(DTO dto, string userId)
        {
            return await this.Update(dto, userId, true);
        }

        protected virtual async Task<bool> Update(DTO dto, string userId, bool andSave = true)
        {
            TEntity entity = mapper.Map<DTO, TEntity>(dto);
            return await this.Update(entity, userId, andSave);
        }

        protected virtual async Task<bool> Update(TEntity entity, string userId, bool andSave = true)
        {
            entity.UpdatedBy = userId;
            entity.UpdatedTimeStamp = DateTime.UtcNow;
            entity.User_ID = userId;
            this.ctxt.Entry<TEntity>(entity).State = EntityState.Modified;

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }


        public virtual async Task<bool> Update(IEnumerable<DTO> dtos, string userId)
        {
            return await this.Update(dtos, userId, true);
        }

        protected virtual async Task<bool> Update(IEnumerable<DTO> dtos, string userId, bool andSave = true)
        {
            IEnumerable<TEntity> entities = mapper.Map<IEnumerable<DTO>, IEnumerable<TEntity>>(dtos);
            return await this.Update(entities, userId, andSave);
        }

        protected virtual async Task<bool> Update(IEnumerable<TEntity> entities, string userId, bool andSave = true)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdatedBy = userId;
                entity.UpdatedTimeStamp = DateTime.UtcNow;
                entity.User_ID = userId;
                this.ctxt.Entry<TEntity>(entity).State = EntityState.Modified;
            }

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }





        public virtual async Task<bool> Delete(int id, string userId)
        {
            TEntity entity = await this.GetById(id, userId, true);
            return await this.Delete(entity, userId, true);
        }

        public virtual async Task<bool> Delete(DTO dto, string userId)
        {
            return await this.Delete(dto, userId, true);
        }

        public virtual async Task<bool> Delete(IEnumerable<DTO> dtos, string userId)
        {
            IEnumerable<TEntity> entities = mapper.Map<IEnumerable<DTO>, IEnumerable<TEntity>>(dtos);
            return await this.Delete(entities, userId, true);
        }

        protected virtual async Task<bool> Delete(DTO dto, string userId, bool andSave = true)
        {
            TEntity entity = mapper.Map<DTO, TEntity>(dto);
            return await this.Delete(entity, userId, andSave);
        }

        protected virtual async Task<bool> Delete(Func<TEntity, bool> predicate, string userId, bool andSave = true)
        {
            IEnumerable<TEntity> entities = this.Query(predicate, userId,  null, true);
            return await this.Delete(entities, userId, andSave);
        }

        protected virtual async Task<bool> Delete(TEntity entity, string userId, bool andSave = true)
        {
            if(entity.User_ID == userId)
            {
                this.ctxt.Set<TEntity>().Remove(entity);
            }
            else
            {
                throw new Exception($"This entity doesn't have the right USER_ID");
            }

            if (andSave)
            {
                await this.Save();
            }

            return true;
        }

        protected virtual async Task<bool> Delete(IEnumerable<TEntity> entities, string userId, bool andSave = true)
        {
            entities = entities.Where(x => x.User_ID == userId);
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
