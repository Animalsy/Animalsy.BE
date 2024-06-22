# AuthAPI Documentation

## Overview

This is the documentation for the AuthAPI. It provides endpoints for managing user authentication and roles within the system. Each endpoint description includes details about the request parameters, request body, response, authorization, and validation requirements.

## Authorization

All endpoints require specific roles for authorization. Ensure that the JWT token includes the appropriate roles to access each endpoint.

## Endpoints

### Register a New User

**Endpoint:** `POST /Api/Auth/Register`

**Description:** Registers a new user.

**Request Body:**
- `emailAddress` (string, required): Email address of the user
- `password` (string, required): Password for the user
- `name` (string, required): First name of the user
- `lastName` (string, required): Last name of the user
- `city` (string, required): City of residence
- `street` (string, required): Street address
- `building` (string, required): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, required): Postal code
- `phoneNumber` (string, required): Phone number

**Responses:**
- `201 Created`: Success
  - Content-Type: application/json
  - Schema: `ResponseDto`
- `400 Bad Request`: Invalid request data
- `409 Conflict`: Conflict in resource creation
- `500 Internal Server Error`: Server error

**Authorization:** None

**Validation:** Yes

---

### Login User

**Endpoint:** `POST /Api/Auth/Login`

**Description:** Authenticates a user and returns a JWT token.

**Request Body:**
- `email` (string, required): Email address of the user
- `password` (string, required): Password for the user

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `ResponseDto`
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Invalid credentials
- `500 Internal Server Error`: Server error

**Authorization:** None

**Validation:** Yes

---

### Assign Role to User

**Endpoint:** `POST /Api/Auth/AssignRole`

**Description:** Assigns a role to a user.

**Request Body:**
- `email` (string, required): Email address of the user
- `roleName` (string, required): Role to be assigned

**Responses:**
- `200 OK`: Success
  - Content-Type: application/json
  - Schema: `ResponseDto`
- `400 Bad Request`: Invalid request data
- `500 Internal Server Error`: Server error

**Authorization:** Yes, requires role `admin`

**Validation:** Yes

## Components

### Schemas

#### RegisterUserDto

- `emailAddress` (string, required): Email address of the user
- `password` (string, required): Password for the user
- `name` (string, required): First name of the user
- `lastName` (string, required): Last name of the user
- `city` (string, required): City of residence
- `street` (string, required): Street address
- `building` (string, required): Building number
- `flat` (string, nullable): Flat number
- `postalCode` (string, required): Postal code
- `phoneNumber` (string, required): Phone number

#### LoginUserDto

- `email` (string, required): Email address of the user
- `password` (string, required): Password for the user

#### AssignRoleDto

- `email` (string, required): Email address of the user
- `roleName` (string, required): Role to be assigned

#### ResponseDto

- `isSuccess` (boolean, required): Indicates if the operation was successful
- `result` (nullable): Result of the operation
- `message` (string, nullable): Message describing the result

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

