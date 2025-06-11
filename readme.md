Property Listing API Documentation
Overview
The Property Listing API is a backend platform for real estate listings with role-based access control. It supports two user roles: Brokers (who can create, update, and delete listings) and Seekers (who can only view listings). The system uses JWT authentication and an MSSQL database.

Setup Instructions
1. Database Configuration
Update the connection string in appsettings.json:

json
"ConnectionStrings": {
  "DefaultConnection": "DATA SOURCE=YOUR_SERVER; DATABASE=littledb; Integrated Security=True; TrustServerCertificate=True;"
}
2. Database Migrations
Run these commands to set up the database schema:

bash
add-migration initial
update-database
3. Run the Application
Start the project with:

bash
dotnet run
4. Default Users
The system automatically seeds these default accounts:

Broker

Username: broker1

Password: broker@123

Seeker

Username: seeker1

Password: seeker@123

Authentication
Login
Endpoint: POST /api/Account/Login

Request:

json
{
  "username": "broker1",
  "password": "broker@123"
}
Response:
Returns a JWT token to be used in the Authorization header for protected endpoints:

text
Authorization: Bearer YOUR_TOKEN_HERE
User Management
Register Role
Endpoint: POST /api/Account/RegisterRole

Request:

json
{
  "description": "Broker",
  "rolePriority": 0
}
Register Broker
Endpoint: POST /api/Account/RegisterHouseBroker

Request:

json
{
  "username": "string",
  "password": "string",
  "email": "string"
}
Register Seeker
Endpoint: POST /api/Account/RegisterHouseSeeker

Request:

json
{
  "username": "string",
  "password": "string",
  "email": "string"
}
Property Endpoints
Get All Properties
Endpoint: GET /api/PropertyListing/GetAllProperty

Parameters:

pageSize (default: 10)

pageNumber (default: 1)

searchTerm (searches title, location, property type)

minPrice (minimum price filter)

maxPrice (maximum price filter)

Example:

text
/api/PropertyListing/GetAllProperty?pageSize=10&pageNumber=1&searchTerm="title"&maxPrice=200&minPrice=100
Get Property by ID
Endpoint: GET /api/PropertyListing/GetAllPropertyById

Create Property (Broker only)
Endpoint: POST /api/PropertyListing/CreateProperty

Request:

json
{
  "title": "Sample Property",
  "propertyType": 1,
  "location": "New York",
  "price": 500000,
  "description": "Beautiful 3-bedroom house",
  "features": "Pool,Garage,Garden",
  "imageUrl": [
    {
      "url": "https://example.com/image1.jpg"
    }
  ],
  "brokerId": "0FC62E23-6D69-4BC1-8E0E-32B5EC395A12",
  "createdAt": "2025-06-11T20:57:54.587Z",
  "isActive": true
}
Update Property (Broker only)
Endpoint: PUT /api/PropertyListing/UpdateProperty

Request:

json
{
  "id": "B3163C40-92A6-4DD7-BCC3-9CBFB6834125",
  "title": "Updated Property Title",
  "propertyType": 1,
  "location": "New York",
  "price": 550000,
  "description": "Updated description",
  "features": "Pool,Garage,Garden",
  "brokerId": "0FC62E23-6D69-4BC1-8E0E-32B5EC395A12",
  "createdAt": "2025-06-11T21:00:20.293Z",
  "isActive": true
}
Delete Property (Broker only)
Endpoint: DELETE /api/PropertyListing/DeleteProperty

Parameters:

Pass property listing ID in query string

Data Model
Property
json
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
Swagger Documentation
Interactive API documentation is available at:

text
https://localhost:{PORT}/swagger
Role Permissions
Role	Permissions
Broker	Create, Read, Update, Delete properties
Seeker	Read properties only