// ReSharper disable once CheckNamespace
namespace OpenTelemetry.Trace;

public static class ClickHouseTracerProviderBuilderExtensions
{
	// https://github.com/DarkWanderer/ClickHouse.Client/blob/main/ClickHouse.Client/Diagnostic/ActivitySourceHelper.cs#L11
	private const string ActivitySourceName = "ClickHouse.Client";

	public static TracerProviderBuilder AddClickHouse(this TracerProviderBuilder builder) {
		ArgumentNullException.ThrowIfNull(builder);
		builder.AddSource(ActivitySourceName);
		return builder;
	}
}