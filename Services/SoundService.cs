using System;
using Microsoft.Extensions.DependencyInjection;
using music_player.Models;
using music_player.Repository;

namespace music_player.Services;

public class SoundService
{
    private readonly ISoundRepository soundRepository;
    
    public string? Author;
    public string? Title;
    public string? MusicGenre;
    public int FileId;
    public int OwnerId;
    
    public SoundService()
    {
        soundRepository = Program.ServiceProvider.GetService<ISoundRepository>();
    }
    
    public string Add()
    {
        Validator();
        
        soundRepository.Add(new Sound()
        {
            FileId = FileId,
            OwnerId = OwnerId,
            Name = Title!,
            Author = Author!,
            Category = MusicGenre!
        });
        
        return "success";
    }

    private void Validator()
    {
        if (string.IsNullOrEmpty(Title))
            throw new Exception("Title cannot be empty!");
        
        if (string.IsNullOrEmpty(Author))
            throw new Exception("Author cannot be empty!");

        if (string.IsNullOrEmpty(MusicGenre))
            throw new Exception("Music genre cannot be empty!");
    }
}