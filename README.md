# Service Template
The goal of this repository is to provide a template .Net Solution that can be used as starting point for creating a 
microservice.

## Architecture
The template relies on Onion Architecture to structure its code. This architecture promotes separation of concerns and
clearly defines the difference between business logic and infrastructural layers like data persistence or external api
communication.

These layers are broken down as follows

**Presentation - User.Api**

The presentation layer defines the external entry point for the application. Controllers and their endpoints are defined 
here along with any kind of validation logic. Dependencies are all registered from this layer within Program.cs but the
implementation of how each layer registers its dependencies are delegated down to each layer themselves.

Request and response models are also defined in this layer.

**Application - User.Application**

Business logic is implemented within this layer through service classes. Services are responsible for making calls out to 
the infrastructure layers and are used to return data to their callers and execute business logic which would
manipulate data.

Messaging consumers are defined in this layer. Consumers directly interact with application services
and can themselves also perform business logic if needed by communicating with infrastructure layers.

Mapping logic is another form of business logic and therefore is defined within the application layer.

**Application - User.Application.Contracts**

**Domain - User.Domain**

**Infrastructure - User.Infrastructure.EfCore**

**Infrastructure - User.Infrastructure.Redis**

**Shared - Shared.Kernel**

## Technology Used
- .Net 8
- Serilog
- MassTransit
- EfCore
- FluentValidation
- XUnit
- Moq
