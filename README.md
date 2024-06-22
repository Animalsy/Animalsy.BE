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
   - **Technical Documentation**: [AuthAPI Technical Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.AuthAPI/Documentation/Technical.md)
   - **API Documentation**: [AuthAPI Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.AuthAPI/Documentation/README.md)

2. **ContractorAPI**
   - **Purpose**: Manages contractor-related data.
   - **Technical Documentation**: [ContractorAPI Technical Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.ContractorAPI/Documentation/Technical.md)
   - **API Documentation**: [ContractorAPI Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.ContractorAPI/Documentation/README.md)

3. **CustomerAPI**
   - **Purpose**: Manages customer-related data.
   - **Technical Documentation**: [CustomerAPI Technical Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.CustomerAPI/Documentation/Technical.md)
   - **API Documentation**: [CustomerAPI Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.CustomerAPI/Documentation/README.md)

4. **PetAPI**
   - **Purpose**: Manages pet-related data.
   - **Technical Documentation**: [PetAPI Technical Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.PetAPI/Documentation/Technical.md)
   - **API Documentation**: [PetAPI Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.PetAPI/Documentation/README.md)

5. **ProductAPI**
   - **Purpose**: Manages product-related data.
   - **Technical Documentation**: [ProductAPI Technical Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.ProductAPI/Documentation/Technical.md)
   - **API Documentation**: [ProductAPI Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.ProductAPI/Documentation/README.md)

6. **VendorAPI**
   - **Purpose**: Manages vendor-related data.
   - **Technical Documentation**: [VendorAPI Technical Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.VendorAPI/Documentation/Technical.md)
   - **API Documentation**: [VendorAPI Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.VendorAPI/Documentation/README.md)

7. **VisitAPI**
   - **Purpose**: Manages visit-related data.
   - **Technical Documentation**: [VisitAPI Technical Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.VisitAPI/Documentation/Technical.md)
   - **API Documentation**: [VisitAPI Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Services.VisitAPI/Documentation/README.md)

### Gateway

The **Animalsy.BE.Gateway** acts as the entry point for all the backend services, providing a unified API gateway that routes requests to the appropriate downstream services. It simplifies communication between the frontend and multiple backend services by offering a single endpoint.

- **Purpose**: Serves as a reverse proxy and routes requests to the corresponding backend services.
- **Technical Documentation**: [Gateway Technical Documentation](https://github.com/Animalsy/Animalsy.BE/blob/main/Animalsy.BE.Gateway/Documentation/Technical.md)

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
