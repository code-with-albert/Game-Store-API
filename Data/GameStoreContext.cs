using GameStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Fighting" },
            new { Id = 2, Name = "Adventure" },
            new { Id = 3, Name = "RPG" }
        );
    }
}
