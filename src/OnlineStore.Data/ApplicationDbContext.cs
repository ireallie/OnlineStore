using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Extensions;
using System.Reflection;

namespace OnlineStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<OptionValue> OptionValues { get; set; }
        public virtual DbSet<Variant> Variants { get; set; }
        public virtual DbSet<VariantOptionValue> VariantOptionValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HandleSoftDeletes();

            base.OnModelCreating(modelBuilder);
        }
    }
}
