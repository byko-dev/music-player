using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using music_player.Database;
using music_player.Models;

namespace music_player.Repository;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly DatabaseConnectionContext context;

    public PlaylistRepository(DatabaseConnectionContext context)
    {
        this.context = context;
    }
    
    public void Add(Playlist playlist)
    {
        context.Playlists.Add(playlist);
        context.SaveChanges();
    }

    public void Delete(Playlist? playlist)
    {
        if (playlist != null)
        {
            context.Playlists.Remove(playlist);
            context.SaveChanges();
        }
    }

    public void DeleteBySoundId(int soundId)
    {
        List<Playlist> playlists = context.Playlists.Where(p => p.SoundId == soundId).ToList();

        if (playlists.Any())
        {
            context.Playlists.RemoveRange(playlists);
            context.SaveChanges();
        }
    }

    public Playlist? GetByOwnerIdAndSoundId(int ownerId, int soundId)
    {
        return context.Playlists.FirstOrDefault(p => p.UserId == ownerId && p.SoundId == soundId);
    }

    public List<Playlist> GetByOwnerId(int ownerId)
    {
        return context.Playlists.Where(p => p.UserId == ownerId).Include(s => s.Sound).ToList();
    }

    public List<Playlist> All()
    {
        return context.Playlists.ToList();
    }

    public Playlist? GetById(int Id)
    {
        return context.Playlists.FirstOrDefault(s => s.Id == Id);
    }
}