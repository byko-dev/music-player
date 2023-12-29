using System.Threading.Tasks;
using music_player.Models;

namespace music_player.Repository;

public interface IFileRecordRepository
{
    FileRecord? GetById(int id);
    Task<int> Add(FileRecord fileRecord);
}