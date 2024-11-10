# Dashboard
Building a Bot and Malicious Player Detection Dashboard

- DashboardAPI is built using .net 8 so make sure you have the corresponding .net sdk installed for it.
- Make sure you choose the `http` start option for the api.
- For Windows based machines, you might need to install SQLite command-line tool from the SQLite Downloads page, unzip it, and add it to your system PATH, but there is a good chance you might already have it installed.
- To run the front-end app, please install node and npm and after that run `npm i` inside the `player-activities-app` folder

# To run the project
## DashboardAPI
Navigate to ./DashboardAPI and run
`dotnet run`
## player-activities-app
Navigate to ./player-activities-app and run
`npm start`

# Dependencies for running the DashboardAPI with SQLite - run these if you want to add migrations
## to install ef tools globally
`dotnet tool install --global dotnet-ef`
## to update ef tools globally
`dotnet tool update --global dotnet-ef`

# Dependencies for running the player-activities-app
## install dependencies
`npm i`


# Nice to haves
 - [ ] global exception handling
 - [ ] add mapper in between the layers 
 - [ ] add a tool like autofixure with automoq to generate random test data
 - [ ] custom assertions to increase test readability
 - [ ] read di configuration from configuration file
 - [ ] logging in the layers
 - [ ] async processing of activity detection
 - [ ] activity detection bulk optimizations
 - [ ] add logging and error handling in PlayerStatusController
 - [ ] fancier ui with cypress tests included