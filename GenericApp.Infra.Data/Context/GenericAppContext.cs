using GenericApp.Domain.Models;
using GenericApp.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GenericApp.Infra.Data.Context
{
    public class GenericAppContext : DbContext, IGenericAppContext
    {
        public GenericAppContext(DbContextOptions<GenericAppContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<JuridicalPerson> JuridicalPerson { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>(u =>
            {
                u.HasIndex(e => e.Cpf).IsUnique();
                u.HasOne(e => e.Creator);
                u.HasOne(e => e.Updater);
            });
            modelBuilder.Entity<Order>(u =>
            {
                u.HasOne(e => e.Creator);
                u.HasOne(e => e.Updater);
            });
            modelBuilder.Entity<User>(u =>
            {
                u.HasIndex(e => e.Email).IsUnique();
                u.HasOne(e => e.Creator);
                u.HasOne(e => e.Updater);
            });
            modelBuilder.Entity<Customer>(u =>
            {
                u.HasIndex(e => e.Cpf).IsUnique();
                u.HasMany(c => c.Orders).WithOne(o => o.Customer)
                .IsRequired();
                u.HasOne(e => e.Creator);
                u.HasOne(e => e.Updater);
            });
            modelBuilder.Entity<Company>(u =>
            {
                u.HasIndex(e => e.Cnpj).IsUnique();
                u.HasMany(c => c.Employees).WithOne(e => e.Company)
                .IsRequired();
                u.HasOne(e => e.Creator);
                u.HasOne(e => e.Updater);
            });

            foreach (var foreingKey in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                foreingKey.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}
