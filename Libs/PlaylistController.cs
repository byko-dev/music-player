using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using music_player.Models;
using music_player.Services;

namespace music_player.Libs;

public class PlaylistController
{
    public void ViewPlaylist()
    {
        ApplicationContext.Instance.DataContextModel.Sounds = 
            ApplicationContext.Instance.DataContextModel.OriginalSounds = GetPlaylist();
    }

    public ObservableCollection<Sound> GetPlaylist()
    {
        ObservableCollection<Sound> Sounds = new ObservableCollection<Sound>();
        
        ApplicationContext.Instance.SoundsInPlaylist = GetSounds();
        
        foreach (Sound sound in ApplicationContext.Instance.SoundsInPlaylist)
        {
            Sounds.Add(sound);
        }
        
        return Sounds;
    }
    
    private List<Sound> GetSounds()
    {
        switch (ApplicationContext.Instance.LoadedPlaylist)
        {
            case PlaylistEnum.AllSounds:
                return (new SoundService()).GetAllSounds();
            case PlaylistEnum.UploadedSounds:
                return ApplicationContext.Instance.LoggedUser!.Sounds;
            case PlaylistEnum.Playlist:
                return (new PlaylistService()).GetUserPlaylist();
            default:
                return (new SoundService()).GetAllSounds();
        }
    }

    public int GetPreviousSoundId()
    {
        int soundIndexInPlaylist = ApplicationContext.Instance.SoundsInPlaylist.FindIndex(s => s.Id == ApplicationContext.Instance.PlayingSoundId);;

        if (soundIndexInPlaylist > 0)
        {
            return ApplicationContext.Instance.SoundsInPlaylist[(soundIndexInPlaylist-1)].Id;
        }

        if (soundIndexInPlaylist == 0)
        {
            return ApplicationContext.Instance.SoundsInPlaylist.Last().Id;
        }

        return -1; //Not Found
    }

    public int GetNextSoundId()
    {
        int soundIndexInPlaylist = ApplicationContext.Instance.SoundsInPlaylist.FindIndex(s => s.Id == ApplicationContext.Instance.PlayingSoundId);;
        
        if (soundIndexInPlaylist == (ApplicationContext.Instance.SoundsInPlaylist.Count - 1))
        {
            return ApplicationContext.Instance.SoundsInPlaylist.First().Id;
        }
        
        if (soundIndexInPlaylist >= 0)
        {
            return ApplicationContext.Instance.SoundsInPlaylist[(soundIndexInPlaylist+1)].Id;
        }

        return -1; //Not Found
    }
}