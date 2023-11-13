using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContext
{
    //DbContext class repesent the database. I.E an abstraction of the database
    //therefore in this class we're going to define anything related to the db such 
    //as the tables, constraints, rules, relationshis, etc.

    public class ShoppingCartContext : IdentityDbContext<IdentityUser>
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
        : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } //note: represents the table Products
        public DbSet<Category> Categories { get; set; } //note: represents the table Categories
        public DbSet<OrderDetail> OrderDetails { get; set; } //note: represents the table OrderDetails


    }
}
