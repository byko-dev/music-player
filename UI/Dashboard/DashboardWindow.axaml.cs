using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using music_player.Libs;
using music_player.Models;
using music_player.Services;
using music_player.UI.AddSound;
using music_player.UI.ErrorDialog;
using music_player.UI.Import;
using music_player.UI.Login;
using music_player.UI.SoundManage;

namespace music_player.UI.Dashboard;

public partial class DashboardWindow : Window
{
    public DashboardWindow()
    {
        InitializeComponent();

        LoadDataContext();
        LoadUserData();
    }

    public void OnLogoutButtonClick(object sender, RoutedEventArgs e)
    {
        AudioPlayer.Instance.Dispose();
        (new LoginWindow()).Show();
        Hide();
    }

    public void AddNewSoundButtonClick(object sender, RoutedEventArgs e)
    {
        (new AddSoundWindow()).Show();
    }

    public void LoadUserUploadedSounds_ButtonEvent(object sender, RoutedEventArgs e)
    {
        ApplicationContext.Instance.LoadedPlaylist = PlaylistEnum.UploadedSounds;
        new PlaylistController().ViewPlaylist();
    }

    public void LoadAllSounds_ButtonEvent(object sender, RoutedEventArgs e)
    {
        ApplicationContext.Instance.LoadedPlaylist = PlaylistEnum.AllSounds;
        new PlaylistController().ViewPlaylist();
    }

    public void LoadUserPlaylist_ButtonEvent(object sender, RoutedEventArgs e)
    {
        ApplicationContext.Instance.LoadedPlaylist = PlaylistEnum.Playlist;
        new PlaylistController().ViewPlaylist();
    }

    public void ImportExportData_ButtonEvent(object sender, RoutedEventArgs e)
    {
        new ImportWindow().Show();
    }
    
    public void Modify_ButtonEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!(sender is Button button && button.CommandParameter is int Id))
                throw new Exception("Invalid sound ID");
            
            Sound? sound = new SoundService().GetById(Id);

            if (sound == null)
                throw new Exception("Sound was not found!");
            
            new SoundManageWindow(sound).Show();
        }
        catch (Exception exception)
        {
            (new ErrorDialogWindow(exception.Message)).Show();
        }
    }
    
    public void Play_ButtonEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!(sender is Button button && button.CommandParameter is int Id))
                throw new Exception("Invalid sound ID");
            
            PlaySound(Id);
        }
        catch (Exception exception)
        {
            (new ErrorDialogWindow(exception.Message)).Show();
        }
    }

    public void PreviousSound_ButtonEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            int Id = new PlaylistController().GetPreviousSoundId();

            if (Id == -1) return;
            
            PlaySound(Id);
        }
        catch (Exception exception)
        {
            (new ErrorDialogWindow(exception.Message)).Show();
        }

    }

    public void NextSound_ButtonEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            int Id = new PlaylistController().GetNextSoundId();

            if (Id == -1) return;
            
            PlaySound(Id);
        }
        catch (Exception exception)
        {
            (new ErrorDialogWindow(exception.Message)).Show();
        }
    }
    
    public void TogglePlayButtonClick(object sender, RoutedEventArgs e)
    {
        //change button icon
        PlayStatus status = PlayButtonAction.Instance.UpdateIcon();

        PlayImage.IsVisible = status == PlayStatus.Play;
        PauseImage.IsVisible = status == PlayStatus.Pause;
    }
    
    private void OnSearchTextBoxKeyUp(object sender, KeyEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.SearchTerm = ((TextBox)sender).Text;
        }
    }

    public void Window_Closed(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }
    
    private void LoadDataContext()
    {
        MainViewModel dataContextModel = new MainViewModel();
        DataContext = dataContextModel;
        ApplicationContext.Instance.DataContextModel = dataContextModel;
    }
    
    private void LoadUserData()
    {
        if(!ApplicationContext.Instance.IsUserLogged()) return;
        
        SetUsernameLabel();
    }
    
    private void SetUsernameLabel()
    {
        UsernameLabel.Text = ApplicationContext.Instance.LoggedUser!.Username;
        MyPlaylistButton.IsVisible = true;
        MyUploadedSoundsButton.IsVisible = true;
    }

    private void PlaySound(int Id)
    {
        Sound? sound = new SoundService().GetById(Id);

        if (sound == null)
            throw new Exception("Sound was not found!");

        PlayButtonAction.Instance.IsPlaying = true;
        PlayImage.IsVisible = false;
        PauseImage.IsVisible = true;
        PlayingNowLabel.Text = (sound.Name + " - " + sound.Author);
            
        ApplicationContext.Instance.PlayingSoundId = sound.Id;
            
        AudioPlayer.Instance.Play(sound.File.FileContent);
        MusicSlider.Maximum = AudioPlayer.Instance.GetTrackLengthInSeconds();
    }
}