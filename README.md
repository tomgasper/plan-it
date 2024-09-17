# PlanIt

PlanIt is a web application built with ASP.NET Core, Entity Framework, .NET Identity, and React. The API is based on Clean Architecture, Domain-Driven Design (DDD), and Command Query Responsibility Segregation (CQRS) patterns.
The frontend utilizes React 18 with Redux RTK for efficient state management, providing a responsive and intuitive user interface with drag-and-drop functionality for enhanced user experience.

### Key technical features
* Clean Architecture & DDD: Ensures clear separation of concerns and a rich domain model
* CQRS with MediatR: Separates read/write operations for scalability
* RESTful API: Serves as the main interface for client interactions, providing clear endpoints for workspace, project, and task management operations
* JWT Authentication: Implements secure user authentication and authorization

## Features
- [x] Create, read, update, and delete workspaces/projects/tasks
- [x] Move tasks from one project to another via drag and drop
- [x] Assign multiple users to a specific task
- [x] Drag tasks and project containers to organize them as you like
- [x] Authentication and authorization using .NET Identity with JWT
- [x] Register, login, and manage user profiles
- [x] Upload user profile pictures
- [x] Browse between different workspaces that encapsulate projects and tasks

## What's missing
- [ ] Persistence of project and task order in their containers
- [ ] More unit tests, especially for the command handlers

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- .NET Identity
- MediatR (for CQRS) with command validation
- SQL Server
- React, TypeScript, Redux Toolkit, RTK Query, DndKit
- xUnit, FluentAssertions, NSubstitute (unit testing)

## Installation
Follow these steps to set up and run the project locally.

### Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) (or any preferred IDE)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation
1. **Clone the repository:**
    ```sh
    git clone https://github.com/tomgasper/PlanIt.git
    cd PlanIt
    ```

2. **Configure the infrastructure:**
    Update the connection string in `appsettings.json` to point to your SQL Server instance.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server;Database=PlanItDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```
    
    Override JWT settings in appsettings.Development.json with your own secret:
    ```json
    "JwtSettings": {
    "Secret": "<YOUR SECRET HERE>",
    "ExpiryMinutes": 60,
    "Issuer": "PlanIt",
    "Audience": "PlanIt"
    }
   ```
   
   Provide your API key for Cloudinary Service in appsettings.json:
    ```json
    "CloudinarySettings": {
    "CloudName": "<YOUR CLOUD NAME>",
    "ApiKey": "<YOUR API KEY>",
    "ApiSecret": "<YOUR API SECRET>"
    }
     ```
 
3. **Apply migrations:**
    Open the terminal in the project directory and run the following command to apply the migrations:
    ```sh
    dotnet ef database update
    ```

4. **Run the application:**
    ```sh
    dotnet run --project src/PlanIt.WebApi
    ```
    Or you can use Visual Studio to run the project by pressing `F5`.