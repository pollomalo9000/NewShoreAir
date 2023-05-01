using AutoMapper;

using System.Linq.Expressions;


namespace NewShoreAir.Domain.Automapper
{
    public static class Mappers
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ConfigProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
       this IMappingExpression<TSource, TDestination> map,
       Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
        public static IMapper Mapper => Lazy.Value;
    }
}
