using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserShoppingCart> UserShoppingCart { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserShoppingCart>()
            .HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.ShopingCartId);

        modelBuilder.Entity<UserShoppingCart>()
            .Navigation(x => x.Items)
            .AutoInclude();

            
    }
}
