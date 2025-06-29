using GameStore.Dtos;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GenereMapping
{
  public static GenreDto ToGenreDto(this Genre genre)
  {
    return new GenreDto(genre.Id, genre.Name);
  }
}
