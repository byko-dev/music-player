using System.Collections.Generic;
using music_player.Models.Export;

namespace music_player.Models;

public class User : UserRaw
{
    public List<Sound> Sounds { get; set; }
    public List<Playlist> Playlists { get; set; } 
}