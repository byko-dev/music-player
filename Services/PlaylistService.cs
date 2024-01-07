using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using music_player.Libs;
using music_player.Models;
using music_player.Models.Export;
using music_player.Repository;

namespace music_player.Services;

public class PlaylistService
{
    private readonly IPlaylistRepository playlistRepository;

    public Sound Sound { get; set; }

    public PlaylistService()
    {
        playlistRepository = Program.ServiceProvider.GetService<IPlaylistRepository>();
    }

    public void UpdatePlaylist()
    {
        if (IsOnPlaylist())
        {
            Remove();
        }
        else
        {
            Add();
        }
    }

    public List<Sound> GetUserPlaylist()
    {
        List<Playlist> soundsOnPlaylist = playlistRepository.GetByOwnerId(ApplicationContext.Instance.LoggedUser!.Id);

        return soundsOnPlaylist.Select(playlist => playlist.Sound).ToList();
    }

    public List<Playlist> GetAllPlaylists()
    {
        return playlistRepository.All();
    }

    public void Add()
    {
        Add(new Playlist
        {
            UserId = ApplicationContext.Instance.LoggedUser!.Id,
            User = ApplicationContext.Instance.LoggedUser,
            Sound = Sound,
            SoundId = Sound.Id
        });
    }

    public void Import(PlaylistRaw playlistRaw)
    {
        if (GetById(playlistRaw.Id) != null)
            throw new Exception("Playlist already exists!");
        
        Sound? sound = (new SoundService()).GetById(playlistRaw.SoundId);

        if (sound == null)
            throw new Exception("Sound with provided id doesnt exists!");

        User? user = (new UserService()).GetById(playlistRaw.UserId);

        if (user == null)
            throw new Exception("User with provided id doesnt exists!");

        Add(new Playlist()
        {
            Id = playlistRaw.Id,
            Sound = sound,
            SoundId = sound.Id,
            User = user,
            UserId = user.Id
        });
    }

    public Playlist? GetById(int id)
    {
        return playlistRepository.GetById(id);
    }
    
    public void Remove()
    {
        Playlist? playlist =
            playlistRepository.GetByOwnerIdAndSoundId(ApplicationContext.Instance.LoggedUser!.Id, Sound.Id);
        playlistRepository.Delete(playlist);
    }

    public void RemoveBySoundId(int soundId)
    {
        playlistRepository.DeleteBySoundId(soundId);
    }

    public bool IsOnPlaylist()
    {
        if (!ApplicationContext.Instance.IsUserLogged())
            throw new Exception("You must be logged in to add sounds to playlist!");
        
        return playlistRepository.GetByOwnerIdAndSoundId(ApplicationContext.Instance.LoggedUser!.Id, Sound.Id) != null;
    }
    
    private void Add(Playlist playlist)
    {
        playlistRepository.Add(playlist);
    }
}