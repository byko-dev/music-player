namespace music_player.Libs;

public class PlayButtonAction
{
    private static PlayButtonAction instance;
    private static readonly object lockObject = new object();
    private bool _isPlaying = false;
    
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
    
    public PlayStatus UpdateIcon()
    {
        _isPlaying = !_isPlaying;
        ButtonAction();
        return _isPlaying ? PlayStatus.Pause : PlayStatus.Play;  
    }
    
    private void ButtonAction()
    {
        if (_isPlaying) AudioPlayer.Instance.UnPause();
        else AudioPlayer.Instance.Pause();
    }
}