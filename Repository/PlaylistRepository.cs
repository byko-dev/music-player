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
}