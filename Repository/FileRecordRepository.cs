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

    public async Task<int> Add(FileRecord file)
    {
        context.Files.Add(file);
        await context.SaveChangesAsync();
        return file.Id;
    }

    public void Update(FileRecord file)
    {
        context.Files.Update(file);
        context.SaveChanges();
    }

    public void Delete(FileRecord? file)
    {
        if (file != null)
        {
            context.Files.Remove(file);
            context.SaveChanges();
        }
    }
}