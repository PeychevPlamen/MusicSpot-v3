using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicSpot_v3.Core.Models;
using MusicSpot_v3.Core.Models.Tracks;

namespace MusicSpot_v3.Core.Services.Tracks
{
    public interface ITrackService
    {
        Task<CreateTrackFormModel> CreateTrack(CreateTrackFormModel track);

        Task<AllTracksViewModel> AllTracks(int albumId);
    }
}
