using ShoppingCart.API.Entities;
using ShoppingCart.API.Exceptions;
using ShoppingCart.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.API.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDbRepository<UserShoppingCart> _shoppingCartRepository;
        private readonly IDbRepository<ShoppingCartItem> shoppingCartItemsRepository;
        private readonly Dictionary<string, decimal> _items = new Dictionary<string, decimal>
        {
            { "coffee", 1 },
            { "orange", 2 },
            { "bread", 3 }

        };

        public ShoppingCartService(IDbRepository<UserShoppingCart> shoppingCartRepository, IDbRepository<ShoppingCartItem> shoppingCartItemsRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            this.shoppingCartItemsRepository = shoppingCartItemsRepository;
        }

        public async Task<decimal> GetShoppingCartAmountAsync(string userId)
        {
            var shoppingCart = await _shoppingCartRepository.SearcAsync(cart => cart.UserId == userId);
            if (shoppingCart == null || shoppingCart.Count ==0 ) throw new NotFoundException("Shopping cart not found.");

            decimal totalAmount = shoppingCart.First().Items?.Sum(item => item.Price) ?? 0m;

            return totalAmount;
        }

        public async Task<bool> AddItemToShoppingCartAsync(string userId, List<string> items)
        {
            ValidateItems(items);

            UserShoppingCart? shoppingCart;

            var shoppingCarts = await _shoppingCartRepository.SearcAsync(cart => cart.UserId == userId);

            if (shoppingCarts == null || shoppingCarts.Count == 0)
            {
                shoppingCart = new UserShoppingCart
                {
                    UserId = userId,
                    Id = Guid.NewGuid()
                };
                await _shoppingCartRepository.AddAsync(shoppingCart);
            }
            else
            {
                shoppingCart = shoppingCarts.First();
            }

            List<ShoppingCartItem> itemsTobeAdd = items.Select( p=> new ShoppingCartItem
            {
                Id = Guid.NewGuid(),
                ShopingCartId = shoppingCart.Id,
                Name = p,
                Price = _items[p.ToLower()]

            } ).ToList();

            await shoppingCartItemsRepository.AddRangeAsync(itemsTobeAdd);

            return true;
        }

        private void ValidateItems(List<string> items)
        {
            foreach (var item in items)
            {
                if (!_items.ContainsKey(item.ToLower()))
                {
                    throw new InvalidOperationException($"Item {item} not found.");
                }
            }
        }
    }
}
