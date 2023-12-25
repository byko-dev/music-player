using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
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
        new DashboardWindow().Show();
        Hide();
    }

    private void LoginAttemptEvent(object sender, RoutedEventArgs e)
    {
        var usernameTextBox = this.FindControl<TextBox>("Username");
        var passwordTextBox = this.FindControl<TextBox>("Password");
        
        try
        {
            LoginService registerService = new LoginService()
            {
                Username = usernameTextBox.Text,
                Password = passwordTextBox.Text
            };
                
            _ = registerService.LoginAttempt();

            (new DashboardWindow(registerService.UserData)).Show();
            Hide();
        }
        catch (Exception exception)
        {
            SetResponseMessage(exception.Message, false);
        }
    }
    
    private void SetResponseMessage(string message, bool success = true)
    {
        var registerResultLabel = this.FindControl<Label>("LoginResult");
        
        registerResultLabel.Content = message;
        registerResultLabel.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
    }
    
}