# Animalsy.BE - Backend Application

## Overview

Animalsy.BE is a comprehensive backend solution for managing and browsing pet services, designed for both pet owners and service providers. The application is developed using .NET 8 and deployed using Docker Compose, ensuring a scalable and efficient architecture.

## Purpose

Animalsy.BE serves as the backbone of the Animalsy application, providing essential services for user authentication, contractor management, customer management, pet management, product management, vendor management, and visit management. The application is tailored to facilitate seamless interaction between pet owners and service providers, offering a platform to manage appointments, services, and user information effectively.

## Technical Architecture

The backend application consists of several microservices, each responsible for a specific domain within the system. These microservices are containerized using Docker and orchestrated with Docker Compose to ensure easy system-agnostic deployment and scalability. Below is a summary of each service and its purpose, along with links to their detailed documentation.

### Services

1. **AuthAPI**
   - **Purpose**: Manages user authentication and roles.
   - **Technical Documentation**: [AuthAPI Technical Documentation](./Animalsy.BE.Services.AuthAPI/Documentation/TechnicalDocumentation.md)
   - **API Documentation**: [AuthAPI Documentation](./Animalsy.BE.Services.AuthAPI/Documentation/ApiDocumentation.md)

2. **ContractorAPI**
   - **Purpose**: Manages contractor-related data.
   - **Technical Documentation**: [ContractorAPI Technical Documentation](./Animalsy.BE.Services.ContractorAPI/Documentation/TechnicalDocumentation.md)
   - **API Documentation**: [ContractorAPI Documentation](./Animalsy.BE.Services.ContractorAPI/Documentation/ApiDocumentation.md)

3. **CustomerAPI**
   - **Purpose**: Manages customer-related data.
   - **Technical Documentation**: [CustomerAPI Technical Documentation](./Animalsy.BE.Services.CustomerAPI/Documentation/TechnicalDocumentation.md)
   - **API Documentation**: [CustomerAPI Documentation](./Animalsy.BE.Services.CustomerAPI/Documentation/ApiDocumentation.md)

4. **PetAPI**
   - **Purpose**: Manages pet-related data.
   - **Technical Documentation**: [PetAPI Technical Documentation](./Animalsy.BE.Services.PetAPI/Documentation/TechnicalDocumentation.md)
   - **API Documentation**: [PetAPI Documentation](./Animalsy.BE.Services.PetAPI/Documentation/ApiDocumentation.md)

5. **ProductAPI**
   - **Purpose**: Manages product-related data.
   - **Technical Documentation**: [ProductAPI Technical Documentation](./Animalsy.BE.Services.ProductAPI/Documentation/TechnicalDocumentation.md)
   - **API Documentation**: [ProductAPI Documentation](./Animalsy.BE.Services.ProductAPI/Documentation/ApiDocumentation.md)

6. **VendorAPI**
   - **Purpose**: Manages vendor-related data.
   - **Technical Documentation**: [VendorAPI Technical Documentation](./Animalsy.BE.Services.VendorAPI/Documentation/TechnicalDocumentation.md)
   - **API Documentation**: [VendorAPI Documentation](./Animalsy.BE.Services.VendorAPI/Documentation/ApiDocumentation.md)

7. **VisitAPI**
   - **Purpose**: Manages visit-related data.
   - **Technical Documentation**: [VisitAPI Technical Documentation](./Animalsy.BE.Services.VisitAPI/Documentation/TechnicalDocumentation.md)
   - **API Documentation**: [VisitAPI Documentation](./Animalsy.BE.Services.VisitAPI/Documentation/ApiDocumentation.md)

### Gateway

The **Animalsy.BE.Gateway** acts as the entry point for all the backend services, providing a unified API gateway that routes requests to the appropriate downstream services. It simplifies communication between the frontend and multiple backend services by offering a single endpoint.

- **Purpose**: Serves as a reverse proxy and routes requests to the corresponding backend services.
- **Technical Documentation**: [Gateway Technical Documentation](./Animalsy.BE.Gateway/Documentation/Technical.md)

## Deployment

The application is deployed using Docker Compose, which allows for easy management and scaling of the microservices. Each service is defined in its Dockerfile, specifying the steps to set up the environment, copy necessary files, restore dependencies, build the project, and expose the required ports.

## Getting Started

To get started with Animalsy.BE, ensure you have Docker and Docker Compose installed. Clone the repository and navigate to the root directory. From there, you can build and start the services using Docker Compose.

This will build and start all the microservices defined in the `docker-compose.yml` file.

## Contributing

We welcome contributions to improve Animalsy.BE. Please fork the repository, create a feature branch, and submit a pull request with your changes.

## Contact

For any questions or support, please open an issue on GitHub or contact the maintainer.

---

Animalsy.BE provides a robust backend framework for managing pet services, designed to facilitate smooth interactions between pet owners and service providers. Each microservice is documented comprehensively, providing all necessary technical details to understand and extend the application. We hope you find this project useful and look forward to your contributions.
