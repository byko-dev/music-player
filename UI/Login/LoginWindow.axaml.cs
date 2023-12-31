using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using music_player.Libs;
using music_player.Services;
using music_player.UI.Register;
using music_player.UI.Dashboard;

namespace music_player.UI.Login;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void OnRegisterButtonClick(object sender, RoutedEventArgs e)
    {
        new RegisterWindow().Show();
        Hide();
    }

    private void OnLoginAsGuest(object sender, RoutedEventArgs e)
    {
        ApplicationContext.Instance.IsLogged = false;
        
        new DashboardWindow().Show();
        Hide();
    }

    private void LoginAttemptEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            LoginService registerService = new LoginService()
            {
                Username = Username?.Text,
                Password = Password?.Text
            };
                
            _ = registerService.LoginAttempt();

            //set ApplicationContext props
            ApplicationContext.Instance.IsLogged = true;
            ApplicationContext.Instance.LoggedUser = registerService.UserData;
            
            (new DashboardWindow()).Show();
            Hide();
        }
        catch (Exception exception)
        {
            SetResponseMessage(exception.Message, false);
        }
    }
    
    public void Window_Closed(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }
    
    private void SetResponseMessage(string message, bool success = true)
    {
        LoginResult.Content = message;
        LoginResult.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
    }
    
}