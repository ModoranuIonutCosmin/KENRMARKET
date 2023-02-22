using System.Reflection;
using AutoMapper;

namespace Testing.Utils.Mapper;

public static class AutoMapperHelpers
{
    public static IMapper CreateMapper(List<Assembly> assemblies)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(assemblies);
        });
        
        return config.CreateMapper();
    }
}