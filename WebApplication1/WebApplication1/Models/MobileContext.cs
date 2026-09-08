using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class MobileContext : DbContext
{
    public DbSet<Phone> Phones { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!; 

    public MobileContext(DbContextOptions<MobileContext> options) : base(options)
    {
    }
}