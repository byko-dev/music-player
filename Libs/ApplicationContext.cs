using System;
using music_player.Models;

namespace music_player.Libs;

public class ApplicationContext
{
    private static readonly Lazy<ApplicationContext> instance = new Lazy<ApplicationContext>(() => new ApplicationContext());
    
    private ApplicationContext() { }
    
    public static ApplicationContext Instance => instance.Value;

    public bool IsLogged { private get; set; }
    public User? LoggedUser { get; set; }


    public bool IsUserLogged()
    {
        return IsLogged && LoggedUser != null;
    }
    
}
