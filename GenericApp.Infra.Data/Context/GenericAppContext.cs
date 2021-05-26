using Flunt.Notifications;
using GenericApp.Domain.Models;
using GenericApp.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GenericApp.Infra.Data.Context
{
    public class GenericAppContext : DbContext
    {
        public GenericAppContext(DbContextOptions<GenericAppContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<JuridicalPerson> JuridicalPerson { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Notification>();
            modelBuilder.Entity<Employee>();//.ToTable("Employee");
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<Customer>()/*.ToTable("Customer")*/.HasMany(c => c.Orders).WithOne(o => o.Customer)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Company>()/*.ToTable("Company")*/.HasMany(c => c.Employees).WithOne(e => e.Company)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity<long> && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity<long>)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity<long>)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
