using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using MusicStore.Core.Album;
using MusicStore.Core.Artist;
using MusicStore.Core.Song;
using MusicStore.Database.Models;
using MusicStore.Database.Repositories;
using MusicStore.Test.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MusicStore.Test
{
    public class MusicUnitTest
    {
        [Theory]
        [InlineData("Pop")]
        [InlineData("Rock")]
        public async Task Searching_ByGenre_ReturnsOnlyArtistsOfSameGenre(string genre)
        {
            var artists = this.GetArtistsFakeData();
            var contextOptions = new DbContextOptions<MusicStoreContext>();
            var mockContext = new Mock<MusicStoreContext>(contextOptions);
            mockContext.Setup(c => c.Artist).Returns(artists.ToMockDbSet().Object);

            var arstistRepository = new ArtistRepository(mockContext.Object);

            var artistService = new ArtistService(arstistRepository);
            var artistsResult = await artistService.GetArtistsByGenreAsync(genre);
            Assert.Equal(artists.Where(c => c.Genre.Name == genre), artistsResult);
        }

        [Fact]
        public async Task SearchingSong_InsideAlbum_ReturnsOnlySongsFromThatAlbum()
        {
            var songs = this.GetSongsFakeData();
            var contextOptions = new DbContextOptions<MusicStoreContext>();
            var mockContext = new Mock<MusicStoreContext>(contextOptions);
            mockContext.Setup(c => c.Song).Returns(songs.ToMockDbSet().Object);

            var songRepository = new SongRepository(mockContext.Object);

            var songService = new SongService(songRepository);
            var songResult = await songService.GetSongsByNameAndAlbumAsync("It", 1);
            Assert.Equal(songs.Where(c => c.AlbumId == 1), songResult);
        }

        [Fact]
        public async Task NumberOfStar_InAlbum_IsTheAverageOfVoteValues()
        {
            var albums = this.GetAlbumsFakeData();
            var contextOptions = new DbContextOptions<MusicStoreContext>();
            var mockContext = new Mock<MusicStoreContext>(contextOptions);
            mockContext.Setup(c => c.Album).Returns(albums.ToMockDbSet().Object);

            var albumRepository = new AlbumRepository(mockContext.Object);

            var albumService = new AlbumService(albumRepository);
            var albumResult = await albumService.GetAlbumByIdAsync(1);
            Assert.Equal(albums.FirstOrDefault(c => c.Id == 1).Ratings.Average(c => c.Value), albumResult.Rating);
        }

        private IQueryable<Song> GetSongsFakeData()
        {
            return new List<Song>
            {
                new Song
                {
                    Name = "I Want It That Way",
                    Artist = new Artist
                    {
                        Name = "Backstreet Boys"
                    }
                },
                new Song
                {
                    Name = "Is It Just Me",
                    Artist = new Artist
                    {
                        Name = "Backstreet Boys"
                    },
                    Album = new Album
                    {
                        Artist = new Artist
                        {
                            Name = "Backstreet Boys"
                        },
                        Name = "DNA",
                    },
                    AlbumId = 1
                }
            }.AsQueryable();
        }
        private IQueryable<Artist> GetArtistsFakeData()
        {
            return new List<Artist>()
            {
                new Artist
                {
                    Genre = new Genre
                    {
                        Name = "Pop"
                    },
                    Name = "Backstreet Boys",
                },
                new Artist
                {
                    Genre = new Genre
                    {
                        Name = "Rock"
                    },
                    Name = "Guns n' Roses",
                },
                new Artist
                {
                    Genre = new Genre
                    {
                        Name = "Pop"
                    },
                    Name = "NSYNC",
                }
            }.AsQueryable();
        }

        private IQueryable<Album> GetAlbumsFakeData()
        {
            return new List<Album>()
            {
                new Album
                {
                    Id = 1,
                    Name = "DNA",
                    Artist = new Artist
                    {
                        Name = "Backstreet Boys"
                    },
                    Ratings = new List<Rating>
                    {
                        new Rating
                        {
                            Value = 4
                        },
                         new Rating
                        {
                            Value = 1
                        },
                          new Rating
                        {
                            Value = 3
                        },
                           new Rating
                        {
                            Value = 1
                        }, new Rating
                        {
                            Value = 2
                        }, new Rating
                        {
                            Value = 4
                        }, new Rating
                        {
                            Value = 5
                        }, new Rating
                        {
                            Value = 2
                        }, new Rating
                        {
                            Value = 3
                        }, new Rating
                        {
                            Value = 4
                        }
                    }
                }
            }.AsQueryable();
        }
    }
}
