using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Api.Models;
using MusicStore.Core.Album;

namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IMapper mapper;

        public AlbumController(IAlbumService albumService, IMapper mapper)
        {
            this.albumService = albumService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<AlbumViewModel> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public async Task<AlbumDetailViewModel> Get(int id)
        {
            var album = await this.albumService.GetAlbumByIdAsync(id);
            return this.mapper.Map<AlbumDetailViewModel>(album);
        }
    }
}
