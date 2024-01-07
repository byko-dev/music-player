using System;

namespace music_player.Models.Export;

public class UserRaw
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
}