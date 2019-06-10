using AutoMapper;
using BergerFlowTrading.Model.Identity;
using BergerFlowTrading.Shared.DTO.Data;
using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Model.Mappers
{
    public class DataMapper: Profile
    {
        public DataMapper()
        {
            AllowNullDestinationValues = true;
            CreateMap<BaseModel, DataDTOBase>();
            CreateMap<DataDTOBase, BaseModel>();

            CreateMap<AsUserModel, UserDataDTO>();
            CreateMap<UserDataDTO, AsUserModel>();

            CreateMap<Exchange, ExchangeDTO>()
                .ForMember(x => x.UserExchangeSecrets, opt => opt.Ignore())
                .ForMember(x => x.ExchangeCustomSettings, opt => opt.Ignore())
                .ForMember(x => x.LimitStrategy4Settings1, opt => opt.Ignore())
                .ForMember(x => x.LimitStrategy4Settings2, opt => opt.Ignore());

            CreateMap<ExchangeDTO, Exchange>()
                .ForMember(x => x.UserExchangeSecrets, opt => opt.Ignore())
                .ForMember(x => x.ExchangeCustomSettings, opt => opt.Ignore())
                .ForMember(x => x.LimitStrategy4Settings1, opt => opt.Ignore())
                .ForMember(x => x.LimitStrategy4Settings2, opt => opt.Ignore());

            CreateMap<AppUser, UserStateDTO>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.Token, opt => opt.Ignore());

            CreateMap<UserStateDTO, AppUser>();

            CreateMap<ExchangeCustomSettings, ExchangeCustomSettingsDTO>();
            CreateMap<ExchangeCustomSettingsDTO, ExchangeCustomSettings>();

            CreateMap<UserExchangeSecret, UserExchangeSecretDTO>()
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<UserExchangeSecretDTO, UserExchangeSecret>()
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<List<UserExchangeSecret>, List<UserExchangeSecretDTO>>();
            CreateMap<List<UserExchangeSecretDTO>, List<UserExchangeSecret>>();

            CreateMap<LimitArbitrageStrategy4Settings, LimitArbitrageStrategy4SettingsDTO>()
                .ForMember(x => x.User, opt => opt.Ignore());
            CreateMap<LimitArbitrageStrategy4SettingsDTO, LimitArbitrageStrategy4Settings>()
                .ForMember(x => x.User, opt => opt.Ignore());
        }
    }
}
