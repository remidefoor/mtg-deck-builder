using Microsoft.EntityFrameworkCore;
using GraphQL.Server;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddDbContext<mtg_v1Context>
(
    options => options.UseSqlServer(config.GetConnectionString("MgtV1Db"))
);
builder.Services.AddScoped<ICardRepository, SqlCardRepository>();
builder.Services.AddScoped<IArtistRepository, SqlArtistRepository>();

builder.Services.AddScoped<RootSchema>();
builder.Services.AddGraphQL()
    .AddGraphTypes(typeof(RootSchema), ServiceLifetime.Scoped)
    .AddDataLoader()
    .AddSystemTextJson();

WebApplication app = builder.Build();

app.UseGraphQL<RootSchema>();
app.UseGraphQLPlayground();

app.Run();
