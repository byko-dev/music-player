namespace music_player.Models;

public class FileRecord
{
    public int Id { get; set; }
    public string FileName  { get; set; }
    public byte[] FileContent { get; set; }
}