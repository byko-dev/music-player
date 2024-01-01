using Avalonia.Controls;

namespace music_player.UI.ErrorDialog;

public partial class ErrorDialogWindow : Window
{
    public ErrorDialogWindow(string messageContent)
    {
        InitializeComponent();
        ErrorMessageContent.Text = messageContent;
    }
}