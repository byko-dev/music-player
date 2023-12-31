using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using music_player.Services;

namespace music_player.UI.AddSound;

public partial class AddSoundWindow : Window
{
    private string FilePath;
    
    public AddSoundWindow()
    {
        InitializeComponent();
    }
    
    private async void OnFileSelectClick(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog();
        var result = await dialog.ShowAsync(this);

        if (result != null && result.Any())
        {
            FilePathBox.Text = result.First();
            FilePath = result.First();
        }
    }
    

    private async void AddSoundButtonClickEvent(object? sender, RoutedEventArgs e)
    {
        try
        {
            FileService fileService = new FileService()
            {
                FilePath = FilePath
            };
            
            int fileId = await fileService.UploadFile();
            
            var selectedItem = MusicGenre.SelectedValue as ComboBoxItem;
            
            SoundService soundService = new SoundService()
            {
                Title = Title.Text,
                Author = Author.Text,
                MusicGenre = selectedItem?.Content?.ToString() ?? "",
                FileId = fileId
            };

            string result = soundService.Add();
            
            SetResponseMessage(result);
        }
        catch (Exception exception)
        {
            SetResponseMessage(exception.Message, false);
        }
    }

    private void SetResponseMessage(string message, bool success = true)
    {
        AddSoundResult.Content = message;
        AddSoundResult.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
    }
}