using System;
using System.IO;
using NAudio.Wave;

namespace music_player.Libs;

public class AudioPlayer
{
    private static AudioPlayer _instance;
    private static readonly object lockObject = new object();

    private IWavePlayer wavePlayer;
    private WaveStream waveStream;
    
    private AudioPlayer() { }
    
    public static AudioPlayer Instance
    {
        get
        {
            lock (lockObject)
            {
                if (_instance == null)
                {
                    _instance = new AudioPlayer();
                }
                return _instance;
            }
        }
    }
    
    public double CurrentPosition
    {
        get
        {
            if (waveStream == null)
                return 0;
            
            return waveStream.CurrentTime.TotalSeconds;
        }
    }
    
    public void Play(byte[] audioData)
    {
        if (wavePlayer == null)
        {
            wavePlayer = new WaveOutEvent();
        }
        else
        {
            wavePlayer.Stop();
        }
        
        waveStream = new BlockAlignReductionStream(
            new WaveChannel32(
                new Mp3FileReader(
                    new MemoryStream(audioData))));

        wavePlayer.Init(waveStream);
        wavePlayer.Play();
    }

    public void Stop()
    {
        wavePlayer?.Stop();
    }

    public void Pause()
    {
        if (wavePlayer == null || wavePlayer.PlaybackState != PlaybackState.Playing) 
            return;
            
        wavePlayer.Pause();
    }

    public void UnPause()
    {
        if (wavePlayer == null || wavePlayer.PlaybackState != PlaybackState.Paused)
            return;
        
        wavePlayer.Play();
    }
    
    public void Dispose()
    {
        waveStream?.Dispose();
        wavePlayer?.Dispose();
    }
    
    public void Seek(int positionInSeconds)
    { 
        if (waveStream != null && waveStream.CanSeek)
        {
            long newPosition = (positionInSeconds * waveStream.WaveFormat.AverageBytesPerSecond);
            newPosition = Math.Min(waveStream.Length, Math.Max(0, newPosition));
            waveStream.Position = (int) newPosition;
        }
    }
    
    public double GetTrackLengthInSeconds()
    {
        return waveStream.TotalTime.TotalSeconds;
    }
    
    public void SetVolume(float volume)
    {
        if (wavePlayer == null) return;
        
        wavePlayer.Volume = (volume) / 100;
    }
}