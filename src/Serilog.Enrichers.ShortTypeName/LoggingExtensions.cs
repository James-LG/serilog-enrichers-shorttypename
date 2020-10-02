using Serilog.Configuration;
using System;

namespace Serilog.Enrichers.ShortTypeName
{
    public static class LoggingExtensions
    {
        public static LoggerConfiguration WithShortTypeName(this LoggerEnrichmentConfiguration enrich)
        {
            _ = enrich ?? throw new ArgumentNullException(nameof(enrich));

            return enrich.With<ShortTypeNameEnricher>();
        }
    }
}
