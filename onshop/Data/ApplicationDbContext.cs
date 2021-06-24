using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using onshop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace onshop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductTypes> productTypes { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
       
    }
}
