using System.Data.Common;
using System.Net;
using ClickHouse.Client;
using ClickHouse.Client.ADO;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension method for setting up ClickHouse services in an <see cref="IServiceCollection" />.
/// </summary>
public static class ClickHouseServiceCollectionExtensions
{
	/// <summary>
	/// Registers an <see cref="ClickHouseDataSource" /> and an <see cref="ClickHouseConnection" /> in the <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="connectionString">A ClickHouse connection string.</param>
	/// <param name="httpClient">instance of HttpClient</param>
	/// <param name="connectionLifetime">
	/// The lifetime with which to register the <see cref="ClickHouseConnection" /> in the container.
	/// Defaults to <see cref="ServiceLifetime.Transient" />.
	/// </param>
	/// <param name="dataSourceLifetime">
	/// The lifetime with which to register the <see cref="ClickHouseDataSource" /> service in the container.
	/// Defaults to <see cref="ServiceLifetime.Singleton" />.
	/// </param>
	/// <param name="serviceKey">The <see cref="ServiceDescriptor.ServiceKey"/> of the data source.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddClickHouseDataSource(
		this IServiceCollection services,
		string connectionString,
		HttpClient? httpClient = null,
		ServiceLifetime connectionLifetime = ServiceLifetime.Transient,
		ServiceLifetime dataSourceLifetime = ServiceLifetime.Singleton,
		object? serviceKey = null
	) => AddClickHouseDataSource(services, (_, _) => {
		if (httpClient == null) {
			// Ensure that we are using the same HTTP client for all connections
#pragma warning disable CA5399
			httpClient = new HttpClient(new HttpClientHandler {
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			});
#pragma warning restore CA5399
		}
		return new ClickHouseDataSource(connectionString, httpClient);
	}, connectionLifetime, dataSourceLifetime, serviceKey);

	/// <summary>
	/// Registers an <see cref="ClickHouseDataSource" /> and an <see cref="ClickHouseConnection" /> in the <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="connectionString">A ClickHouse connection string.</param>
	/// <param name="httpClientFactory">The factory to be used for creating the clients.</param>
	/// <param name="httpClientName">
	/// The name of the HTTP client you want to be created using the provided factory.
	/// If left empty, the default client will be created.
	/// </param>
	/// <param name="connectionLifetime">
	/// The lifetime with which to register the <see cref="ClickHouseConnection" /> in the container.
	/// Defaults to <see cref="ServiceLifetime.Transient" />.
	/// </param>
	/// <param name="dataSourceLifetime">
	/// The lifetime with which to register the <see cref="ClickHouseDataSource" /> service in the container.
	/// Defaults to <see cref="ServiceLifetime.Singleton" />.
	/// </param>
	/// <param name="serviceKey">The <see cref="ServiceDescriptor.ServiceKey"/> of the data source.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddClickHouseDataSource(
		this IServiceCollection services,
		string connectionString,
		IHttpClientFactory httpClientFactory,
		string httpClientName = "",
		ServiceLifetime connectionLifetime = ServiceLifetime.Transient,
		ServiceLifetime dataSourceLifetime = ServiceLifetime.Singleton,
		object? serviceKey = null
	) => AddClickHouseDataSource(services, (_, _) => new ClickHouseDataSource(connectionString, httpClientFactory, httpClientName), connectionLifetime, dataSourceLifetime, serviceKey);

	/// <summary>
	/// Registers an <see cref="ClickHouseDataSource" /> and an <see cref="ClickHouseConnection" /> in the <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="dataSourceFactory">A factory for ClickHouseDataSource instances.</param>
	/// <param name="connectionLifetime">
	/// The lifetime with which to register the <see cref="ClickHouseConnection" /> in the container.
	/// Defaults to <see cref="ServiceLifetime.Transient" />.
	/// </param>
	/// <param name="dataSourceLifetime">
	/// The lifetime with which to register the <see cref="ClickHouseDataSource" /> service in the container.
	/// Defaults to <see cref="ServiceLifetime.Singleton" />.
	/// </param>
	/// <param name="serviceKey">The <see cref="ServiceDescriptor.ServiceKey"/> of the data source.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddClickHouseDataSource(
		this IServiceCollection services,
		Func<IServiceProvider, object?, ClickHouseDataSource> dataSourceFactory,
		ServiceLifetime connectionLifetime = ServiceLifetime.Transient,
		ServiceLifetime dataSourceLifetime = ServiceLifetime.Singleton,
		object? serviceKey = null
	) {
		services.TryAdd(new ServiceDescriptor(typeof(ClickHouseDataSource), serviceKey, dataSourceFactory, dataSourceLifetime));
		if (serviceKey is not null) {
			services.TryAdd(
				new ServiceDescriptor(
					typeof(IClickHouseDataSource),
					serviceKey,
					static (sp, key) => sp.GetRequiredKeyedService<ClickHouseDataSource>(key),
					dataSourceLifetime));

			services.TryAdd(
				new ServiceDescriptor(
					typeof(DbDataSource),
					serviceKey,
					static (sp, key) => sp.GetRequiredKeyedService<ClickHouseDataSource>(key),
					dataSourceLifetime));

			services.TryAdd(
				new ServiceDescriptor(
					typeof(IClickHouseConnection),
					serviceKey,
					static (sp, key) => sp.GetRequiredKeyedService<ClickHouseDataSource>(key).CreateConnection(),
					connectionLifetime));

			services.TryAdd(
				new ServiceDescriptor(
					typeof(ClickHouseConnection),
					serviceKey,
					static (sp, key) => sp.GetRequiredKeyedService<ClickHouseDataSource>(key).CreateConnection(),
					connectionLifetime));

			services.TryAdd(
				new ServiceDescriptor(
					typeof(DbConnection),
					serviceKey,
					static (sp, key) => sp.GetRequiredKeyedService<ClickHouseConnection>(key),
					connectionLifetime));
		} else {
			services.TryAdd(
				new ServiceDescriptor(
					typeof(IClickHouseDataSource),
					static sp => sp.GetRequiredService<ClickHouseDataSource>(),
					dataSourceLifetime));

			services.TryAdd(
				new ServiceDescriptor(
					typeof(DbDataSource),
					static sp => sp.GetRequiredService<ClickHouseDataSource>(),
					dataSourceLifetime));

			services.TryAdd(
				new ServiceDescriptor(
					typeof(IClickHouseConnection),
					static sp => sp.GetRequiredService<ClickHouseDataSource>().CreateConnection(),
					connectionLifetime));

			services.TryAdd(
				new ServiceDescriptor(
					typeof(ClickHouseConnection),
					static sp => sp.GetRequiredService<ClickHouseDataSource>().CreateConnection(),
					connectionLifetime));

			services.TryAdd(
				new ServiceDescriptor(
					typeof(DbConnection),
					static sp => sp.GetRequiredService<ClickHouseConnection>(),
					connectionLifetime));
		}

		return services;
	}
}
