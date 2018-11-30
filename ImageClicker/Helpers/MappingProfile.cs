using AutoMapper;
using ImageClicker.ViewModels;
using Repositories.Entities;

namespace ImageClicker.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<AppUser, RegistrationViewModel>();
            CreateMap<RegistrationViewModel, AppUser>();
        }
    }
}
