# ContractorAPI Documentation

## Overview

This is the documentation for the ContractorAPI. It provides endpoints for managing customers within the system. Each endpoint description includes details about the request parameters, request body, response, authentication, authorization, and validation requirements.

## Authentication and Authorization

All endpoints require a Bearer token for authentication. Add the token to the Authorization header as follows: `Bearer Generated-JWT-Token`.

## Endpoints

### Get All Customers

**Endpoint:** `GET /api/customer`

**Description:** Retrieves a list of all customers.

**Responses:**
- `200 OK`: Success
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authentication:** Yes

**Authorization:** Yes, requires `CustomerViewer` role

**Validation:** None

---

### Create a New Customer

**Endpoint:** `POST /api/customer`

**Description:** Creates a new customer.

**Request Body:**
- `userId` (string, uuid): Unique identifier for the user
- `name` (string, nullable): First name of the customer
- `lastName` (string, nullable): Last name of the customer
- `city` (string, nullable): City of residence
- `street` (string, nullable): Street address
- `building` (string, nullable): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, nullable): Postal code
- `phoneNumber` (string, nullable): Phone number
- `emailAddress` (string, nullable): Email address

**Responses:**
- `201 Created`: Success
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `409 Conflict`: Conflict in resource creation
- `500 Internal Server Error`: Server error

**Authentication:** Yes

**Authorization:** Yes, requires `CustomerCreator` role

**Validation:** Yes

---

### Update a Customer

**Endpoint:** `PUT /api/customer`

**Description:** Updates an existing customer.

**Request Body:**
- `userId` (string, uuid): Unique identifier for the user
- `name` (string, nullable): First name of the customer
- `lastName` (string, nullable): Last name of the customer
- `city` (string, nullable): City of residence
- `street` (string, nullable): Street address
- `building` (string, nullable): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, nullable): Postal code
- `phoneNumber` (string, nullable): Phone number
- `emailAddress` (string, nullable): Email address

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authentication:** Yes

**Authorization:** Yes, requires `CustomerEditor` role

**Validation:** Yes

---

### Get Customer by ID

**Endpoint:** `GET /api/customer/{customerId}`

**Description:** Retrieves a customer by their unique ID.

**Path Parameters:**
- `customerId` (string, uuid): Unique identifier for the customer

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authentication:** Yes

**Authorization:** Yes, requires `CustomerViewer` role

**Validation:** None

---

### Delete Customer by ID

**Endpoint:** `DELETE /api/customer/{userId}`

**Description:** Deletes a customer by their unique ID.

**Path Parameters:**
- `userId` (string, uuid): Unique identifier for the user

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authentication:** Yes

**Authorization:** Yes, requires `CustomerDeleter` role

**Validation:** None

---

### Get Customer Profile by User ID

**Endpoint:** `GET /api/customer/profile/{userId}`

**Description:** Retrieves the profile of a customer by their user ID.

**Path Parameters:**
- `userId` (string, uuid): Unique identifier for the user

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authentication:** Yes

**Authorization:** Yes, requires `CustomerViewer` role

**Validation:** None

## Components

### Schemas

#### CreateCustomerDto

- `userId` (string, uuid): Unique identifier for the user
- `name` (string, nullable): First name of the customer
- `lastName` (string, nullable): Last name of the customer
- `city` (string, nullable): City of residence
- `street` (string, nullable): Street address
- `building` (string, nullable): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, nullable): Postal code
- `phoneNumber` (string, nullable): Phone number
- `emailAddress` (string, nullable): Email address

#### UpdateCustomerDto

- `userId` (string, uuid): Unique identifier for the user
- `name` (string, nullable): First name of the customer
- `lastName` (string, nullable): Last name of the customer
- `city` (string, nullable): City of residence
- `street` (string, nullable): Street address
- `building` (string, nullable): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, nullable): Postal code
- `phoneNumber` (string, nullable): Phone number
- `emailAddress` (string, nullable): Email address

#### ProblemDetails

- `type` (string, nullable): A URI reference that identifies the problem type
- `title` (string, nullable): A short, human-readable summary of the problem type
- `status` (integer, format: int32, nullable): The HTTP status code
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

