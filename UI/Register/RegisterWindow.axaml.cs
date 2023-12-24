using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using music_player.Services;

namespace music_player;

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
        var usernameTextBox = this.Find<TextBox>("Username");
        var passwordTextBox = this.Find<TextBox>("Password");
        var retypedPasswordTextBox = this.Find<TextBox>("PasswordRetyped");
        
        try
        {
            RegisterService registerService = new RegisterService()
            {
                Username = usernameTextBox.Text,
                Password = passwordTextBox.Text,
                RetypedPassword = retypedPasswordTextBox.Text
            };
                
            string result = registerService.RegisterAttempt();

            SetResponseMessage(result);
        }
        catch (Exception exception)
        {
            SetResponseMessage(exception.Message, false);
        }
    }

    private void SetResponseMessage(string message, bool success = true)
    {
        var registerResultLabel = this.FindControl<Label>("RegisterResult");
        
        registerResultLabel.Content = message;
        registerResultLabel.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
    }
}