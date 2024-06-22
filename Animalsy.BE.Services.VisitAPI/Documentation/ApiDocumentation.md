# VisitAPI Documentation

## Overview

This is the documentation for the VisitAPI. It provides endpoints for managing visits within the system. Each endpoint description includes details about the request parameters, request body, response, authorization, and validation requirements.

## Authorization

All endpoints require specific roles for authorization. Ensure that the JWT token includes the appropriate roles to access each endpoint.

## Endpoints

### Get Visit by ID

**Endpoint:** `GET /Api/Visit/{visitId}`

**Description:** Retrieves a visit by its unique ID.

**Path Parameters:**
- `visitId` (uuid, required): Unique identifier for the visit

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `VisitDto`
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Get Visits by Vendor

**Endpoint:** `GET /Api/Visit/Vendor/{vendorId}`

**Description:** Retrieves visits by vendor ID.

**Path Parameters:**
- `vendorId` (uuid, required): Unique identifier for the vendor

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of VisitDto`
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Get Visits by Customer

**Endpoint:** `GET /Api/Visit/Customer/{customerId}`

**Description:** Retrieves visits by customer ID.

**Path Parameters:**
- `customerId` (uuid, required): Unique identifier for the customer

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of VisitDto`
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Create a New Visit

**Endpoint:** `POST /Api/Visit`

**Description:** Creates a new visit.

**Request Body:**
- `id` (uuid, required): Unique identifier for the visit
- `product` (ProductDto, required): Product details
- `vendor` (VendorDto, required): Vendor details
- `contractor` (ContractorDto, required): Contractor details
- `pet` (PetDto, required): Pet details
- `customer` (CustomerDto, required): Customer details
- `date` (string, required, format: date-time): Date of the visit
- `comment` (string, nullable): Comment about the visit
- `status` (string, nullable): Status of the visit

**Responses:**
- `201 Created`: Success
  - Content-Type: application/json
  - Schema: `uuid`
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Update a Visit

**Endpoint:** `PUT /Api/Visit`

**Description:** Updates an existing visit.

**Request Body:**
- `id` (uuid, required): Unique identifier for the visit
- `product` (ProductDto, required): Product details
- `vendor` (VendorDto, required): Vendor details
- `contractor` (ContractorDto, required): Contractor details
- `pet` (PetDto, required): Pet details
- `customer` (CustomerDto, required): Customer details
- `date` (string, required, format: date-time): Date of the visit
- `comment` (string, nullable): Comment about the visit
- `status` (string, nullable): Status of the visit

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Delete Visit by ID

**Endpoint:** `DELETE /Api/Visit/{visitId}`

**Description:** Deletes a visit by its unique ID.

**Path Parameters:**
- `visitId` (uuid, required): Unique identifier for the visit

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `admin`

**Validation:** Yes

## Components

### Schemas

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

#### CreateVisitDto

- `id` (uuid, required): Unique identifier for the visit
- `product` (ProductDto, required): Product details
- `vendor` (VendorDto, required): Vendor details
- `contractor` (ContractorDto, required): Contractor details
- `pet` (PetDto, required): Pet details
- `customer` (CustomerDto, required): Customer details
- `date` (string, required, format: date-time): Date of the visit
- `comment` (string, nullable): Comment about the visit
- `status` (string, nullable): Status of the visit

#### UpdateVisitDto

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

#### VendorDto

- `id` (uuid, required): Unique identifier for the vendor
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the vendor
- `description` (string, required): Description of the vendor
- `location` (string, required): Location of the vendor
- `contact` (string, required): Contact details of the vendor
- `email` (string, required): Email address of the vendor

#### ContractorDto

- `id` (uuid, required): Unique identifier for the contractor
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the contractor
- `lastName` (string, required): Last name of the contractor
- `specialization` (string, required): Specialization of the contractor
- `imageUrl` (string, nullable): URL of the contractor's image

#### PetDto

- `id` (uuid, required): Unique identifier for the pet
- `userId` (uuid, required): Unique identifier for the user
- `species` (string, required): Species of the pet
- `race` (string, required): Race of the pet
- `name` (string, required): Name of the pet
- `dateOfBirth` (string, required, format: date-time): Date of birth of the pet
- `imageUrl` (string, nullable): URL of the pet's image

#### CustomerDto

- `id` (uuid, required): Unique identifier for the customer
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): First name of the customer
- `lastName` (string, required): Last name of the customer
- `email` (string, required): Email address of the customer
- `phoneNumber` (string, required): Phone number of the customer

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

