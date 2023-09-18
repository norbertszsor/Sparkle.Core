# Sparkle Core v1.0.0 
![logo](https://github.com/norbertszsor/Sparkle.Core/assets/47736350/a1d87941-6cf4-4b24-93da-230aab0743a0)

Sparkle Core is a component of the Sparkle project, developed using .NET Core 7 and following the clean architecture approach. This core module is responsible for managing data flow, utilizing a suite of .NET technologies. It handles data storage and presentation to other system layers, enabling fast access to extensive telemetry records necessary for building predictive models. Sparkle Core provides key interfaces that facilitate seamless communication between [user interface](https://github.com/norbertszsor/Sparkle.Web) and the [regression module](https://github.com/norbertszsor/Sparkle.Regressor). It also supports two forecasting methods: predictive and comparative.

**Key Features:**

- **Data Management:** Sparkle Core efficiently manages data flow, making extensive telemetry records readily accessible for predictive modeling.

- **Modular Clean Architecture:** The application is designed using clean architecture principles, ensuring modularity, flexibility, and maintainability.

- **Database Agnostic:** It uses [Linq2DB](https://linq2db.github.io/) to achieve high-speed queries and data seeding, making it adaptable to different database engines.
  
- **Auto Migrator:** Sparkle Core includes an auto migrator that simplifies database schema updates and migrations, ensuring smooth transitions when evolving the database structure.

- **Seeder:** The seeder functionality allows for easy population of the database with sample data, simplifying the testing and development process.

- **Standalone DLL - Sparkle.Regressor.Client:** Sparkle Core provides a standalone DLL called Sparkle.Regressor.Client that can be used independently in other projects to communicate with [Sparkle.Reggresor](https://github.com/norbertszsor/Sparkle.Regressor). This enables seamless integration and interaction with the regression module in diverse applications.

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
   - Use the provided data seeding functionality to populate the database with sample data for testing.(optionl, default on)

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

The current version of Sparkle Core is 1.0.0.

## Contribution

Contributions to Sparkle Core are highly encouraged. If you wish to contribute, please follow these steps:

1. Fork the repository on GitHub.

2. Create a new branch with a descriptive name for your feature or bug fix.

3. Implement your changes while ensuring that the code passes all tests.

4. Submit a pull request with a clear description of the changes made.

We value your contributions in making Sparkle Core even better. For questions or further information, feel free to contact us. Thank you for choosing Sparkle Core!
