using System;

namespace BrumWithMe.Services.Providers.TimeProviders
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
