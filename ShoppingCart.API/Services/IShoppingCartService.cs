using ShoppingCart.API.Entities;

namespace ShoppingCart.API.Services
{
    public interface IShoppingCartService
    {
        Task<decimal> GetShoppingCartAmountAsync(string userId);
        Task<bool> AddItemToShoppingCartAsync(string userId, List<string> items);
       
    }
}
