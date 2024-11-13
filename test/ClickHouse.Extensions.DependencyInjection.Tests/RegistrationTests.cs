using ClickHouse.Client.ADO;
using Microsoft.Extensions.DependencyInjection;

namespace ClickHouse.Client;

public sealed class RegistrationTests
{
	[Fact]
	public async Task can_add_clickhouse_datasource() {
		const string connectionString = "Host=localhost;Port=1234";
		await using var services = new ServiceCollection()
			.AddClickHouseDataSource(connectionString)
			.BuildServiceProvider();
		var dataSource = services.GetRequiredService<IClickHouseDataSource>();
		Assert.Equal(connectionString, dataSource.ConnectionString);

		using var fromService = services.GetRequiredService<IClickHouseConnection>();
		using var rawConnection = new ClickHouseConnection(connectionString);
		Assert.Equal(rawConnection.ConnectionString, fromService.ConnectionString);
	}
}
