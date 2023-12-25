using Avalonia.Controls;
using Avalonia.Interactivity;
using music_player.Models;
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
    
    private void LoadUserData()
    {
        if(userLogged == null) return;
        
        SetUsernameLabel();
    }

    private void SetUsernameLabel()
    {
        var usernameLabel = this.FindControl<TextBlock>("UsernameLabel");
        usernameLabel.Text = userLogged.Username;
    }
}