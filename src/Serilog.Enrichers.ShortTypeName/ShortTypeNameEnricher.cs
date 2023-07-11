using Serilog.Core;
using Serilog.Events;
using System;
using System.Linq;

namespace Serilog.Enrichers.ShortTypeName
{
    /// <summary>
    /// Enrich the log with the short name of the SourceContext type for use in log strings
    /// Ex: Serilog.Enrichers.ShortTypeName.ShortTypeNameEnricher -> ShortTypeNameEnricher
    /// </summary>
    public class ShortTypeNameEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            _ = logEvent ?? throw new ArgumentNullException(nameof(logEvent));
            _ = propertyFactory ?? throw new ArgumentNullException(nameof(propertyFactory));

            if (logEvent.Properties.TryGetValue("SourceContext", out var value))
            {
                var typeNameWithoutGenerics = value.ToString().Replace("\"", string.Empty, StringComparison.Ordinal).Split('`').First();
                var source = typeNameWithoutGenerics.Split('.').Last();

                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ShortTypeName", source));
            }
        }
    }
}
