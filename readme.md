Property Listing API
This project is a property listing platform backend with role-based access for Brokers and Seekers. Brokers can create, update, and delete property listings, while Seekers can only view them. The project uses MSSQL database and includes authentication via JWT tokens.

Setup Instructions


1. Configure MSSQL Connection String
Update your database connection string in the configuration file (e.g., appsettings.json) to match your MSSQL server setup:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DB_NAME;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
}

2. Apply Database Migrations
Run the following commands in the Package Manager Console or terminal to set up the database schema:

add-migration initial
update-database


3. Seed Database
The database will seed default roles and users automatically:


Broker

Username: broker1

Password: broker@123

Seeker

Username: seeker1

Password: seeker@123


4. Run the Project
Start the project using your IDE or the command line:

dotnet run


API Usage
Authentication

Use the /login endpoint to authenticate with username and password.

Upon success, you will receive a JWT token.

Use this token in the Authorization header for all protected endpoints:

Authorization: Bearer YOUR_TOKEN_HERE


Roles and Permissions

Role	Permissions
Broker	Create, Read, Update, Delete properties
Seeker	Read properties only

Property Listing Endpoints
GET /properties
Retrieve property listings (available for both Broker and Seeker).

Supports filters:

searchTerm — searches title, location, and property type

minPrice — minimum price filter

maxPrice — maximum price filter

Pagination parameters (page, pageSize)

POST /properties
Create a new property listing (Broker only).

PUT /properties/{id}
Update a property listing by ID (Broker only).

DELETE /properties/{id}
Delete a property listing by ID (Broker only).

Property Model Example
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "string",
  "propertyType": 1,
  "location": "string",
  "price": 0,
  "description": "string",
  "features": "feature1,feature2,feature3",
  "imageUrl": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "url": "string",
      "propertyListingId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
  ],
  "brokerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "createdAt": "2025-06-11T19:56:39.694Z",
  "isActive": true
}
features: Comma-separated values (e.g., "Pool,Gym,Garage").

Search: You can search properties using searchTerm that checks title, location, and property type.

Swagger Documentation
Swagger UI is enabled for this project to explore and test the API endpoints interactively.

Access Swagger at: https://localhost:{PORT}/swagger

Follow the schema and examples to create or update properties.