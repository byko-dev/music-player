using System;
using Microsoft.EntityFrameworkCore;
using music_player.Models;

namespace music_player.Database;

public class DatabaseConnectionContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sound> Sounds { get; set; }
    public DbSet<FileRecord> Files { get; set; }
    public DbSet<Playlist> Playlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(@"Server=localhost;Port=3306;Database=music_player;User=root;Password=root;", 
            new MySqlServerVersion(new Version(8, 0, 21)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //relation one User to many Sounds
        modelBuilder.Entity<User>()
            .HasMany(s => s.Sounds)
            .WithOne(u => u.User);

        //relation one User to many Playlists
        modelBuilder.Entity<User>()
            .HasMany(p => p.Playlists)
            .WithOne(u => u.User);

        //relation one to one, Sound to FileRecord
        modelBuilder.Entity<FileRecord>()
            .HasOne(s => s.Sound)
            .WithOne(f => f.File)
            .HasForeignKey<Sound>(k => k.FileId);
    }
    
}