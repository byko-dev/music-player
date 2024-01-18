using Avalonia.Controls;
using Avalonia.Interactivity;

namespace music_player.UI.ErrorDialog;

public partial class ErrorDialogWindow : Window
{
    public ErrorDialogWindow(string messageContent)
    {
        InitializeComponent();
        ErrorMessageContent.Text = messageContent;
        Topmost = true;
    }

    public void CloseWindow_ButtonEvent(object sender, RoutedEventArgs e)
    {
        Hide();
    }
    
}