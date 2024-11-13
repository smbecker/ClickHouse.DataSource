# ClickHouse Client Extensions

_[![Build status](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/ci.yaml/badge.svg?branch=main)](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/ci.yaml)_
_[![CodeQL analysis](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/codeql.yaml/badge.svg?branch=main)](https://github.com/smbecker/ClickHouse.DataSource/actions/workflows/codeql.yaml)_

Provides additional capabilities on top of the great [ClickHouse.Client](https://github.com/DarkWanderer/ClickHouse.Client/) library.

| Package                                                                                                                                                                                                                                                                        | Downloads                                                                                                                                                     | NuGet Latest |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------| ------------ |
| `ClickHouse.DataSource`<br />Abstracts creation of `ClickHouseConnection`  | [![Nuget](https://img.shields.io/nuget/dt/ClickHouse.DataSource)](https://www.nuget.org/packages/ClickHouse.DataSource) | [![Nuget](https://img.shields.io/nuget/v/ClickHouse.DataSource)](https://www.nuget.org/packages/ClickHouse.DataSource) |
| `ClickHouse.Extensions.DependencyInjection`<br />Enables easy integration with [.Net Generic Host](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host?tabs=appbuilder) applications                                                                          | [![Nuget](https://img.shields.io/nuget/dt/ClickHouse.Extensions.DependencyInjection)](https://www.nuget.org/packages/ClickHouse.Extensions.DependencyInjection) | [![Nuget](https://img.shields.io/nuget/v/ClickHouse.Extensions.DependencyInjection)](https://www.nuget.org/packages/ClickHouse.Extensions.DependencyInjection) |
| `ClickHouse.Extensions.OpenTelemetry`<br />Adds extension methods to facilitate easy integration with the [OpenTelemetry SDK](https://github.com/open-telemetry/opentelemetry-dotnet).                                                                                          | [![Nuget](https://img.shields.io/nuget/dt/ClickHouse.Extensions.OpenTelemetry)](https://www.nuget.org/packages/ClickHouse.Extensions.OpenTelemetry)             | [![Nuget](https://img.shields.io/nuget/v/ClickHouse.Extensions.OpenTelemetry)](https://www.nuget.org/packages/ClickHouse.Extensions.OpenTelemetry) |

## Versioning

We use [SemVer](http://semver.org/) along with [MinVer](https://github.com/adamralph/minver) for versioning. For the versions available, see the [tags on this repository](https://github.com/smbecker/ClickHouse.DataSource/tags).

## Contributing

Contributions are welcomed and greatly appreciated. See also the list of [contributors](https://github.com/smbecker/ClickHouse.DataSource/contributors) who participated in this project. Read the [CONTRIBUTING](CONTRIBUTING.md) guide for how to participate.

Git Hooks are enabled on this repository. You will need to run `git config --local core.hooksPath .githooks/` to enable them in your environment.

## License

This project is licensed under [Apache License, Version 2.0](https://apache.org/licenses/LICENSE-2.0).
