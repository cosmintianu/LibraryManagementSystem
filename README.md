# LibraryManagementSystem

## Configuration
- Clone the repository

- Set up the Microsoft SQL Server (https://www.microsoft.com/pt-pt/sql-server/sql-server-downloads),
also https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms is useful.

- I used Visual Studio 2022, here you go to View > Server Explorer, right click on Data Connections > Create New SQL Server DB, then login into your Microsoft SQL Server, also set up a name for the db AND CHANGE ENCRYPT TO OPTIONAL(FALSE) and check the trust server certificate checkbox.

- Now the db is set up, a window will appear with its properties or you can use F4 key. Here you will need the Connection String which youll use in the appsetting.json.

- Now go to Tools > NUGet Packet Manager > Packet Manager Console. Since the BookContext is set up already, you just need to write in the newly opened console **add-migration "retrieve db"** and then **update-database** to save it, now the db should be populated.

- Finally, you can run the application and check the API calls in the file **LibraryManagementSystem.http** and also can see the database change in the SQL server management studio.

## Extra feature
The project's extra feature is the function for the library administrator to quickly see which books are low on stock generally speaking. The user of the app can see all the books from the database that have an actual quantity less than the threshold number (which by default is 3) given by him.