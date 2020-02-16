using System;
using KazkySuspilne.Models;

namespace KazkySuspilne.Services
{
    public interface IAudioService
    {
        void Play(StorySong story);
        void Stop();
    }
}
