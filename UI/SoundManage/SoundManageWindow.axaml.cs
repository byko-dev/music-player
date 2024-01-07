using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using music_player.Libs;
using music_player.Models;
using music_player.Services;
using music_player.UI.ErrorDialog;

namespace music_player.UI.SoundManage;

public partial class SoundManageWindow : Window
{
    private Sound sound;
    private string FilePath;
    
    public SoundManageWindow(Sound sound)
    {
        InitializeComponent();
        this.sound = sound;
        LoadSoundData();
    }

    public async void OnFileSelectClick_ButtonEvent(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog();
        var result = await dialog.ShowAsync(this);

        if (result != null && result.Any())
        {
            FilePathBox.Text = result.First();
            FilePath = result.First();
        }
    }
    
    public void Update_ButtonEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            FileService fileService = new FileService()
            {
                FilePath = FilePath
            };
            fileService.Update(sound.File);
            
            var selectedItem = MusicGenre.SelectedValue as ComboBoxItem;
            
            SoundService soundService = new SoundService()
            {
                Title = Title.Text,
                Author = Author.Text,
                MusicGenre = selectedItem?.Content?.ToString() ?? ""
            };
            string resultMessage = soundService.Update(sound);

            //reload playlist
            new PlaylistController().ViewPlaylist();
            
            SetResponseMessage(resultMessage);
        }
        catch (Exception exception)
        {
            SetResponseMessage(exception.Message, false);
        }
    }

    public void Delete_ButtonEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            (new PlaylistService()).RemoveBySoundId(sound.Id);
            (new SoundService()).Remove(sound);
            (new FileService()).Remove(sound.File);
            
            //reload playlist
            new PlaylistController().ViewPlaylist();
        
            Hide();
        }
        catch (Exception exception)
        {
            (new ErrorDialogWindow(exception.Message)).Show();
        }
    }
    
    public void AddToPlaylist_ButtonEvent(object sender, RoutedEventArgs e)
    {
        PlaylistService playlistService = new PlaylistService();
        playlistService.Sound = sound;
        playlistService.UpdatePlaylist();
        
        //update button styles
        SetStyleForAddToPlaylistButton();
    }
    
    private void LoadSoundData()
    {
        Title.Text = sound.Name;
        Author.Text = sound.Author;
        FilePathBox.Text = sound.File.FileName;
        SetDefaultComboBoxValue();
        SetStyleForAddToPlaylistButton();
        
        if (!ApplicationContext.Instance.IsUserLogged() || sound.UserId != ApplicationContext.Instance.LoggedUser!.Id)
        {
            Title.IsReadOnly = true;
            Author.IsReadOnly = true;
            MusicGenre.IsEnabled = false;
            SoundManageResult.IsVisible = false;
            DeleteButton.IsVisible = false;
            ChangeFileButton.IsVisible = false;
            UpdateButton.IsVisible = false;
        }
    }
    
    private void SetDefaultComboBoxValue()
    {
        foreach (ComboBoxItem item in MusicGenre.Items)
        {
            if (item.Content.ToString() == sound.Category)
            {
                MusicGenre.SelectedItem = item;
                break;
            }
        }
    }
    
    private void SetResponseMessage(string message, bool success = true)
    {
        SoundManageResult.Content = message;
        SoundManageResult.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
    }

    private void SetStyleForAddToPlaylistButton()
    {
        PlaylistService playlistService = new PlaylistService();
        playlistService.Sound = sound;
        
        if(playlistService.IsOnPlaylist())
        {
            AddToPlaylistButton.BorderBrush = new SolidColorBrush(Colors.Red);
            AddToPlaylistButtonLabel.Text = "Remove from Playlist";
        }
        else
        {
            AddToPlaylistButton.BorderBrush = new SolidColorBrush(Colors.Green);
            AddToPlaylistButtonLabel.Text = "Add to Playlist";
        }
    }
}