using GameStore.Data;
using GameStore.Dtos;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
  const string GameGameEndpointName = "GetGame";

  private static readonly List<GameSummaryDto> games = [
  new (1, "Final Fantasy 7", "RPG", 20.00M, new DateOnly(1992,7,15)),
  new (2, "Expedition 33", "RPG", 60.99M, new DateOnly(2024,4,15)),
  new (3, "Dialbo 4", "Action", 75.00M, new DateOnly(2020,9,20)),
  new (4, "The last of us", "Spooky", 20.00M, new DateOnly(2023,2,5)),
];

  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("games").WithParameterValidation();

    // GET /games
    group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games.Include(game => game.Genre).Select(game => game.ToGameSummaryDto()).AsNoTracking().ToListAsync());

    // GET /games/:id
    group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
    {
      Game? game = await dbContext.Games.FindAsync(id);

      return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailDto());

    }).WithName(GameGameEndpointName);

    // POST /games
    group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
    {
      Game game = newGame.ToEntity();

      dbContext.Games.Add(game);
      await dbContext.SaveChangesAsync();

      return Results.CreatedAtRoute(GameGameEndpointName, new { id = game.Id }, game.ToGameDetailDto());
    });

    // PUT /games/:id
    group.MapPut("/{id}", async (int id, UpdateGameDTO gameToUpdate, GameStoreContext dbContext) =>
    {
      var existingGame = await dbContext.Games.FindAsync(id);

      if (existingGame is null) return Results.NotFound();

      dbContext.Entry(existingGame).CurrentValues.SetValues(gameToUpdate.ToUpdatedEntity(id));
      await dbContext.SaveChangesAsync();

      return Results.NoContent();

    });

    // DELETE /games/:id
    group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
    {
      await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
      return Results.NoContent();
    });
    return group;
  }

}
