using System;
using System.Collections.Generic;

namespace music_player.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Sound> Sounds { get; set; }
    public List<Playlist> Playlists { get; set; } 
    
}