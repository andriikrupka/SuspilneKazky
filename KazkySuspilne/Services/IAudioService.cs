using System;
using System.Collections.Generic;
using KazkySuspilne.Models;

namespace KazkySuspilne.Services
{
    public enum PlaybackMode
    {
        None,
        RepeatOne,
        Repeat
    }

    public enum AudioMode
    {
        Pause,
        Playing,
        Buffering
    }

    public class AudioModeEventArgs : EventArgs
    {
        public AudioModeEventArgs(AudioMode audioMode)
        {
            AudioMode = audioMode;
        }

        public AudioMode AudioMode { get; }
    }

    public interface IAudioService
    {
        void SetPlaylist(List<StorySong> playlist);
        void Play();
        void Play(StorySong story);
        bool PlayNext();
        bool PlayPrevious();
        StorySong CurrentSong { get; }
        PlaybackMode PlaybackMode { get; }
        AudioMode AudioMode { get; }
        void Stop();
        void Seek(double position);
        void SetPlaybackMode(PlaybackMode playbackMode);
        
        event EventHandler SongChanged;
        event EventHandler SongEnded;
        event EventHandler<PositionEventArgs> PositionChanged;
        event EventHandler<AudioModeEventArgs> StatusChanged;
    }

    public class PositionEventArgs : EventArgs
    {
        public PositionEventArgs(double currentPosition, double totalDuration)
        {
            CurrentPosition = currentPosition;
            TotalDuration = totalDuration;
        }

        public double CurrentPosition { get; }
        public double TotalDuration { get; }
    }
}
