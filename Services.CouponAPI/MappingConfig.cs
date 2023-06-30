using AutoMapper;
using Services.CouponAPI.Models;
using Services.CouponAPI.Models.Dto;

namespace Services.CouponAPI
{
    public class MappingConfig
    {
       public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                x.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
