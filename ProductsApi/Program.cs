using ProductsApi.Data;
// Removed invalid using directive
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.AddDbContext<ProductDatabase>(options =>
    options.UseInMemoryDatabase("ProductsDb"));
    
var googleClientId = "your-google-client-id"; // Replace with your actual Google Client ID

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://accounts.google.com";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Varmistaa, että token on Googlen myöntämä
            ValidIssuer = "https://accounts.google.com",
            ValidateAudience = true, // Varmistaa, että token on tarkoitettu tälle API:lle
            ValidAudience = googleClientId, // Aseta Google Client ID tähän
            ValidateLifetime = true // Varmistaa, että token ei ole vanhentunut
        };
    });

// builder lets build application
var app = builder.Build();
        var webapi = builder.Build();
        webapi.MapControllers();

app.UseAuthentication(); // Käynnistää autentikointijärjestelmän

app.Run();