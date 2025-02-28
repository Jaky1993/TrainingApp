﻿using AutoMapper;
using TrainingApp_WebAPI.MODEL;
using TrainingApp_WebAPI.VIEWMODEL;

namespace TrainingApp_WebAPI.UTILITY
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
