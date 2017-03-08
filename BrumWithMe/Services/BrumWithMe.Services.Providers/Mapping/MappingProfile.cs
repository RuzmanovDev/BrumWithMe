using BrumWithMe.Services.Providers.Mapping.Profiles;
using AutoMapper;

namespace BrumWithMe.Services.Providers.Mapping
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebMappingProfile());  //mapping between Web and Business layer objects
                cfg.AddProfile(new BLProfile());  // mapping between Business and DB layer objects
            });

            return config;
        }
    }
}
