namespace Application.Interfaces.AutoMappers
{
	public interface IMapper
	{
		TDestination Map<TSource, TDestination>(TSource source, string? ignore = null);
		IList<TDestination> Map<TSource, TDestination>(IList<TSource> sources, string? ignore = null);
		TDestination Map<TDestination>(object source, string? ignore = null);
		IList<TDestination> Map<TDestination>(IList<object> sources, string? ignore = null);
	}
}
