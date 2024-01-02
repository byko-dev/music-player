using System;
using Microsoft.Extensions.DependencyInjection;
using music_player.Libs;
using music_player.Models;
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
    
    public void Add()
    {
        playlistRepository.Add(new Playlist
        {
            OwnerId = ApplicationContext.Instance.LoggedUser!.Id,
            User = ApplicationContext.Instance.LoggedUser,
            Sound = Sound,
            SoundId = Sound.Id
        });
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

        Console.WriteLine(ApplicationContext.Instance.LoggedUser!.Id + " " + Sound.Id);
        return playlistRepository.GetByOwnerIdAndSoundId(ApplicationContext.Instance.LoggedUser!.Id, Sound.Id) != null;
    }
    
    
}