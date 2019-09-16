using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Api.Models;
using MusicStore.Core.Artist;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class MusicController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IMapper mapper;

        public MusicController(IArtistService artistService, IMapper mapper)
        {
            this.artistService = artistService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ArtistViewModel>> Get([FromQuery(Name = "genre")] string genre)
        {
            var artists = await this.artistService.GetArtistsByGenreAsync(genre);
            return this.mapper.Map<IEnumerable<ArtistViewModel>>(artists);
        }
    }
}
