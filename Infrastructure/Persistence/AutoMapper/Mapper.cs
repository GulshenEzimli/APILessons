using Application.Interfaces.AutoMappers;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using IMapper = AutoMapper.IMapper;

namespace Persistence.AutoMapper
{
	public class Mapper : Application.Interfaces.AutoMappers.IMapper
	{
		private IMapper MapperContainer;
		private static List<TypePair> typePairs = new();
		public TDestination Map<TSource, TDestination>(TSource source, string? ignore = null)
		{
			Config<TSource, TDestination>(5, ignore);
			return MapperContainer.Map<TSource,TDestination>(source);
		}

		public IList<TDestination> Map<TSource, TDestination>(IList<TSource> sources, string? ignore = null)
		{
			Config<TSource, TDestination>(5, ignore);
			return MapperContainer.Map<IList<TSource>, IList<TDestination>>(sources);
		}

		public TDestination Map<TDestination>(object source, string? ignore = null)
		{
			Config<object, TDestination>(5, ignore);
			return MapperContainer.Map<TDestination>(source);
		}

		public IList<TDestination> Map<TDestination>(IList<object> sources, string? ignore = null)
		{
			Config<object, TDestination>(5, ignore);
			return MapperContainer.Map<IList<TDestination>>(sources);
		}

		private void Config<TSource, TDestination>(int depth = 5, string? ignore = null)
		{
			var typePair = new TypePair(typeof(TSource), typeof(TDestination));

			if (typePairs.Any(t => t.DestinationType == typePair.DestinationType && t.SourceType == typePair.SourceType) && ignore is null)
				return;

			typePairs.Add(typePair);


			var config = new MapperConfiguration(cfg =>
			{
				foreach (var item in typePairs)
				{
					if (ignore is not null)
						cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ForMember(ignore, x => x.Ignore()).ReverseMap();
					else
						cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ReverseMap();
				}
			},new LoggerFactory());

			MapperContainer = config.CreateMapper();
		}
	}
}
