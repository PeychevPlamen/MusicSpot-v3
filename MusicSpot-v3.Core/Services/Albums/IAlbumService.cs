using MusicSpot_v3.Core.Models.Albums;
using MusicSpot_v3.Infrastructure.Data.Models;
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

        Task<CreateAlbumFormModel> CreateAlbum(string userId, CreateAlbumFormModel album);

        Task<EditAlbumFormModel> EditAlbum(int? albumId, EditAlbumFormModel album);

        Task<DetailsAlbumFormModel> DeleteAlbum(int? albumId, DetailsAlbumFormModel album);

        Task<List<Album>> Albums(int id);

        bool AlbumExists(int albumId);

        Task<TotalUserAlbumsViewModel> TotalUserAlbums(string userId, string searchTerm, int p, int s);
    }
}
