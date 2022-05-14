WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.
builder.Services.AddEndpointDefinitions(typeof(Program).Assembly);

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseEndpointDefinitions();

app.Run();
