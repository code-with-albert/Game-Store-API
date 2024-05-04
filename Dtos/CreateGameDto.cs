using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Dtos;

public record class CreateGameDto(
    [Required][StringLength(30)] string Name,
    int GenreId,
    [Required][Range(0, 100)] decimal Price,
    DateOnly ReleaseDate
    );