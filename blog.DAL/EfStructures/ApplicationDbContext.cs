using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
// using Entities;

#nullable disable

namespace blog.DAL
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Image> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<CartItem>()
            // .HasOne(p => p.ProductNavigation)
            // .WithOne();

            // modelBuilder.Entity<ShoppingCart>()
            // .HasMany(s => s.CartItems)
            // .WithOne();

            // modelBuilder.Entity<Product>(entity =>
            // {

            //     entity.Property(e => e.Name).IsFixedLength(true);
            // });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
