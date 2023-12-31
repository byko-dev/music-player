using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using music_player.Libs;
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
        Validator();
        
        byte[] fileContent = File.ReadAllBytes(FilePath);

        FileRecord fileRecord = new FileRecord
        {
            FileName = Path.GetFileName(FilePath),
            FileContent = fileContent
        };

        return await fileRepository.Add(fileRecord);
    }

    public FileRecord? GetById(int Id)
    {
        return fileRepository.GetById(Id);
    }


    private void Validator()
    {
        if (!ApplicationContext.Instance.IsUserLogged())
            throw new Exception("You must be logged in to upload music tracks!");

        if (string.IsNullOrEmpty(FilePath) || !IsValidFileFormat())
            throw new Exception("Unsupported audio file type!");
        
        if (!File.Exists(FilePath))
            throw new Exception("File doesn't exists!");
    }

    private bool IsValidFileFormat()
    {
        string extension = Path.GetExtension(FilePath).ToLower();

        switch (extension)
        {
            case ".mp3":
            case ".wav":
            case ".aac":
                return true;
            default:
                return false;
        }
    }
}