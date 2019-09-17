using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.Api.Models;
using MusicStore.Core.Album;

namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IMapper mapper;
        private readonly ILogger<AlbumController> logger;

        public AlbumController(IAlbumService albumService, IMapper mapper, ILogger<AlbumController> logger)
        {
            this.albumService = albumService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var album = await this.albumService.GetAlbumByIdAsync(id);
                return Ok(this.mapper.Map<AlbumDetailViewModel>(album));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
