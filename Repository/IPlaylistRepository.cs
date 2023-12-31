using music_player.Models;

namespace music_player.Repository;

public interface IPlaylistRepository
{
    void Add(Playlist playlist);
}