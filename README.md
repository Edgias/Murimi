# Murimi Application

Application for farm management.

> ### VERSIONS
> #### The `main` branch is currently running ASP.NET Core 7.0.
> #### Older versions will be tagged.

## Features

- Manage seasons
- Crop production
- Field management
- Client management
- Procurement (requisitions, quotations, orders(sales and purchase), invoices(sales and purchase), goods received)
- Stock management
- Asset management
- Multi-currency
- Income and Expenses
- Todos

## Running the application

The app's home page should look like this:

![eShopOnWeb home page screenshot](https://user-images.githubusercontent.com/782127/88414268-92d83a00-cdaa-11ea-9b4c-db67d95be039.png)

After cloning or downloading the code you must setup your database. 
To use a persistent database, you will need to run its Entity Framework Core migrations before you will be able to run the app.

You can also run the samples in Docker (see below).

### Configuring the app to use SQL Server

1. By default, the project uses a real database. If you want an in memory database, you can add in `appsettings.json`

    ```json
   {
       "UseOnlyInMemoryDatabase": true
   }

    ```

1. Ensure your connection strings in `appsettings.json` point to a local SQL Server instance.
1. Ensure the tool EF was already installed. You can find some help [here](https://docs.microsoft.com/ef/core/miscellaneous/cli/dotnet)

    ```
    dotnet tool update --global dotnet-ef
    ```

1. Open a command prompt in the WebApp folder and execute the following commands:

    ```
    dotnet restore
    dotnet tool restore
    dotnet ef database update -c murimicontext -p ../Infrastructure/Infrastructure.csproj -s WebApp.csproj    
    ```

    These commands will create a database for the app's data as well as the app's user credentials and identity data.

1. Run the application.

    The first time you run the application, it will seed the database with data such that you should see reference data in the app, and you should be able to log in using the demouser@murimi.com account.

    Note: If you need to create migrations, you can use these commands:

    ```
    -- create migration (from Web folder CLI)
    dotnet ef migrations add InitialModel --context murimicontext -p ../Infrastructure/Infrastructure.csproj -s WebApp.csproj -o Data/Migrations
    ```

## Running the sample using Docker

You can run the Web sample by running these commands from the root folder (where the .sln file is located):

```
docker-compose build
docker-compose up
```

You should be able to make requests to localhost:7074 for the Web project, and localhost:7292 for the API project once these commands complete. If you have any problems, especially with login, try from a new guest or incognito browser instance.

You can also run the applications by using the instructions located in their `Dockerfile` file in the root of each project. Again, run these commands from the root of the solution (where the .sln file is located).
