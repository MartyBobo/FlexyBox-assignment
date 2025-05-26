# FlexyBox - Restaurant Management System

A modern Blazor Server application for managing restaurants with features like search, favorites, user profiles, and real-time opening hours tracking.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Setup & Installation](#setup--installation)
- [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Key Components](#key-components)
- [Database](#database)
- [Usage](#usage)
- [Development Notes](#development-notes)

## Overview

FlexyBox is a restaurant management system built with .NET 9 and Blazor Server. It provides a clean, modern interface for browsing restaurants, managing favorites, and viewing detailed restaurant information including dynamic opening hours.

## Features

- **Restaurant Browsing**: View all restaurants with real-time open/closed status
- **Search Functionality**: Search restaurants by name or address with debounced input
- **Favorites System**: Toggle and manage favorite restaurants per user
- **User Profiles**: Complete user profile management with validation
- **Dynamic Opening Hours**: Real-time calculation of restaurant status based on current time
- **Responsive Design**: Custom CSS with modern UI/UX (no external frameworks)
- **Multiple Operating Modes**: Support for different restaurant modes (Restaurant, Takeaway)

## Architecture

The project follows Clean Architecture principles with clear separation of concerns:

```
FlexyBox/
├── Domain/           # Entities and domain logic
├── Application/      # Business logic, DTOs, queries/commands
├── Infrastructure/   # Data access, Entity Framework
├── Components/       # Blazor components and pages
├── Services/         # Application services
└── Controllers/      # API controllers
```

### Technology Stack

- **.NET 9**: Latest .NET framework
- **Blazor Server**: Interactive server-side rendering
- **Entity Framework Core**: ORM for database operations
- **MediatR**: CQRS pattern implementation
- **SQL Server**: Database (configurable)
- **Custom CSS**: No external CSS frameworks (Bootstrap/Tailwind removed)

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB is sufficient)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Setup & Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd FlexyBox
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update database connection string** (if needed)
   
   Edit `appsettings.json` or `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FlexyBoxDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Build the solution**
   ```bash
   dotnet build
   ```

## Running the Application

### Development Mode

```bash
dotnet run
```

The application will start and be available at:
- **HTTPS**: `https://localhost:7070`
- **HTTP**: `http://localhost:5000`

### Production Mode

```bash
dotnet run --configuration Release
```

## Project Structure

### Domain Layer (`Domain/`)
- **Entities/**: Core business entities
  - `Restaurant.cs`: Restaurant entity
  - `OpeningHours.cs`: Opening hours with time spans
  - `User.cs`: User profile entity
  - `Favorite.cs`: User favorites relationship
  - `GalleryImage.cs`: Restaurant images

### Application Layer (`Application/`)
- **DTOs/**: Data transfer objects
- **Queries/**: CQRS query handlers
- **Commands/**: CQRS command handlers
- **Handlers/**: MediatR request handlers
- **Interfaces/**: Application interfaces
- **Helpers/**: Utility classes (e.g., dynamic IsOpen calculation)

### Infrastructure Layer (`Infrastructure/`)
- **Data/**: Entity Framework configuration
  - `ApplicationDbContext.cs`: Main DbContext
  - `ApplicationDbContextSeed.cs`: Sample data seeding
  - **Migrations/**: EF Core migrations

### Presentation Layer (`Components/`)
- **Pages/**: Blazor pages
  - `Home.razor`: Restaurant listing
  - `RestaurantDetail.razor`: Individual restaurant view
  - `Search.razor`: Restaurant search
  - `Favorites.razor`: User favorites
  - `Profile.razor`: User profile management
- **Layout/**: Application layout components
- **wwwroot/**: Static files, CSS, icons

### Services (`Services/`)
- `IRestaurantService.cs` / `RestaurantService.cs`: Restaurant operations
- `IUserService.cs` / `UserService.cs`: User management
- `ICurrentUserService.cs` / `CurrentUserService.cs`: Current user context

## Key Components

### Dynamic Opening Hours

The system calculates restaurant status in real-time using:
- Current day of the week
- Current time
- Restaurant's opening hours for different modes
- Support for cross-midnight operations

**Implementation**: `Application/Helpers/RestaurantHelper.cs`

### Search System

- Debounced search input (300ms delay)
- Search by restaurant name or address
- Real-time results with loading states

### Favorites System

- User-specific favorites with database persistence
- Toggle functionality with visual feedback
- Toast notifications for user actions

### Custom Icon System

All icons are custom SVG files located in `/wwwroot/images/icons/`:
- No external icon libraries (Bootstrap Icons removed)
- Optimized SVG icons with hover effects
- Consistent design language

## Database

### Seeded Data

The application includes sample data:
- **2 Restaurants**: Main restaurant and Copenhagen Bistro
- **Opening Hours**: Complete weekly schedules for both Restaurant and Takeaway modes
- **Gallery Images**: Sample restaurant photos
- **Default User**: Test user for development

### Migration Commands

```bash
# Create new migration
dotnet ef migrations add <MigrationName>

# Apply migrations
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## Usage

### Browsing Restaurants

1. Visit the home page to see all restaurants
2. Open/Closed status updates in real-time based on current time
3. Click on any restaurant to view details

### Searching

1. Navigate to `/search`
2. Type restaurant name or address
3. Results appear automatically with debounced input

### Managing Favorites

1. Click the heart icon on any restaurant
2. View all favorites at `/favorites`
3. Remove favorites by clicking the heart again

### User Profile

1. Navigate to `/profile`
2. Update personal information
3. Form validation ensures data integrity

## Development Notes

### CSS Architecture

- **No External Frameworks**: Bootstrap and Tailwind completely removed
- **Shared Styles**: Centralized in `/wwwroot/css/shared.css`
- **CSS Custom Properties**: Consistent design tokens
- **Component-Specific CSS**: Each component has its own CSS file

### CQRS Pattern

- **Queries**: Read operations using MediatR
- **Commands**: Write operations with validation
- **Handlers**: Separate handlers for each operation
- **DTOs**: Clean data transfer between layers

### Testing the Dynamic IsOpen Feature

To test the dynamic opening hours:
1. Check current time vs seeded opening hours
2. Restaurant mode: Monday-Friday 7:00-22:00, Saturday 7:00-23:00, Sunday 8:00-21:00
3. Takeaway mode: Monday-Sunday 8:00-21:00
4. The status should update automatically based on current time

### Performance Considerations

- **Blazor Server**: Real-time updates without client-side complexity
- **Entity Framework**: Optimized queries with Include statements
- **Debounced Search**: Reduces API calls during typing
- **CSS Variables**: Efficient styling with minimal overhead

---

**Happy coding! 🚀**

For questions or issues, please refer to the codebase documentation or create an issue in the repository.
