# ContractorAPI Documentation

## Overview

This is the documentation for the ContractorAPI. It provides endpoints for managing contractors within the system. Each endpoint description includes details about the request parameters, request body, response, authorization, and validation requirements.

## Authorization

All endpoints require specific roles for authorization. Ensure that the JWT token includes the appropriate roles to access each endpoint.

## Endpoints

### Get Contractor by ID

**Endpoint:** `GET /Api/Contractor/{contractorId}`

**Description:** Retrieves a contractor by their unique ID.

**Path Parameters:**
- `contractorId` (uuid, required): Unique identifier for the contractor

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `ContractorDto`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Get Contractors by Vendor and Specialization

**Endpoint:** `GET /Api/Contractor/Vendor/{vendorId}/{specialization}`

**Description:** Retrieves contractors by vendor ID and specialization.

**Path Parameters:**
- `vendorId` (uuid, required): Unique identifier for the vendor
- `specialization` (string, required): Specialization of the contractors

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of ContractorDto`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Get Contractors by Vendor

**Endpoint:** `GET /Api/Contractor/Vendor/{vendorId}`

**Description:** Retrieves contractors by vendor ID.

**Path Parameters:**
- `vendorId` (uuid, required): Unique identifier for the vendor

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of ContractorDto`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Create a New Contractor

**Endpoint:** `POST /Api/Contractor`

**Description:** Creates a new contractor.

**Request Body:**
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the contractor
- `lastName` (string, required): Last name of the contractor
- `specialization` (string, required): Specialization of the contractor
- `imageUrl` (string, nullable): URL of the contractor's image

**Responses:**
- `201 Created`: Success
  - Content-Type: application/json
  - Schema: `uuid`
- `400 Bad Request`: Invalid request data
- `403 Forbidden`: Insufficient permissions
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor` or `admin`

**Validation:** Yes

---

### Update a Contractor

**Endpoint:** `PUT /Api/Contractor`

**Description:** Updates an existing contractor.

**Request Body:**
- `id` (uuid, required): Unique identifier for the contractor
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the contractor
- `lastName` (string, required): Last name of the contractor
- `specialization` (string, required): Specialization of the contractor
- `imageUrl` (string, nullable): URL of the contractor's image

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor` or `admin`

**Validation:** Yes

---

### Delete Contractor by ID

**Endpoint:** `DELETE /Api/Contractor/{contractorId}`

**Description:** Deletes a contractor by their unique ID.

**Path Parameters:**
- `contractorId` (uuid, required): Unique identifier for the contractor

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor` or `admin`

**Validation:** Yes

## Components

### Schemas

#### ContractorDto

- `id` (uuid, required): Unique identifier for the contractor
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the contractor
- `lastName` (string, required): Last name of the contractor
- `specialization` (string, required): Specialization of the contractor
- `imageUrl` (string, nullable): URL of the contractor's image

#### CreateContractorDto

- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the contractor
- `lastName` (string, required): Last name of the contractor
- `specialization` (string, required): Specialization of the contractor
- `imageUrl` (string, nullable): URL of the contractor's image

#### UpdateContractorDto

- `id` (uuid, required): Unique identifier for the contractor
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the contractor
- `lastName` (string, required): Last name of the contractor
- `specialization` (string, required): Specialization of the contractor
- `imageUrl` (string, nullable): URL of the contractor's image

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

