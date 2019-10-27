# netcore-boilerplate

Boilerplate of API in `.NET Core 2.2`.

[![Build Status](https://travis-ci.com/lkurzyniec/netcore-boilerplate.svg?branch=master)](https://travis-ci.com/lkurzyniec/netcore-boilerplate)

## Source code contains

1. [Autofac](https://autofac.org/)
1. [Swagger](https://swagger.io/) + [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle)
1. [EF Core](https://docs.microsoft.com/ef/)
    1. [MySQL provider from Oracle](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core.html)
    1. [MsSQL from Microsoft](https://github.com/aspnet/EntityFrameworkCore/)
1. Tests
    1. [Integration tests](test/HappyCode.NetCoreBoilerplate.Api.IntegrationTests/EmployeesTests.cs) with InMemory database
    1. [Unit tests](test/HappyCode.NetCoreBoilerplate.Api.UnitTests/Controllers/EmployeesControllerTests.cs)
1. Code quality
    1. [editorconfig](.editorconfig)
    1. Analizers ([Microsoft.CodeAnalysis.Analyzers](https://github.com/dotnet/roslyn-analyzers), [Microsoft.AspNetCore.Mvc.Api.Analyzers](https://github.com/aspnet/AspNetCore/tree/master/src/Analyzers))
    1. [Rules](HappyCode.NetCoreBoilerplate.ruleset)
1. Docker
    1. [Dockerfile](dockerfile)
    1. [Docker-compose](docker-compose.yml)
1. [Serilog](https://serilog.net/)

## Architecture

### Api

[HappyCode.NetCoreBoilerplate.Api](src/HappyCode.NetCoreBoilerplate.Api)

* Simple Startup class - [Startup.cs](src/HappyCode.NetCoreBoilerplate.Api/Startup.cs)
  * MvcCore
  * DbContext (with MySQL)
  * DbContext (with MsSQL)
  * Swagger and SwaggerUI (Swashbuckle)
  * HostedService
  * HttpClient
  * HealthCheck
* Filters
  * Global exception handler - [HttpGlobalExceptionFilter.cs](src/HappyCode.NetCoreBoilerplate.Api/Infrastructure/Filters/HttpGlobalExceptionFilter.cs)
  * Action filter to validate `ModelState` - [ValidateModelStateFilter.cs](src/HappyCode.NetCoreBoilerplate.Api/Infrastructure/Filters/ValidateModelStateFilter.cs)
* Container registration place - [ContainerConfigurator.cs](src/HappyCode.NetCoreBoilerplate.Api/Infrastructure/Configurations/ContainerConfigurator.cs)
* Simple exemplary API controllers - [EmployeesController.cs](src/HappyCode.NetCoreBoilerplate.Api/Controllers/EmployeesController.cs), [CarsController.cs](src/HappyCode.NetCoreBoilerplate.Api/Controllers/CarsController.cs)
* Example of BackgroundService - [PingWebsiteBackgroundService.cs](src/HappyCode.NetCoreBoilerplate.Api/BackgroundServices/PingWebsiteBackgroundService.cs)

![HappyCode.NetCoreBoilerplate.Api](https://kurzyniec.pl/wp-content/uploads/2019/10/netcore-boilerplate-api.png "HappyCode.NetCoreBoilerplate.Api")

### Core

[HappyCode.NetCoreBoilerplate.Core](src/HappyCode.NetCoreBoilerplate.Core)

* Simple MySQL DbContext - [EmployeesContext.cs](src/HappyCode.NetCoreBoilerplate.Core/EmployeesContext.cs)
* Simple MsSQL DbContext - [CarsContext.cs](src/HappyCode.NetCoreBoilerplate.Core/CarsContext.cs)
* Exemplary MySQL repository - [EmployeeRepository.cs](src/HappyCode.NetCoreBoilerplate.Core/Repositories/EmployeeRepository.cs)
* Exemplary MsSQL service - [CarService.cs](src/HappyCode.NetCoreBoilerplate.Core/Services/CarService.cs)

![HappyCode.NetCoreBoilerplate.Core](https://kurzyniec.pl/wp-content/uploads/2019/10/netcore-boilerplate-core.png "HappyCode.NetCoreBoilerplate.Core")

## Tests

### Integration tests

[HappyCode.NetCoreBoilerplate.Api.IntegrationTests](test/HappyCode.NetCoreBoilerplate.Api.IntegrationTests)

* Fixture with TestServer - [TestServerClientFixture.cs](test/HappyCode.NetCoreBoilerplate.Api.IntegrationTests/Infrastructure/TestServerClientFixture.cs)
* TestStartup with InMemory databases - [TestStartup.cs](test/HappyCode.NetCoreBoilerplate.Api.IntegrationTests/Infrastructure/TestStartup.cs)
* Simple data feeders - [EmployeeContextDataFeeder.cs](test/HappyCode.NetCoreBoilerplate.Api.IntegrationTests/Infrastructure/EmployeeContextDataFeeder.cs), [CarsContextDataFeeder.cs](test/HappyCode.NetCoreBoilerplate.Api.IntegrationTests/Infrastructure/CarsContextDataFeeder.cs)
* Exemplary tests - [EmployeesTests.cs](test/HappyCode.NetCoreBoilerplate.Api.IntegrationTests/EmployeesTests.cs), [CarsTests.cs](test/HappyCode.NetCoreBoilerplate.Api.IntegrationTests/CarsTests.cs)

![HappyCode.NetCoreBoilerplate.Api.IntegrationTests](https://kurzyniec.pl/wp-content/uploads/2019/10/netcore-boilerplate-itests.png "HappyCode.NetCoreBoilerplate.Api.IntegrationTests")

### Unit tests

[HappyCode.NetCoreBoilerplate.Api.UnitTests](test/HappyCode.NetCoreBoilerplate.Api.UnitTests)

* Exemplary tests - [EmployeesControllerTests.cs](test/HappyCode.NetCoreBoilerplate.Api.UnitTests/Controllers/EmployeesControllerTests.cs)

[HappyCode.NetCoreBoilerplate.Core.UnitTests](test/HappyCode.NetCoreBoilerplate.Core.UnitTests)

* Some test classes to be able mock DbContext - [TestAsyncEnumerable.cs](test/HappyCode.NetCoreBoilerplate.Core.UnitTests/Infrastructure/TestAsyncEnumerable.cs), [TestAsyncEnumerator.cs](test/HappyCode.NetCoreBoilerplate.Core.UnitTests/Infrastructure/TestAsyncEnumerator.cs), [TestAsyncQueryProvider.cs](test/HappyCode.NetCoreBoilerplate.Core.UnitTests/Infrastructure/TestAsyncQueryProvider.cs)
* Extension method to quickly mock of DbSet - [EnumerableExtensions.cs](test/HappyCode.NetCoreBoilerplate.Core.UnitTests/Infrastructure/EnumerableExtensions.cs)
* Exemplary tests - [EmployeeRepositoryTests.cs](test/HappyCode.NetCoreBoilerplate.Core.UnitTests/Repositories/EmployeeRepositoryTests.cs), [CarServiceTests.cs](test/HappyCode.NetCoreBoilerplate.Core.UnitTests/Repositories/CarServiceTests.cs)

![HappyCode.NetCoreBoilerplate.Core.UnitTests](https://kurzyniec.pl/wp-content/uploads/2019/10/netcore-boilerplate-utests.png "HappyCode.NetCoreBoilerplate.Core.UnitTests")

## Build the solution

Just execute `dotnet build` in the root directory, it takes `HappyCode.NetCoreBoilerplate.sln` and build everything.

## Start the application

### Standalone

At first, you need to have up and running [MySQL](https://www.mysql.com/downloads/) and [MsSQL](https://www.microsoft.com/sql-server/sql-server-downloads) database servers on localhost with initialized
database by [mysql script](db/mysql-employees.sql) and [mssql script](db/mssql-cars.sql).

Then the application (API) can be started by `dotnet run` command executed in the `/src/HappyCode.NetCoreBoilerplate.Api` directory.
By default it will be available under `http://localhost:5000`, but keep in mind that documentation is available under
`http://localhost:5000/swagger/`.

### Docker (recommended)

Just run `docker-compose up` command in the root directory and after successful start of services visit `http://localhost:5000/swagger/`.

It will run only MySQL database, without MsSQL database unfortunately.

## Run unit tests

Run `dotnet test` command in the root directory, it will look for test projects in `HappyCode.NetCoreBoilerplate.sln` and run them.

## To Do

* [DbUp](http://dbup.github.io/) as a database migration tool
* feature branch with .NET Core 3.0 (IMHO not yet ready for PROD)
* move README info to Wiki, leave here real boilerplate info

## Be like a star, give me a star! :star:

If:

* you like this repo/code,
* you learn something,
* you are using it in your project/application,

then please give me a star, appreciate my work. Thanks!

## Contribution

You are very welcome to submit either issues or pull requests to this repository!

For pull request please follow this rules:

* Commit messages should be clear and as much as possible descriptive.
* Rebase if required.
* Make sure that your code compile and run locally.
* Changes do not break any code quality rules.
