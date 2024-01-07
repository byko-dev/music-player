using music_player.Models.Export;

namespace music_player.Models;

public class Playlist : PlaylistRaw
{
    public User User { get; set; }
    public Sound Sound { get; set; }
}