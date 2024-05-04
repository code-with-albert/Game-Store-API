namespace GameStoreAPI.Dtos;
using System.ComponentModel.DataAnnotations;

public record class UpdateGameDto(
    [StringLength(30)] string Name,
    int GenreId,
    [Range(0, 100)] decimal Price,
    DateOnly ReleaseDate
    );