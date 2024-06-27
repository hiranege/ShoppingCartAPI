using Moq;
using NUnit.Framework;
using ShoppingCart.API.Entities;
using ShoppingCart.API.Exceptions;
using ShoppingCart.API.Repositories;
using ShoppingCart.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Test.UnitTests
{
    [TestFixture]
    public class ShopingCartTests
    {
        private Mock<IDbRepository<UserShoppingCart>> _mockShoppingCartRepository;
        private Mock<IDbRepository<ShoppingCartItem>> _mockShoppingCartItemsRepository;
        private ShoppingCartService _shoppingCartService;

        [SetUp]
        public void Setup()
        {
            _mockShoppingCartRepository = new Mock<IDbRepository<UserShoppingCart>>();
            _mockShoppingCartItemsRepository = new Mock<IDbRepository<ShoppingCartItem>>();
            _shoppingCartService = new ShoppingCartService(_mockShoppingCartRepository.Object, _mockShoppingCartItemsRepository.Object);
        }

        [Test]
        public async Task GetShoppingCartAmountAsync_ReturnsTotalAmount()
        {
            var userId = "testUser";
            var cartId = Guid.NewGuid();
            var shoppingCarts = new List<UserShoppingCart>
            {
                new UserShoppingCart
                {
                    Id =cartId,
                    UserId = userId,
                    Items = new List<ShoppingCartItem>
                    {
                        new ShoppingCartItem { Price = 1, Name="coffee", Id=Guid.NewGuid(), ShopingCartId=cartId },
                        new ShoppingCartItem {Price = 1, Name = "coffee", Id = Guid.NewGuid(), ShopingCartId=cartId}
                    }
                }
            };
            _mockShoppingCartRepository.Setup(repo => repo.SearcAsync(p => p.UserId == userId))
                                       .ReturnsAsync(shoppingCarts);

            var totalAmount = await _shoppingCartService.GetShoppingCartAmountAsync(userId);

            Assert.That(totalAmount, Is.EqualTo(2));
        }

        [Test]
        public void GetShoppingCartAmountAsync_ThrowsNotFoundException()
        {
            var userId = "testUser";
            _mockShoppingCartRepository.Setup(repo => repo.SearcAsync(p => p.UserId == userId))
                                       .ReturnsAsync(new List<UserShoppingCart>());

            Assert.ThrowsAsync<NotFoundException>(() => _shoppingCartService.GetShoppingCartAmountAsync(userId));
        }

        [Test]
        public async Task AddItemToShoppingCartAsync_AddsItemsSuccessfully()
        {
            var userId = "testUser";
            var items = new List<string> { "coffee", "bread" };
            var shoppingCarts = new List<UserShoppingCart> { new UserShoppingCart { UserId = userId } };
            _mockShoppingCartRepository.Setup(repo => repo.SearcAsync(p => p.UserId == userId))
                                       .ReturnsAsync(shoppingCarts);
            _mockShoppingCartItemsRepository.Setup(repo => repo.AddRangeAsync(It.IsAny<List<ShoppingCartItem>>()))
                                            .Returns(Task.CompletedTask);

            var result = await _shoppingCartService.AddItemToShoppingCartAsync(userId, items);

            Assert.IsTrue(result);
            _mockShoppingCartItemsRepository.Verify(repo => repo.AddRangeAsync(It.IsAny<List<ShoppingCartItem>>()), Times.Once);
        }


        [Test]
        public void AddItemToShoppingCartAsync_ThrowsInvalidOperationExceptionForInvalidItem()
        {
            var userId = "testUser";
            var items = new List<string> { "invalidItem" };

            Assert.ThrowsAsync<InvalidOperationException>(() => _shoppingCartService.AddItemToShoppingCartAsync(userId, items));
        }

        [Test]
        public async Task AddItemToShoppingCartAsync_CreatesNewCartIfNotExists()
        {
            var userId = "testUser";
            var items = new List<string> { "coffee", "bread" };
            _mockShoppingCartRepository.Setup(repo => repo.SearcAsync(p => p.UserId == userId))
                                       .ReturnsAsync(new List<UserShoppingCart>());
            _mockShoppingCartItemsRepository.Setup(repo => repo.AddRangeAsync(It.IsAny<List<ShoppingCartItem>>()))
                                            .Returns(Task.CompletedTask);

            var result = await _shoppingCartService.AddItemToShoppingCartAsync(userId, items);

            Assert.IsTrue(result);
            _mockShoppingCartRepository.Verify(repo => repo.AddAsync(It.IsAny<UserShoppingCart>()), Times.Once);
        }
    }
}
