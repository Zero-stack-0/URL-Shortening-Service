# URL Shortening API

This is a simple URL shortening API built using C# with .NET and SQL Server. The API allows users to shorten URLs and retrieve the original URL by using a shortcode. The project uses Entity Framework Core for data access and SQL Server for database storage.

## Features

- Shorten a URL.
- Retrieve the original URL using the shortcode.
- Track the access count of each shortened URL.

## Technology Stack

- **Backend**: C# with .NET 6/7 (ASP.NET Core)
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **API**: RESTful API

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- [**.NET SDK (6 or later)**](https://dotnet.microsoft.com/download)
- [**SQL Server**](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [**Visual Studio**](https://visualstudio.microsoft.com/) or [**Visual Studio Code**](https://code.visualstudio.com/)
- [**Git**](https://git-scm.com/)

## Setup Instructions

### 1. Clone the Repository

Clone this repository to your local machine using Git:

```bash
git clone https://github.com/your-username/url-shortening-api.git
cd url-shortening-api
### 2. Configure SQL Server Connection String

The project uses SQL Server to store data. You need to update the connection string in the `appsettings.json` file.

- Open the `appsettings.json` file in the root of the project.
- In the `"ConnectionStrings"` section, update the `DefaultConnection` string to match your local SQL Server setup:

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "AddConnectionStringHere"
  }
}

