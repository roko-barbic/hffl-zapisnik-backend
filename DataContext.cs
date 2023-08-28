namespace roko_test.Data;

using roko_test.Entities;
using Microsoft.EntityFrameworkCore;
public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }

    // public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Tournament> Tournaments {get; set;}
    public DbSet<Game> Games {get; set;}
    public DbSet<Event> Events {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       
    }
    
}
