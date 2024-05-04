using GameStoreAPI.Entities;
using GameStoreAPI.Dtos;
namespace GameStoreAPI;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
