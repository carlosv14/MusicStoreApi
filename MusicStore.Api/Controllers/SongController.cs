using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.Api.Models;
using MusicStore.Core.Song;


namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class SongController : Controller
    {
        private readonly ISongService songService;
        private readonly IMapper mapper;
        private readonly ILogger<SongController> logger;

        public SongController(ISongService songService, IMapper mapper, ILogger<SongController> logger)
        {
            this.songService = songService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "name")] string name, [FromQuery(Name = "albumId")] int albumId)
        {
            try
            {
                var songs = await this.songService.GetSongsByNameAndAlbumAsync(name, albumId);
                return Ok(this.mapper.Map<IEnumerable<SongViewModel>>(songs));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var song = await this.songService.GetSongById(id);
                return Ok(this.mapper.Map<SongViewModel>(song));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
