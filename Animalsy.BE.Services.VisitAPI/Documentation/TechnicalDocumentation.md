# VisitAPI Technical Documentation

## Overview

The VisitAPI service is responsible for managing visit-related data within the Animalsy application. This document provides an overview of the technical setup, including the libraries used, database configuration, Docker setup, and other relevant details.

## Libraries Used

- **Entity Framework Core** (v6.0.0): For database interactions and migrations.
- **Microsoft SQL Server**: As the database server.
- **Swashbuckle** (v6.2.3): For generating Swagger documentation.
- **IdentityModel** (v5.2.0): For handling authentication and authorization with JWT tokens.

## Dockerfile

The Dockerfile is used to build a Docker image for the VisitAPI service. It defines the steps to set up the environment, copy the necessary files, restore dependencies, build the project, and expose the required ports.

## Database Configuration

The VisitAPI service uses Microsoft SQL Server as its database. Below are the configurations for different environments:

### Development Configuration

Configured in `appsettings.Development.json`:

- **JWT Options**: Includes settings for generating and validating JWT tokens, such as the secret key, issuer, audience, and token expiration time.

### Development Local Configuration

Configured in `appsettings.Development.Local.json`:

- **Connection String**: Connects to a local SQL Server instance using LocalDB. Useful for developers running the database locally.
- **Service URLs**: Defines URLs for other services such as `ContractorApi`, `CustomerApi`, `PetApi`, `ProductApi`, and `VendorApi` running locally.

### Development Docker Configuration

Configured in `appsettings.Development.Docker.json`:

- **Connection String**: Connects to a SQL Server instance running in a Docker container. Uses the `dbserver` hostname and appropriate port.
- **Service URLs**: Defines URLs for other services within the Docker environment such as `ContractorApi`, `CustomerApi`, `PetApi`, `ProductApi`, and `VendorApi`.

### Production Configuration

Configured in `appsettings.Production.json`:

- **Connection String**: Connects to a SQL Server instance suitable for a production environment. Uses a specific server, user ID, and password.
- **JWT Options**: Production-specific settings for generating and validating JWT tokens.
- **Service URLs**: Defines URLs for other services such as `ContractorApi`, `CustomerApi`, `PetApi`, `ProductApi`, and `VendorApi` within the production environment.

## Database Context

The `AppDbContext` class defines the database context for the VisitAPI service, including DbSet properties for each entity type, such as `Visit`.

## Entities

### Visit

Represents the visit entity with properties such as `Id`, `CustomerId`, `VendorId`, `VisitDate`, and `Description`.

## Additional Configuration

### Logging

Specified in `appsettings.json`, it defines log levels for different parts of the application, including default and ASP.NET Core-specific logs.

### Program.cs

Sets up the web host, configures services, and specifies middleware, calling the `Startup` class to configure the application.

### Database Schema

The database schema for the VisitAPI includes the following tables, derived from the `AppDbContext` setup:

#### Tables

##### Visits

Stores visit information, including custom properties defined in `Visit`.

- **Id** (uniqueidentifier, PK): The primary key of the visit.
- **CustomerId** (uniqueidentifier): The customer ID associated with the visit.
- **VendorId** (uniqueidentifier): The vendor ID associated with the visit.
- **VisitDate** (datetime): The date of the visit.
- **Description** (nvarchar(max)): The description of the visit.

## Conclusion

This documentation provides a comprehensive overview of the technical setup for the VisitAPI service, including library usage, database configuration, Docker setup, and code structure.
