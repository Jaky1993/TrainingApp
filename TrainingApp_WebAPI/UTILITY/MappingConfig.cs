using AutoMapper;
using TrainingApp_WebAPI.VIEWMODEL;
using TrainingAppData.MODEL;

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
