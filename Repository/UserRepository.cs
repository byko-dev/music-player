using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using music_player.Database;
using music_player.Models;

namespace music_player.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseConnectionContext context;

    public UserRepository(DatabaseConnectionContext context)
    {
        this.context = context;
    }
    
    public User? GetById(int id)
    {
        return context.Users.Find(id);
    }

    public void Add(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }

    public User? GetByUsername(string username)
    {
        return context.Users.Include(s => s.Sounds)
            .Include(p => p.Playlists)
            .FirstOrDefault(u => u.Username == username);
    }

    public List<User> All()
    {
        return context.Users.ToList();
    }
}