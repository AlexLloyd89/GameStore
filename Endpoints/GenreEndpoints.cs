using System;
using GameStore.Data;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GenreEndpoints
{
  public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("genres");

    group.MapGet("/", async (GameStoreContext dbContext) =>
    await dbContext.Genres.Select(genre => genre.ToGenreDto()).AsNoTracking().ToListAsync()
    );

    return group;
  }
}
