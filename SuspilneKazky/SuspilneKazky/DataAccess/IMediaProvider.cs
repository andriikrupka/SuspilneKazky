using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuspilneKazky.Models;

namespace SuspilneKazky.DataAccess
{
    public interface IMediaProvider
    {

        Task<List<StorySong>> LoadStoriesAsync();

        string OnlineUrl { get; }
    }
}
