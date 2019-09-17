using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.Api.Models;
using MusicStore.Core.Artist;

namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class MusicController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IMapper mapper;
        private readonly ILogger<MusicController> logger;

        public MusicController(IArtistService artistService, IMapper mapper, ILogger<MusicController> logger)
        {
            this.artistService = artistService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "genre")] string genre)
        {
            try
            {
                var artists = await this.artistService.GetArtistsByGenreAsync(genre);
                return Ok(this.mapper.Map<IEnumerable<ArtistViewModel>>(artists));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
