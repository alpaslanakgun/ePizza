using ePizza.Data.Concrete.EntityFramework.Mappings;
using ePizza.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace ePizza.Data.Concrete.EntityFramework.Contexts
{
    public class ePizzaContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {

        public ePizzaContext()
        {

        }
        //configuration from settings
        public ePizzaContext(DbContextOptions<ePizzaContext> options) : base(options)
        {

        }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        //needed for migration
      


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=ALPASLAN_AKGUN\MSSQLSERVERA;Database=ePizza;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PaymentDetailsMap());
            builder.ApplyConfiguration(new CartMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new ProductTypeMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new OrderItemMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new UserTokenMap());
            builder.ApplyConfiguration(new UserRoleMap());
            builder.ApplyConfiguration(new UserLoginMap());
            builder.ApplyConfiguration(new UserClaimMap());
            builder.ApplyConfiguration(new RoleClaimMap());
        }
    }
}
