using Domain.AppUsers;
using Domain.Carts;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Persistence
{
    public interface IAppDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Domain.Carts.Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}