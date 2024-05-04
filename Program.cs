using GameStoreAPI.Data;
using GameStoreAPI.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGenresEndPoints();
app.MapGamesEndpoints();

await app.MigrateDbAsync();

app.Run();
