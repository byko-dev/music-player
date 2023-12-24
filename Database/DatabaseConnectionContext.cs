using System;
using Microsoft.EntityFrameworkCore;
using music_player.Models;

namespace music_player.Database;

public class DatabaseConnectionContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(@"Server=localhost;Port=3306;Database=music_player;User=root;Password=root;", 
            new MySqlServerVersion(new Version(8, 0, 21)));
    }
}