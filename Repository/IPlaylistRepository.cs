using music_player.Models;

namespace music_player.Repository;

public interface IPlaylistRepository
{
    void Add(Playlist playlist);
    void Delete(Playlist? playlist);
    void DeleteBySoundId(int soundId);
    Playlist? GetByOwnerIdAndSoundId(int ownerId, int soundId);
}