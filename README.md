# ShoppingCart API

## Overview
The ShoppingCart API is a RESTful web service designed to manage shopping cart operations for an e-commerce platform. It allows users to add, remove, and view items in their shopping cart, as well as calculate the total amount of their current selections. This API is built using ASP.NET Core and utilizes an in-memory database for storage.

## Features
- **User Cart Management**: Users can manage their shopping cart items, including adding new items, removing existing items, and clearing the cart.
- **Total Amount Calculation**: The API provides functionality to calculate the total amount of the items in the user's shopping cart.
- **In-Memory Storage**: Utilizes an in-memory database to store cart items, making it fast and responsive.
- **AutoWrapper**: The API utilizes AutoWrapper to seamlessly handle request and response logging, as well as to standardize the API response format.

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

## Tests

The ShoppingCart API includes comprehensive testing to ensure reliability and performance. Tests are divided into two main categories: Unit Tests and Integration Tests.

### Unit Tests

Unit tests in the `ShopingCartTests` class focus on testing the functionality of the `ShoppingCartService` in isolation, using mocked dependencies for `IDbRepository<UserShoppingCart>` and `IDbRepository<ShoppingCartItem>`.

- **GetShoppingCartAmountAsync_ReturnsTotalAmount**: Tests that the service correctly calculates and returns the total amount of items in a user's shopping cart.
- **GetShoppingCartAmountAsync_ThrowsNotFoundException**: Ensures that the service throws a `NotFoundException` when attempting to calculate the total amount for a non-existent user's shopping cart.
- **AddItemToShoppingCartAsync_AddsItemsSuccessfully**: Verifies that items can be successfully added to a user's shopping cart and confirms the addition by checking the return value.
- **AddItemToShoppingCartAsync_ThrowsInvalidOperationExceptionForInvalidItem**: Checks that the service throws an `InvalidOperationException` when attempting to add an invalid item to the shopping cart.
- **AddItemToShoppingCartAsync_CreatesNewCartIfNotExists**: Confirms that a new shopping cart is created for a user if one does not already exist when attempting to add items.


### Integration Tests

Integration tests in the `ShoppingCartControllerIntegrationTests` class verify the interactions between components and the integration of different parts of the application, focusing on the ShoppingCart controller's responses and interactions with the in-memory database.

- **GetCartTotalAmount_ReturnsNotFound**: Verifies that a request for the total amount in the shopping cart of a non-existent user returns a NotFound status code.
- **GetCartTotalAmount_ReturnsOkWithTotalAmount**: Tests that after adding items to a user's shopping cart, the total amount returned by the API is correct and the status is OK.
- **AddItemsToCart_ReturnsBadRequest**: Ensures that attempting to add an invalid item to the shopping cart returns a BadRequest status code.
- **AddItemsToCart_ReturnsOk**: Checks that valid items can be successfully added to the shopping cart, returning an OK status.
- **AddItemsToCart_CreatesNewCartIfNotExists**: Confirms that adding items for a new user successfully creates a new shopping cart and returns an OK status.
- **GetCartTotalAmount_ReturnsUpdatedAmount**: After adding items to the shopping cart twice, this test verifies that the total amount is updated accordingly and returns the correct sum.

## Contributing
Contributions are welcome! Please feel free to submit pull requests or open issues to suggest improvements or add new features.

## License
This project is licensed under the MIT License - see the LICENSE file for details.


     
   
