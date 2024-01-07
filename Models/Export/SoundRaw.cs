namespace music_player.Models.Export;

public class SoundRaw
{
    public int Id { get; set; }
    public int FileId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
}