using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using music_player.Database;
using music_player.Models;

namespace music_player.Repository;

public class SoundRepository : ISoundRepository
{
    
    private readonly DatabaseConnectionContext context;

    public SoundRepository(DatabaseConnectionContext context)
    {
        this.context = context;
    }
    
    public List<Sound> All()
    {
        return context.Sounds.ToList();
    }

    public void Add(Sound sound)
    {
        context.Sounds.Add(sound);
        context.SaveChanges();
    }

    public List<Sound> GetByOwnerId(int ownerId)
    {
        throw new System.NotImplementedException();
    }

    public Sound? GetById(int id)
    {
        return context.Sounds.Include(s => s.File).FirstOrDefault(s => s.Id == id);
    }
}