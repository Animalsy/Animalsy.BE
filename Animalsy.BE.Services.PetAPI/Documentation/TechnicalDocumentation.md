# PetAPI Technical Documentation

## Overview

The PetAPI service is responsible for managing pet-related data within the Animalsy application. This document provides an overview of the technical setup, including the libraries used, database configuration, Docker setup, and other relevant details.

## Libraries Used

- **Entity Framework Core** (v6.0.0): For database interactions and migrations.
- **Microsoft SQL Server**: As the database server.
- **Swashbuckle** (v6.2.3): For generating Swagger documentation.
- **IdentityModel** (v5.2.0): For handling authentication and authorization with JWT tokens.

## Dockerfile

The Dockerfile is used to build a Docker image for the PetAPI service. It defines the steps to set up the environment, copy the necessary files, restore dependencies, build the project, and expose the required ports.

## Database Configuration

The PetAPI service uses Microsoft SQL Server as its database. Below are the configurations for different environments:

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

The `AppDbContext` class defines the database context for the PetAPI service, including DbSet properties for each entity type, such as `Pet`.

## Entities

### Pet

Represents the pet entity with properties such as `Id`, `UserId`, `Species`, `Race`, `Name`, `DateOfBirth`, and `ImageUrl`.

## Additional Configuration

### Logging

Specified in `appsettings.json`, it defines log levels for different parts of the application, including default and ASP.NET Core-specific logs.

### Program.cs

Sets up the web host, configures services, and specifies middleware, calling the `Startup` class to configure the application.

### Database Schema

The database schema for the PetAPI includes the following tables, derived from the `AppDbContext` setup:

#### Tables

##### Pets

Stores pet information, including custom properties defined in `Pet`.

- **Id** (uniqueidentifier, PK): The primary key of the pet.
- **UserId** (uniqueidentifier): The user ID associated with the pet.
- **Species** (nvarchar(max)): The species of the pet.
- **Race** (nvarchar(max)): The race of the pet.
- **Name** (nvarchar(max)): The name of the pet.
- **DateOfBirth** (datetime): The date of birth of the pet.
- **ImageUrl** (nvarchar(max), nullable): The URL of the pet's image.

## Conclusion

This documentation provides a comprehensive overview of the technical setup for the PetAPI service, including library usage, database configuration, Docker setup, and code structure.
