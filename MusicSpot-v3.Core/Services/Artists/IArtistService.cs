using MusicSpot_v3.Core.Models.Artists;
using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot_v3.Core.Services.Artists
{
    public interface IArtistService
    {
        Task<AllArtistsViewModel> AllArtists(string userId, string searchTerm, int p, int s);

        Task<List<Artist>> ArtistsList(string userId);

        Task<DetailsArtistFormModel> ArtistDetails(int? artistId);

        Task<CreateArtistFormModel> CreateArtist(CreateArtistFormModel artist);

        Task<EditArtistFormModel> EditArtist(int? artistId, EditArtistFormModel artist);

        Task<DetailsArtistFormModel> DeleteArtist(int? artistId, DetailsArtistFormModel artistToDelete);

        bool ArtistExist(int artistId);

    }
}
