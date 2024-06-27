# ShoppingCart API

## Overview
The ShoppingCart API is a RESTful web service designed to manage shopping cart operations for an e-commerce platform. It allows users to add, remove, and view items in their shopping cart, as well as calculate the total amount of their current selections. This API is built using ASP.NET Core and utilizes an in-memory database for storage.

## Features
- **User Cart Management**: Users can manage their shopping cart items, including adding new items, removing existing items, and clearing the cart.
- **Total Amount Calculation**: The API provides functionality to calculate the total amount of the items in the user's shopping cart.
- **In-Memory Storage**: Utilizes an in-memory database to store cart items, making it fast and responsive.

## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or suitable code editor (VS Code, Rider, etc.)

### Running the API Locally
1. **Clone the Repository**
 2. **Launch the API**
   - **Using Visual Studio**: Open the solution file (`ShoppingCart.API.sln`), set the `ShoppingCart.API` project as the startup project, and press F5 to start debugging.
   - **Using the Command Line**:`dotnet restore
 dotnet build
 dotnet run --project ShoppingCart.API`

3. **Accessing the API**
   Once the API is running, you can access it by navigating to `https://localhost:5001/swagger` in your web browser to view the Swagger UI and test the API endpoints.

## API Endpoints
- `GET /api/ShoppingCart/user/{userId}/items` - Retrieves all items in the user's shopping cart.
- `POST /api/ShoppingCart/AddItemsToCart/{userId}` - Adds items to the user's shopping cart.
- `DELETE /api/ShoppingCart/RemoveItemFromCart/{userId}/{itemId}` - Removes a specific item from the user's shopping cart.
- `GET /api/ShoppingCart/user/{userId}/totalamount` - Calculates the total amount of the items in the user's shopping cart.

## Testing
Integration tests for the ShoppingCart API are located in the `ShoppingCart.Test/IntegrationTests` directory. These tests use NUnit and a WebApplicationFactory to simulate requests to the API and ensure functionality.
To run the tests, navigate to the test project directory and use the following command: `dotnet test`


## Contributing
Contributions are welcome! Please feel free to submit pull requests or open issues to suggest improvements or add new features.

## License
This project is licensed under the MIT License - see the LICENSE file for details.


     
   
