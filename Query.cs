using System.Linq;
using dotnet_graphql_postgres.Models;
using HotChocolate;
using HotChocolate.Data;

namespace dotnet_graphql_postgres
{
  public class Query
  {
    [UseProjection]
    [UseFiltering]
    public IQueryable<Track> GetTracks([Service] postgresContext ctx)
    {
      return ctx.Tracks;
    }

    [UseProjection]
    [UseFiltering]
    public IQueryable<Album> GetAlbums([Service] postgresContext ctx)
    {
      return ctx.Albums;
    }
    
    [UseProjection]
    [UseFiltering]
    public IQueryable<Artist> GetArtists([Service] postgresContext ctx)
    {
      return ctx.Artists;
    }

    public Artist GetArtistById([Service] postgresContext ctx)
    {
      return ctx.Artists.Find(37);
    }

    [UseProjection]
    public IQueryable<Album> GetAlbumsTracksGenreSome([Service] postgresContext ctx)
    {
      return ctx.Albums.Where(it => it.ArtistId == 127);
    }

    [UseProjection]
    public IQueryable<Track> GetTracksMediaSome([Service] postgresContext ctx)
    {
      return ctx.Tracks.Where(it => it.Composer == "Kurt Cobain");
    }

    public IQueryable<Artist> GetArtistsCollaboration([Service] postgresContext ctx)
    {
      return from artist in ctx.Artists
             join album in ctx.Albums on artist.Id equals album.ArtistId
             join track in ctx.Tracks on album.Id equals track.AlbumId
             where track.Composer == "Ludwig van Beethoven"
             select artist;
    }

  }

}