using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApp.Data.DAL.Models;

namespace WebApplication1.DAL.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<URLModel> URLTable { get; set; } = null!;
    }
}
