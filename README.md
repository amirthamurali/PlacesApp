# Places App
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
