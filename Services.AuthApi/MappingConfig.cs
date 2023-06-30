using AutoMapper;
using Services.AuthApi.Models;

namespace Services.AuthApi
{
    public class MappingConfig
    {
       public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                //x.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
