
namespace GameStoreAPI.EndPoints;
using GameStoreAPI.Data;
using GameStoreAPI.Dtos;
using GameStoreAPI.Entities;
using GameStoreAPI.Mapping;
using Microsoft.EntityFrameworkCore;

public static class GenresEndPoints
{
    public static RouteGroupBuilder MapGenresEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        // Get
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Genres.Select(genre => genre.ToDto())
                .AsNoTracking()
                .ToListAsync()
        );

        return group;
    }
}
