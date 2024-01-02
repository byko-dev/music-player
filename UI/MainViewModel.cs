using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using music_player.Libs;
using music_player.Models;

namespace music_player.UI;

public class MainViewModel : INotifyPropertyChanged
{
    private Timer updateSliderTimer;
    private bool isUpdatingFromTimer;
    
    public ObservableCollection<Sound> OriginalSounds { get; set; }

    private ObservableCollection<Sound> _sounds;
    
    public ObservableCollection<Sound> Sounds
    {
        get => _sounds;
        set
        {
            _sounds = value;
            OnPropertyChanged(nameof(Sounds));
        }
    }
    
    private string _searchTerm;
    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            _searchTerm = value;
            FilterSounds();
            OnPropertyChanged(nameof(SearchTerm));
        }
    }
    
    
    private double _sliderPlayingValue;
    
    public double SliderPlayingValue
    {
        get => _sliderPlayingValue;
        set
        {
            if (_sliderPlayingValue != value)
            {
                _sliderPlayingValue = value;
                OnPropertyChanged(nameof(SliderPlayingValue));

                if (!isUpdatingFromTimer)
                {
                    AudioPlayer.Instance.Seek((int) _sliderPlayingValue);
                }
            }
        }
    }
    
    
    private float _sliderVolumeValue = 10;
    
    public float SliderVolumeValue
    {
        get => _sliderVolumeValue;
        set
        {
            if (_sliderVolumeValue != value)
            {
                _sliderVolumeValue = value;
                
                AudioPlayer.Instance.SetVolume(_sliderVolumeValue);
                OnPropertyChanged(nameof(SliderVolumeValue));
            }
        }
    }
    
    public MainViewModel()
    {
        LoadSounds();
        
        updateSliderTimer = new Timer(300); 
        updateSliderTimer.Elapsed += UpdateSlider;
        updateSliderTimer.Start();
    }

    private void LoadSounds()
    {
        OriginalSounds = new PlaylistController().GetPlaylist();
        Sounds = new PlaylistController().GetPlaylist();
    }
    
    private void FilterSounds()
    {
        if (string.IsNullOrEmpty(SearchTerm))
        {
            Sounds = new ObservableCollection<Sound>(OriginalSounds);
        }
        else
        {
            Sounds = new ObservableCollection<Sound>(OriginalSounds
                .Where(s => s.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) 
                            || s.Author.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                            || s.Category.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)));
        }
        OnPropertyChanged(nameof(Sounds));
    }
    
    private void UpdateSlider(object sender, ElapsedEventArgs e)
    {
        isUpdatingFromTimer = true;
        SliderPlayingValue = AudioPlayer.Instance.CurrentPosition;
        isUpdatingFromTimer = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}