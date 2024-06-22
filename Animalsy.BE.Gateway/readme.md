# Animalsy.BE.Gateway Documentation

## Overview

The `Animalsy.BE.Gateway` is the entry point for all the backend services of the Animalsy application, excluding the `AuthAPI`. It uses Ocelot to provide a unified API gateway that routes requests to the appropriate downstream services. This setup simplifies the communication between the frontend and multiple backend services by providing a single endpoint.

### Purpose

- Acts as a reverse proxy and routes requests to the corresponding backend services.
- Handles authentication and authorization.
- Aggregates multiple services into a single endpoint for easier consumption by the frontend.

## Technical Documentation

### Libraries Used

- **Ocelot**: For creating the API gateway and routing requests to downstream services.
- **Swashbuckle**: For generating Swagger documentation.
- **IdentityModel**: For handling authentication and authorization with JWT tokens.

### Configuration

#### Global Configuration

The global configuration is defined in `ocelot.global.json` and sets the base URL for the gateway.

{
  "GlobalConfiguration": {
    "BaseUrl": "http://animalsy-be:8080"
  }
}

### Route Configurations

Each API has its own route configuration file defined in the `ocelot.*.api.json` files. Below are the details for each service:

#### Contractor API

Configured in `ocelot.contractor.api.json`:

- **Routes:**
  - `GET /api/contractor/{contractorId}`: Retrieves a contractor by their ID. (Authorization required)
  - `DELETE /api/contractor/{contractorId}`: Deletes a contractor by their ID. (Authorization required)
  - `GET /api/contractor/vendor/{vendorId}`: Retrieves contractors by vendor ID.
  - `GET /api/contractor/vendor/{vendorId}/{specialization}`: Retrieves contractors by vendor ID and specialization.
  - `POST /api/contractor`: Creates a new contractor. (Authorization required)
  - `PUT /api/contractor`: Updates an existing contractor. (Authorization required)

#### Customer API

Configured in `ocelot.customer.api.json`:

- **Routes:**
  - `GET /api/customer`: Retrieves a list of all customers. (Authorization required)
  - `POST /api/customer`: Creates a new customer. (Authorization required)
  - `PUT /api/customer`: Updates an existing customer. (Authorization required)
  - `GET /api/customer/{customerId}`: Retrieves a customer by their ID. (Authorization required)
  - `GET /api/customer/profile/{userId}`: Retrieves a customer profile by user ID. (Authorization required)
  - `DELETE /api/customer/{userId}`: Deletes a customer by their user ID. (Authorization required)

#### Pet API

Configured in `ocelot.pet.api.json`:

- **Routes:**
  - `GET /api/pet/user/{userId}`: Retrieves pets by user ID. (Authorization required)
  - `GET /api/pet/{petId}`: Retrieves a pet by its ID. (Authorization required)
  - `DELETE /api/pet/{petId}`: Deletes a pet by its ID. (Authorization required)
  - `POST /api/pet`: Creates a new pet. (Authorization required)
  - `PUT /api/pet`: Updates an existing pet. (Authorization required)

#### Product API

Configured in `ocelot.product.api.json`:

- **Routes:**
  - `GET /api/product`: Retrieves a list of all products.
  - `GET /api/product/vendor/{vendorId}`: Retrieves products by vendor ID.
  - `GET /api/product/vendor/{categoryAndSubCategory}`: Retrieves vendor IDs by product category.
  - `GET /api/product/vendor/{vendorId}/{categoryAndSubCategory}`: Retrieves products by vendor ID and category.
  - `GET /api/product/{productId}`: Retrieves a product by its ID. (Authorization required)
  - `POST /api/product`: Creates a new product. (Authorization required)
  - `PUT /api/product`: Updates an existing product. (Authorization required)
  - `DELETE /api/product/{productId}`: Deletes a product by its ID. (Authorization required)

#### Vendor API

Configured in `ocelot.vendor.api.json`:

- **Routes:**
  - `GET /api/vendor`: Retrieves a list of all vendors.
  - `GET /api/vendor/{vendorId}`: Retrieves a vendor by their ID.
  - `GET /api/vendor/profiles/{userId}`: Retrieves vendor profiles by user ID. (Authorization required)
  - `POST /api/vendor`: Creates a new vendor. (Authorization required)
  - `PUT /api/vendor`: Updates an existing vendor. (Authorization required)
  - `DELETE /api/vendor/{vendorId}`: Deletes a vendor by their ID. (Authorization required)

#### Visit API

Configured in `ocelot.visit.api.json`:

- **Routes:**
  - `GET /api/visit/{visitId}`: Retrieves a visit by its unique ID. (Authorization required)
  - `DELETE /api/visit/{visitId}`: Deletes a visit by its unique ID. (Authorization required)
  - `GET /api/visit/vendor/{vendorId}`: Retrieves visits by vendor ID. (Authorization required)
  - `GET /api/visit/customer/{customerId}`: Retrieves visits by customer ID. (Authorization required)
  - `POST /api/visit`: Creates a new visit. (Authorization required)
  - `PUT /api/visit`: Updates an existing visit. (Authorization required)

