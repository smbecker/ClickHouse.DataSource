# ClickHouse Dependency Injection Extensions

_[![Build status](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/ci.yaml/badge.svg?branch=main)](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/ci.yaml)_
_[![CodeQL analysis](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/codeql.yaml/badge.svg?branch=main)](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/codeql.yaml)_

Enables easy integration of [ClickHouse.DataSource](https://www.nuget.org/packages/ClickHouse.DataSource) with the new [.NET Generic Host](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host?tabs=appbuilder) project types.

## Installation

```sh
dotnet add package ClickHouse.Extensions.DependencyInjection
```

## Usage

```c#
services.AddClickHouseDataSource("Host=localhost");
```

This will register the `IClickHouseDataSource` as a singleton in the service collection with the connection string.

## License

This project is licensed under [Apache License, Version 2.0](https://apache.org/licenses/LICENSE-2.0).
