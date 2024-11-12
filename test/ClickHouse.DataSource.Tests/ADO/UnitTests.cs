namespace ClickHouse.Client.ADO;

public sealed class UnitTests
{
	[Fact]
	public void can_create_connection_from_datasource() {
		const string connectionString = "Host=localhost;Port=1234";
		using var dataSource = new ClickHouseDataSource(connectionString);

		using var fromDataSource = dataSource.CreateConnection();
		using var rawConnection = new ClickHouseConnection(connectionString);
		Assert.Equal(rawConnection.ConnectionString, fromDataSource.ConnectionString);
	}
}
