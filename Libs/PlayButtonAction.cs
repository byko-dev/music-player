using Avalonia.Media.Imaging;
namespace music_player.Libs;

public class PlayButtonAction
{
    private static PlayButtonAction instance;
    private static readonly object lockObject = new object();
    private bool _isPlaying = false;
    private const string PLAY_ICON = "avares::/../../../../UI/Assets/Img/play.png";
    private const string PAUSE_ICON = "avares::/../../../../UI/Assets/Img/pause.png";
    
    public static PlayButtonAction Instance
    {
        get
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new PlayButtonAction();
                }
                return instance;
            }
        }
    }

    public bool IsPlaying
    {
        get { return _isPlaying; }
        set { _isPlaying = value; }
    }
    
    public Bitmap UpdateIcon()
    {
        _isPlaying = !_isPlaying;
        var pathData = _isPlaying ? PAUSE_ICON : PLAY_ICON;
        ButtonAction();
        return new Bitmap(pathData);  
    }

    public Bitmap SetPlaying()
    {
        _isPlaying = true;
        return new Bitmap(PAUSE_ICON);  
    }
    
    private void ButtonAction()
    {
        if (_isPlaying) AudioPlayer.Instance.UnPause();
        else AudioPlayer.Instance.Pause();
    }
}