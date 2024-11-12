# ClickHouse.DataSource

_[![Build status](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/ci.yaml/badge.svg?branch=main)](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/ci.yaml)_
_[![CodeQL analysis](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/codeql.yaml/badge.svg?branch=main)](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/codeql.yaml)_

Abstracts the creation of `ClickHouseConnection` instances from [ClickHouse.Client](https://github.com/DarkWanderer/ClickHouse.Client/).

## Installation

```sh
dotnet add package ClickHouse.DataSource
```

## Usage

```c#
var dataSource = new ClickHouseDataSource("connection string");
await using var cn = await dataSource.OpenConnectionAsync().ConfigureAwait(false);
```

## License

This project is licensed under [Apache License, Version 2.0](https://apache.org/licenses/LICENSE-2.0).
