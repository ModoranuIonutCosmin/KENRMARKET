# KENRMARKET - Microservices based online shop

* application that provides the main functionalities of an online shop, using microservices architecture.
* it was designed so it can easily scale up when it receives a lot of traffic, each separate concern is intuitively split into a separate logical microservice.
* follows best practices in microservices design (identifying bounded contexts, designing communication between, picking adecvate technologies for implementing certain bussiness requiremetns)
* follows common patterns used for microservices top level architecture as well as DDD patterns for each separate microservice.

## Features
- allows users to find products they want with the help of a wide range of available filters
- tracks user's shopping cart and updates the status of shopping cart / orders in realtime using SignalR (for example when an item from the shopping cart has its price changed)
- allows paying with a credit cart using Stripe payment service

## Technologies

### Back end
- 7 microservices (Products, Cart, Orders, Orders.SignalR, Payments, Customers, Microservices Gateway) \+ Watchdog web app for monitoring availability of each infrastructure element.
- Web APIs using ASP .NET Core 6.
- Domain Driven Design patterns  (including CQRS / services - repository pattern / simple CRUD), the pattern was chosen for each microservices is based on the expected size of the microservice.
- Multiple data stores including SQL Server (using Entity Framework 5 ORM for Persistance Layer),
  MongoDB
- Async communication between microservices using RabbitMQ MassTransit for integration events between microservices
- SignalR for updating resources status in real time to client apps.
- Payments handling with Stripe
- ASP .NET Identity for Authentication and Authorization module at the Microservices Gateway level

### Architectural decisions
- designed for scalability -> for deployment using AKS (Azure Kubernetes Service), Azure SQL, Azure Cosmos DB, Azure Service Bus
- using gateway microservice
    + acts as a reverse proxy
    + handles user authorization
    + only service that is publicly reachable, the rest of the microservices are used internally
- using Outbox / Inbox pattern to ensure resilience when using async communication with RabbitMQ -> exactly once behaviour, idempotent messages
- using resilience strategies for accessing other infrastructure resources like DBs, other microservices with the help of Polly library (allowing things like exponential backoff retry, circuit breaker)
- picking NoSQL DB, MongoDB for Products so the app benefits from a performance boost (not having to do bulky relational JOINS for getting properties about a products -> for example technical specifications key/value details).
- using Generic repository pattern for ensuring easier testing of the data access layer.
- using DDD patterns like CQRS with Mediator Pattern.