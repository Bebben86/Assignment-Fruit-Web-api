using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreasFruit_api.Models;
using Microsoft.EntityFrameworkCore;

namespace AndreasFruit_api.Data
{
    public class FruitContext : DbContext
    {
        public DbSet<Fruit> Fruits {get; set;}
        public DbSet<Category> Categories {get; set;}
        public FruitContext(DbContextOptions options) : base(options)
        {
        }
    }
}