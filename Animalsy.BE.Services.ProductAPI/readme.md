# ProductAPI Documentation

## Overview

This is the documentation for the ProductAPI. It provides endpoints for managing products within the system. Each endpoint description includes details about the request parameters, request body, response, authorization, and validation requirements.

## Authorization

All endpoints require specific roles for authorization. Ensure that the JWT token includes the appropriate roles to access each endpoint.

## Endpoints

### Get All Products

**Endpoint:** `GET /Api/Product`

**Description:** Retrieves a list of all products.

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of ProductDto`
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** None

---

### Get Products by Vendor

**Endpoint:** `GET /Api/Product/Vendor/{vendorId}`

**Description:** Retrieves products by vendor ID.

**Path Parameters:**
- `vendorId` (uuid, required): Unique identifier for the vendor

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of ProductDto`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Get Vendor IDs by Product Category

**Endpoint:** `GET /Api/Product/Vendor/{categoryAndSubCategory}`

**Description:** Retrieves vendor IDs by product category.

**Path Parameters:**
- `categoryAndSubCategory` (string, required): Category and subcategory of the product

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of uuid`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Get Products by Vendor and Category

**Endpoint:** `GET /Api/Product/Vendor/{vendorId}/{categoryAndSubCategory}`

**Description:** Retrieves products by vendor ID and category.

**Path Parameters:**
- `vendorId` (uuid, required): Unique identifier for the vendor
- `categoryAndSubCategory` (string, required): Category and subcategory of the product

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `array of ProductDto`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Get Product by ID

**Endpoint:** `GET /Api/Product/{productId}`

**Description:** Retrieves a product by its unique ID.

**Path Parameters:**
- `productId` (uuid, required): Unique identifier for the product

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `ProductDto`
- `400 Bad Request`: Invalid request data
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes

**Validation:** Yes

---

### Create a New Product

**Endpoint:** `POST /Api/Product`

**Description:** Creates a new product.

**Request Body:**
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the product
- `description` (string, required): Description of the product
- `categoryAndSubCategory` (string, required): Category and subcategory of the product
- `minPrice` (number, required, format: double): Minimum price of the product
- `maxPrice` (number, required, format: double): Maximum price of the product
- `promoPrice` (number, required, format: double): Promotional price of the product
- `duration` (string, required, format: date-span): Duration of the product

**Responses:**
- `201 Created`: Success
  - Content-Type: application/json
  - Schema: `uuid`
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor` or `admin`

**Validation:** Yes

---

### Update a Product

**Endpoint:** `PUT /Api/Product`

**Description:** Updates an existing product.

**Request Body:**
- `id` (uuid, required): Unique identifier for the product
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the product
- `description` (string, required): Description of the product
- `categoryAndSubCategory` (string, required): Category and subcategory of the product
- `minPrice` (number, required, format: double): Minimum price of the product
- `maxPrice` (number, required, format: double): Maximum price of the product
- `promoPrice` (number, required, format: double): Promotional price of the product
- `duration` (string, required, format: date-span): Duration of the product

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor` or `admin`

**Validation:** Yes

---

### Delete Product by ID

**Endpoint:** `DELETE /Api/Product/{productId}`

**Description:** Deletes a product by its unique ID.

**Path Parameters:**
- `productId` (uuid, required): Unique identifier for the product

**Responses:**
- `200 OK`: Success
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Missing or invalid authentication token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `vendor` or `admin`

**Validation:** Yes

## Components

### Schemas

#### ProductDto

- `id` (uuid, required): Unique identifier for the product
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the product
- `description` (string, required): Description of the product
- `categoryAndSubCategory` (string, required): Category and subcategory of the product
- `minPrice` (number, required, format: double): Minimum price of the product
- `maxPrice` (number, required, format: double): Maximum price of the product
- `promoPrice` (number, required, format: double): Promotional price of the product
- `duration` (string, required, format: date-span): Duration of the product

#### CreateProductDto

- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the product
- `description` (string, required): Description of the product
- `categoryAndSubCategory` (string, required): Category and subcategory of the product
- `minPrice` (number, required, format: double): Minimum price of the product
- `maxPrice` (number, required, format: double): Maximum price of the product
- `promoPrice` (number, required, format: double): Promotional price of the product
- `duration` (string, required, format: date-span): Duration of the product

#### UpdateProductDto

- `id` (uuid, required): Unique identifier for the product
- `userId` (uuid, required): Unique identifier for the user
- `name` (string, required): Name of the product
- `description` (string, required): Description of the product
- `categoryAndSubCategory` (string, required): Category and subcategory of the product
- `minPrice` (number, required, format: double): Minimum price of the product
- `maxPrice` (number, required, format: double): Maximum price of the product
- `promoPrice` (number, required, format: double): Promotional price of the product
- `duration` (string, required, format: date-span): Duration of the product

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

