using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Customer>().HasData(
               new Customer
               {
                   CustomerId = 1,
                   Name = "Ahmed",
                   Email = "a@ahmed.com",
                   Address = "Maadi"
               }, new Customer
               {
                   CustomerId = 2,
                   Name = "Mohamed",
                   Email = "m@mohamed.com",
                   Address = "Helwan"
               }
           );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "P1",
                    Price = 15,
                    Description = "P1 Desc",
                    StockQuantity = 10
                }, new Product
                {
                    ProductId = 2,
                    Name = "P2",
                    Price = 30,
                    Description = "P2 Desc",
                    StockQuantity = 50
                }
            );
        }
    }
}
