using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using music_player.Libs;
using music_player.Models;
using music_player.Services;

namespace music_player.UI;

public class MainViewModel : INotifyPropertyChanged
{
    private Timer updateSliderTimer;
    private bool isUpdatingFromTimer;
    private float _sliderVolumeValue = 10;
    private double _sliderPlayingValue;
    
    public MainViewModel()
    {
        Sounds = new ObservableCollection<Sound>();
        LoadSounds();
        
        updateSliderTimer = new Timer(300); 
        updateSliderTimer.Elapsed += UpdateSlider;
        updateSliderTimer.Start();
    }

    private void LoadSounds()
    {
        foreach (Sound sound in (new SoundService()).GetAllSounds())
        {
            Sounds.Add(sound);
        }
    }
    
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