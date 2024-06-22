# ProductAPI Technical Documentation

## Overview

The ProductAPI service is responsible for managing product-related data within the Animalsy application. This document provides an overview of the technical setup, including the libraries used, database configuration, Docker setup, and other relevant details.

## Libraries Used

- **Entity Framework Core** (v6.0.0): For database interactions and migrations.
- **Microsoft SQL Server**: As the database server.
- **Swashbuckle** (v6.2.3): For generating Swagger documentation.
- **IdentityModel** (v5.2.0): For handling authentication and authorization with JWT tokens.

## Dockerfile

The Dockerfile is used to build a Docker image for the ProductAPI service. It defines the steps to set up the environment, copy the necessary files, restore dependencies, build the project, and expose the required ports.

## Database Configuration

The ProductAPI service uses Microsoft SQL Server as its database. Below are the configurations for different environments:

### Development Configuration

Configured in `appsettings.Development.json`:

- **JWT Options**: Includes settings for generating and validating JWT tokens.

### Development Local Configuration

Configured in `appsettings.Development.Local.json`:

- **Connection String**: Connects to a local SQL Server instance using LocalDB. Useful for developers running the database locally.

### Development Docker Configuration

Configured in `appsettings.Development.Docker.json`:

- **Connection String**: Connects to a SQL Server instance running in a Docker container. Uses the `dbserver` hostname and appropriate port.

### Production Configuration

Configured in `appsettings.Production.json`:

- **Connection String**: Connects to a SQL Server instance suitable for a production environment. Uses a specific server, user ID, and password.
- **JWT Options**: Production-specific settings for generating and validating JWT tokens.

## Database Context

The `AppDbContext` class defines the database context for the ProductAPI service, including DbSet properties for each entity type, such as `Product`.

## Entities

### Product

Represents the product entity with properties such as `Id`, `Name`, `Description`, `Price`, `Stock`, and `ImageUrl`.

## Additional Configuration

### Logging

Specified in `appsettings.json`, it defines log levels for different parts of the application, including default and ASP.NET Core-specific logs.

### Program.cs

Sets up the web host, configures services, and specifies middleware, calling the `Startup` class to configure the application.

### Database Schema

The database schema for the ProductAPI includes the following tables, derived from the `AppDbContext` setup:

#### Tables

##### Products

Stores product information, including custom properties defined in `Product`.

- **Id** (uniqueidentifier, PK): The primary key of the product.
- **Name** (nvarchar(max)): The name of the product.
- **Description** (nvarchar(max)): The description of the product.
- **Price** (decimal): The price of the product.
- **Stock** (int): The stock quantity of the product.
- **ImageUrl** (nvarchar(max), nullable): The URL of the product's image.

## Conclusion

This documentation provides a comprehensive overview of the technical setup for the ProductAPI service, including library usage, database configuration, Docker setup, and code structure.
