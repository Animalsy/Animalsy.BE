# CustomerAPI Documentation

## Overview

This is the documentation for the CustomerAPI. It provides endpoints for managing customers within the system. Each endpoint description includes details about the request parameters, request body, response, authorization, and validation requirements.

## Authorization

All endpoints require specific roles for authorization. Ensure that the JWT token includes the appropriate roles to access each endpoint.

## Endpoints

### Get All Customers

**Endpoint:** `GET /Api/Customer`

**Description:** Retrieves a list of all customers.

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of CustomerDto`
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires `Admin` role

**Validation:** None

---

### Create a New Customer

**Endpoint:** `POST /Api/Customer`

**Description:** Creates a new customer.

**Request Body:**
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the customer
- `lastName` (string, required): Last name of the customer
- `city` (string, required): City of residence
- `street` (string, required): Street address
- `building` (string, required): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, required): Postal code
- `phoneNumber` (string, required): Phone number
- `emailAddress` (string, required): Email address

**Responses:**
- `201 Created`: Success
  - Content-Type: application/json
  - Schema: `uuid`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `409 Conflict`: Conflict in resource creation
- `500 Internal Server Error`: Server error

**Authorization:** None

**Validation:** Yes

---

### Update a Customer

**Endpoint:** `PUT /Api/Customer`

**Description:** Updates an existing customer.

**Request Body:**
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the customer
- `lastName` (string, required): Last name of the customer
- `city` (string, required): City of residence
- `street` (string, required): Street address
- `building` (string, required): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, required): Postal code
- `phoneNumber` (string, required): Phone number
- `emailAddress` (string, required): Email address

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Get Customer by ID

**Endpoint:** `GET /Api/Customer/{customerId}`

**Description:** Retrieves a customer by their unique ID.

**Path Parameters:**
- `customerId` (uuid, required): Unique identifier for the customer

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `CustomerDto`
- `400 Bad Request`: Invalid request data
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor or admin`

**Validation:** None

---

### Delete Customer by ID

**Endpoint:** `DELETE /Api/Customer/{userId}`

**Description:** Deletes a customer by their unique ID.

**Path Parameters:**
- `userId` (uuid, required): Unique identifier for the user

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** None

---

### Get Customer Profile by User ID

**Endpoint:** `GET /Api/Customer/Profile/{userId}`

**Description:** Retrieves the profile of a customer by their user ID.

**Path Parameters:**
- `userId` (uuid, required): Unique identifier for the user

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `CustomerProfileDto`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** None

## Components

### Schemas

#### CustomerDto

- `id` (uuid, required): Unique identifier for the customer
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the customer
- `lastName` (string, required): Last name of the customer
- `city` (string, required): City of residence
- `street` (string, required): Street address
- `building` (string, required): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, required): Postal code
- `phoneNumber` (string, required): Phone number
- `emailAddress` (string, required): Email address

#### CreateCustomerDto

- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the customer
- `lastName` (string, required): Last name of the customer
- `city` (string, required): City of residence
- `street` (string, required): Street address
- `building` (string, required): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, required): Postal code
- `phoneNumber` (string, required): Phone number
- `emailAddress` (string, required): Email address

#### UpdateCustomerDto

- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the customer
- `lastName` (string, required): Last name of the customer
- `city` (string, required): City of residence
- `street` (string, required): Street address
- `building` (string, required): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, required): Postal code
- `phoneNumber` (string, required): Phone number
- `emailAddress` (string, required): Email address

#### CustomerProfileDto

- `customer` (CustomerDto, required): Customer details
- `pets` (array of PetDto, nullable): List of customer's pets
- `visits` (array of VisitDto, nullable): List of customer's visits
- `responseDetails` (array of StringStringKeyValuePair, nullable): Additional response details

#### PetDto

- `id` (uuid, required): Unique identifier for the pet
- `userId` (uuid, required): Unique identifier for the user
- `species` (string, required): Species of the pet
- `race` (string, required): Race of the pet
- `name` (string, required): Name of the pet
- `dateOfBirth` (string, required, format: date-time): Date of birth of the pet
- `imageUrl` (string, nullable): URL of the pet's image

#### VisitDto

- `id` (uuid, required): Unique identifier for the visit
- `product` (ProductDto, required): Product details
- `vendor` (VendorDto, required): Vendor details
- `contractor` (ContractorDto, required): Contractor details
- `pet` (PetDto, required): Pet details
- `customer` (CustomerDto, required): Customer details
- `date` (string, required, format: date-time): Date of the visit
- `comment` (string, nullable): Comment about the visit
- `status` (string, nullable): Status of the visit

#### ProductDto

- `id` (uuid, required): Unique identifier for the product
- `vendorId` (uuid, required): Unique identifier for the vendor
- `name` (string, required): Name of the product
- `description` (string, required): Description of the product
- `categoryAndSubCategory` (string, required): Category and subcategory of the product
- `minPrice` (number, required, format: double): Minimum price of the product
- `maxPrice` (number, required, format: double): Maximum price of the product
- `promoPrice` (number, required, format: double): Promotional price of the product
- `duration` (string, required, format: date-span): Duration of the product

#### StringStringKeyValuePair

- `key` (string, required): Key of the pair
- `value` (string, required): Value of the pair

#### ProblemDetails

- `type` (string, nullable): A URI reference that identifies the problem type
- `title` (string, nullable): A short, human-readable summary of the problem type
- `status` (integer, nullable, format: int32): The HTTP status code
- `detail` (string, nullable): A human-readable explanation specific to this occurrence of the problem
- `instance` (string, nullable): A URI reference that identifies the specific occurrence of the problem

## Security Schemes

- `Bearer`: 
  - **Type:** apiKey
  - **Description:** Enter the Bearer Authorization string as follows: `Bearer Generated-JWT-Token`
  - **Name:** Authorization
  - **In:** header

## Security

- `Bearer`: []

