using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplication1.DAL.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; } = null!;
    }
}
