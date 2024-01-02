using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using music_player.Libs;
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
    
    public SoundService()
    {
        soundRepository = Program.ServiceProvider.GetService<ISoundRepository>();
    }
    
    public Sound? GetById(int Id)
    {
        return soundRepository.GetById(Id);
    }
    
    public string Add()
    {
        Validator();
        
        soundRepository.Add(new Sound()
        {
            FileId = FileId,
            OwnerId = ApplicationContext.Instance.LoggedUser!.Id,
            Name = Title!,
            Author = Author!,
            Category = MusicGenre!,
            User = ApplicationContext.Instance.LoggedUser
        });
        
        return "Sound was uploaded successful!";
    }

    public string Update(Sound sound)
    {
        Validator();

        sound.Name = Title!;
        sound.Author = Author!;
        sound.Category = MusicGenre!;
        
        soundRepository.Update(sound);
        return "Sound was update successful!";
    }

    public void Remove(Sound sound)
    {
        soundRepository.Delete(sound);
    }
    
    public List<Sound> GetAllSounds()
    {
        return soundRepository.All();
    }
    
    private void Validator()
    {
        if (!ApplicationContext.Instance.IsUserLogged())
            throw new Exception("You must be logged in to upload music tracks!"); 
        
        if (string.IsNullOrEmpty(Title))
            throw new Exception("Title cannot be empty!");
        
        if (string.IsNullOrEmpty(Author))
            throw new Exception("Author cannot be empty!");

        if (string.IsNullOrEmpty(MusicGenre))
            throw new Exception("Music genre cannot be empty!");
    }
}