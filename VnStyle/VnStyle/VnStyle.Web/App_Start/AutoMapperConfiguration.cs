using AutoMapper;
using VnStyle.Services.Business.Dtos;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Web
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<File, FileDto>();
                //cfg.CreateMap<Review, ReviewDto>();

                //Review, ReviewDto
            });
        }
    }
}