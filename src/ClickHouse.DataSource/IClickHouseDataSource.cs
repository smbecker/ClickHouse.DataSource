namespace ClickHouse.Client;

public interface IClickHouseDataSource
{
	string ConnectionString {
		get;
	}

	IClickHouseConnection CreateConnection();

	IClickHouseConnection OpenConnection();

	Task<IClickHouseConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);
}
