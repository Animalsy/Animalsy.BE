# VendorAPI Documentation

## Overview

This is the documentation for the VendorAPI. It provides endpoints for managing vendors within the system. Each endpoint description includes details about the request parameters, request body, response, authorization, and validation requirements.

## Authorization

All endpoints require specific roles for authorization. Ensure that the JWT token includes the appropriate roles to access each endpoint.

## Endpoints

### Get All Vendors

**Endpoint:** `GET /Api/Vendor`

**Description:** Retrieves a list of all vendors.

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of VendorDto`
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** None

**Validation:** None

---

### Get Vendor by ID

**Endpoint:** `GET /Api/Vendor/{vendorId}`

**Description:** Retrieves a vendor by their unique ID.

**Path Parameters:**
- `vendorId` (uuid, required): Unique identifier for the vendor

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `VendorDto`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** None

**Validation:** Yes

---

### Get Vendor Profiles by User ID

**Endpoint:** `GET /Api/Vendor/Profiles/{userId}`

**Description:** Retrieves vendor profiles by user ID.

**Path Parameters:**
- `userId` (uuid, required): Unique identifier for the user

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `VendorProfileDto`
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor or admin`

**Validation:** Yes

---

### Create a New Vendor

**Endpoint:** `POST /Api/Vendor`

**Description:** Creates a new vendor.

**Request Body:**
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the vendor
- `description` (string, required): Description of the vendor
- `location` (string, required): Location of the vendor
- `contact` (string, required): Contact details of the vendor
- `email` (string, required): Email address of the vendor

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

### Update a Vendor

**Endpoint:** `PUT /Api/Vendor`

**Description:** Updates an existing vendor.

**Request Body:**
- `id` (uuid, required): Unique identifier for the vendor
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the vendor
- `description` (string, required): Description of the vendor
- `location` (string, required): Location of the vendor
- `contact` (string, required): Contact details of the vendor
- `email` (string, required): Email address of the vendor

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor or admin`

**Validation:** Yes

---

### Delete Vendor by ID

**Endpoint:** `DELETE /Api/Vendor/{vendorId}`

**Description:** Deletes a vendor by their unique ID.

**Path Parameters:**
- `vendorId` (uuid, required): Unique identifier for the vendor

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor or admin`

**Validation:** Yes

## Components

### Schemas

#### VendorDto

- `id` (uuid, required): Unique identifier for the vendor
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the vendor
- `description` (string, required): Description of the vendor
- `location` (string, required): Location of the vendor
- `contact` (string, required): Contact details of the vendor
- `email` (string, required): Email address of the vendor

#### CreateVendorDto

- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the vendor
- `description` (string, required): Description of the vendor
- `location` (string, required): Location of the vendor
- `contact` (string, required): Contact details of the vendor
- `email` (string, required): Email address of the vendor

#### UpdateVendorDto

- `id` (uuid, required): Unique identifier for the vendor
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the vendor
- `description` (string, required): Description of the vendor
- `location` (string, required): Location of the vendor
- `contact` (string, required): Contact details of the vendor
- `email` (string, required): Email address of the vendor

#### VendorProfileDto

- `vendor` (VendorDto, required): Vendor details
- `products` (array of ProductDto, nullable): List of vendor's products
- `responseDetails` (array of StringStringKeyValuePair, nullable): Additional response details

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

