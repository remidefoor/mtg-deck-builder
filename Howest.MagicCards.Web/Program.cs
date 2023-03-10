using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAutoMapper(new System.Type[]
    {
        typeof(Howest.MagicCards.Shared.Mappings.CardsProfile),
        typeof(Howest.MagicCards.Shared.Mappings.DeckCardsProfile)
    }
);

builder.Services.AddHttpClient("WebApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7103/api/");
});
builder.Services.AddHttpClient("MinimalApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7061/api/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
