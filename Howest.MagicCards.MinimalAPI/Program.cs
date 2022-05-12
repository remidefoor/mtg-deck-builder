using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.
builder.Services.AddEndpointDefinitions(typeof(Program).Assembly);
builder.Services.AddDbContext<mtg_v1Context>(options =>
    options.UseSqlServer(config.GetConnectionString("MgtV1Db"))); // TODO move to DeckEndPoints.cs

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseEndpointDefinitions();

app.Run();
