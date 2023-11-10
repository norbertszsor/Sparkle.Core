# Sparkle Core

Sparkle Core is a component of the Sparkle project, developed using .NET Core 7 and following the clean architecture approach. This core module is responsible for managing data flow, utilizing a suite of .NET technologies. It handles data storage and presentation to other system layers, enabling fast access to extensive telemetry records necessary for building predictive models. Sparkle Core provides key interfaces that facilitate seamless communication between [user interface](https://github.com/norbertszsor/Sparkle.Web) and the [regression module](https://github.com/norbertszsor/Sparkle.Regressor). It also supports two forecasting methods: predictive and comparative.

**Key Features:**

- **Data Management:** Sparkle Core efficiently manages data flow, making extensive telemetry records readily accessible for predictive modeling.

- **Modular Clean Architecture:** The application is designed using clean architecture principles, ensuring modularity, flexibility, and maintainability.

- **Database Agnostic:** It uses [Linq2DB](https://linq2db.github.io/) to achieve high-speed queries and data seeding, making it adaptable to different database engines.

- **MediatR:** Sparkle Core employs the MediatR library for efficient communication between application components, promoting a clean and loosely-coupled architecture.
  
- **Naive Auto Migrator:** Sparkle Core includes an naive auto migrator that simplifies database schema updates and migrations, ensuring smooth transitions when evolving the database structure.

- **Seeder:** The seeder functionality allows for easy population of the database with sample data, simplifying the testing and development process.

- **Sparkle.Regressor.Client:** [SparkleRegressor.Client](https://github.com/norbertszsor/SparkleRegressor.Client) Client that can be used independently in other projects to communicate with [Sparkle.Reggresor](https://github.com/norbertszsor/Sparkle.Regressor).

## Getting Started

To get started with Sparkle Core, follow these steps:

1. **Local Development**:
   - Clone this repository to your local machine.
   - Open the solution in your preferred IDE (e.g., Visual Studio or Visual Studio Code).
   - Ensure you have .NET Core 7 installed.

2. **Database Configuration**:
   - By default, Sparkle Core is configured to use a local SQLite database.
   - You can switch to a different database engine by modifying the database connection settings in the `appsettings.json` file.

3. **Running the Application**:
   - Build and run the application.
   - Access the Swagger documentation at [http://localhost:7011/swagger/index.html](https://localhost:7011/swagger/index.html) to interact with the API.

4. **Seeding Data**:
   - Use the provided data seeding functionality to populate the database with sample data for testing. (optional, default on)

5. **Set SPARKLE_API_TOKEN Environment Variable**:
   - Before running the application, ensure that you have set the `SPARKLE_API_TOKEN` environment variable. This token is required for authentication and authorization. You can obtain the token from [insert instructions on how to obtain or generate the token].

   Note: Failure to set the `SPARKLE_API_TOKEN` environment variable may result in authentication issues or exceptions when interacting with the API.

## Deployment

To deploy Sparkle Core as an HTTPS service, you have two options:

- **Docker**: Utilize the provided Dockerfile to build a Docker image and deploy it.

   ```bash
   docker build -t sparkle-core .
   docker run -p 7011:7011 sparkle-core
   ```

- **Manual Deployment**: For manual deployment, ensure you have the .NET 7 runtime installed on the target server.

   - Publish the application to a folder of your choice.
   
   ```bash
   dotnet publish -c Release -o <publish-folder>
   ```

   - Configure your server to run the application as an HTTPS service.

   - Start the application using:

   ```bash
   dotnet <publish-folder>/Sparkle.Core.dll
   ```

   Access the API at `https://<your-server-url>/swagger/index.html`.

## Live Version

You can access the live version of Sparkle Core at [https://sparklecore7.bsite.net/swagger/index.html](https://sparklecore7.bsite.net/swagger/index.html).

## Version

The current version of Sparkle Core can be found there [https://github.com/norbertszsor/Sparkle.Core/releases](https://github.com/norbertszsor/Sparkle.Core/releases)
