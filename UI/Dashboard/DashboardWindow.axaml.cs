using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using music_player.Libs;
using music_player.Models;
using music_player.Services;
using music_player.UI.AddSound;
using music_player.UI.Login;

namespace music_player.UI.Dashboard;

public partial class DashboardWindow : Window
{
    public DashboardWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
        
        LoadUserData();
    }

    public void OnLogoutButtonClick(object sender, RoutedEventArgs e)
    {
        (new LoginWindow()).Show();
        Hide();
    }

    public void AddNewSoundButtonClick(object sender, RoutedEventArgs e)
    {
        (new AddSoundWindow()).Show();
    }

    public void LoadUserUploadedSounds_ButtonEvent(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel && ApplicationContext.Instance.IsUserLogged())
        {
            var Sounds = new ObservableCollection<Sound>();

            foreach (Sound sound in ApplicationContext.Instance.LoggedUser.Sounds)
            {
                Sounds.Add(sound);
            }
            
            viewModel.Sounds = Sounds;
        }
    }

    public void LoadAllSounds_ButtonEvent(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            var Sounds = new ObservableCollection<Sound>();
            foreach (Sound sound in (new SoundService()).GetAllSounds())
            {
                Sounds.Add(sound);
            }
            
            viewModel.Sounds = Sounds;
        }
    }
    
    public void PlayButtonEvent(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int Id)
        {
            PlayImage.Source = PlayButtonAction.Instance.SetPlaying();
            
            Sound? sound = (new SoundService()).GetById(Id);

            PlayingNowLabel.Text = (sound!.Name + " - " + sound.Author);
            
            AudioPlayer.Instance.Play(sound!.File.FileContent);

            MusicSlider.Maximum = AudioPlayer.Instance.GetTrackLengthInSeconds();
        }
    }
    
    public void TogglePlayButtonClick(object sender, RoutedEventArgs e)
    {
        //change button icon
        PlayImage.Source = PlayButtonAction.Instance.UpdateIcon();
    }

    public void Window_Closed(object sender, EventArgs e)
    {
        Environment.Exit(0);
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
}