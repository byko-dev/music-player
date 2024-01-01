using System;
using System.Collections.Generic;
using music_player.Models;
using music_player.UI;

namespace music_player.Libs;

public class ApplicationContext
{
    private static readonly Lazy<ApplicationContext> instance = new Lazy<ApplicationContext>(() => new ApplicationContext());
    
    private ApplicationContext() { }
    
    public static ApplicationContext Instance => instance.Value;

    public bool IsLogged { private get; set; }
    public User? LoggedUser { get; set; }
    
    public PlaylistEnum LoadedPlaylist { get; set; } = PlaylistEnum.AllSounds;

    public MainViewModel DataContextModel { get; set; }

    public List<Sound> SoundsInPlaylist { get; set; }

    public int PlayingSoundId { get; set; }

    public bool IsUserLogged()
    {
        return IsLogged && LoggedUser != null;
    }
    
}
