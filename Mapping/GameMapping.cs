using GameStore.Dtos;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GameMapping
{
  public static Game ToEntity(this CreateGameDto game)
  {
    return new Game()
    {
      Name = game.Name,
      GenreId = game.GenreId,
      Price = game.Price,
      ReleaseDate = game.ReleaseDate
    };
  }

  public static GameSummaryDto ToGameSummaryDto(this Game game)
  {
    return new GameSummaryDto(
   game.Id,
   game.Name,
   game.Genre!.Name,
   game.Price,
   game.ReleaseDate
 );
  }
  public static GameDetailDto ToGameDetailDto(this Game game)
  {
    return new GameDetailDto(
   game.Id,
   game.Name,
   game.GenreId,
   game.Price,
   game.ReleaseDate
 );
  }

  public static Game ToUpdatedEntity(this UpdateGameDTO game, int id)
  {
    return new Game()
    {
      Id = id,
      Name = game.Name,
      GenreId = game.GenreId,
      Price = game.Price,
      ReleaseDate = game.ReleaseDate
    };
  }


}
