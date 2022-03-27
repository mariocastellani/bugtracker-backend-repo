# BugTracker backend repo
This is a simple backend application to record issues and track them.

## Motivation
Show a little bit of my codding skills and my knowledge about software architecture and patterns.

## Architecture
* This is an example application that was developed following the [DDD (Domain-Driven Design)](https://martinfowler.com/bliki/DomainDrivenDesign.html) approach.

* The application projects were organized according to [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) principle.

## Projects Organization

1. **Application Core**

   * **`Domain`** Domain Layer containing all the business logic and is agnostic of any technology.
   * **`Application`** App Layer containing all about application logic and presentation concerns.
   * **`SharedKernel`** Class Library containing all interfaces and some basic implementations of a common framework.

2. **Infrastructure**
   
   * **`Infrastructure.Data`** Data Access Layer containing repositories and data context implementations.
   * **`SharedKernel.EntityFrameworkCore`** Class Library containing some SharedKernel implementations with [EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/).
   > _Note:_ There are other libs which name start with _"SharedKernel."_, each one implements different interfaces of main SharedKernel in a specific technology.

3. **Services**

   * [**`GatewayService`**](docs/GATEWAYSERVICE.md) It's just an entry point for any front-end.
   * [**`IdentityService`**](docs/IDENTITYSERVICE.md) It's the identity and authentication service.
   * [**`ApiService`**](docs/APISERVICE.md) It's the main backend API service.
   * [**`JobService`**](docs/JOBSERVICE.md) It's a worker service for background scheduled tasks.

## VS Solution
This repo contain a Visual Studio solution file **`BugTrackerBackend.sln`** that you can open to see how the projects were organized.