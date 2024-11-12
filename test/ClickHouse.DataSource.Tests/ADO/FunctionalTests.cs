using System.Data;
using Testcontainers.ClickHouse;

namespace ClickHouse.Client.ADO;

[Trait("Category", "FUNCTIONAL")]
public sealed class FunctionalTests : IAsyncLifetime
{
	private readonly ClickHouseContainer container = new ClickHouseBuilder().Build();

	[Fact]
	public async Task can_get_open_connection() {
#pragma warning disable CA2007
		await using var dataSource = new ClickHouseDataSource(container.GetConnectionString());
		await using var cn = await dataSource.OpenConnectionAsync();
		Assert.Equal(ConnectionState.Open, cn.State);
#pragma warning restore CA2007
	}

	public Task InitializeAsync() {
		return container.StartAsync();
	}

	public Task DisposeAsync() {
		return container.DisposeAsync().AsTask();
	}
}
