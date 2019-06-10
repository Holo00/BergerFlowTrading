using AutoMapper;
using BergerFlowTrading.DataTier.Context;
using BergerFlowTrading.Model;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.Encryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.DataTier.Repository
{
    public class UserExchangeSecretRepository : RepositoryByUser<UserExchangeSecretDTO, UserExchangeSecret>
    {
        protected IConfiguration config;
        protected StringCipher cipher;

        public UserExchangeSecretRepository(ApplicationDbContext ctxt, IMapper mapper, IConfiguration config) : base(ctxt, mapper)
        {
            this.usualIncludes.Add("Exchange");
        }

        protected virtual async Task<bool> Insert(UserExchangeSecret entity, string userId, bool andSave = true)
        {
            entity.Api_Secret = cipher.EncryptText(entity.Api_Secret, this.config["SecretsEncryptionKey"]);
            return await base.Insert(entity, userId, andSave);
        }

        protected virtual async Task<bool> Insert(IEnumerable<UserExchangeSecret> entities, string userId, bool andSave = true)
        {
            foreach (UserExchangeSecret entity in entities)
            {
                entity.Api_Secret = cipher.EncryptText(entity.Api_Secret, this.config["SecretsEncryptionKey"]);
            }

            return await base.Insert(entities, userId, andSave);
        }

        protected virtual async Task<bool> Update(UserExchangeSecret entity, string userId, bool andSave = true)
        {
            entity.Api_Secret = cipher.EncryptText(entity.Api_Secret, this.config["SecretsEncryptionKey"]);
            return await base.Update(entity, userId, andSave);
        }

        protected override async Task<bool> Update(IEnumerable<UserExchangeSecret> entities, string userId, bool andSave = true)
        {
            foreach (UserExchangeSecret entity in entities)
            {
                entity.Api_Secret = cipher.EncryptText(entity.Api_Secret, this.config["SecretsEncryptionKey"]);
            }

            return await base.Update(entities, userId, andSave);
        }

        public async Task<UserExchangeSecretDTO> GetByExchangeId(int id)
        {
            var result = this.Query(x => x.Exchange_ID == id).FirstOrDefault();
            return mapper.Map<UserExchangeSecret, UserExchangeSecretDTO>(result);
        }

    }
}
