using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomersService.Core.Models;

namespace CustomersService.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Society> Societies { get; set; } = null!;
        public DbSet<Entrepreneur> Entrepreneurs { get; set; } = null!;
        public DbSet<Requisites> Requisites { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options, IServiceProvider serviceProvider) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Requisites)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasOne(x => x.Entrepreneur)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Customer>()
                .HasOne(x => x.Society)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}