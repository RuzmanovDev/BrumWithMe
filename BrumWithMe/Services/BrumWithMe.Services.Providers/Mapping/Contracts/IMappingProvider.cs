namespace BrumWithMe.Services.Providers.Mapping.Contracts
{
    public interface IMappingProvider
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
