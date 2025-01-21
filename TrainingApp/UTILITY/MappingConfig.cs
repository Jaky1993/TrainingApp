using AutoMapper;
using TrainingApp.VIEWMODEL;
using TrainingAppData.MODEL;

namespace TrainingApp.UTILITY
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
