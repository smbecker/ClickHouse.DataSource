using System.Data.Common;
using Microsoft.Extensions.Logging;

namespace ClickHouse.Client.ADO;

public sealed class ClickHouseDataSource : DbDataSource, IClickHouseDataSource
{
	private readonly Func<ClickHouseConnection> connectionFactory;

	/// <summary>
	/// Initializes a new instance of the <see cref="ClickHouseDataSource"/> class using provided HttpClient.
	/// Note that HttpClient must have AutomaticDecompression enabled if compression is not disabled in connection string
	/// </summary>
	/// <param name="connectionString">Connection string</param>
	/// <param name="httpClient">instance of HttpClient</param>
	public ClickHouseDataSource(string connectionString, HttpClient? httpClient = null) {
		ArgumentNullException.ThrowIfNull(connectionString);
		ConnectionString = connectionString;
		if (httpClient != null) {
			connectionFactory = () => new ClickHouseConnection(connectionString, httpClient);
		} else {
			connectionFactory = () => new ClickHouseConnection(connectionString);
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ClickHouseDataSource"/> class using an HttpClient generated by the provided <paramref name="httpClientFactory"/>.
	/// </summary>
	/// <param name="connectionString">The ClickHouse connection string.</param>
	/// <param name="httpClientFactory">The factory to be used for creating the clients.</param>
	/// <param name="httpClientName">
	/// The name of the HTTP client you want to be created using the provided factory.
	/// If left empty, the default client will be created.
	/// </param>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// If compression is not disabled in the <paramref name="connectionString"/>, the <paramref name="httpClientFactory"/>
	/// must be configured to enable <see cref="HttpClientHandler.AutomaticDecompression"/> for its generated clients.
	/// <example>
	/// For example you can do this while registering the HTTP client:
	/// <code>
	/// services.AddHttpClient("ClickHouseClient").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
	/// {
	///     AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
	/// });
	/// </code>
	/// </example>
	/// </item>
	/// <item>
	/// The <paramref name="httpClientFactory"/> must set the timeout for its clients if needed.
	/// <example>
	/// For example, you can do this while registering the HTTP client:
	/// <code>
	/// services.AddHttpClient("ClickHouseClient", c => c.Timeout = TimeSpan.FromMinutes(5));
	/// </code>
	/// </example>
	/// </item>
	/// </list>
	/// </remarks>
	public ClickHouseDataSource(string connectionString, IHttpClientFactory httpClientFactory, string httpClientName = "") {
		ArgumentNullException.ThrowIfNull(connectionString);
		ArgumentNullException.ThrowIfNull(httpClientFactory);
		ArgumentNullException.ThrowIfNull(httpClientName);
		ConnectionString = connectionString;
		connectionFactory = () => new ClickHouseConnection(connectionString, httpClientFactory, httpClientName);
	}

	public override string ConnectionString {
		get;
	}

	public ILogger? Logger {
		get;
		set;
	}

	protected override DbConnection CreateDbConnection() {
		var cn = connectionFactory();
		if (cn.Logger == null && Logger != null) {
			cn.Logger = Logger;
		}
		return cn;
	}

	public new ClickHouseConnection CreateConnection() => (ClickHouseConnection)CreateDbConnection();

	IClickHouseConnection IClickHouseDataSource.CreateConnection() => CreateConnection();

	public new ClickHouseConnection OpenConnection() => (ClickHouseConnection)OpenDbConnection();

	IClickHouseConnection IClickHouseDataSource.OpenConnection() => OpenConnection();

	public new async Task<ClickHouseConnection> OpenConnectionAsync(CancellationToken cancellationToken) {
		var cn = await OpenDbConnectionAsync(cancellationToken).ConfigureAwait(false);
		return (ClickHouseConnection)cn;
	}

	async Task<IClickHouseConnection> IClickHouseDataSource.OpenConnectionAsync(CancellationToken cancellationToken) {
		var cn = await OpenDbConnectionAsync(cancellationToken).ConfigureAwait(false);
		return (IClickHouseConnection)cn;
	}
}
