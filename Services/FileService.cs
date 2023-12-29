using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using music_player.Models;
using music_player.Repository;

namespace music_player.Services;

public class FileService
{
    private readonly IFileRecordRepository fileRepository;

    public string FilePath;

    public FileService()
    {
        fileRepository = Program.ServiceProvider.GetService<IFileRecordRepository>();
    }

    public async Task<int> UploadFile()
    {
        if (!File.Exists(FilePath))
            throw new Exception("File doesn't exists!");
        
        byte[] fileContent = File.ReadAllBytes(FilePath);

        FileRecord fileRecord = new FileRecord
        {
            FileName = Path.GetFileName(FilePath),
            FileContent = fileContent
        };

        return await fileRepository.Add(fileRecord);
    }

    



}