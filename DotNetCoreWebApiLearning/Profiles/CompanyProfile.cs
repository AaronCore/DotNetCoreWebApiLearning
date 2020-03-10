using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreWebApiLearning.DtoModel;
using DotNetCoreWebApiLearning.Entitys;

namespace DotNetCoreWebApiLearning.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>().ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
