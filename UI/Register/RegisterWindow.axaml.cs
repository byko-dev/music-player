using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using music_player.Services;
using music_player.UI.Login;

namespace music_player.UI.Register;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
    }
    
    private void OnLoginButtonClick(object sender, RoutedEventArgs e)
    {
        new LoginWindow().Show();
        Hide();
    }

    private void RegisterAttemptEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            RegisterService registerService = new RegisterService()
            {
                Username = Username!.Text,
                Password = Password!.Text,
                RetypedPassword = PasswordRetyped!.Text
            };
                
            string result = registerService.RegisterAttempt();

            SetResponseMessage(result);
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
        RegisterResult.Content = message;
        RegisterResult.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
    }
}