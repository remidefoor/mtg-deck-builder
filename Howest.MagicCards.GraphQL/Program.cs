WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICardRepository, SqlCardRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
