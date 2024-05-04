namespace GameStoreAPI.EndPoints;

using GameStoreAPI.Data;
using GameStoreAPI.Dtos;
using GameStoreAPI.Entities;
using GameStoreAPI.Mapping;
using Microsoft.EntityFrameworkCore;

public static class GamesEndPoints
{
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        const string GETGAMEENDPOINT = "get-game";
        var group = app.MapGroup("games");

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync()
                );

        // GET /games/{id}
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);
            return game is null ?
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
            .WithName(GETGAMEENDPOINT);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GETGAMEENDPOINT,
                new { id = game.Id },
                game.ToGameDetailsDto()
                );
        })
        .WithParameterValidation();

        // PUT /games
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame)
                        .CurrentValues
                        .SetValues(updateGame.ToEntity(id)
                        );
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }).WithParameterValidation(); ;

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }

}
