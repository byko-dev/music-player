using music_player.Models.Export;

namespace music_player.Models;

public class Sound : SoundRaw
{
    public FileRecord File { get; set; }
    public User User { get; set; }
}