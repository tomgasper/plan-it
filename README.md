# Plan It

PlanIt is a web application built with ASP.NET Core, Entity Framework, and .NET Identity.
The app allows for creating new workspaces and projects, managing tasks within a given project(including assigning them to different users) and typical modify, sort, delete operations.
It also features a user profile page where users can adjust settings related to their account.

The app is based on Clean Architecture and DDD(Domain Driven Desing) design patterns. However sometimes more pragmatic decisions have been made. The backend has been written in .Net Core 8.0.7 and the frontend in React 18.

## Features

- [x] User registration and authentication using .NET Identity
- [x] Register, login, and manage user profiles.
- [x] Upload user profile
- [x] Create, read, update, and delete workspaces/projects/tasks
- [x] Workspace main page
- [x] List of categories with dropdown selection
- [x] Profile Settings (Account Delection, Account Visibility and Login History)

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- .NET Identity
- MediatR (for CQRS)
- SQL Server
- React, TypeScript, Redux Toolkit, RTK Query
- xUnit, FluentAssertions, NSubstitute (testing)