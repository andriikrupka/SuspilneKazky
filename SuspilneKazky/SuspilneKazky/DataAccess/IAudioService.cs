using System;
using System.Collections.Generic;
using SuspilneKazky.Models;

namespace SuspilneKazky.DataAccess
{
    public interface IAudioManager
    {
        void SetupItems(List<StorySong> items);
        StorySong CurrentSong { get; }
        bool IsPlaying { get; }
        void Play();
        void Play(StorySong song);
        void Pause();
        void Stop();
        void PlayNext();
        void PlayPrevious();
    }
}
