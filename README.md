# N5 Challenge

## Introduction

This repository contains the solution for the N5 Challenge, which involves building a full-stack application to manage user permissions. The project includes a backend Web API built with ASP.NET Core, a frontend application developed in ReactJS, and containerized services orchestrated using Docker.

## Challenge Requirements

N5 company requests a Web API for registering user permissions. The solution must comply with the following requirements:

1. **Database Design**:

   - Create a `Permissions` table with the following fields:
     - `Id`: Integer, Primary Key, Auto-Increment.
     - `EmployeeFirstName`: String, Required.
     - `EmployeeLastName`: String, Required.
     - `PermissionDate`: DateTime, Required.
     - `PermissionTypeId`: Integer, Foreign Key, Required.
   - Create a `PermissionTypes` table with the following fields:
     - `Id`: Integer, Primary Key, Auto-Increment.
     - `Description`: String, Required.
   - Establish a relationship between `Permissions` and `PermissionTypes`.

2. **Backend Development**:

   - Build a Web API using ASP.NET Core and persist data on SQL Server.
   - Use Entity Framework for database operations.
   - Implement three services:
     - **Request Permission**: Create a new permission record.
     - **Modify Permission**: Update an existing permission record.
     - **Get Permissions**: Retrieve all permission records.
   - Persist permission records in an Elasticsearch index with the same structure as the `Permissions` table.
   - Use Apache Kafka to create a topic that logs every operation with the following DTO structure:
     - `Id`: Random GUID.
     - `NameOperation`: "modify", "request", or "get".

3. **Architecture**:

   - Follow the Repository Pattern, Unit of Work, and CQRS Pattern (desired).
   - Implement proper service architecture with layered design and dependency injection.

4. **Frontend Development**:

   - Build a ReactJS application using Axios to connect to the backend.
   - Create forms to consume the Web API.
   - Use Material-UI for visual components, leveraging the provided customized theme.

5. **Testing**:

   - Create Unit Tests and Integration Tests for the three backend services.

6. **Containerization**:

   - Prepare the solution to be containerized in Docker images.

7. **Repository**:
   - Upload the solution to a repository (e.g., GitHub, GitLab).

## Stack Used

The following technologies and tools were used to build the solution:

- **Backend**:

  - ASP.NET Core
  - Entity Framework Core
  - SQL Server
  - Elasticsearch
  - Apache Kafka
  - AutoMapper
  - MediatR
  - FluentValidation

- **Frontend**:

  - ReactJS
  - Axios
  - Material-UI

- **Testing**:

  - xUnit
  - Moq
  - In-Memory Database for Integration Tests

- **Containerization**:
  - Docker
  - Docker Compose

## Instructions to Run Locally with Docker

Follow these steps to run the project locally using Docker:

1. **Prerequisites**:

   - Install Docker and Docker Compose on your machine.

2. **Clone the Repository**:

   ```bash
   git clone <repository-url>
   cd n5-challenge
   ```

3. **Build and Start Services**: Run the following command to build and start all services:

   ```bash
   docker-compose up --build
   ```

4. **Access the Services**:

   - **Backend API**:

     - HTTP: [http://localhost:18080](http://localhost:18080)
     - HTTPS: [https://localhost:18081](https://localhost:18081)

   - **Frontend Application**:

     - [http://localhost:13000](http://localhost:13000)

   - **SQL Server**:

     - Port: `11433`

   - **Elasticsearch**:

     - [http://localhost:19200](http://localhost:19200)

   - **Kafka**:

     - Broker: `localhost:29092`

   - **Kafdrop UI** (Kafka visual debugging tool):
     - [http://localhost:19000](http://localhost:19000)

5. **Stop Services**: To stop all services, run:
   ```bash
   docker-compose down
   ```

## License

This project is developed for the N5 Challenge and is intended for educational and assessment purposes only.
