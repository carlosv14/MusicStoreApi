using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Core.Album
{
    public interface IAlbumService
    {
        Task<AlbumModel> GetAlbumByIdAsync(int id);
    }
}
