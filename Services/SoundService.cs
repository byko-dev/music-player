using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using music_player.Libs;
using music_player.Models;
using music_player.Models.Export;
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
        
        Add(new Sound()
        {
            FileId = FileId,
            UserId = ApplicationContext.Instance.LoggedUser!.Id,
            Name = Title!,
            Author = Author!,
            Category = MusicGenre!,
            User = ApplicationContext.Instance.LoggedUser
        });
        
        return "Sound was uploaded successful!";
    }

    public void Import(SoundRaw soundRaw)
    {
        if (GetById(soundRaw.Id) != null)
            throw new Exception("Sound already exists!");
        
        FileRecord? fileRecord = (new FileService()).GetById(soundRaw.FileId);

        if (fileRecord == null)
            throw new Exception("File with provided id doesnt exists!");

        User? user = (new UserService()).GetById(soundRaw.UserId);

        if (user == null)
            throw new Exception("User with provided id doesnt exists!");

        Add(new Sound()
        {
            Id = soundRaw.Id,
            Name = soundRaw.Name,
            Category = soundRaw.Category,
            Author = soundRaw.Author,
            File = fileRecord,
            FileId = soundRaw.FileId,
            User = user,
            UserId = user.Id,
        });
    }
    
    public void Add(Sound sound)
    {
        soundRepository.Add(sound);
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