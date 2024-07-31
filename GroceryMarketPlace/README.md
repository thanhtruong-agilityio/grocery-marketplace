# Grocery Market APIs - ASP.NET Core Training Practice

## Grocery Marketplace

  * This is a practice to build APIs for an grocery marketplace store.
  * [Database diagram](https://dbdiagram.io/d/66263a0003593b6b619ea2be)
  * [Design UI](https://www.figma.com/file/csqXfrBOQuLUMs8O4ACaVa/grocery-marketplace-tradly.app-(Copy)?type=design&node-id=0-367&mode=design&t=4OFVnXTNmIesB0ps-0)
  * [Estimation plan](https://docs.google.com/document/d/1OjCQTPggXBrgDYH2hUJf-pvuL64I0Jjp10xmItyWZqI/edit?usp=sharing)
  * [Gitlab board](https://gitlab.asoft-python.com/thanh.truong/dotnet-training/-/boards)
  * [Gitlab repo](https://gitlab.asoft-python.com/thanh.truong/dotnet-training)

## Technical Stack
- Architecture: Traditional N-Layer architecture
- Database: PostgreSQL service
- EF Core for data access
  - Generic Repository Pattern
  - Unit of work patterns
- Dependency Injection
- AutoMapper object mapping
- Swashbuckle For API documentation
- FluentValidation for Model validations
- Serilog for logging capabilities
- Source code analysis
  - Using .Net Analyzer
- Caching using redis cache
- API versioning
- XUnit & MOQ for Unit Testing
- Integration Test

## Getting started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 8.0](https://dotnet.microsoft.com/download/dotnet-core/8.0)
* EF Core 8.0 or later
* Docker

### Installing
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)

Clone project:
```bash
git clone git@gitlab.asoft-python.com:thanh.truong/dotnet-training.git
```

Checkout branch:
```bash
git checkout feature/grocery-marketplace
```
Go to project:
```bash
cd GroceryMarketplace/
```

At the root directory of solution which include **docker-compose.yml** files, run below command:
```bash
docker-compose -f docker-compose.yml build
docker-compose -f docker-compose.yml up
```

Visit [http://localhost:7072](http://localhost:7072) and you'll see your site up and running 🧘‍♀️

### API documentation
Written version:
  * [APIs](doc/swagger.yml)

Auto-generated version:
  * [Swagger](http://localhost:7072/swagger/index.html)

### Unit Testing and coverage

To run all tests:

```bash
cd GroceryMarketplace.Tests
dotnet test --settings test.runsettings
```

![coverage-report]()

To generate the test coverage report

```bash
cd GroceryMarketplace.Tests
dotnet test --collect:"XPlat Code Coverage" --results-directory:"./.coverage"
dotnet reportgenerator -reports:"TestResults/**/*.cobertura.xml" -targetdir:".coverage-report/" -reporttypes:"HTML"
```
### Integration Tests

To run all tests:

```bash
cd GroceryMarketplace.IntegrationTests
dotnet test --settings test.runsettings
```

To generate the test coverage report

```bash
cd GroceryMarketplace.IntegrationTests
dotnet test --collect:"XPlat Code Coverage" --results-directory:"./.coverage"
dotnet reportgenerator -reports:"TestResults/**/*.cobertura.xml" -targetdir:".coverage-report/" -reporttypes:"HTML"
```

## Structure of Project
Repository include layers divided by **4 project**;
- BDD

## Authors

* **Thanh Truong (thanh.truong@asnet.com.vn)**