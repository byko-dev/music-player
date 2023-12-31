using System.Threading.Tasks;
using music_player.Database;
using music_player.Models;

namespace music_player.Repository;

public class FileRecordRepository : IFileRecordRepository
{
    private readonly DatabaseConnectionContext context;

    public FileRecordRepository(DatabaseConnectionContext context)
    {
        this.context = context;
    }
    
    public FileRecord? GetById(int id)
    {
        return context.Files.Find(id);
    }

    public async Task<int> Add(FileRecord fileRecord)
    {
        context.Files.Add(fileRecord);
        await context.SaveChangesAsync();
        return fileRecord.Id;
    }
}