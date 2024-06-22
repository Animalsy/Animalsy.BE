# AuthAPI Technical Documentation

## Overview

The AuthAPI service is responsible for managing user authentication and roles within the Animalsy application. This document provides an overview of the technical setup, including the libraries used, database configuration, and other relevant details.

## Libraries Used

- **Entity Framework Core** (v6.0.0): For database interactions and migrations.
- **ASP.NET Core Identity** (v6.0.0): For managing user authentication and authorization.
- **Microsoft SQL Server**: As the database server.
- **Swashbuckle** (v6.2.3): For generating Swagger documentation.
- **IdentityModel** (v5.2.0): For handling authentication and authorization with JWT tokens.

## Database Configuration

The AuthAPI service uses Microsoft SQL Server as its database. Below are the configurations for different environments:

### Development Configuration

Configured in `appsettings.Development.json`:

- **Connection String**: Connects to a local SQL Server instance using LocalDB. Suitable for general development work.
- **JWT Options**: Includes settings such as the secret key, issuer, audience, and token expiration time for generating and validating JWT tokens.

### Development Local Configuration

Configured in `appsettings.Development.Local.json`:

- **Connection String**: Connects to a local SQL Server instance using LocalDB. Useful for developers running the database locally.
- **JWT Options**: Same as in the general development configuration, used to generate and validate JWT tokens.

### Development Docker Configuration

Configured in `appsettings.Development.Docker.json`:

- **Connection String**: Connects to a SQL Server instance running in a Docker container. Uses the `dbserver` hostname and appropriate port.
- **Service URLs**: Defines URLs for other services, such as `CustomerApi`, within the Docker environment.

### Production Configuration

Configured in `appsettings.Production.json`:

- **Connection String**: Connects to a SQL Server instance suitable for a production environment. Uses a specific server, user ID, and password.
- **JWT Options**: Production-specific settings for the secret key, issuer, audience, and token expiration time for JWT tokens.

## Database Context

The `AppDbContext` class defines the database context for the AuthAPI service, including DbSet properties for each entity type, such as `ApplicationUser` and `ApplicationRole`.

## Entities

### ApplicationUser

Represents the user entity with properties such as `Name`, `LastName`, `City`, `Street`, `Building`, `Flat`, `PostalCode`, and `PhoneNumber`.

### ApplicationRole

Represents the role entity, extending from `IdentityRole`.

## Additional Configuration

### Logging

Specified in `appsettings.json`, it defines log levels for different parts of the application, including default and ASP.NET Core-specific logs.

### Program.cs

Sets up the web host, configures services, and specifies middleware, calling the `Startup` class to configure the application.

### Database Schema

The database schema for the AuthAPI includes the following tables, derived from the `AppDbContext` and `Identity` setup:

#### Default Identity Tables

- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- AspNetUserClaims
- AspNetRoleClaims
- AspNetUserLogins
- AspNetUserTokens

#### Custom Tables

##### AspNetUsers

Stores user information, including custom properties defined in `ApplicationUser`.

- **Id** (nvarchar(450), PK): The primary key of the user.
- **UserName** (nvarchar(256)): The username of the user.
- **NormalizedUserName** (nvarchar(256)): The normalized username of the user.
- **Email** (nvarchar(256)): The email address of the user.
- **NormalizedEmail** (nvarchar(256)): The normalized email address of the user.
- **EmailConfirmed** (bit): Whether the email is confirmed.
- **PasswordHash** (nvarchar(max)): The password hash of the user.
- **SecurityStamp** (nvarchar(max)): A random value that changes whenever a user’s credentials change (password, login, etc.).
- **ConcurrencyStamp** (nvarchar(max)): A random value that should change whenever a user is persisted to the store.
- **PhoneNumber** (nvarchar(max)): The phone number of the user.
- **PhoneNumberConfirmed** (bit): Whether the phone number is confirmed.
- **TwoFactorEnabled** (bit): Whether two-factor authentication is enabled.
- **LockoutEnd** (datetimeoffset): The date and time until the user lockout ends.
- **LockoutEnabled** (bit): Whether the user can be locked out.
- **AccessFailedCount** (int): The number of failed access attempts.
- **Name** (nvarchar(max)): The first name of the user.
- **LastName** (nvarchar(max)): The last name of the user.
- **City** (nvarchar(max)): The city of the user.
- **Street** (nvarchar(max)): The street address of the user.
- **Building** (nvarchar(max)): The building number of the user.
- **Flat** (nvarchar(max)): The flat number of the user.
- **PostalCode** (nvarchar(max)): The postal code of the user.

##### AspNetRoles

Stores role information.

- **Id** (nvarchar(450), PK): The primary key of the role.
- **Name** (nvarchar(256)): The name of the role.
- **NormalizedName** (nvarchar(256)): The normalized name of the role.
- **ConcurrencyStamp** (nvarchar(max)): A random value that should change whenever a role is persisted to the store.

##### AspNetUserRoles

Stores the relationships between users and roles.

- **UserId** (nvarchar(450), PK, FK): The primary key linking to the AspNetUsers table.
- **RoleId** (nvarchar(450), PK, FK): The primary key linking to the AspNetRoles table.

## Conclusion

This documentation provides a comprehensive overview of the technical setup for the AuthAPI service, including library usage, database configuration, and code structure.
