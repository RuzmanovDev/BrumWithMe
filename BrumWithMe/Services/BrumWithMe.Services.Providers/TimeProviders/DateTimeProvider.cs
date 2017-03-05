using System;

namespace BrumWithMe.Services.Providers.TimeProviders
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
