using MusicSpot_v3.Core.Models.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot_v3.Core.Services.Albums
{
    public interface IAlbumService
    {
        Task<AllAlbumsViewModel> AllAlbums(string userId, int artistId, string searchTerm, int p, int s);

        Task<DetailsAlbumFormModel> DetailsAlbum(int? albumId);

        Task<CreateAlbumFormModel> CreateAlbum(CreateAlbumFormModel album);

        Task<EditAlbumFormModel> EditAlbum(int? albumId, EditAlbumFormModel album);

        bool AlbumExists(int albumId);
    }
}
