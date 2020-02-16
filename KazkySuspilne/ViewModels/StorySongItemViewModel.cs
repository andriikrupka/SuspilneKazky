using System;
using KazkySuspilne.Models;
using KazkySuspilne.Services;
using MvvmCross.ViewModels;

namespace KazkySuspilne.ViewModels
{
    public class StorySongItemViewModel : MvxViewModel
    {
        
        public StorySongItemViewModel(StorySong storySong)
        {
            StorySong = storySong;
        }

        public StorySong StorySong { get; private set; }

        public string StoryName => StorySong.Name;

        public string StoryAuthor => StorySong.Auth;

    }
}
