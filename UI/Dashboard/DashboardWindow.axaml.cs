using Avalonia.Controls;
using Avalonia.Interactivity;
using music_player.Models;
using music_player.UI.AddSound;
using music_player.UI.Login;

namespace music_player.UI.Dashboard;

public partial class DashboardWindow : Window
{
    private User? userLogged;

    public DashboardWindow(User? userLogged = null)
    {
        InitializeComponent();
        this.userLogged = userLogged;
        
        LoadUserData();
    }

    public void OnLogoutButtonClick(object sender, RoutedEventArgs e)
    {
        (new LoginWindow()).Show();
        Hide();
    }

    public void AddNewSoundButtonClick(object sender, RoutedEventArgs e)
    {
        (new AddSoundWindow(userLogged)).Show();
    }
    
    private void LoadUserData()
    {
        if(userLogged == null) return;
        
        SetUsernameLabel();
    }

    private void SetUsernameLabel()
    {
        UsernameLabel.Text = userLogged.Username;
    }
}