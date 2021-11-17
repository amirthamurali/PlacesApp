# Places App
 # About
The web application provides a UI with cascading drop downs of the popular trio Country-State-City, typically used for filling in place details. The data is retrieved from a REST API exposed by Universal Tutorial - https://www.universal-tutorial.com/rest-apis/free-rest-api-for-country-state-city

Key solution considerations:

The solution houses a Web API Core running on dotnet 5 (net5.0) that has:
- Typed Clients used for forming requests effieciently without causing socket exhaustion.  (One of Microsoft recommended methods for using HttpClients. Check - https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests)
- Abstraction of External API calls through custom services - IUniversalService acts as an abstraction over the API calls and IUniversalAuthenticationService abstracts the authentication details
- Abstraction of bearer token handling with each request through custom handler - BearerTokenHandler
- Global exception handling using custom exception middleware. A simplified version has been implemented presently, with logging any excpetion that occurs within the API
- Logging using light weight file logger for ILogger provided by SeriLog for use in small projects or over the initial stages of development. Log files are configured to generate one per day and stored in "Logs" folder in the PlacesApp/src/Places folder

Web API Core project is consumed by MVC Core Project:
- Typed Clients used for forming requests
- Abstraction of Web API calls through custom service - IPlaceServices
- Cascading dropdown in View handled by AJAX jQuery on client side
- Logging using light-weight SeriLog for ILogger file logging
- Exception Handling - Current version uses in-built exception handling. Work is being done to include a custom middleware for global exception handling

 # Running the app through VS Code
- Step 1: Setup SSH token using ssh agent at local machine and update the key in GitHub account
- Step 2: In Dotnet CLI, browse to a BaseDirectory which should house the PlacesApp local repository and run the command: git clone git@github.com:amirthamurali/PlacesApp.git
- Step 3: The API is configured to run on Ports 7152 (for https) and 5105 (for http), and the MVC Core App is configured to run on ports 5001 (for https) and 5000 (for http). Check if they are free, otherwise configure port numbers in Properties/launchSettings.js 
- Step 4: Make sure you have a self-signed SSL installed for local machine. If not run the Dotnet CLI command: dotnet dev-certs https --trust
- Step 5: Open a split terminal and change directory to PlacesApp/src/PlacesAPI and PlacesApp/src/PlacesWeb respectively. Build each project
- Step 6: Run the PlacesAPI. Once it is up and running, run PlacesWeb

# Run unit tests to generate BDDfy Report
- Step 1: Browse to PlacesApp/Test/PlacesAPI.Test
- Step 2: Run tests using dotnet test
- Step 3: From file Manager, browse to location "\PlacesApp\test\PlacesAPI.Test\bin\Debug\net5.0" and open BDDfy HTML file with browser. It shows up an interactive HTML
