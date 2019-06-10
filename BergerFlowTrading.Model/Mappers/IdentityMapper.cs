using AutoMapper;
using BergerFlowTrading.Model.Identity;
using BergerFlowTrading.Shared.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Model.Mappers
{
    public class IdentityMapper: Profile
    {
        public IdentityMapper()
        {
            CreateMap<AppUser, RegisterDTO>();

            CreateMap<RegisterDTO, AppUser>().ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email)
            );
        }
    }
}
