# ClickHouse OpenTelemetry Extensions

_[![Build status](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/ci.yaml/badge.svg?branch=main)](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/ci.yaml)_
_[![CodeQL analysis](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/codeql.yaml/badge.svg?branch=main)](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/codeql.yaml)_

Integrates [ClickHouse.Client](https://github.com/DarkWanderer/ClickHouse.Client/) with the [OpenTelemetry SDK](https://github.com/open-telemetry/opentelemetry-dotnet) APIs.

## Installation

```sh
dotnet add package ClickHouse.OpenTelemetry
```

## Usage

```c#
services.AddOpenTelemetry().WithTracing(static tracing => tracing.AddClickHouse());
```

## License

This project is licensed under [Apache License, Version 2.0](https://apache.org/licenses/LICENSE-2.0).
