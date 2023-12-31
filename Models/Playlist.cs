namespace music_player.Models;

public class Playlist
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public User User { get; set; }
    public int SoundId { get; set; }
    public Sound Sound { get; set; }
}