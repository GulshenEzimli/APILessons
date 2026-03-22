using Application.Interfaces.AutoMapper;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Logging;

namespace Application.Mapping
{
    public class Mapper : ICustomMapper
    {
        private static List<TypePair> typePairs = new();
        private IMapper mapperContainer;

        public TDestination Map<TSource, TDestination>(TSource source, string? ignore = null)
        {
            CreateMap<TSource, TDestination>(ignore, 5);
            return mapperContainer.Map<TSource, TDestination>(source);
        }

        public IList<TDestination> Map<TSource, TDestination>(IList<TSource> source, string? ignore = null)
        {
            CreateMap<TSource, TDestination>(ignore);
            return mapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public TDestination Map<TDestination>(object source, string? ignore = null)
        {
            CreateMap<object, TDestination>(ignore);
            return mapperContainer.Map<object, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
        {
            CreateMap<object, TDestination>(ignore);
            return mapperContainer.Map<IList<object>, IList<TDestination>>(source);
        }

        public void AddMap<TSource, TDestination>()
        {
            CreateMap<TSource, TDestination>();
        }

        private void CreateMap<TSource, TDestination>(string? ignore = null, int depth = 5)
        {
            TypePair typePair = new TypePair(typeof(TSource), typeof(TDestination));
            if (typePairs.Any(t => t.SourceType == typePair.SourceType && t.DestinationType == typePair.DestinationType) && ignore is null)
                return;
                
            typePairs.Add(typePair);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var pair in typePairs)
                {
                    if(ignore is not null)
                        cfg.CreateMap(pair.SourceType, pair.DestinationType).MaxDepth(depth).ForMember(ignore, x => x.Ignore()).ReverseMap();
                    else
                        cfg.CreateMap(pair.SourceType, pair.DestinationType).MaxDepth(depth).ReverseMap();

                }

            }, new LoggerFactory());

            mapperContainer = config.CreateMapper();
        }
    }
}
