using AutoMapper;
using MusicStore.Api.Models;
using MusicStore.Core.Album;

namespace MusicStore.Api.Profiles
{
    public class AlbumDetailProfile : Profile
    {
        public AlbumDetailProfile()
        {
            CreateMap<AlbumModel, AlbumDetailViewModel>();
        }
    }
}
