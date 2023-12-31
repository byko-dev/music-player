namespace music_player.Models;

public class Sound
{
    public int Id { get; set; }
    public int FileId { get; set; }
    public FileRecord File { get; set; }
    public int OwnerId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
}