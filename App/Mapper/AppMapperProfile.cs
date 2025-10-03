using AutoMapper;

namespace App.Mapper
{
    public class AppMapperProfile
    {
        private static MapperConfiguration Config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductMapper>();
            cfg.AddProfile<ProductDetailMapper>();
            cfg.AddProfile<ProductMappingProfile>();
            cfg.AddProfile<BrandMapper>();
            cfg.AddProfile<TypeProductMapper>();
            cfg.AddProfile<BrandUpdateMapper>();
            cfg.AddProfile<TypeProductUpdateMapper>();
        }, new LoggerFactory());

        public static IMapper Create()
        {
            Config.AssertConfigurationIsValid();
            return Config.CreateMapper();
        }
    }
}
