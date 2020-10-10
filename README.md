# ASP.NET Core Hero - Boilerplate Template

![.NET Core](https://github.com/iammukeshm/AspNetCoreHero-Boilerplate/workflows/.NET%20Core/badge.svg?branch=master)
![GitHub stars](https://img.shields.io/github/stars/iammukeshm/AspNetCoreHero-Boilerplate)
[![Twitter Follow](https://img.shields.io/twitter/follow/codewithmukesh?style=social&label=follow)](https://twitter.com/codewithmukesh)


![ASP.NET Core Hero - Boilerplate Template](https://www.codewithmukesh.com/wp-content/uploads/2020/10/Featured-Image.png)
## Beta V1.0 is Out!
Feel Free to Clone the Project and try it out.

## Releases
v1.1 - Beta Release - Clone the Repository to get the latest codebase

v1.0 - Beta Release - [Download Beta-V1.0 Release](https://github.com/iammukeshm/AspNetCoreHero-Boilerplate/releases/tag/v1.0) 

### Changelog
#### v1.1
* added fluent validation
* added product images
* UI improvement
* activity log integration
* added toast notification
* Superadmins can now create users directly from Dashboard! Client / Server side Validations enabled


## Problem It will Solve

The Basic Idea is to save hours of development time. The users should be able to start off from the point all the technical aspects are already covered. The only thhing we need to worry is about implementing the Business Logic. I am planning this Project to have 2 realms - A Fluid UI Based ASP.NET Core 3.1 Razor Project and a WebAPI Project that provided data to public via valid JWT.

## Give a Star ⭐️
If you found this Implementation helpful or used it in your Projects, do give it a star. Thanks!
Or, If you are feeling really generous, [Support the Project with a small contribution!](https://www.buymeacoffee.com/codewithmukesh)

### Tech Stack
- ASP.NET Core 3.1 Razor Pages / Controllers / Web API 
- Entity Framework Core
- MSSQL (Supports other RDBMS too)
- Javascript / JQuery
- Bootstrap 4 / AdminLTE

# Getting Started with ASP.NET Core Hero - Boilerplate Template
0. Make sure you have EF CLI Tools installed. Open up Powershell and run the following command
`dotnet tool install --global dotnet-ef`
1. Clone this Repository and Extract it to a Folder.
3. Change the Connection Strings for the Application and Identity in the PublicAPI/appsettings.json and Web/appsettings.json
2. Run the following commands on Powershell in the Web Project's Directory.
- `dotnet restore`
- `dotnet ef database update -c ApplicationContext`
- `dotnet ef database update -c IdentityContext`
- `dotnet run` (OR) Run the Solution using Visual Studio 2019

PS - If the above code doesnt work for some reason, try using -c instead of -context or vice versa.

Check out my [blog](https://www.codewithmukesh.com) or say [Hi on Twitter!](https://twitter.com/codewithmukesh)
   
### Default Roles & Credentials
As soon you build and run your application, default users and roles get added to the database.

Default Roles are as follows.
- SuperAdmin
- Admin
- Moderator
- Basic

Here are the credentials for the default users.
- Email - superadmin@gmail.com  / Password - 123Pa$$word!
- Email - basic@gmail.com  / Password - 123Pa$$word!

### Project Structure
- ASP.NET Core 3.1 Razor Project with Identity
- ASP.NET Core 3.1 WebAPI Public API Project with JWT Auth
- Application Layer
- Domain Layer
- Infrastructure.Shared Layer
- Infrastructure.Persistence Layer

### Architecture
Check out a [Diagramatic Representation of the Architecture here](https://www.codewithmukesh.com/wp-content/uploads/2020/10/ASP.NET-Core-Hero-Boilerplate-template.png)

### Feature Overview
- Onion / Hexagonal Architecture
- Clean Code Practices
- CQRS with MediatR
- Cached Repository with In-Memory Caching and Redis Caching
- Generic Repository with Unit Of Work Pattern
- Complete User Management Module*
- Role Management* (Add / Edit / Delete Roles)
- Add Roles to Users
- Automapper
- Validation
- Auditable Entity (Track Changes on any Entity based on User and DateTime)
- Policy Based Permission Management*
- Mail Service
- Project Wise
- CRUD on Product Entity Implemented for Reference

#### ASP.NET Core Razor Page
- DARK Mode
- MultiLingual
- Fluid UI - Blazing Fast
- AdminLTE
- Responsive & Clean Design
- RTL Support for Arabic Scripts
- Cookie Authentication
- Default User / Roles / Claims Seeding
- Serilog Logging
- Super Quick CRUD with Razor Page / Partial Views and JQuery
- jQuery Datatable
- Bootstrap Modal

### ASP.NET Core WebAPI
- JWT Authentication
- Doesnot depend on the UI Project - Should run Individually
- CQRS Approach to communicate with Application Layer
- Response Wrappers
- Swagger UI with Bearer Auth
- API Versioning


### Hangfire Server
- Not yet used*

and much more. Feel Free to Clone the Project and try it out. Raise any issues / requests that you may find.

## Support
Support This Project to keep it active.

<a href="https://www.buymeacoffee.com/codewithmukesh" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" width="200"  ></a>

## Questions? Bugs? Suggestions for Improvement?
Having any issues or troubles getting started? [Get in touch with me](https://www.codewithmukesh.com/contact) or [Raise a Bug or Feature Request](https://github.com/iammukeshm/AspNetCoreHero-Boilerplate/issues/new/choose). Always happy to help.

## About the Author
### Mukesh Murugan
- Blogs at [codewithmukesh.com](https://www.codewithmukesh.com)
- Facebook - [codewithmukesh](https://www.facebook.com/codewithmukesh)
- Twitter - [Mukesh Murugan](https://www.twitter.com/iammukeshm)
- Twitter - [codewithmukesh](https://www.twitter.com/codewithmukesh)
- Linkedin - [Mukesh Murugan](https://www.linkedin.com/in/iammukeshm/)

## Licensing
iammukeshm/AspNetCoreHero-Boilerplate Project is licensed with the [MIT License](https://github.com/iammukeshm/AspNetCoreHero-Boilerplate/blob/master/LICENSE).
